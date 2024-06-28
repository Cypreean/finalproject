using APBD_Proj.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Proj.Endpoints;
[ApiController]
[Route("api/employees")]
public class EmployeeEndpoints : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    
    public EmployeeEndpoints(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterEmployee(string username, string password, string role)
    {
        try
        {
            return Ok(await _employeeService.RegisterEmployee(username, password, role));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("login")]
    
    public async Task<IActionResult> LoginEmployee(string username, string password)
    {
        try
        {
            return Ok(await _employeeService.LoginEmployee(username, password));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
}