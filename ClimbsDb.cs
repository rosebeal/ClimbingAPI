using Microsoft.EntityFrameworkCore;
namespace ClimbingAPI.Models
{
    public class ClimbsDb : DbContext
{
    public ClimbsDb(DbContextOptions options) : base(options) { }
    public DbSet<Climb> Climbs { get; set; } = null!;
}
}