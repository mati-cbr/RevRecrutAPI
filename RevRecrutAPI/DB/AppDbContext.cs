using Microsoft.EntityFrameworkCore;
using RevRecrutAPI.Entities.Candidate;
using RevRecrutAPI.Entities.User;

namespace RevRecrutAPI.DB;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Profile> Profiles => Set<Profile>();
}
