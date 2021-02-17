using Battleships.Business.Models;
using Battleships.Business.Models.GameModels;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Battleships.Business.Services
{
    public class Game
    {
        public Player Player { get; set; }
        public Player ComputerPlayer { get; set; }
        public CoordinateTranslationService CTS { get; set; }

        public Game(Player player, Player computerPlayer)
        {
            Player = player;
            ComputerPlayer = computerPlayer;
            CTS = new CoordinateTranslationService();
        }

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
            return new Move(shootingCoord, CTS.Translate(shootingCoord));
        }

        public Move ComputerMove(RandomShotService rss)
        {
            var shootingCoord = rss.Shoot();
            ComputerPlayer.ShotsTaken.Add(shootingCoord);
            return new Move(shootingCoord, CTS.Translate(shootingCoord));
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
