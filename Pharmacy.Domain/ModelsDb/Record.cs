using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Domain.ModelsDb
{
    [Table("record")]
    public class Record
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("description")]
        public string Description { get; set; }
    }
}