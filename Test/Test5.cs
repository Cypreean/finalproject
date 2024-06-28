
using APBD_Proj.Context;
using APBD_Proj.Models;
using APBD_Proj.RequestModels;
using APBD_Proj.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;


public class ManageContractServiceTests
{
    private DatabaseContext _context;
    private ManageContractService _service;

    public ManageContractServiceTests()
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
        _service = new ManageContractService(_context);
    }

    [Fact]
    public async Task CreateContract_ShouldAddNewContract_WhenValidRequest()
    {
        // Arrange
        var newSoftware = new Software 
        { 
            Name = "Software",
            Version = "1.0",
            Description = "Software Description",
            Category = "Software Category"
        };
        _context.Softwares.Add(newSoftware);
        await _context.SaveChangesAsync();
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

        var newContractRequest = new ContractRequestModel 
        { 
            Pesel = 12345678901,
            KRS = 0, // Assuming this is optional if customer exists
            SoftwareId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(10),
            Price = 1000,
            AdditionalSupportYears = 2
        };

        // Act
        var result = await _service.CreateContract(newContractRequest);

        // Assert
        var contractInDb = await _context.Contracts.FirstOrDefaultAsync(c => c.IdCustomer == newCustomer.IdCustomer);
        Assert.NotNull(contractInDb);
        Assert.Equal(newContractRequest.Price + 2000, contractInDb.TotalAmount);
    }

    [Fact]
    public async Task CreateContract_ShouldThrowNotFoundException_WhenCustomerAndCompanyDoNotExist()
    {
        // Arrange
        var newContractRequest = new ContractRequestModel 
        { 
            Pesel = 12345678901,
            KRS = 0,
            SoftwareId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(10),
            Price = 1000,
            AdditionalSupportYears = 2
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.CreateContract(newContractRequest));
    }

    [Fact]
    public async Task AddPayment_ShouldAddNewPayment_WhenValidRequest()
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

        var newContract = new Contracts
        {
            IdCustomer = newCustomer.IdCustomer,
            IdSoftware = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(10),
            TotalAmount = 1000,
            IsPaid = false,
            IsSigned = false
        };
        _context.Contracts.Add(newContract);
        await _context.SaveChangesAsync();

        var newPaymentRequest = new PaymentRequestModel 
        { 
            ContractId = newContract.IdContract,
            Amount = 1000,
            ClientInfo = "Payment Info"
        };

        // Act
        var result = await _service.AddPayment(newPaymentRequest);

        // Assert
        var paymentInDb = await _context.Payments.FirstOrDefaultAsync(p => p.IdContract == newContract.IdContract);
        Assert.NotNull(paymentInDb);
        Assert.Equal(newPaymentRequest.Amount, paymentInDb.Amount);
    }

    [Fact]
    public async Task AddPayment_ShouldThrowNotFoundException_WhenContractDoesNotExist()
    {
        // Arrange
        var newPaymentRequest = new PaymentRequestModel 
        { 
            ContractId = 999, // Non-existing ID
            Amount = 1000,
            ClientInfo = "Payment Info"
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.AddPayment(newPaymentRequest));
    }
}
