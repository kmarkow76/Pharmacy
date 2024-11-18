using System;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmacy.Domain.Enum;

namespace Pharmacy.Domain.ModelsDb
{
    [Table("order")]
    public class Order
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [Column("status")]
        public OrderStatus Status { get; set; }

        [Column("created_at")]
        public TimeSpan CreatedAt { get; set; }

        [Column("medic_id")]
        public Guid MedicId { get; set; }

        public User User { get; set; }
        public Medicine Medicine { get; set; }
    }
}