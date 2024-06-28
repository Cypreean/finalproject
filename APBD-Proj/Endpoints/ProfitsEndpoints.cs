using APBD_Proj.RequestModels;
using APBD_Proj.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Proj.Endpoints;
[ApiController]
[Route("api/profits")]
public class ProfitsEndpoints : ControllerBase
{
    private readonly ICompanyProfitsService _companyProfitsService;
    
    public ProfitsEndpoints(ICompanyProfitsService companyProfitsService)
    {
        _companyProfitsService = companyProfitsService;
    }
    
    [HttpPost]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetCompanyProfits(ProfitRequestModel profitRequestModel)
    {
        try
        {
            return Ok(await _companyProfitsService.GetCompanyProfits(profitRequestModel));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    [HttpPost("expected")]
    [Authorize(Roles = "user")]
    public async Task<IActionResult> GetExpectedProfits()
    {
        try
        {
            return Ok(await _companyProfitsService.GetExpectedProfits());
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}