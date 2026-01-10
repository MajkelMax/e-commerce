using e_commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Brand> Brands { get; set; } = default!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Adult", NormalizedName = "ADULT" });


    }
}
