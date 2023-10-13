using Microsoft.EntityFrameworkCore;
using Weinmann.Domain.Models;

namespace Weinmann.DataAccess
{
    public class WeinmannDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BusinessLocation> BusinessLocations { get; set; }
        public DbSet<EmployeeBusinessLocation> EmployeeBusinessLocations { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public WeinmannDataContext(DbContextOptions<WeinmannDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(customer => customer.Id);

            modelBuilder.Entity<Customer>()
                .HasOne(customer => customer.BusinessLocation)
                .WithMany(businessLocation => businessLocation.Customers);

            modelBuilder.Entity<BusinessLocation>()
                .HasKey(businessLocation => businessLocation.Id);

            modelBuilder.Entity<BusinessLocation>()
                .HasMany(businessLocation => businessLocation.Customers)
                .WithOne(customer => customer.BusinessLocation);

            modelBuilder.Entity<BusinessLocation>().HasData(new BusinessLocation 
            {
                Id = 1,
                Name = "Initial Business Location",
                Address = "Some street",
                PhoneNumber = "1234567890"
            });

            modelBuilder.Entity<Employee>()
                .HasKey(employee => employee.Id);

            modelBuilder.Entity<EmployeeBusinessLocation>()
                .HasKey(ebc => new { ebc.BusinessLocationId, ebc.EmployeeId });

            modelBuilder.Entity<EmployeeBusinessLocation>()
                .HasOne(ebc => ebc.Employee)
                .WithMany(e => e.EmployeeBusinessLocations)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<EmployeeBusinessLocation>()
                .HasOne(ebc => ebc.BusinessLocation)
                .WithMany(e => e.EmployeeBusinessLocations)
                .HasForeignKey(e => e.BusinessLocationId);

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = 1,
                FirstName = "Initial",
                LastName = "Employee",
                Email = "initial@Employee.mail",
                PhoneNumber = "1234567890"
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
