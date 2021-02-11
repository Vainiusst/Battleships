using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public int Column { get; set; }
        public int Row { get; set; }

        public Coordinate(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public bool Equals(Coordinate other)
        {
            return this.Column == other.Column && this.Row == other.Row;
        }
    }
}
