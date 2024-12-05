namespace Pharmacy.Domain.ViewModels.Medecine;

public class ListOfMedicinesViewModel
{
    public List<MedicinesForListOfMedicinesViewModel> Medicines { get; set; }
    public Guid CategoryId { get; set; }
}

public class MedicinesForListOfMedicinesViewModel
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public DateTime CreatedAt { get; set; }
}