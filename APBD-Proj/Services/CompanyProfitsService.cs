using APBD_Proj.Context;
using APBD_Proj.RequestModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace APBD_Proj.Services;

public interface ICompanyProfitsService
{
 Task<decimal> GetCompanyProfits(ProfitRequestModel profitRequestModel);
 Task<decimal> GetExpectedProfits();
}

public class CompanyProfitsService ( DatabaseContext context) : ICompanyProfitsService


{
    
    private async Task<decimal> GetExchangeRateAsync(string targetCurrency)
    {
        using (var client = new HttpClient())
        {
            var response = await client.GetStringAsync($"https://api.nbp.pl/api/exchangerates/rates/A/{targetCurrency}/");
            var data = JObject.Parse(response);
            return data["rates"][0]["mid"].Value<decimal>();
        }
    }
 
    public async Task<decimal> GetCompanyProfits(ProfitRequestModel profitRequestModel)
    {
        
        
        
        if (profitRequestModel.SoftwareId == 0)
        {
            var payments = await context.SubscriptionsPayments.ToListAsync();
            var subscriptions = await context.Subscriptions.ToListAsync();
            var contracts = await context.Contracts.ToListAsync();
            
            decimal total = 0;
            
            foreach (var payment in payments)
            {
                total += payment.Ammount;
            }
            
            foreach (var subscription in subscriptions)
            {
                total += subscription.Price;
            }
            
            foreach (var contract in contracts)
            {
                if (contract.IsPaid)
                    total += contract.TotalAmount;
            }
            
            if (profitRequestModel.Currency != "string" && profitRequestModel.Currency != "PLN")
            {
                var exchangeRate = await GetExchangeRateAsync(profitRequestModel.Currency);
                total /= exchangeRate;
            }
            
                
            
            return total;
        }
        
        var softwareId = profitRequestModel.SoftwareId;
        
        var paymentsForSoftware = await context.SubscriptionsPayments
            .Where(p => p.Subscriptions.IdSoftware == softwareId)
            .ToListAsync();
        var subscriptionsForSoftware = await context.Subscriptions
            .Where(s => s.IdSoftware == softwareId)
            .ToListAsync();
        var contractsForSoftware = await context.Contracts
            .Where(c => c.IdSoftware == softwareId)
            .ToListAsync();
        
        decimal totalForSoftware = 0;
        
        foreach (var payment in paymentsForSoftware)
        {
            totalForSoftware += payment.Ammount;
        }
        foreach (var subscription in subscriptionsForSoftware)
        {
            totalForSoftware += subscription.Price;
        }
        foreach (var contract in contractsForSoftware)
        {
            if (contract.IsPaid)
                totalForSoftware += contract.TotalAmount;
        }
        
        if (profitRequestModel.Currency != "string" && profitRequestModel.Currency != "PLN")
        {
            var exchangeRate = await GetExchangeRateAsync(profitRequestModel.Currency);
            totalForSoftware /= exchangeRate;
        }
        return totalForSoftware;
        

    }
    public async Task<decimal> GetExpectedProfits()
    {
        var payments = await context.SubscriptionsPayments.ToListAsync();
        var subscriptions = await context.Subscriptions.ToListAsync();
        var contracts = await context.Contracts.ToListAsync();
        
        decimal total = 0;
        
        foreach (var payment in payments)
        {
            total += payment.Ammount;
        }
        
        foreach (var subscription in subscriptions)
        {
            total += subscription.Price;
        }
        
        foreach (var contract in contracts)
        {
            total += contract.TotalAmount;
        }
        // zkladamy ze uzytkownicy odnowia swoja subskrypcje
        var subscriptionsRenewal = await context.Subscriptions.ToListAsync();
        foreach (var subscription in subscriptionsRenewal)
        {
            total += subscription.Price;
        }
        
        return total;
    }
}