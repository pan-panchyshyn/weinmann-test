using Microsoft.EntityFrameworkCore;
using Weinmann.Domain.Models;

namespace Weinmann.DataAccess
{
    public class WeinmannDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public WeinmannDataContext(DbContextOptions<WeinmannDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
