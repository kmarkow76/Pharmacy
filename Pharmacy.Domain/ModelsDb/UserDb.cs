using System;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmacy.Domain.Enum;
namespace Pharmacy.Domain.ModelsDb;

public class UserDb
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? PartImage { get; set; }
    [Column("CreatedAt",TypeName ="timestamp")]
    public DateTime CreatedAt { get; set; }
    public Role Role { get; set; }
    public List<CartDb> Carts { get; set; }  // Связь с корзинами
    public List<OrderDb> Orders { get; set; }  // Связь с заказами
}
