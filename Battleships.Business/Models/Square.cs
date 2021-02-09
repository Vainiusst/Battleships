using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models
{
    public class Square
    {
        public Coordinate Coordinate { get; set; }
        public bool IsOccupied { get; set; }

        public Square(int column, int row)
        {
            Coordinate = new Coordinate(column, row);
            IsOccupied = false;
        }
    }
}
