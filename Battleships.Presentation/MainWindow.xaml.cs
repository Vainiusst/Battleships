using Battleships.Business.Models;
using Battleships.Business.Services;
using Battleships.Presentation.Models;
using Battleships.Presentation.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskCompletionSource<Coordinate> ClickSomewhere { get; set; }
        private Task<Coordinate> ClickSomewhereTask { get; set; }
        private Coordinate CurrentCoordinate { get; set; }
        private bool ShipOrientation { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            var p1 = new Player();
            var pc = new Player();
            var sps = new ShipPlacementService(p1.Grid.Height, p1.Grid.Width);
            var spsPC = new ShipPlacementService(pc.Grid.Height, pc.Grid.Width);
            var bfs = new ButtonFillingService();
            ShipOrientation = true;

            SetComputerShips(pc, spsPC);

            //ClickSomewhere = new TaskCompletionSource<Coordinate>();
            //ClickSomewhereTask = ClickSomewhere.Task;

            
            //bfs.FillWithButtons(PlayerBoxGrid, p1.Grid, false, WaitingForMouseClick);
            //bfs.FillWithButtons(PlayerGuessBoxGrid, p1.GuessGrid, true, null);

            //SetShips(p1, sps);
        }

        private void SetComputerShips(Player p, ShipPlacementService sps)
        {
            while (p.Ships.Count > sps.OccupiedCoordinates.Count)
            {
                var theShip = p.Ships[sps.OccupiedCoordinates.Count];
                sps.PlaceShipRandom(theShip);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("The computer ships have been placed.");

            foreach(Ship ship in p.Ships)
            {
                sb.AppendLine("Coordinates:");
                foreach(Coordinate coord in ship.Placement)
                {
                    sb.Append($"{coord.Column};{coord.Row} ");
                }
                sb.AppendLine();
            }
            
            MessageBox.Show(sb.ToString());
        }

        private string OrientationSetter(bool shipOrient)
        {
            return shipOrient ? Orientation.Horizontal.ToString() : Orientation.Vertical.ToString();
        }

        public void WaitingForMouseClick(object sender, RoutedEventArgs args)
        {
            if (ClickSomewhere != null)
            {
                ButtonExtended btn = (ButtonExtended)sender;
                CurrentCoordinate = btn.Coordinate;
                ClickSomewhere.TrySetResult(btn.Coordinate);
                ClickSomewhere = null;
            }    
        }

        private async Task<Coordinate> UserClickedOnCoordinateBoard()
        {
            ClickSomewhere = new TaskCompletionSource<Coordinate>();
            return await ClickSomewhere.Task;
        }

        private void btnHorizontal_Click(object sender, RoutedEventArgs e)
        {
            ShipOrientation = true;
        }

        private void btnVertical_Click(object sender, RoutedEventArgs e)
        {
            ShipOrientation = false;
        }

        private async void SetShips(Player p, ShipPlacementService sps)
        {
            while (p.Ships.Count > sps.OccupiedCoordinates.Count)
            {                
                var theShip = p.Ships[sps.OccupiedCoordinates.Count];
                ShipLabel.Content = $"Setting the coordinates for ship of size {theShip.Size}";
                await UserClickedOnCoordinateBoard();
                sps.PlaceShip(OrientationSetter(ShipOrientation), CurrentCoordinate, theShip);

                CurrentCoordinate = null;
                ColourTheShip(theShip.Placement);
            }
            ShipLabel.Content = null;
        }

        enum Orientation
        {
            Horizontal,
            Vertical
        }

        private void ColourTheShip(List<Coordinate> coords)
        {
            List<ButtonExtended> btns = new List<ButtonExtended>();

            foreach (var item in PlayerBoxGrid.Children)
            {
                btns.Add((ButtonExtended)item);
            }

            foreach (Coordinate coord in coords)
            {
                var btn = btns.FirstOrDefault(b => b.Coordinate.Equals(coord));

                btn.Opacity = 1;
                btn.Background = Brushes.Gray;
            }            
        }
    }
}
