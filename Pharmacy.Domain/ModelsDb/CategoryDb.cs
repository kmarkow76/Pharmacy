using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Domain.ModelsDb
{

    public class CategoryDb
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PathImage { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<MedicineDb> Medicines { get; set; }  // Связь с медикаментами
    }

}