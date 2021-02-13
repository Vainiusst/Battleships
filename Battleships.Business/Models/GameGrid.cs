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

        public GameGrid()
        {
            //Default option for grid is 10x10
            Height = 10;
            Width = 10;
        }
    }
}
