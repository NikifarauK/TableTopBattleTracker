using Microsoft.EntityFrameworkCore;
using Npgsql;
using TableTopBattleTracker.Model;


namespace TableTopBattleTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Monster>? Monsters { get; set; }
    }
}
