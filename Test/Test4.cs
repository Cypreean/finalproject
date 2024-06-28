
using APBD_Proj.Context;
using APBD_Proj.Models;
using APBD_Proj.RequestModels;
using APBD_Proj.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

public class CompanyProfitsServiceTests
{
    private DatabaseContext _context;
    private CompanyProfitsService _service;

    public CompanyProfitsServiceTests()
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
        _service = new CompanyProfitsService(_context);
    }

    [Fact]
    public async Task GetCompanyProfits_ShouldReturnTotalProfits_WhenSoftwareIdIsZero()
    {
        // Arrange
        var payments = new List<SubscriptionsPayments>
        {
            new SubscriptionsPayments { Ammount = 100 },
            new SubscriptionsPayments { Ammount = 200 }
        };
        _context.SubscriptionsPayments.AddRange(payments);
        await _context.SaveChangesAsync();

        var profitRequestModel = new ProfitRequestModel
        {
            SoftwareId = 0,
            Currency = "PLN"
        };

        // Act
        var result = await _service.GetCompanyProfits(profitRequestModel);

        // Assert
        Assert.Equal(300, result);
    }
    
    [Fact]
    public async Task GetExpectedProfits_ShouldReturnCorrectTotal_WhenDataExists()
    {
        // Arrange
        var payments = new List<SubscriptionsPayments>
        {
            new SubscriptionsPayments { Ammount = 100 },
            new SubscriptionsPayments { Ammount = 200 }
        };
        
        var contracts = new List<Contracts>
        {
            new Contracts { TotalAmount = 500 },
            new Contracts { TotalAmount = 600 }
        };

        await _context.SubscriptionsPayments.AddRangeAsync(payments);
        
        await _context.Contracts.AddRangeAsync(contracts);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetExpectedProfits();

        // Assert
        var expectedTotal = 100 + 200 + 500 + 600;
        Assert.Equal(expectedTotal, result);
    }

   
}
