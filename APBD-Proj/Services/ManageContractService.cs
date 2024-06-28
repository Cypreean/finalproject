using System.Diagnostics.Contracts;
using APBD_Proj.Context;
using APBD_Proj.RequestModels;
using APBD_Proj.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Proj.Services;

public interface IManageContractService
{
    Task<ContractRequestModel> CreateContract(ContractRequestModel contractRequestModel);
    Task<PaymentRequestModel> AddPayment(PaymentRequestModel paymentRequestModel);
}
public class ManageContractService (DatabaseContext context) : IManageContractService
{
    public async Task<ContractRequestModel> CreateContract(ContractRequestModel contractRequestModel)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(e => e.Pesel == contractRequestModel.Pesel);
        var company = await context.Companies.FirstOrDefaultAsync(e => e.KRS == contractRequestModel.KRS);
        
        if (customer == null && company == null)
        {
            throw new NotFoundException("Klient nie istnieje.");
        }
        
        var clientId = customer?.IdCustomer ?? company.IdCompany;
        var activeContracts = await context.Contracts
            .AnyAsync(e => (e.IdCustomer == clientId || e.IdCompany == clientId) && e.IdSoftware == contractRequestModel.SoftwareId && e.EndDate > DateTime.Now);

        if (activeContracts)
        {
            throw new BadHttpRequestException("Klient ma już aktywną subskrypcję lub umowę na ten produkt.");
        }
        
        if ((contractRequestModel.EndDate - contractRequestModel.StartDate).TotalDays < 3 ||
            (contractRequestModel.EndDate - contractRequestModel.StartDate).TotalDays > 30)
        {
            throw new BadHttpRequestException("Przedział czasowy umowy powinien wynosić co najmniej 3 dni i maksymalnie 30 dni.");
        }
        decimal totalAmount = contractRequestModel.Price;
        var discounts = await context.Discounts
            .Where(e => e.StartDate <= DateTime.Now && e.EndDate >= DateTime.Now)
            .OrderByDescending(e => e.Percentage)
            .ToListAsync();
        
        var discountId = discounts.FirstOrDefault()?.IdDiscount;

        if (discounts.Any())
        {
            totalAmount -= totalAmount * (discounts.First().Percentage / 100);
        }
        
        var hasPreviousContracts = await context.Contracts
            .AnyAsync(e => (e.IdCustomer == clientId || e.IdCompany == clientId) && e.IsPaid);
    
        if (hasPreviousContracts)
        {
            totalAmount -= totalAmount * (decimal)0.05;
        }
        
        if (contractRequestModel.AdditionalSupportYears > 0)
        {
            totalAmount += 1000 * contractRequestModel.AdditionalSupportYears;
        }
        
       
        var software = await context.Softwares.FirstOrDefaultAsync(e => e.IdSoftware == contractRequestModel.SoftwareId);
        if (software == null)
        {
            throw new NotFoundException("Oprogramowanie nie istnieje.");
        }
        
        
        var contract = new Contracts
        { 
            IdCustomer= customer?.IdCustomer,
            IdCompany = company?.IdCompany,
            IdSoftware = contractRequestModel.SoftwareId,
            StartDate = contractRequestModel.StartDate,
            EndDate = contractRequestModel.EndDate,
            TotalAmount = totalAmount,
            IsPaid = false,
            IsSigned = false,
            IdDiscount = discountId,
            YearsOfSupport = contractRequestModel.AdditionalSupportYears + 1
        };
        
        context.Contracts.Add(contract);
        await context.SaveChangesAsync();
        
        return contractRequestModel;
        
    }
    
    public async Task<PaymentRequestModel> AddPayment(PaymentRequestModel paymentRequestModel)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var contract = await context.Contracts.FirstOrDefaultAsync(e => e.IdContract == paymentRequestModel.ContractId);
            if (contract == null)
            {
                throw new NotFoundException("Umowa nie istnieje.");
            }

            if (contract.IsPaid)
            {
                throw new BadHttpRequestException("Umowa jest już opłacona.");
            }

            decimal totalPaid = 0;
            totalPaid = await context.Payments
                .Where(p => p.IdContract == paymentRequestModel.ContractId)
                .SumAsync(p => p.Amount);
            if (totalPaid + paymentRequestModel.Amount > contract.TotalAmount)
            {
                throw new BadHttpRequestException("Kwota przekracza wartość umowy.");
            }
            totalPaid += paymentRequestModel.Amount;

            if (totalPaid == contract.TotalAmount)
            {
                contract.IsPaid = true;
                contract.IsSigned = true;
            }
            context.Contracts.Update(contract);
            await context.SaveChangesAsync();

            var payment = new Payments
            {
                IdContract = paymentRequestModel.ContractId,
                Amount = paymentRequestModel.Amount,
                PaymentDate = DateTime.Now,
                ClientInfo = paymentRequestModel.ClientInfo
            };

            context.Payments.Add(payment);
            await context.SaveChangesAsync();

            transaction.Commit();

            return paymentRequestModel;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}