using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APBD_Proj.Models;

public class Software
{
    [Key]
    [Column("SoftwareID")]
    public int IdSoftware { get; set; }
    [MaxLength(100)]
    [Required]
    [Column("Name")]
    public string Name { get; set; }
    [Required]
    [MaxLength(255)]
    [Column("Description")]
    public string Description { get; set; }
    [Required]
    [Column("Version")]
    public string Version { get; set; }
    [Required]
    [Column("Category")]
    public string Category { get; set; }
    
    [JsonIgnore]
    public ICollection<Contracts> Contracts { get; set; }
    public ICollection<Subscriptions> Subscriptions { get; set; }
    
}