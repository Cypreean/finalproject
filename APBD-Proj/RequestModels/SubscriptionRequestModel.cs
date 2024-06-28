namespace APBD_Proj.RequestModels;

public class SubscriptionRequestModel
{
    public long Pesel { get; set; }
    public int KRS { get; set; }
    public int SoftwareId { get; set; }
    public int RenewalPeriod { get; set; }
    public decimal Price { get; set; }
}