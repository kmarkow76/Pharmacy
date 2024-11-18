using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; 
using Pharmacy.Domain.ModelsDb;


namespace Pharmacy.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Medicine> Medicines { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<Cart> Carts { get; set; }

}

