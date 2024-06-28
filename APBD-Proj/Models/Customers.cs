using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace APBD_Proj.Models;

public class Customers
{
    [Key]
    [Column("CustomerID")]
    public int IdCustomer { get; set; }
    [MaxLength(50)]
    [Required]
    [Column("FirstName")]
    public string FirstName { get; set; }
    [MaxLength(60)]
    [Required]
    [Column("LastName")]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [Column("Email")]
    public string Email { get; set; }
    [Required]
    [Phone]
    [Column("PhoneNumber")]
    public int PhoneNumber { get; set; }
    [Required]
    [MaxLength(11)]
    [Column("Pesel")]
    public long Pesel { get; set; }
    [Required]
    [Column("IsDeleted")]
    public bool IsDeleted { get; set; }
    [JsonIgnore]
    public ICollection<Contracts> Contracts { get; set; }
    public ICollection<Subscriptions> Subscriptions { get; set; }
    
    
}