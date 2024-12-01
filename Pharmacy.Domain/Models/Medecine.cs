using Pharmacy.Domain.Enum;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.Domain.Models;

public class Medecine
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool PrescriptionRequired { get; set; }
    public string Image { get; set; }
    public TimeSpan CreatedAt { get; set; }

    public Guid CategoryId { get; set; }
    public CategoryDb CategoryDb { get; set; }  // Связь с категорией

    public List<CartDb> Carts { get; set; }  // Связь с корзинами
    public List<OrderDb> Orders { get; set; }  // Связь с заказами
}