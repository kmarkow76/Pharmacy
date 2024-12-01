using Pharmacy.Domain.Enum;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.Domain.Models;

public class Order
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public TimeSpan CreatedAt { get; set; }

    public List<MedicineDb> Medicines { get; set; }  // Связь с медикаментами
    public UserDb User { get; set; }  // Связь с пользователем
}