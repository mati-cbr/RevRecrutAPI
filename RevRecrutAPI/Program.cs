using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using RevRecrutAPI.DB;
using RevRecrutAPI.Services.Candidate.Profile;
using Scalar.AspNetCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddLocalization(options =>
    options.ResourcesPath = "Resources");

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en", "pl" };
    options.SetDefaultCulture("pl")
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringToDatabase")));

// --------------------------
// RevRecrut services BEGIN
builder.Services.AddScoped<IProfileService, ProfileService>();
// --------------------------
// RevRecrut services END

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
