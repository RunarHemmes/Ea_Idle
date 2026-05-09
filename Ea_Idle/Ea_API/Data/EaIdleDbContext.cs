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
                new(1, "Harold", "passwordHarold", "Harold@mail.com", "Player"),
                new(2, "John", "passwordJohn", "John@mail.com", "Parent")
                );

            //Connection data = new(2, 1);
            //modelBuilder.Entity<Connection>().HasData(
            //    data 
            //    );
        }
    }
}
