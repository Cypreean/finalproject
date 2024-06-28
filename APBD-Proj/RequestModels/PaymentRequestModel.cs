namespace APBD_Proj.RequestModels;

public class PaymentRequestModel
{
    public int ContractId { get; set; }
    public decimal Amount { get; set; }
    public string ClientInfo { get; set; }
}