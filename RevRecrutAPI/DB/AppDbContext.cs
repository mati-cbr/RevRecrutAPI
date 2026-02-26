using Microsoft.EntityFrameworkCore;
using RevRecrutAPI.Entities.Candidate;

namespace RevRecrutAPI.DB;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Profile> Profiles => Set<Profile>();
}
