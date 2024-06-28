using APBD_Proj.Context;
using APBD_Proj.Models;
using APBD_Proj.RequestModels;
using APBD_Proj.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace APBD_Proj.Services;

public interface IManageSubscriptionsService
{
    Task<SubscriptionResponseModel> AddSubscription(SubscriptionRequestModel subscription);
    Task<SubscriptionResponseModel> AddPayment(SubscriptionPaymentRequestModel payment);
}

public class ManageSubscriptionsService (DatabaseContext context) : IManageSubscriptionsService
{
    public async Task<SubscriptionResponseModel> AddSubscription(SubscriptionRequestModel subscription)
    {
       
        var customer = await context.Customers.FirstOrDefaultAsync(e => e.Pesel == subscription.Pesel);
        var company = await context.Companies.FirstOrDefaultAsync(e => e.KRS == subscription.KRS);

        if (customer == null && company == null)
        {
            throw new NotFoundException("Klient nie istnieje.");
        }
        
        var clientId = customer?.IdCustomer ?? company?.IdCompany;

        
        var activeSubscriptions = await context.Subscriptions
            .AnyAsync(s => (s.IdCustomer == clientId || s.IdCompany == clientId) && s.IdSoftware == subscription.SoftwareId && s.IsActive);

        if (activeSubscriptions)
        {
            throw new BadHttpRequestException("Klient ma już aktywną subskrypcję na ten produkt.");
        }
        
        var startDate = DateTime.Now;
        var endDate = startDate.AddMonths(subscription.RenewalPeriod);
        
        decimal totalAmount = subscription.Price;
        var discounts = await context.Discounts
            .Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
            .OrderByDescending(d => d.Percentage)
            .ToListAsync();

        if (discounts.Any())
        {
            totalAmount -= totalAmount * (discounts.First().Percentage / 100);
        }

        var hasPreviousSubscriptions = await context.Subscriptions
            .AnyAsync(s => (s.IdCustomer == clientId || s.IdCompany == clientId));

        if (hasPreviousSubscriptions)
        {
            totalAmount -= totalAmount * (decimal)0.05;
        }
        
        var subscriptionresult = new Subscriptions
        {
            IdCustomer = customer?.IdCustomer,
            IdCompany = company?.IdCompany,
            IdSoftware = subscription.SoftwareId,
            RenewalPeriod = subscription.RenewalPeriod,
            StartDate = startDate,
            EndDate = endDate,
            Price = totalAmount,
            IsActive = true
        };

        context.Subscriptions.Add(subscriptionresult);
        await context.SaveChangesAsync();
        
        var response = new SubscriptionResponseModel
        {
            
            EndDate = endDate
        };

        return response;
    }
    
    public async Task<SubscriptionResponseModel> AddPayment(SubscriptionPaymentRequestModel payment)
    {
        var subscription = await context.Subscriptions.FirstOrDefaultAsync(s => s.IdSubscription == payment.SubscriptionId);
    
        if (subscription == null)
        {
            throw new NotFoundException("Subskrypcja nie istnieje.");
        }
        
        var existingPayment = await context.SubscriptionsPayments
            .Where(p => p.IdSubscription == payment.SubscriptionId && DateTime.Today >= subscription.StartDate && DateTime.Today <= subscription.EndDate)
            .FirstOrDefaultAsync();
        
       
        
        var endDate = subscription.EndDate;
        var today = DateTime.Today;
        
        if (today < endDate)
        {
            throw new BadHttpRequestException("Subskrypcja jest jeszcze aktywna.");
        }

        if ( subscription.IsActive == true && existingPayment != null)
        {
            throw new BadHttpRequestException("Płatność za bieżący okres odnowienia została już dokonana.");
        }
    
        var missedPayments = await context.SubscriptionsPayments
            .Where(p => p.IdSubscription == payment.SubscriptionId && DateTime.Now > subscription.EndDate)
            .ToListAsync();

        if (missedPayments.Count != 0)
        {
            subscription.IsActive = false;
            await context.SaveChangesAsync();
            throw new BadHttpRequestException("Brak płatności za poprzednie okresy odnowienia. Subskrypcja anulowana.");
        }
        
        if (payment.Amount != subscription.Price)
        {
            throw new WrongPriceException("Kwota nie zgadza się z ceną subskrypcji.");
        }
        
        var hasPreviousPayments = await context.SubscriptionsPayments
            .AnyAsync(p => p.IdSubscription == payment.SubscriptionId);
        
        if (hasPreviousPayments)
        {
            payment.Amount -= payment.Amount * (decimal)0.05;
        }

        var response = new SubscriptionResponseModel
        {
            EndDate = subscription.EndDate
        };

        return response;
    
    }
}


public class WrongPriceException : Exception
{
    public WrongPriceException(string kwotaNieZgadzaSięZCenąSubskrypcji)
    {
        throw new NotImplementedException();
    }
}
