﻿using Battleships.Business.Models;
using Battleships.Business.Services;
using Battleships.Presentation.Controls;
using Battleships.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.Presentation
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private TaskCompletionSource<Coordinate> ClickSomewhere { get; set; }
        private Orientation ShipOrientation { get; set; }
        private Game CurrentGame { get; set; }
        public Player PlayerPC { get; set; }
        public Player PlayerHum { get; set; }
        public GameWindow(User user)
        {
            InitializeComponent();

            PlayerPC = new Player(new User(0, "Computer"));
            PlayerHum = new Player(user);

            ShipOrientation = Orientation.Horizontal; //Default orientation is horizontal

            PrepareComputerForGame(PlayerPC);
            PreparePlayerForGame(PlayerHum);
            PopulateButtonsWithEH();
        }

        public void InitiateGame()
        {
            CurrentGame = new Game(PlayerHum, PlayerPC);
            var playerToStart = CoinToss.Toss();

            if (playerToStart == 0)
            {
                MessageBox.Show($"{PlayerHum.Name} starts first!");
                while (PlayerHum.Ships.Count > 0 || PlayerPC.Ships.Count > 0)
                {
                    AddInfoLabel.Content = $"Waiting for the {PlayerHum.Name}'s move.";
                    var t = Task.Run(() => PlayersShot());
                    t.Wait();
                    CurrentGame.FullComputerMove(InfoLabel);
                }
            }
            else
            {
                MessageBox.Show("Computer starts first!");
                while (PlayerHum.Ships.Count > 0 || PlayerPC.Ships.Count > 0)
                {
                    CurrentGame.FullComputerMove(InfoLabel);
                    AddInfoLabel.Content = $"Waiting for the {PlayerHum.Name}'s move.";
                    var t = Task.Run(() => PlayersShot());
                    t.Wait();
                }
            }
        }

        private void PrepareComputerForGame(Player pc)
        {
            var rsps = new RandomShipPlacementService(pc.Grid.Height, pc.Grid.Width, new Random());
            SetComputerShips(pc, rsps);
        }

        private void PreparePlayerForGame(Player player)
        {
            var sps = new ShipPlacementService(player.Grid.Height, player.Grid.Width);
            var bfs = new ButtonFillingService();

            ClickSomewhere = new TaskCompletionSource<Coordinate>();

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

        private async void PlayersShot()
        {
            var shootingCoord = await UserClickedOnCoordinateBoard();
            CurrentGame.FullPlayerMove(shootingCoord, InfoLabel);
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
                ClickSomewhere.SetResult(btn.Coordinate);
                ClickSomewhere = null;
            }
        }

        private async Task<Coordinate> UserClickedOnCoordinateBoard()
        {
            ClickSomewhere = new TaskCompletionSource<Coordinate>();
            return await ClickSomewhere.Task; //<--- Something happens here
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
                var btn = btns.FirstOrDefault(b => b.Coordinate.Equals(coord, b.Coordinate));

                btn.Opacity = 1;
                btn.Background = Brushes.Gray;
            }
        }

        private void PopulateButtonsWithEH()
        {
            foreach(ButtonExtended btn in PlayerGuessBoxGrid.Children)
            {
                btn.Click += SetShipClick;
            }
        }

        private void Launcher(object sender, RoutedEventArgs e)
        {
            InitiateGame();
        }

        private void GenerateStartButton()
        {
            var btn = new StartGameButton();
            btn.Click += btnStartGame_Click;
            btn.Click += Launcher;
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
