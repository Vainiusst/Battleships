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

        public Game(Player player, Player computerPlayer)
        {
            Player = player;
            ComputerPlayer = computerPlayer;
        }

        //private void StartGame()
        //{
        //    RandomShotService rss = new RandomShotService(ComputerPlayer, new Random());
        //    var whoStarts = CoinToss.Toss();
        //    MessageBox.Show($"{whoStarts.ToString()} will start the game!");

        //    var PlayerShipsAfloat = Player.Ships.Where(s => s.IsSunk == false).ToList().Count();
        //    var ComputerShipsAfloat = ComputerPlayer.Ships.Where(s => s.IsSunk == false).ToList().Count();

        //    while (PlayerShipsAfloat > 0 || ComputerShipsAfloat > 0)
        //    {
        //        GameInitiate(whoStarts);
        //    }
        //}

        //private Coordinate Shoot(Player player, Coordinate coord)
        //{
        //    player.ShotsTaken.Add(coord);
        //    return coord;
        //}

        //private void GameInitiate(CoinToss.Players player)
        //{
        //    if(player == CoinToss.Players.Player)
        //    {
        //        PlayerFirst();
        //    }
        //    else
        //    {
        //        ComputerFirst();
        //    }
        //}

        public Round PlayerFirst(Coordinate plrCoord, RandomShotService rss)
        {
            return new Round(PlayerMove(plrCoord), ComputerMove(rss));
        }

        public Round ComputerFirst(RandomShotService rss, Coordinate plrCoord)
        {
            return new Round(ComputerMove(rss), PlayerMove(plrCoord));
        }

        private Move PlayerMove(Coordinate shootingCoord)
        {
            Player.ShotsTaken.Add(shootingCoord);
            return new Move(shootingCoord, CoordinateTranslationService.Translate(shootingCoord));
        }

        private Move ComputerMove(RandomShotService rss)
        {
            var shootingCoord = rss.Shoot();
            ComputerPlayer.ShotsTaken.Add(shootingCoord);
            return new Move(shootingCoord, CoordinateTranslationService.Translate(shootingCoord));
        }

        //private bool IsHit(Player plr, Coordinate coordShotAt)
        //{
        //    return plr.Ships
        //        .Where(s => !s.IsSunk)
        //        .SelectMany(c => c.Placement)
        //        .ToList()
        //        .Contains(coordShotAt);
        //}

        //private int FindShipSize(Player plr, Coordinate coord)
        //{
        //    var shipList = plr.Ships.Select(s => s.Placement).ToList();
        //    foreach(var list in shipList)
        //    {
        //        if (list.Contains(coord))
        //        {
        //            return list.Count;
        //        }
        //    }
        //    return -1;
        //}
    }
}
