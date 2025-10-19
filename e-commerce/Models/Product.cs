using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models;

public class Product
{
    
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nazwa produktu jest wymagana.")]
    [MaxLength(255)]
    public string Name { get; set; }

    public string? Description { get; set; }

    [MaxLength(500)]
    public string? ShortDescription { get; set; }

    [Required]
    [MaxLength(100)]
    public string Sku { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? SalePrice { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
    
    public bool IsPublished { get; set; } = true;

    [MaxLength(1024)]
    public string? ImageUrl { get; set; }
    
    public int CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    
    public int? BrandId { get; set; }
    
    [ForeignKey("BrandId")]
    public Brand? Brand { get; set; }

    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}