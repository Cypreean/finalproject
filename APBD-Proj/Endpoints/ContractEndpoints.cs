using APBD_Proj.Models;
using APBD_Proj.RequestModels;
using APBD_Proj.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Proj.Endpoints;
[ApiController]
[Route("api/contracts")]
public class ContractEndpoints : ControllerBase
{
    private readonly IManageContractService _manageContractService;
    
    public ContractEndpoints(IManageContractService manageContractService)
    {
        _manageContractService = manageContractService;
    }
    
    [HttpPost]
    [Authorize(Roles = "admin,user" )]
    public async Task<IActionResult> AddContract(ContractRequestModel contractRequestModel)
    {
        try
        {
            return Ok(await _manageContractService.CreateContract(contractRequestModel));
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
    [Authorize(Roles = "admin,user" )]
    public async Task<IActionResult> AddPayment(PaymentRequestModel paymentRequestModel)
    {
        try
        {
            return Ok(await _manageContractService.AddPayment(paymentRequestModel));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}