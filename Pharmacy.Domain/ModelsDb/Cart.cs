using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy.Domain.ModelsDb
{
    [Table("cart")]
    public class Cart
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("medicine_id")]
        public Guid MedicineId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("added_at")]
        public DateTime AddedAt { get; set; }

        public User User { get; set; }
        public Medicine Medicine { get; set; }
    }
}
