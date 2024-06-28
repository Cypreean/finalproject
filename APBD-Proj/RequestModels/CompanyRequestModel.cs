using System.ComponentModel.DataAnnotations;

namespace APBD_Proj.RequestModels;

public class CompanyRequestModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    
    public int PhoneNumber { get; set; }
    
    public int KRS { get; set; }
    
}