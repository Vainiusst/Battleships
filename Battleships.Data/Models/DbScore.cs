using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Battleships.Data.Models
{
    //Table for storing users' scores
    public class DbScore
    {
        [Key]
        public int ScoreId { get; set; }
        public int UserId { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Rank => Wins - Losses;

        [ForeignKey("UserId")]
        public virtual DbUser User { get; set; }
    }
}
