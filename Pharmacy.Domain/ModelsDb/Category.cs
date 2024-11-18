using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Domain.ModelsDb
{
    [Table("category")]
    public class Category
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}