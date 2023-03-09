using Microsoft.EntityFrameworkCore;
using UsageDemo.Models;

namespace UsageDemo.DataAcessLayer
{
    public class UsageDbContext : DbContext
    {
        public UsageDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Usage> UsageInformation { get; set; }
    }
}
