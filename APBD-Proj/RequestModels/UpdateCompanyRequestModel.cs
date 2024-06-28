using HostingEnvironmentExtensions = Microsoft.AspNetCore.Hosting.HostingEnvironmentExtensions;

namespace APBD_Proj.RequestModels;

public class UpdateCompanyRequestModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    
}