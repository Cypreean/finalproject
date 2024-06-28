using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APBD_Proj.Models;

public class Companies
{
    [Key]
    [Column("CompanyID")]
    public int IdCompany { get; set; }
    [MaxLength(100)]
    [Required]
    [Column("Name")]
    public string Name { get; set; }
    [Required]
    [MaxLength(255)]
    [Column("Address")]
    public string Address { get; set; }
    [Required]
    [Column("Email")]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Column("PhoneNumber")]
    [Phone]
    public int PhoneNumber { get; set; }
    [Required]
    [Column("KRS")]
    [MaxLength(14)]
    public long KRS { get; set; }
    [JsonIgnore]
    public ICollection<Contracts> Contracts { get; set; }
    public ICollection<Subscriptions> Subscriptions { get; set; }
}