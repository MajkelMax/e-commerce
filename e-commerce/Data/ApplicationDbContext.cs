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
    /// <summary>
    ///  Here we register the Entities with database
    ///  Examples below
    /// </summary>
    /// 
    //public DbSet<HomeFinances.Models.Category> Category { get; set; } = default!;
    //public DbSet<HomeFinances.Models.Entry> Entry { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "Adult", NormalizedName = "ADULT" });


    }
}
