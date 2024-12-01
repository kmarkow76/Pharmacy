using Pharmacy.Domain.Enum;
using Pharmacy.Domain.ModelsDb;

namespace Pharmacy.Domain.Models;

public class Record
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    
}