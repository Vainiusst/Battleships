using System.ComponentModel.DataAnnotations;

namespace Battleships.Data.Models
{
    public class DbUser
    {
        [Key]
        public int UserId { get; set; }
        [MaxLength(20), Required]
        public string Username { get; set; }
        [MaxLength(100), Required]
        public string Email { get; set; }
        [MaxLength(150), Required]
        public string Password { get; set; }
        [MaxLength(400), Required]
        public string Salt { get; set; }
    }
}
