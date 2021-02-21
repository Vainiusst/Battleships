using Battleships.Business.Models;
using System.Windows.Controls;

namespace Battleships.Presentation.Controls
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
