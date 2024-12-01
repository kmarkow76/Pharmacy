using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; 
using Pharmacy.Domain.ModelsDb;


namespace Pharmacy.DAL;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    public DbSet<UserDb> UserDb { get; set; }
    public DbSet<MedicineDb> MedicineDb{ get; set; }
    public DbSet<OrderDb> OrderDb { get; set; }
    public DbSet<CategoryDb> CategoryDb { get; set; }
    public DbSet<RecordDb> RecordDb { get; set; }
    public DbSet<CartDb> CartDb { get; set; }

}

