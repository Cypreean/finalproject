using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_Proj.Context;
using APBD_Proj.Models;
using APBD_Proj.RequestModels;
using APBD_Proj.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class ManageCustomerServiceTests
{
    private DatabaseContext _context;
    private ManageCustomerService _service;

    public ManageCustomerServiceTests()
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
        _service = new ManageCustomerService(_context);
    }

    [Fact]
    public async Task AddCustomer_ShouldAddNewCustomer_WhenCustomerDoesNotExist()
    {
        // Arrange
        var newCustomer = new CustomerRequestModel 
        { 
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = 123456789,
            Pesel = 12345678901
        };

        // Act
        var result = await _service.AddCustomer(newCustomer);

        // Assert
        var customerInDb = await _context.Customers.FirstOrDefaultAsync(c => c.Pesel == newCustomer.Pesel);
        Assert.NotNull(customerInDb);
        Assert.Equal(newCustomer.Pesel, customerInDb.Pesel);
    }

    [Fact]
    public async Task AddCustomer_ShouldThrowException_WhenCustomerAlreadyExists()
    {
        // Arrange
        var existingCustomer = new Customers 
        { 
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            PhoneNumber = 987654321,
            Pesel = 12345678901
        };
        _context.Customers.Add(existingCustomer);
        await _context.SaveChangesAsync();

        var newCustomer = new CustomerRequestModel 
        { 
            FirstName = "John",
            LastName = "Smith",
            Email = "john.smith@example.com",
            PhoneNumber = 123123123,
            Pesel = 12345678901
        };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.AddCustomer(newCustomer));
    }

    [Fact]
    public async Task DeleteCustomer_ShouldMarkCustomerAsDeleted_WhenCustomerExists()
    {
        // Arrange
        var existingCustomer = new Customers 
        { 
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            PhoneNumber = 987654321,
            Pesel = 12345678901
        };
        _context.Customers.Add(existingCustomer);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.DeleteCustomer(existingCustomer.Pesel);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsDeleted);
    }

    [Fact]
    public async Task DeleteCustomer_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
    {
        // Arrange
        long nonExistingPesel = 98765432101;

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteCustomer(nonExistingPesel));
    }

    [Fact]
    public async Task UpdateCustomer_ShouldUpdateCustomerDetails_WhenCustomerExists()
    {
        // Arrange
        var existingCustomer = new Customers 
        { 
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@example.com",
            PhoneNumber = 987654321,
            Pesel = 12345678901
        };
        _context.Customers.Add(existingCustomer);
        await _context.SaveChangesAsync();

        var updatedCustomer = new UpdateCustomerRequestModel
        {
            FirstName = "Jane Updated",
            LastName = "Doe Updated",
            Email = "jane.updated@example.com",
            PhoneNumber = 987654321,
            
        };

        // Act
        var result = await _service.UpdateCustomer(existingCustomer.Pesel, updatedCustomer);

        // Assert
        var customerInDb = await _context.Customers.FirstOrDefaultAsync(c => c.Pesel == existingCustomer.Pesel);
        Assert.NotNull(customerInDb);
        Assert.Equal(updatedCustomer.FirstName, customerInDb.FirstName);
        Assert.Equal(updatedCustomer.LastName, customerInDb.LastName);
        Assert.Equal(updatedCustomer.Email, customerInDb.Email);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
    {
        // Arrange
        long nonExistingPesel = 98765432101;
        var updatedCustomer = new UpdateCustomerRequestModel
        {
            FirstName = "Non Existing",
            LastName = "Customer",
            Email = "non.existing@example.com",
            PhoneNumber = 123456789
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateCustomer(nonExistingPesel, updatedCustomer));
    }
}
