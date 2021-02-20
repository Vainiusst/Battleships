using Battleships.Business.Models;
using Battleships.Business.Models.GameModels;
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
        private TaskCompletionSource<Coordinate> CoordinateTask { get; set; }
        private TaskCompletionSource<Move> MoveTask { get; set; }
        private Orientation ShipOrientation { get; set; }
        private Game CurrentGame { get; set; }
        public Player PlayerPC { get; set; }
        public Player PlayerHum { get; set; }
        public List<Round> Rounds { get; set; }
        public GameWindow(User user)
        {
            InitializeComponent();

            Rounds = new List<Round>();
            MovesGrid.ItemsSource = Rounds;

            PlayerPC = new Player(new User(0, "Computer"));
            PlayerHum = new Player(user);

            ShipOrientation = Orientation.Horizontal; //Default orientation is horizontal

            PrepareComputerForGame(PlayerPC);
            PreparePlayerForGame(PlayerHum);
            PopulateButtonsWithEH();
        }

        public async void InitiateGame()
        {
            CurrentGame = new Game(PlayerHum, PlayerPC);
            var playerToStart = CoinToss.Toss();

            if (playerToStart == CoinToss.Players.Player)
            {
                MessageBox.Show($"{PlayerHum.Name} starts first!");
                while (PlayerHum.Ships.Count > 0 && PlayerPC.Ships.Count > 0)
                {
                    await PlayerFirst();
                }
            }
            else
            {
                MessageBox.Show("Computer starts first!");
                while (PlayerHum.Ships.Count > 0 && PlayerPC.Ships.Count > 0)
                {
                    await PCFirst();
                }
            }
        }

        public async Task PCFirst()
        {
            var pcMove = CurrentGame.FullComputerMove(PCShotInfo);
            Rounds.Add(new Round(null, pcMove));            
            MovesGrid.Items.Refresh();
            AddInfoLabel.Content = $"Waiting for the {PlayerHum.Name}'s move.";
            var plrMove = await PlayersShot();
            MoveTask = null;
            Rounds.Last().PlayerMove = plrMove;
            MovesGrid.Items.Refresh();
            AddInfoLabel.Content = "";
        }

        public async Task PlayerFirst()
        {
            AddInfoLabel.Content = $"Waiting for the {PlayerHum.Name}'s move.";
            var plrMove = await PlayersShot();
            Rounds.Add(new Round(plrMove, null));
            MovesGrid.Items.Refresh();
            AddInfoLabel.Content = "";
            var pcMove = CurrentGame.FullComputerMove(PCShotInfo);
            MoveTask = null;
            Rounds.Last().ComputerMove = pcMove;
            MovesGrid.Items.Refresh();
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

            CoordinateTask = new TaskCompletionSource<Coordinate>();

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
                PCShotInfo.Content = $"Setting the coordinates for ship of size {theShip.Size}";
                Coordinate coordToUse = await UserClickedOnCoordinateBoard();
                CoordinateTask = null;
                sps.PlaceShip(ShipOrientation, coordToUse, theShip);
                ColourTheShip(theShip.Placement);
            }

            PlayerButtonsPanel.Children.Clear();
            PCShotInfo.Content = null;
            GenerateStartButton();
        }

        private async Task<Move> PlayersShot()
        {
            var shootingCoord = await UserClickedOnCoordinateBoard();
            CoordinateTask = null;
            MoveTask = new TaskCompletionSource<Move>();
            var move = CurrentGame.FullPlayerMove(shootingCoord, PlayerShotInfo);
            MoveTask.SetResult(move);
            return MoveTask.Task.Result;
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
            if (CoordinateTask != null)
            {
                ButtonExtended btn = (ButtonExtended)sender;
                CoordinateTask.SetResult(btn.Coordinate);
            }
        }

        private async Task<Coordinate> UserClickedOnCoordinateBoard()
        {
            CoordinateTask = new TaskCompletionSource<Coordinate>();
            return await CoordinateTask.Task;
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
