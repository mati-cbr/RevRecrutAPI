using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RevRecrutAPI.DB;
using RevRecrutAPI.DTOs.UserDto;
using RevRecrutAPI.DTOs.UserLoginDto;
using RevRecrutAPI.DTOs.UserRegisterDto;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using User = RevRecrutAPI.Entities.User.User;

namespace RevRecrutAPI.Services.Auth;

public class AuthService(AppDbContext context, IConfiguration configuration) : IAuthService
{
    public async Task<TokenResponse?> LoginAsync(UserLoginDto request)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user is null)
        {
            return null;
        }

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
            == PasswordVerificationResult.Failed)
        {
            return null;
        }

        // Allow login only for users with confirmed email
        if (!user.EmailConfirmed)
        {
            return null;
        }

        return await CreateTokenResponse(user);
    }

    public async Task<User?> RegisterAsync(UserRegisterDto request)
    {
        if (await context.Users.AnyAsync(u => u.Email == request.Email))
        {
            return null;
        }

        User user = new();

        var hashedPassword = new PasswordHasher<User>()
            .HashPassword(user, request.Password);

        user.PasswordHash = hashedPassword;
        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        user.EmailConfirmationToken = GenerateRefreshToken();
        user.EmailConfirmationTokenExpiry = DateTime.UtcNow.AddHours(24);

        context.Users.Add(user);
        await context.SaveChangesAsync();

        await SendEmailConfirmationAsync(user);

        return user;
    }

    public async Task<TokenResponse?> RefreshTokensAsync(TokenRequest request)
    {
        var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
        if (user is null)
        {
            return null;
        }
        return await CreateTokenResponse(user);
    }

    public async Task SendEmailConfirmationAsync(User user)
    {
        // Use client URI from configuration if available
        var client = configuration.GetValue<string>("AppSettings:ClientUri");
        string confirmUrl;
        if (!string.IsNullOrEmpty(client))
        {
            confirmUrl = $"{client.TrimEnd('/')}/api/auth/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(user.EmailConfirmationToken ?? string.Empty)}";
        }
        else
        {
            confirmUrl = $"/api/Auth/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(user.EmailConfirmationToken ?? string.Empty)}";
        }

        var smtpHost = configuration.GetValue<string>("Smtp:Host");
        var smtpPort = configuration.GetValue<int?>("Smtp:Port") ?? 25;
        var smtpUser = configuration.GetValue<string>("Smtp:User");
        var smtpPass = configuration.GetValue<string>("Smtp:Pass");
        var from = configuration.GetValue<string>("Smtp:From") ?? "no-reply@gmail.com";

        if (string.IsNullOrEmpty(smtpHost))
        {
            return;
        }

        var mail = new MailMessage(from, user.Email)
        {
            Subject = "Confirm your email",
            Body = $"Please confirm your email by clicking the following link: {confirmUrl}",
            IsBodyHtml = false
        };

        using var clientSmtp = new SmtpClient(smtpHost, smtpPort);
        if (!string.IsNullOrEmpty(smtpUser))
        {
            clientSmtp.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass);
        }
        clientSmtp.EnableSsl = true;

        try
        {
            await clientSmtp.SendMailAsync(mail);
        }
        catch (SmtpException ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public async Task<bool> ConfirmEmailAsync(Guid userId, string token)
    {
        var user = await context.Users.FindAsync(userId);
        if (user is null || user.EmailConfirmationToken != token || user.EmailConfirmationTokenExpiry <= DateTime.UtcNow)
        {
            return false;
        }

        user.EmailConfirmed = true;
        user.EmailConfirmationToken = null;
        user.EmailConfirmationTokenExpiry = null;
        await context.SaveChangesAsync();
        return true;
    }

    private async Task<TokenResponse> CreateTokenResponse(User user)
    {
        return new TokenResponse
        {
            AccessToken = CreateToken(user),
            Refresh = await GenerateAndSaveRefreshTokenAsync(user)
        };
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims, 
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
    {
        var refreshToken = GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefrreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(60);
        await context.SaveChangesAsync();
        return refreshToken;
    }

    private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var user = await context.Users.FindAsync(userId);
        if (user is null || user.RefreshToken != refreshToken
            || user.RefrreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }
}
