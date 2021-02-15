using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Battleships.Data.Models
{
    public class DbScore
    {
        [Key]
        public int ScoreId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        [NotMapped]
        public int Rank => Wins - Losses;

        public virtual DbUser User { get; set; }
    }
}
