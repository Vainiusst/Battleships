using System.ComponentModel.DataAnnotations;

namespace Battleships.Data.Models
{
    public class DbUser
    {
        [Key]
        public int UserId { get; set; }
        [MaxLength(20)]
        public string Username { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
