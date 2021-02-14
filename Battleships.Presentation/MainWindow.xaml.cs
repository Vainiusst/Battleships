using Battleships.Business.Models;
using Battleships.Business.Services;
using Battleships.Presentation.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Battleships.Presentation.Controls;

namespace Battleships.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TaskCompletionSource<Coordinate> ClickSomewhere { get; set; }
        private Task<Coordinate> ClickSomewhereTask { get; set; }
        private Orientation ShipOrientation { get; set; }
        
        public MainWindow()
        {
            Player PlayerPC = new Player();
            Player Player = new Player();

            ShipOrientation = Orientation.Horizontal; //Default orientation is horizontal

            InitializeComponent();            
            PrepareComputerForGame(PlayerPC);
            PreparePlayerForGame(Player);
        }

        private void PrepareComputerForGame(Player pc)
        {
            var rsps = new RandomShipPlacementService(pc.Grid.Height, pc.Grid.Width);
            SetComputerShips(pc, rsps);
        }

        private void PreparePlayerForGame(Player player)
        {
            var sps = new ShipPlacementService(player.Grid.Height, player.Grid.Width);
            var bfs = new ButtonFillingService();

            ClickSomewhere = new TaskCompletionSource<Coordinate>();
            ClickSomewhereTask = ClickSomewhere.Task;

            bool[] ShouldHaveContent = new bool[] { true, false };
            bfs.FillWithButtons(PlayerBoxGrid, player.Grid, ShouldHaveContent[1], SetShipClick);
            bfs.FillWithButtons(PlayerGuessBoxGrid, player.GuessGrid, ShouldHaveContent[0], null);

            SetShips(player, sps);
        }

        private async void SetShips(Player p, ShipPlacementService sps)
        {
            while (p.Ships.Count > sps.OccupiedCoordinates.Count)
            {
                var theShip = p.Ships[sps.OccupiedCoordinates.Count];
                InfoLabel.Content = $"Setting the coordinates for ship of size {theShip.Size}";
                Coordinate coordToUse = await UserClickedOnCoordinateBoard();
                sps.PlaceShip(ShipOrientation, coordToUse, theShip);

                ColourTheShip(theShip.Placement);
            }

            PlayerButtonsPanel.Children.Clear();
            InfoLabel.Content = null;
            GenerateStartButton();
        }

        private void SetComputerShips(Player p, RandomShipPlacementService rsps)
        {
            while (p.Ships.Count > rsps.OccupiedCoordinates.Count)
            {
                var theShip = p.Ships[rsps.OccupiedCoordinates.Count];
                rsps.PlaceShipRandom(theShip);
            }
        }

        public void SetShipClick(object sender, RoutedEventArgs args)
        {
            if (ClickSomewhere != null)
            {
                ButtonExtended btn = (ButtonExtended)sender;
                ClickSomewhere.TrySetResult(btn.Coordinate);
                ClickSomewhere = null;
            }    
        }

        private async Task<Coordinate> UserClickedOnCoordinateBoard()
        {
            ClickSomewhere = new TaskCompletionSource<Coordinate>();
            return await ClickSomewhere.Task;
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

        private void GenerateStartButton()
        {
            var btn = new StartGameButton();
            btn.Click += btnStartGame_Click;
            PlayerButtonsPanel.Children.Add(btn);
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            btnSubmitAction.IsEnabled = true;
            PlayerButtonsPanel.Children.Clear();
        }

        private void btnHorizontal_Click(object sender, RoutedEventArgs e)
        {
            ShipOrientation = Orientation.Horizontal;
            btnVertical.Background = Brushes.LightGray;
            btnHorizontal.Background = Brushes.Green;
        }

        private void btnVertical_Click(object sender, RoutedEventArgs e)
        {
            ShipOrientation = Orientation.Vertical;
            btnHorizontal.Background = Brushes.LightGray;
            btnVertical.Background = Brushes.Green;
        }
    }
}
