using APBD_Proj.RequestModels;
using APBD_Proj.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Proj.Endpoints;
[ApiController]
[Route("api/subscriptions")]
public class SubscriptionEndpoints : ControllerBase
{
    private readonly IManageSubscriptionsService _manageSubscriptionsService;
    
    public SubscriptionEndpoints(IManageSubscriptionsService manageSubscriptionsService)
    {
        _manageSubscriptionsService = manageSubscriptionsService;
    }
    
    [HttpPost]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> AddSubscription(SubscriptionRequestModel subscriptionRequestModel)
    {
        try
        {
            return Ok(await _manageSubscriptionsService.AddSubscription(subscriptionRequestModel));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("payments")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> AddPayment(SubscriptionPaymentRequestModel paymentRequestModel)
    {
        try
        {
            return Ok(await _manageSubscriptionsService.AddPayment(paymentRequestModel));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (WrongPriceException e)
        {
            return BadRequest(e.Message);
        }
    }
    
}