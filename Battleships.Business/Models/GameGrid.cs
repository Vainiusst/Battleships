using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models
{
    public class GameGrid
    {
        public int Height { get; }
        public int Width { get; }
        public List<Square> Squares { get; set; }

        public GameGrid()
        {
            //Default option for grid is 10x10
            Height = 10;
            Width = 10;
            Squares = new List<Square>();

            for(int i = 1; i <= this.Width; i++)
            {
                for(int j = 1; i <= this.Height; j++)
                {
                    Squares.Add(new Square(i, j));
                }
            }
        }
    }
}
