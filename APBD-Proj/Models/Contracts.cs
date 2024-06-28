using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APBD_Proj.Models;

public class Contracts
{
    [Key]
    [Column("ContractID")]
    public int IdContract { get; set; }
        
   
    [Column("CustomerID")]
    [ForeignKey("Customers")]
    public int? IdCustomer { get; set; }
    [JsonIgnore]
    public Customers Customers { get; set; }
        

    [Column("CompanyID")]
    [ForeignKey("Companies")]
    public int? IdCompany { get; set; }
    [JsonIgnore]
    public Companies Companies { get; set; }
        
    [Required]
    [Column("SoftwareID")]
    [ForeignKey("Software")]
    public int IdSoftware { get; set; }
    [JsonIgnore]
    public Software Software { get; set; }
        
    [Required]
    [Column("StartDate")]
    public DateTime StartDate { get; set; }
        
    [Required]
    [Column("EndDate")]
    public DateTime EndDate { get; set; }
        
    [Required]
    [Column("TotalAmount")]
    public decimal TotalAmount { get; set; }
    
    [Required]
    [Column("YearsOfSupport")]
    public int YearsOfSupport { get; set; }
        
    [Column("DiscountID")]
    [ForeignKey("Discounts")]
    public int? IdDiscount { get; set; }
    public Discounts Discounts { get; set; }
        
    [Column("IsPaid")]
    public bool IsPaid { get; set; }
        
    [Column("IsSigned")]
    public bool IsSigned { get; set; }
        
    public ICollection<Payments> Payments { get; set; }
}