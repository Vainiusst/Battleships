﻿using Battleships.Business.Models;
using Battleships.Business.Services;
using Battleships.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.Presentation.Services
{
    public class PreparationForGameService : IPreparationForGameService
    {
        private ButtonFillingService BFS { get; set; }
        private TaskCompletionSource<Coordinate> CoordinateTask { get; set; }
        private GameWindow GW { get; }
        public Orientation ShipOrientation { get; set; }

        public PreparationForGameService(GameWindow gw)
        {
            BFS = new ButtonFillingService();
            CoordinateTask = new TaskCompletionSource<Coordinate>();
            GW = gw;
            ShipOrientation = Orientation.Horizontal; //Default orientation is horizontal.
        }

        public void Prepare()
        {
            PrepareComputerForGame(GW.PlayerPC);
            PreparePlayerForGame(GW.PlayerHum);
            BFS.AddHandlerAndEnableButtons(GW.PlayerGuessBoxGrid.Children, GW.SetShipClick);
        }

        private void PrepareComputerForGame(Player pc)
        {
            var rsps = new RandomShipPlacementService(pc.Grid.Height, pc.Grid.Width, new Random());
            SetComputerShips(pc, rsps);
        }

        private void SetComputerShips(Player p, RandomShipPlacementService rsps)
        {
            while (p.Ships.Count > rsps.OccupiedCoordinates.Count)
            {
                var theShip = p.Ships[rsps.OccupiedCoordinates.Count];
                rsps.PlaceShipRandom(theShip);
            }
        }

        private void PreparePlayerForGame(Player player)
        {
            var sps = new ShipPlacementService(player.Grid.Height, player.Grid.Width);

            CoordinateTask = new TaskCompletionSource<Coordinate>();

            bool[] ShouldHaveContent = new bool[] { true, false };

            BFS.FillWithButtons(GW.PlayerBoxGrid, player.Grid, ShouldHaveContent[1], GW.SetShipClick);
            BFS.FillWithButtons(GW.PlayerGuessBoxGrid, player.GuessGrid, ShouldHaveContent[0], null);

            SetShips(player, sps);
        }

        private async void SetShips(Player p, ShipPlacementService sps)
        {
            while (p.Ships.Count > sps.OccupiedCoordinates.Count)
            {
                var theShip = p.Ships[sps.OccupiedCoordinates.Count];
                GW.PCShotInfo.Content = $"Setting the coordinates for ship of size {theShip.Size}";
                Coordinate coordToUse = await GW.UserClickedOnCoordinateBoard();
                CoordinateTask = null;
                sps.PlaceShip(ShipOrientation, coordToUse, theShip);
                ColourTheShip(theShip.Placement);
            }

            GW.PlayerButtonsPanel.Children.Clear();
            GW.PCShotInfo.Content = null;
            GenerateStartButton();
        }

        private void ColourTheShip(List<Coordinate> coords)
        {
            List<ButtonExtended> btns = new List<ButtonExtended>();

            foreach (var item in GW.PlayerBoxGrid.Children)
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
            btn.Click += Launcher;
            GW.PlayerButtonsPanel.Children.Add(btn);
        }

        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            GW.PlayerButtonsPanel.Children.Clear();
        }

        private void Launcher(object sender, RoutedEventArgs e)
        {
            GW.InitiateGame();
        }
    }
}
