using Microsoft.EntityFrameworkCore;
using Ea_API.Models;

namespace Ea_API.Data
{
    public class EaIdleDbContext(DbContextOptions<EaIdleDbContext> options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        
        public DbSet<GameProgress> GameProgresses { get; set; }
    }
}
