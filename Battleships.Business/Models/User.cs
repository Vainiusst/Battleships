using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Rank { get; set; }

        public User(int userId, string name)
        {
            UserId = userId;
            Name = name;
            Wins = 0;
            Losses = 0;
            Rank = Wins - Losses;
        }

        public User(int userId, string name, int wins, int losses)
        {
            UserId = userId;
            Name = name;
            Wins = wins;
            Losses = losses;
            Rank = Wins - Losses;
        }
    }
}
