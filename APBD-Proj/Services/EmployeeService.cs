using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using APBD_Proj.Context;
using APBD_Proj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APBD_Proj.Services;

public interface IEmployeeService
{
  Task<int>RegisterEmployee(string username, string password, string role);
  Task<string> LoginEmployee(string username, string password);
}

public class EmployeeService(DatabaseContext context,  IConfiguration configuration) : IEmployeeService
{
    public async Task<int> RegisterEmployee(string username, string password, string role)
    {
       
        var employee = new Employee
        {
            Username = username,
            PasswordHash = createpasswordhash(password),
            Role = role
        };
        context.Employees.Add(employee);
        await context.SaveChangesAsync();
        return employee.Id;
    }
    public async Task<string> LoginEmployee(string username, string password)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(e => e.Username == username);
        if (employee is null)
        {
            throw new Exception("Invalid username or password");
        }
        if (employee.PasswordHash != createpasswordhash(password))
        {
            throw new Exception("Invalid username or password");
        }
        var tokenhandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["SecretKey:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, employee.Username),
                new Claim(ClaimTypes.Role, employee.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenhandler.CreateToken(tokenDescriptor);
        return tokenhandler.WriteToken(token);
    }
    private string createpasswordhash(string password)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        using var sha = SHA256.Create();
        var hashBytes = sha.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hashBytes);
    }
}
