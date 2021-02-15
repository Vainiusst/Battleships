using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models
{
    public class User
    {
        public string Name { get; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        //public User()
        //{
        //    Name = name;
        //    Wins = 0;
        //    Losses = 0;
        //}
    }
}
