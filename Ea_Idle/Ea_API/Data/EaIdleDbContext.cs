using Microsoft.EntityFrameworkCore;
using Ea_API.Models;

namespace Ea_API.Data
{
    public class EaIdleDbContext(DbContextOptions<EaIdleDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<GameProgress> GameProgresses { get; set; }

        public DbSet<Connection> Connections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new(1, "Harold", "fSP67HucoSVmtUinGSITVQ==.tceWYgZT5EE3khRFS1Y5/L6A8K3GhUyvqUHxiNFcnXU=", "Harold@mail.com", "Player", 123456),
                new(2, "John", "qB2uM3X3+C1wob9GYHZy3A==./Fmbnk5d3hpyoopv0/9/Nsb5kfMAjbP5yWAFo6+xb7o=", "John@mail.com", "Parent", 666666)
                );
        }
    }
}
