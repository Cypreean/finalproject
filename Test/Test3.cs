
using APBD_Proj.Context;
using APBD_Proj.Models;
using APBD_Proj.RequestModels;

using APBD_Proj.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;


public class ManageSubscriptionsServiceTests
{
    private DatabaseContext _context;
    private ManageSubscriptionsService _service;

    public ManageSubscriptionsServiceTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDb")
            .UseInternalServiceProvider(serviceProvider)
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        _context = new DatabaseContext(options);
        _service = new ManageSubscriptionsService(_context);
    }

    [Fact]
    public async Task AddSubscription_ShouldAddNewSubscription_WhenValidRequest()
    {
        // Arrange
        var newCustomer = new Customers 
        { 
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = 123456789,
            Pesel = 12345678901
        };
        _context.Customers.Add(newCustomer);
        await _context.SaveChangesAsync();

        var newSubscription = new SubscriptionRequestModel 
        { 
            Pesel = 12345678901,
            KRS = 0, // Assuming this is optional if customer exists
            SoftwareId = 1,
            RenewalPeriod = 12,
            Price = 100
        };

        // Act
        var result = await _service.AddSubscription(newSubscription);

        // Assert
        var subscriptionInDb = await _context.Subscriptions.FirstOrDefaultAsync(s => s.IdCustomer == newCustomer.IdCustomer);
        Assert.NotNull(subscriptionInDb);
        Assert.Equal(newSubscription.Price, subscriptionInDb.Price);
    }

    [Fact]
    public async Task AddSubscription_ShouldThrowNotFoundException_WhenCustomerAndCompanyDoNotExist()
    {
        // Arrange
        var newSubscription = new SubscriptionRequestModel 
        { 
            Pesel = 12345678901,
            KRS = 0,
            SoftwareId = 1,
            RenewalPeriod = 12,
            Price = 100
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.AddSubscription(newSubscription));
    }

    

    [Fact]
    public async Task AddPayment_ShouldThrowNotFoundException_WhenSubscriptionDoesNotExist()
    {
        // Arrange
        var newPayment = new SubscriptionPaymentRequestModel 
        { 
            SubscriptionId = 999, // Non-existing ID
            Amount = 100
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.AddPayment(newPayment));
    }
}
