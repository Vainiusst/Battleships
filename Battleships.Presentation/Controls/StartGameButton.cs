using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Battleships.Presentation;

namespace Battleships.Presentation.Controls
{
    public class StartGameButton : Button
    {
        public StartGameButton()
        {
            this.Content = "Begin the game!";
            this.FontSize = 20;
            this.Width = 150;
            this.Background = Brushes.LimeGreen;
            this.Name = "btnStartGame";
        }
    }
}
