namespace APBD_Proj.RequestModels;

public class ContractRequestModel
{
    public long Pesel { get; set; }
    public int KRS { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public int AdditionalSupportYears { get; set; }
    public int SoftwareId { get; set; }
    
}