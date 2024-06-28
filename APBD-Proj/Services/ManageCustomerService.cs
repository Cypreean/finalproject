using APBD_Proj.Context;
using APBD_Proj.Models;
using APBD_Proj.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace APBD_Proj.Services;

public interface IManageCustomerService
{ 
    Task<CustomerRequestModel> AddCustomer(CustomerRequestModel customer);
    Task<Customers> DeleteCustomer(long pesel);
    Task<UpdateCustomerRequestModel> UpdateCustomer(long pesel, UpdateCustomerRequestModel customer);
  
}

public class ManageCustomerService (DatabaseContext context) : IManageCustomerService
{
    

        public async Task<CustomerRequestModel> AddCustomer(CustomerRequestModel customer)
        {
            var customerExists = await context.Customers.FirstOrDefaultAsync(e => e.Pesel == customer.Pesel);
            if (customerExists != null)
            {
                throw new Exception($"Customer with Pesel:{customer.Pesel} already exists");
            }
            var newCustomer = new Customers
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Pesel = customer.Pesel,
                IsDeleted = false
            };

            context.Customers.Add(newCustomer);
            await context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customers> DeleteCustomer(long pesel)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(e => e.Pesel == pesel);
            if (customer is null)
            {
                throw new NotFoundException($"Customer with Pesel:{pesel} does not exist");
            }

            customer.IsDeleted = true;
            context.Customers.Update(customer);
            await context.SaveChangesAsync();

            return customer;
        }

        public async Task<UpdateCustomerRequestModel> UpdateCustomer(long pesel, UpdateCustomerRequestModel customer)
        {
            var existingCustomer = await context.Customers.FirstOrDefaultAsync(e => e.Pesel == pesel);
            if (existingCustomer is null)
            {
                throw new NotFoundException($"Customer with Pesel:{pesel} does not exist");
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Email = customer.Email;
            existingCustomer.PhoneNumber = customer.PhoneNumber;

            context.Customers.Update(existingCustomer);
            await context.SaveChangesAsync();

            return customer;
        }
    
}