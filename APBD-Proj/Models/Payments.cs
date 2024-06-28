using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Proj.Models;

public class Payments
{
    [Key]
    [Column("PaymentID")]
    public int PaymentId { get; set; }

    
    [Column("ContractID")]
    [ForeignKey("Contracts")]
    public int IdContract { get; set; }
    public Contracts Contracts { get; set; }
    
    [Required]
    [Column("Amount")]
    public decimal Amount { get; set; }

    [Required]
    [Column("PaymentDate")]
    public DateTime PaymentDate { get; set; }
    
    [Required]
    [Column("ClientInfo")]
    public string ClientInfo { get; set; }
    
    
}