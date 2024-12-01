using Pharmacy.Domain.Enum;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.Domain.Models;

public class Card
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public Guid MedicineId { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime AddedAt { get; set; }

    public UserDb User { get; set; }  // Связь с пользователем
    public MedicineDb MedicineDb { get; set; }  // Связь с медикаментом
}