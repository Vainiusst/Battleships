using System;
using System.Collections.Generic;

namespace Battleships.Business.Models
{
    public class Coordinate : IEqualityComparer<Coordinate>, IEquatable<Coordinate>
    {
        public int Column { get; set; }
        public int Row { get; set; }

        public Coordinate(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public bool Equals(Coordinate x, Coordinate y)
        {
            return x.Column == y.Column && x.Row == y.Row;
        }

        public int GetHashCode(Coordinate obj)
        {
            return new { obj.Column, obj.Row }.GetHashCode();
        }

        public bool Equals(Coordinate other)
        {
            return this.Column == other.Column && this.Row == other.Row;
        }
    }
}
