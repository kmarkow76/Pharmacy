using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Domain.ModelsDb
{

    public class RecordDb
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }

}