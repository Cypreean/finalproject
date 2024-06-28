using APBD_Proj.Context;
using APBD_Proj.Models;
using APBD_Proj.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace APBD_Proj.Services;

public interface IManageCompanyService
{
 
 Task<CompanyRequestModel> AddCompany(CompanyRequestModel companyRequestModel);
 Task<Companies> UpdateCompany(long Krs, UpdateCompanyRequestModel updateCompanyRequestModel);
}

public class ManageCompanyService (DatabaseContext context) : IManageCompanyService
{
    public async Task<CompanyRequestModel> AddCompany(CompanyRequestModel company)
    {
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var companyExists = await context.Companies.FirstOrDefaultAsync(e => e.KRS == company.KRS);
            if (companyExists != null)
            {
                throw new Exception($"Company with KRS:{company.KRS} already exists");
            }
            var newCompany = new Companies
            {
                Name = company.Name,
                Address = company.Address,
                Email = company.Email,
                PhoneNumber = company.PhoneNumber,
                KRS = company.KRS
            };
            context.Companies.Add(newCompany);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception(e.Message);
        }
        
        return company;
    }
    
    public async Task<Companies> UpdateCompany(long Krs, UpdateCompanyRequestModel updateCompanyRequestModel)
    {
        var company = await context.Companies.FirstOrDefaultAsync(e => Equals(e.KRS, Krs));
        if (company is null)
        {
            throw new NotFoundException($"Company with Krs:{Krs} does not exist");
        }
        company.Name = updateCompanyRequestModel.Name;
        company.Address = updateCompanyRequestModel.Address;
        company.Email = updateCompanyRequestModel.Email;
        company.PhoneNumber = updateCompanyRequestModel.PhoneNumber;
        await context.SaveChangesAsync();
        
        return company;
    }
}

public class NotFoundException : Exception
{
    public NotFoundException(string s) : base(s)
    {
    }
}

