using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "Nazwa kategori")]
    public string Name { get; set; }
    
    [MaxLength(100)]
    [Display(Name = "Krótki opis")]
    public string Slug { get; set; }
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}