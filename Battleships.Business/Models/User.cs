using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models
{
    public class User
    {
        public int UserId { get; }
        public string Name { get; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public User(int userId, string name)
        {
            UserId = userId;
            Name = name;
            Wins = 0;
            Losses = 0;
        }
    }
}
