using System;
using System.Collections.Generic;

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
            return Column == other.Column && Row == other.Row;
        }
    }
}
