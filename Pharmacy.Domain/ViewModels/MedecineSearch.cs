namespace Pharmacy.Domain.ViewModels;

public class MedecineSearch
{
    public string SearchQuery { get; set; }  // Поисковый запрос
    public List<Medication> Medications { get; set; }  // Список найденных медикаментов
}

public class Medication
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}