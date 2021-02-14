﻿using Battleships.Business.Models;
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
        private Orientation ShipOrientation { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            var p1 = new Player();
            var pc = new Player();
            var rsps = new ShipPlacementService(p1.Grid.Height, p1.Grid.Width);
            var rrsps = new RandomShipPlacementService(pc.Grid.Height, pc.Grid.Width);
            var bfs = new ButtonFillingService();
            ShipOrientation = Orientation.Horizontal;

            SetComputerShips(pc, rrsps);

            ClickSomewhere = new TaskCompletionSource<Coordinate>();
            ClickSomewhereTask = ClickSomewhere.Task;

            bool[] ShouldHaveContent = new bool[] { true, false };
            bfs.FillWithButtons(PlayerBoxGrid, p1.Grid, ShouldHaveContent[1], WaitingForMouseClick);
            bfs.FillWithButtons(PlayerGuessBoxGrid, p1.GuessGrid, ShouldHaveContent[0], null);

            SetShips(p1, rsps);
        }

        private async void SetShips(Player p, ShipPlacementService sps)
        {
            while (p.Ships.Count > sps.OccupiedCoordinates.Count)
            {
                var theShip = p.Ships[sps.OccupiedCoordinates.Count];
                InfoLabel.Content = $"Setting the coordinates for ship of size {theShip.Size}";
                await UserClickedOnCoordinateBoard();
                sps.PlaceShip(ShipOrientation, CurrentCoordinate, theShip);

                CurrentCoordinate = null;
                ColourTheShip(theShip.Placement);
            }
            InfoLabel.Content = null;
        }

        private void SetComputerShips(Player p, RandomShipPlacementService rsps)
        {
            while (p.Ships.Count > rsps.OccupiedCoordinates.Count)
            {
                var theShip = p.Ships[rsps.OccupiedCoordinates.Count];
                rsps.PlaceShipRandom(theShip);
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
            ShipOrientation = Orientation.Horizontal;
        }

        private void btnVertical_Click(object sender, RoutedEventArgs e)
        {
            ShipOrientation = Orientation.Vertical;
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
