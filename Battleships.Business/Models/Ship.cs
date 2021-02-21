using System.Collections.Generic;

namespace Battleships.Business.Models
{
    public class Ship
    {
        public int Size { get; }
        public List<Coordinate> HitsTaken { get; set; }
        public bool IsSunk => HitsTaken.Count >= Size;
        public List<Coordinate> Placement { get; set; }

        public Ship(int size)
        {
            Size = size;
            HitsTaken = new List<Coordinate>();
            Placement = new List<Coordinate>();
        }
    }
}
