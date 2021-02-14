using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.Presentation.Controls
{
    public class StartGameButton : Button
    {
        public StartGameButton()
        {
            this.Content = "Start game!";
            this.FontSize = 20;
            this.Width = 150;
            this.Background = Brushes.LimeGreen;
        }
    }
}
