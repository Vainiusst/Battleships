using Battleships.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Battleships.Presentation.Models
{
    public class ButtonExtended : Button
    {
        public Coordinate Coordinate { get; set; }

        public ButtonExtended(int x, int y)
        {
            Coordinate = new Coordinate(x, y);
        }
    }
}
