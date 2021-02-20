using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models
{
    public class Ship
    {
        public int Size { get; }
        public int HitsTaken { get; set; }
        public bool IsSunk => HitsTaken >= Size;
        public List<Coordinate> Placement { get; set; }

        public Ship(int size)
        {
            Size = size;
            HitsTaken = 0;
            Placement = new List<Coordinate>();
        }
    }
}
