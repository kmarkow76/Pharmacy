namespace Pharmacy.Domain.ViewModels;

public class MedicineFilter
{
    public Guid CategoryId { get; set; }
    public decimal PriceMin { get; set; }
    public decimal PriceMax { get; set; }

    public int OrderBy { get; set; }
}