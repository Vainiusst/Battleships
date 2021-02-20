using Battleships.Business.Models;
using Battleships.Business.Models.GameModels;
using Battleships.Business.Services;
using Battleships.Presentation.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.Presentation.Services
{
    public class HitOutputService
    {
        private Game Game { get; }
        private GameWindow GW { get; set; }

        public HitOutputService(Game game, GameWindow gw)
        {
            Game = game;
            GW = gw;
        }

        public void OutputTheHit(TextBlock lbl, Move mv, Player plr, Player opponent)
        {
            StringBuilder outputString = new StringBuilder($"{plr.Name} fired at square {mv.MoveStr}. ");

            if (IsHit(opponent, mv.MoveCoord))
            {
                var shipHit = opponent.Ships.FirstOrDefault(s => s.Placement.Contains(mv.MoveCoord));
                outputString.Append($"The shot hit the ship of size {FindShipSize(opponent, mv.MoveCoord)}.");
                ColourButton(plr, mv.MoveCoord);
                shipHit.HitsTaken++;
                if (shipHit.IsSunk) outputString.Append(" The ship has sunk.");
            }
            else
            {
                ColourButton(plr, mv.MoveCoord);
                outputString.Append("The shot missed.");
            }

            lbl.Text = outputString.ToString();
        }


        public bool IsHit(Player plr, Coordinate coordShotAt)
        {
            return plr.Ships
                .Where(s => !s.IsSunk)
                .SelectMany(c => c.Placement)
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


        public void ColourButton(Player plr, Coordinate coord)
        {
            if (plr.Name == "Computer")
            {
                if (IsHit(Game.Player, coord))
                {
                    FindBtn(coord, GW.PlayerBoxGrid.Children).Background = Brushes.Red;
                    return;
                }
                FindBtn(coord, GW.PlayerBoxGrid.Children).Background = Brushes.Blue;
                return;
            }
            if (IsHit(Game.ComputerPlayer, coord))
            {
                FindBtn(coord, GW.PlayerGuessBoxGrid.Children).Background = Brushes.Red;
                return;
            }
            FindBtn(coord, GW.PlayerGuessBoxGrid.Children).Background = Brushes.Blue;
        }

        private ButtonExtended FindBtn(Coordinate coord, UIElementCollection gridChildren)
        {
            List<ButtonExtended> btns = new List<ButtonExtended>();
            foreach (var btn in gridChildren)
            {
                btns.Add((ButtonExtended)btn);
            }

            return btns.FirstOrDefault(b => b.Coordinate.Equals(coord));
        }
    }
}
