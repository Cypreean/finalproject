using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Proj.Models;

public class Discounts
{
    [Key]
    [Column("DiscountID")]
    public int IdDiscount { get; set; }
        
    [MaxLength(100)]
    [Required]
    [Column("Name")]
    public string Name { get; set; }
        
    [Required]
    [Column("Percentage")]
    public decimal Percentage { get; set; }
        
    [Required]
    [Column("StartDate")]
    public DateTime StartDate { get; set; }
        
    [Required]
    [Column("EndDate")]
    public DateTime EndDate { get; set; }
    
    public ICollection<Contracts> Contracts { get; set; }
}