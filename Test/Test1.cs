using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using APBD_Proj.Context;
using APBD_Proj.Models;
using APBD_Proj.RequestModels;
using APBD_Proj.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using Xunit;

public class ManageCompanyServiceTests
{
    private DatabaseContext _context;
    private ManageCompanyService _service;

    public ManageCompanyServiceTests()
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
        _service = new ManageCompanyService(_context);
    }

    [Fact]
    public async Task AddCompany_ShouldAddNewCompany_WhenCompanyDoesNotExist()
    {
        // Arrange
        var newCompany = new CompanyRequestModel 
        { 
            KRS = 1234567890,
            Name = "Test Company",
            Address = "Test Address",
            Email = "test@example.com",
            PhoneNumber = 123456789
        };

        // Act
        var result = await _service.AddCompany(newCompany);

        // Assert
        var companyInDb = await _context.Companies.FirstOrDefaultAsync(c => c.KRS == newCompany.KRS);
        Assert.NotNull(companyInDb);
        Assert.Equal(newCompany.KRS, companyInDb.KRS);
    }

    [Fact]
    public async Task AddCompany_ShouldThrowException_WhenCompanyAlreadyExists()
    {
        // Arrange
        var existingCompany = new Companies 
        { 
            KRS = 1234567890,
            Name = "Test Company",
            Address = "Test Address",
            Email = "test@example.com",
            PhoneNumber = 123456789
        };
        _context.Companies.Add(existingCompany);
        await _context.SaveChangesAsync();

        var newCompany = new CompanyRequestModel 
        { 
            KRS = 1234567890,
            Name = "Test Company 2",
            Address = "Test Address 2",
            Email = "test2@example.com",
            PhoneNumber = 987654321
        };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _service.AddCompany(newCompany));
    }

    [Fact]
    public async Task UpdateCompany_ShouldReturnCompany_WhenCompanyExists()
    {
        // Arrange
        var existingCompany = new Companies 
        { 
            KRS = 1234567890,
            Name = "Test Company",
            Address = "Test Address",
            Email = "test@example.com",
            PhoneNumber = 123456789
        };
        _context.Companies.Add(existingCompany);
        await _context.SaveChangesAsync();
        
        var updateCompany = new UpdateCompanyRequestModel 
        { 
            Name = "Test Company 2",
            Address = "Test Address 2",
            Email = "elo@wp.pl",
            PhoneNumber = 123456789
                };

        // Act
        var result = await _service.UpdateCompany(existingCompany.KRS, updateCompany);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingCompany.KRS, result.KRS);
    }

    [Fact]
    public async Task UpdateCompany_ShouldThrowNotFoundException_WhenCompanyDoesNotExist()
    {
        // Arrange
        long nonExistingKrs = 9876543210;
        var updateCompany = new UpdateCompanyRequestModel
        {
            Name = "Test Company 2",
            Address = "Test Address 2",
            Email = "test@wp.pl",
            PhoneNumber = 123456789
        };

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateCompany(nonExistingKrs,updateCompany ));
    }
}
