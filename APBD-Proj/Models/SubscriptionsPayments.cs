using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Proj.Models;

public class SubscriptionsPayments
{
    [Key]
    public int IdPayment { get; set; }
    public int IdSubscription { get; set; }
    [ForeignKey("IdSubscription")]
    public Subscriptions Subscriptions { get; set; }
    [Required]
    public decimal Ammount { get; set; }
}