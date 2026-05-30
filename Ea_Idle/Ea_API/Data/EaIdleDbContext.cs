using Ea_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Ea_API.Data
{
    public class EaIdleDbContext(DbContextOptions<EaIdleDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<GameProgress> GameProgresses { get; set; }

        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameProgress>().Property(e => e.MiningUpgrades)
                .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, float>>>(v, (JsonSerializerOptions)null));

            modelBuilder.Entity<Account>().HasData(
                new(1, "Harold", "fSP67HucoSVmtUinGSITVQ==.tceWYgZT5EE3khRFS1Y5/L6A8K3GhUyvqUHxiNFcnXU=", "Harold@mail.com", "Player", 123456),
                new(2, "John", "qB2uM3X3+C1wob9GYHZy3A==./Fmbnk5d3hpyoopv0/9/Nsb5kfMAjbP5yWAFo6+xb7o=", "John@mail.com", "Parent", 666666)
                );
        }
    }
}
