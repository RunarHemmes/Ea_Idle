using Microsoft.EntityFrameworkCore;
using Ea_API.Models;

namespace Ea_API.Data
{
    public class EaIdleDbContext(DbContextOptions<EaIdleDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<GameProgress> GameProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new(1, "Harold", "passwordHarold", "Harold@mail.com"),
                new(2, "John", "passwordJohn", "John@mail.com")
                );
        }
    }
}
