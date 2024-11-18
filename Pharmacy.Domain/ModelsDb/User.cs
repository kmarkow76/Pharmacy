using System;
using System.ComponentModel.DataAnnotations.Schema;
using Pharmacy.Domain.Enum;
namespace Pharmacy.Domain.ModelsDb;

//[Table("user")]
public class User
{
   // [Column("id")]
    public Guid Id { get; set; }

   // [Column("login")]
    public string Login { get; set; }

   // [Column("username")]
    public string Username { get; set; }

   // [Column("password")]
    public string Password { get; set; }

   // [Column("email")]
    public string Email { get; set; }

    //[Column("part_image")]
    public string PartImage { get; set; }

   // [Column("created_at")]
    public TimeSpan CreatedAt { get; set; }

   // [Column("role")]
    public Role Role { get; set; }
    public Cart Cart { get; set; }
    public List<Order> Orders { get; set; }
}