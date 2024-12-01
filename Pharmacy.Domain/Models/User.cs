using Pharmacy.Domain.Enum;
using Pharmacy.Domain.ModelsDb;


namespace Pharmacy.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? PartImage { get; set; }
    public DateTime CreatedAt { get; set; }
    public Role Role { get; set; }
    
    public List<CartDb> Carts { get; set; }  // Связь с корзинами
    public List<OrderDb> Orders { get; set; }  // Связь с заказами
}