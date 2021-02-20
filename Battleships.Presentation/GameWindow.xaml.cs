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
        private PreparationForGameService PFGS { get; set; }
        private HitColouringService HCS { get; set; }
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

            PFGS = new PreparationForGameService(this);
            PFGS.Prepare();
        }

        public async void InitiateGame()
        {
            CurrentGame = new Game(PlayerHum, PlayerPC);
            HCS = new HitColouringService(CurrentGame, this);
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
            HCS.ColourButton(PlayerPC, pcMove.MoveCoord);
            Rounds.Add(new Round(null, pcMove));
            MovesGrid.Items.Refresh();
            AddInfoLabel.Content = $"Waiting for the {PlayerHum.Name}'s move.";
            var plrMove = await PlayersShot();
            MoveTask = null;
            HCS.ColourButton(PlayerHum, plrMove.MoveCoord);
            Rounds.Last().PlayerMove = plrMove;
            MovesGrid.Items.Refresh();
            AddInfoLabel.Content = "";
        }

        public async Task PlayerFirst()
        {
            AddInfoLabel.Content = $"Waiting for the {PlayerHum.Name}'s move.";
            var plrMove = await PlayersShot();
            MoveTask = null;
            HCS.ColourButton(PlayerHum, plrMove.MoveCoord);
            Rounds.Add(new Round(plrMove, null));
            MovesGrid.Items.Refresh();
            AddInfoLabel.Content = "";
            var pcMove = CurrentGame.FullComputerMove(PCShotInfo);
            HCS.ColourButton(PlayerPC, pcMove.MoveCoord);
            Rounds.Last().ComputerMove = pcMove;
            MovesGrid.Items.Refresh();
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

        public async Task<Coordinate> UserClickedOnCoordinateBoard()
        {
            CoordinateTask = new TaskCompletionSource<Coordinate>();
            return await CoordinateTask.Task;
        }




        //Event handlers are down here

        public void SetShipClick(object sender, RoutedEventArgs args)
        {
            if (CoordinateTask != null)
            {
                ButtonExtended btn = (ButtonExtended)sender;
                CoordinateTask.SetResult(btn.Coordinate);
            }
        }

        private void btnHorizontal_Click(object sender, RoutedEventArgs e)
        {
            PFGS.ShipOrientation = Orientation.Horizontal;
            btnVertical.Background = Brushes.LightGray;
            btnHorizontal.Background = Brushes.Green;
        }

        private void btnVertical_Click(object sender, RoutedEventArgs e)
        {
            PFGS.ShipOrientation = Orientation.Vertical;
            btnHorizontal.Background = Brushes.LightGray;
            btnVertical.Background = Brushes.Green;
        }
    }
}
