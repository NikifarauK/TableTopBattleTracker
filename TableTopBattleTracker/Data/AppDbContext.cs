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
        public DbSet<DamageType>? DamageTypes { get; set; }

        public DbSet<MonsterSize>? MonsterSizes { get; set; }
    }
}
