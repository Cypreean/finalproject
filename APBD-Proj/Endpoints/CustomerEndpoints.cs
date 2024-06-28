using APBD_Proj.RequestModels;
using APBD_Proj.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Proj.Endpoints;
[ApiController]
[Route("api/customers")]
public class CustomerEndpoints : ControllerBase
{
    private readonly IManageCustomerService _manageCustomerService;
    
    public CustomerEndpoints(IManageCustomerService manageCustomerService)
    {
        _manageCustomerService = manageCustomerService;
    }
    
    [HttpPost]
    [Authorize(Roles = "admin,user" )]
    public async Task<IActionResult> AddCustomer(CustomerRequestModel customer)
    {
        try
        {
            return Ok(await _manageCustomerService.AddCustomer(customer));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete("{pesel:long}")]
    [Authorize(Roles = "admin" )]
    public async Task<IActionResult> DeleteCustomer(long pesel)
    {
        try
        {
            return Ok(await _manageCustomerService.DeleteCustomer(pesel));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPut("update/{pesel:long}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateCustomer(long pesel, UpdateCustomerRequestModel customer)
    {
        try
        {
            return Ok(await _manageCustomerService.UpdateCustomer(pesel, customer));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}

