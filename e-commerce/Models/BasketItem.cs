using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models;

public class BasketItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Produkt")]
    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; }

    [Required]
    [Display(Name = "Ilość")]
    public int Quantity { get; set; }

    [Required]
    [Display(Name = "Koszyk")]
    public int BasketId { get; set; }

    [ForeignKey("BasketId")]
    public Basket Basket { get; set; }
}
