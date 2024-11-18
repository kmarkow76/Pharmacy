
using System.ComponentModel.DataAnnotations.Schema;
namespace Pharmacy.Domain.ModelsDb;

    [Table("medicines")]
    public class Medicine
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("prescription_required")]
        public bool PrescriptionRequired { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("created_at")]
        public TimeSpan CreatedAt { get; set; }

        [Column("category_id")]
        public Guid CategoryId { get; set; }
        
        public Category Category { get; set; }
    }
