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
    public class HitOutputService : IHitOutputService
    {
        private GameWindow GW { get; set; }

        public HitOutputService(GameWindow gw)
        {
            GW = gw;
        }

        public void OutputTheHit(TextBlock lbl, Move mv, Player plr, Player opponent)
        {
            StringBuilder outputString = new StringBuilder($"{plr.Name} fired at square {mv.MoveStr}. ");

            if (IsHit(opponent, mv.MoveCoord))
            {
                outputString.Append(ActionsAfterHit(plr, opponent, mv.MoveCoord));
            }
            else
            {
                ColourButtonBlue(plr, mv.MoveCoord);
                outputString.Append("The shot missed.");
            }

            lbl.Text = outputString.ToString();
        }

        private string ActionsAfterHit(Player plr, Player opponent, Coordinate coord)
        {
            var shipHit = opponent.Ships.FirstOrDefault(s => s.Placement.Contains(coord));

            if (shipHit.IsSunk)
            {
                return "The shot hit sunken ship.";
            }
            else if (shipHit.HitsTaken.Contains(coord))
            {
                return "The shot hit a damaged part of the ship.";
            }
            ColourButtonRed(plr, coord);
            var str = $"The shot hit the ship of size {FindShipSize(opponent, coord)}.";
            if (!shipHit.HitsTaken.Contains(coord)) shipHit.HitsTaken.Add(coord);
            if (shipHit.IsSunk) str = $"{str} The ship has sunk.";
            return str;
        }

        public bool IsHit(Player plr, Coordinate coordShotAt)
        {
            return plr.Ships
                .SelectMany(c => c.Placement)
                .Contains(coordShotAt);
        }

        private int FindShipSize(Player plr, Coordinate coord)
        {
            var shipList = plr.Ships.Select(s => s.Placement).ToList();
            foreach (var list in shipList)
            {
                if (list.Contains(coord)) return list.Count;
            }
            return -1;
        }

        private void ColourButtonBlue(Player plr, Coordinate coord)
        {
            if (plr.Name == "Computer")
            {
                FindBtn(coord, GW.PlayerBoxGrid.Children).Background = Brushes.Blue;
                return;
            }
            FindBtn(coord, GW.PlayerGuessBoxGrid.Children).Background = Brushes.Blue;
        }

        private void ColourButtonRed(Player plr, Coordinate coord)
        {
            if (plr.Name == "Computer")
            {
                FindBtn(coord, GW.PlayerBoxGrid.Children).Background = Brushes.Red;
                return;
            }
            FindBtn(coord, GW.PlayerGuessBoxGrid.Children).Background = Brushes.Red;
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
