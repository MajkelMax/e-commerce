using Humanizer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models;

public class Product
{
    
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nazwa produktu jest wymagana.")]
    [MaxLength(255)]
    [Display(Name = "Nazwa produktu")]
    public string Name { get; set; }
    [Display(Name = "Opis produktu")]
    public string? Description { get; set; }

    [MaxLength(500)]
    [Display(Name = "Krótki opis produktu")]
    public string? ShortDescription { get; set; }

    [Required]
    [MaxLength(100)]
    [Display(Name = "SKU produktu")]
    public string Sku { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Cena produktu")]
    public decimal Price { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Cena promocyjna produktu")]
    public decimal? SalePrice { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    [Display(Name = "Ilość w magazynie")]
    public int StockQuantity { get; set; }
    [Display(Name = "Czy produkt jest opublikowany?")]
    public bool IsPublished { get; set; } = true;

    [MaxLength(1024)]
    [Display(Name = "URL obrazka produktu")]
    public string? ImageUrl { get; set; }
    [ValidateNever] //To sprawia że formularz przechodzi bez błędu mimo tego że wszystko zapisywane jest do bazy poprawnie
    public int? CategoryId { get; set; }
    [Display(Name = "Kategoria")]
    [ForeignKey("CategoryId")]
    [ValidateNever] //To sprawia że formularz przechodzi bez błędu mimo tego że wszystko zapisywane jest do bazy poprawnie
    public Category? Category { get; set; }
    
    public int? BrandId { get; set; }
    [Display(Name = "Marka")]
    [ForeignKey("BrandId")]
    public Brand? Brand { get; set; }
    [Display(Name = "Stworzone")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    [Display(Name = "Edytowane ostatnio")]
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}