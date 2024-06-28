namespace APBD_Proj.RequestModels;

public class CustomerRequestModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int PhoneNumber { get; set; }
    public long Pesel { get; set; }
}