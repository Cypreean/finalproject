using APBD_Proj.Models;
using Microsoft.EntityFrameworkCore;


namespace APBD_Proj.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=master;User Id=SA;Password=yourStrong(!)Password;TrustServerCertificate=True;");
    }
    
    public DbSet<Customers> Customers { get; set; }
    public DbSet<Companies> Companies { get; set; }
    public DbSet<Contracts> Contracts { get; set; }
    public DbSet<Discounts> Discounts { get; set; }
    public DbSet<Payments> Payments { get; set; }
    public DbSet<Software> Softwares { get; set; }
    public DbSet<Subscriptions> Subscriptions { get; set; }
    public DbSet<SubscriptionsPayments> SubscriptionsPayments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Customers>().HasData(
            new Customers
            {
                IdCustomer = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com",
                PhoneNumber = 123456789, Pesel = 12345678901, IsDeleted = false
            },
            new Customers
            {
                IdCustomer = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com",
                PhoneNumber = 987654321, Pesel = 98765432101, IsDeleted = false
            }
        );

       
        modelBuilder.Entity<Companies>().HasData(
            new Companies
            {
                IdCompany = 1, Name = "TechCorp", Address = "123 Tech Street", Email = "contact@techcorp.com",
                PhoneNumber = 555123456, KRS = 12345678901234
            },
            new Companies
            {
                IdCompany = 2, Name = "BizCorp", Address = "456 Biz Avenue", Email = "info@bizcorp.com",
                PhoneNumber = 555654321, KRS = 43210987654321
            }
        );

        
        modelBuilder.Entity<Software>().HasData(
            new Software
            {
                IdSoftware = 1, Name = "SuperApp", Description = "A super application", Version = "1.0",
                Category = "Utility"
            },
            new Software
            {
                IdSoftware = 2, Name = "MegaApp", Description = "A mega application", Version = "2.0",
                Category = "Business"
            }
        );

        
        modelBuilder.Entity<Discounts>().HasData(
            new Discounts
            {
                IdDiscount = 1, Name = "Summer Sale", Percentage = 10.0m, StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(20)
            },
            new Discounts
            {
                IdDiscount = 2, Name = "Winter Sale", Percentage = 20.0m, StartDate = DateTime.Now.AddDays(-30),
                EndDate = DateTime.Now.AddDays(-10)
            }
        );

        
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, Username = "admin", PasswordHash = "hashedpassword", Role = "Administrator" },
            new Employee { Id = 2, Username = "user", PasswordHash = "hashedpassword", Role = "User" }
        );

        
        modelBuilder.Entity<Contracts>().HasData(
            new Contracts
            {
                IdContract = 1, IdCustomer = 1, IdCompany = 1, IdSoftware = 1, StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1), TotalAmount = 1000.0m, YearsOfSupport = 1, IdDiscount = 1,
                IsPaid = true, IsSigned = true
            },
            new Contracts
            {
                IdContract = 2, IdCustomer = 2, IdCompany = 2, IdSoftware = 2, StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1), TotalAmount = 2000.0m, YearsOfSupport = 2, IdDiscount = 2,
                IsPaid = false, IsSigned = false
            }
        );

        
        modelBuilder.Entity<Payments>().HasData(
            new Payments
            {
                PaymentId = 1, IdContract = 1, Amount = 500.0m, PaymentDate = DateTime.Now,
                ClientInfo = "John Doe"
            },
            new Payments
            {
                PaymentId = 2, IdContract = 2, Amount = 1000.0m, PaymentDate = DateTime.Now,
                ClientInfo = "Jane Smith"
            }
        );

        
        modelBuilder.Entity<Subscriptions>().HasData(
            new Subscriptions
            {
                IdSubscription = 1, IdCustomer = 1, IdCompany = 1, IdSoftware = 1, RenewalPeriod = 12,
                StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1), Price = 100.0m, IsActive = true
            },
            new Subscriptions
            {
                IdSubscription = 2, IdCustomer = 2, IdCompany = 2, IdSoftware = 2, RenewalPeriod = 24,
                StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(2), Price = 200.0m, IsActive = false
            }
        );

        
        modelBuilder.Entity<SubscriptionsPayments>().HasData(
            new SubscriptionsPayments { IdPayment = 1, IdSubscription = 1, Ammount = 100.0m },
            new SubscriptionsPayments { IdPayment = 2, IdSubscription = 2, Ammount = 200.0m }
        );
    }
}