using Pharmacy.Domain.Enum;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.Domain.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<MedicineDb> Medicines { get; set; }  // Связь с медикаментами
}