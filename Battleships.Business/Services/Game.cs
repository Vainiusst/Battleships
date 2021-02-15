using Battleships.Business.Models;
using Battleships.Business.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        //public Round ComputerFirst(RandomShotService rss, Label lbl, Coordinate coord)
        //{
        //    var move1 = FullComputerMove(rss, lbl);
        //    var move2 = FullPlayerMove(coord, lbl);

        //    return new Round(move2, move1);
        //}

        //public Round PlayerFirst(RandomShotService rss, Label lbl, Coordinate coord)
        //{
        //    var move1 = FullPlayerMove(coord, lbl);
        //    var move2 = FullComputerMove(rss, lbl);

        //    return new Round(move1, move2);
        //}

        public Move FullPlayerMove(Coordinate coord, Label lbl)
        {
            var returnMv = PlayerMove(coord);
            OutputTheHit(lbl, returnMv, Player);

            return returnMv;
        }

        public Move FullComputerMove(RandomShotService rss, Label lbl)
        {
            var returnMv = ComputerMove(rss);
            OutputTheHit(lbl, returnMv, ComputerPlayer);

            return returnMv;
        }

        public Move PlayerMove(Coordinate shootingCoord)
        {
            Player.ShotsTaken.Add(shootingCoord);
            return new Move(shootingCoord, CoordinateTranslationService.Translate(shootingCoord));
        }

        public Move ComputerMove(RandomShotService rss)
        {
            var shootingCoord = rss.Shoot();
            ComputerPlayer.ShotsTaken.Add(shootingCoord);
            return new Move(shootingCoord, CoordinateTranslationService.Translate(shootingCoord));
        }

        public bool IsHit(Player plr, Coordinate coordShotAt)
        {
            return plr.Ships
                .Where(s => !s.IsSunk)
                .SelectMany(c => c.Placement)
                .ToList()
                .Contains(coordShotAt);
        }

        public int FindShipSize(Player plr, Coordinate coord)
        {
            var shipList = plr.Ships.Select(s => s.Placement).ToList();
            foreach (var list in shipList)
            {
                if (list.Contains(coord)) return list.Count;
            }
            return -1;
        }

        public void OutputTheHit(Label lbl, Move mv, Player plr)
        {
            StringBuilder outputString = new StringBuilder($"{plr.Name} fired at square {mv.MoveStr}. ");

            if (IsHit(plr, mv.MoveCoord))
            {
                outputString.Append($"The shot hit a ship of size {FindShipSize(plr, mv.MoveCoord)}");
            }
            else
            {
                outputString.Append("The shot missed.");
            }

            lbl.Content = outputString.ToString();
        }
    }
}
