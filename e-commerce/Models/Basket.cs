using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models;

public class Basket
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Identyfikator użytkownika")]
    public string UserId { get; set; } = string.Empty;
    [Display(Name = "Elementy koszyka")]
    public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
    public decimal TotalPrice => Items.Sum(i => { 
        if (i.Product.SalePrice != i.Product.Price) return i.Product.SalePrice * i.Quantity; 
        return i.Product.Price * i.Quantity; 
    });
}
