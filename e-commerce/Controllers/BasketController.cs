using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using e_commerce.Data;
using e_commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


[Authorize]
public class BasketController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public BasketController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    private async Task<string> GetUserIdAsync() =>
        (await _userManager.GetUserAsync(User))?.Id ?? throw new Exception("User not found");

    private async Task<Basket> GetOrCreateBasketAsync(string userId)
    {
        var basket = await _context.Baskets
            .Include(b => b.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        if (basket == null)
        {
            basket = new Basket { UserId = userId };
            _context.Baskets.Add(basket);
            await _context.SaveChangesAsync();
        }
        return basket;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = await GetUserIdAsync();
        var basket = await GetOrCreateBasketAsync(userId);
        return View("BasketView", basket);
    }
    [HttpGet]
    public async Task<IActionResult> Popup()
    {
        var userId = await GetUserIdAsync();
        var basket = await GetOrCreateBasketAsync(userId);
        return PartialView("_BasketPopupPartial", basket);
    }


    [HttpPost]
    public async Task<IActionResult> Add(int productId, int quantity)
    {
        var userId = await GetUserIdAsync();
        var basket = await GetOrCreateBasketAsync(userId);

        var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            item.Quantity += quantity;
        }
        else
        {
            basket.Items.Add(new BasketItem { ProductId = productId, Quantity = quantity });
        }

        await _context.SaveChangesAsync();

        basket = await _context.Baskets
            .Include(b => b.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        return View("BasketView", basket);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(int productId)
    {
        var userId = await GetUserIdAsync();
        var basket = await GetOrCreateBasketAsync(userId);

        var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            basket.Items.Remove(item);
            _context.BasketItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        basket = await _context.Baskets
           .Include(b => b.Items)
           .ThenInclude(i => i.Product)
           .FirstOrDefaultAsync(b => b.UserId == userId);

        return View("BasketView", basket);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int productId, int quantity)
    {
        var userId = await GetUserIdAsync();
        var basket = await GetOrCreateBasketAsync(userId);

        var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            item.Quantity = quantity;
            await _context.SaveChangesAsync();
        }
        basket = await _context.Baskets
           .Include(b => b.Items)
           .ThenInclude(i => i.Product)
           .FirstOrDefaultAsync(b => b.UserId == userId);

        return View("BasketView", basket);
    }
    [HttpPost]
    public async Task<IActionResult> ApplyDiscount(string discountCode)
    {
        var userId = await GetUserIdAsync();
        var basket = await _context.Baskets
            .Include(b => b.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(b => b.UserId == userId);
        if (string.IsNullOrWhiteSpace(discountCode))
        { 
            TempData["Error"] = "Wprowadź kod rabatowy.";
            return View("BasketView", basket);
        }
        
        var discount = await _context.DiscountCodes.FirstOrDefaultAsync(d => d.Code == discountCode); 
        if (!discount.IsActive) 
        { 
            TempData["Error"] = "Nieprawidłowy lub nieaktywny kod rabatowy.";
            return View("BasketView", basket);
        }
        foreach (var item in basket.Items)
        {
            if (item.Product != null)
            {
                item.Product.Price = item.Product.SalePrice;
                _context.Entry(item).State = EntityState.Unchanged;
            }
        }
        TempData["Success"] = $"Kod rabatowy '{discountCode}' został zastosowany.";
        return View("BasketView", basket);
    }


    [HttpPost]
    public async Task<IActionResult> Clear()
    {
        var userId = await GetUserIdAsync();
        var basket = await GetOrCreateBasketAsync(userId);

        _context.BasketItems.RemoveRange(basket.Items);
        basket.Items.Clear();
        await _context.SaveChangesAsync();

        basket = await _context.Baskets
           .Include(b => b.Items)
           .ThenInclude(i => i.Product)
           .FirstOrDefaultAsync(b => b.UserId == userId);

        return View("BasketView", basket);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAjax(int productId, int quantity)
    {
        var userId = await GetUserIdAsync();
        var basket = await GetOrCreateBasketAsync(userId);

        var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            item.Quantity = quantity;
            await _context.SaveChangesAsync();
        }
        basket = await _context.Baskets
            .Include(b => b.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        return PartialView("_BasketPopupPartial", basket);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveAjax(int productId)
    {
        var userId = await GetUserIdAsync();
        var basket = await GetOrCreateBasketAsync(userId);

        var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            basket.Items.Remove(item);
            _context.BasketItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        basket = await _context.Baskets
            .Include(b => b.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        return PartialView("_BasketPopupPartial", basket);
    }

    [HttpPost]
    public async Task<IActionResult> ClearAjax()
    {
        var userId = await GetUserIdAsync();
        var basket = await GetOrCreateBasketAsync(userId);

        _context.BasketItems.RemoveRange(basket.Items);
        basket.Items.Clear();
        await _context.SaveChangesAsync();

        basket = await _context.Baskets
            .Include(b => b.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(b => b.UserId == userId);

        return PartialView("_BasketPopupPartial", basket);
    }


}
