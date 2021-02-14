using Battleships.Business.Models;
using Battleships.Business.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battleships.Business.Services
{
    public class Game
    {
        public Player Player { get; set; }
        public Player ComputerPlayer { get; set; }
        private TaskCompletionSource<Coordinate> WaitForUserMove { get; set; }
        private Task<Coordinate> UserMoveTask { get; set; }

        public Game(Player player, Player computerPlayer)
        {
            Player = player;
            ComputerPlayer = computerPlayer;
        }

        private void StartGame()
        {
            RandomShotService rss = new RandomShotService(ComputerPlayer, new Random());
            var whoStarts = CoinToss.Toss();
            MessageBox.Show($"{whoStarts.ToString()} will start the game!");

            var PlayerShipsAfloat = Player.Ships.Where(s => s.IsSunk == false).ToList().Count();
            var ComputerShipsAfloat = ComputerPlayer.Ships.Where(s => s.IsSunk == false).ToList().Count();

            while (PlayerShipsAfloat > 0 || ComputerShipsAfloat > 0)
            {
                GameInitiate(whoStarts);
            }
        }

        private Coordinate Shoot(Player player, Coordinate coord)
        {
            player.ShotsTaken.Add(coord);
            return coord;
        }

        private void GameInitiate(CoinToss.Players player)
        {
            if(player == CoinToss.Players.Player)
            {
                PlayerFirst();
            }
            else
            {
                ComputerFirst();
            }
        }

        private void PlayerFirst()
        {
            //PlayerMove()
            //ComputerMove()
        }

        private void ComputerFirst()
        {
            //ComputerMove()
            //PlayerMove()
        }

        private Move ComputerMove(RandomShotService rss)
        {
            rss.Shoot();
        }


        //public async Task<Coordinate> WaitForShot()
        //{

        //}

        //private bool IsHit(Player plr, Coordinate coordShotAt)
        //{
        //    foreach(Ship ship in plr.Ships)
        //    {
        //        foreach(Coordinate coord in ship.Placement)
        //        {
        //            if (coordShotAt.Equals(coord))
        //            {

        //            }
        //        }
        //    }
        //}
    }
}
