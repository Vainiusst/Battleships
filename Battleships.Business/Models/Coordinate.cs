using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models
{
    public class Coordinate
    {
        public int Column { get; set; }
        public int Row { get; set; }

        public Coordinate(int column, int row)
        {
            Column = column;
            Row = row;
        }
    }
}
