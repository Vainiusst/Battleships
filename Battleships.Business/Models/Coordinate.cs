using System;

namespace Battleships.Business.Models
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public int Column { get; }
        public int Row { get; }

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
