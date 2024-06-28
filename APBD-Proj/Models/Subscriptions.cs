using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Proj.Models;

public class Subscriptions
{
    [Key]
    [Column("SubscriptionID")]
    public int IdSubscription { get; set; }
        
    
    [Column("CustomerID")]
    [ForeignKey("Customers")]
    public int? IdCustomer { get; set; }
    public Customers Customers { get; set; }
        
    
    [Column("CompanyID")]
    [ForeignKey("Companies")]
    public int? IdCompany { get; set; }
    public Companies Companies { get; set; }
        
    [Required]
    [Column("SoftwareID")]
    [ForeignKey("Software")]
    public int? IdSoftware { get; set; }
    public Software Software { get; set; }
        
    [Required]
    [Column("RenewalPeriod")]
    [MaxLength(50)]
    public int RenewalPeriod { get; set; }
        
    [Required]
    [Column("StartDate")]
    public DateTime StartDate { get; set; }
        
    [Required]
    [Column("EndDate")]
    public DateTime EndDate { get; set; }
        
    [Required]
    [Column("Price")]
    public decimal Price { get; set; }
        
    [Column("IsActive")]
    public bool IsActive { get; set; }
    
    public ICollection<SubscriptionsPayments> PaymentsCollection { get; set; }
}