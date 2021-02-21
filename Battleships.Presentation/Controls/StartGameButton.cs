using System.Windows.Controls;
using System.Windows.Media;

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
