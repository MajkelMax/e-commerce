using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models;

public class Brand
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "Nazwa producenta")]
    public string Name { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}