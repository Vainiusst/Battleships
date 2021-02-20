using Battleships.Business.Models;
using Battleships.Business.Services;
using Battleships.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.Presentation.Services
{
    public class HitColouringService
    {
        private Game Game { get; }
        private GameWindow GW { get; set; }

        public HitColouringService(Game game, GameWindow gw)
        {
            Game = game;
            GW = gw;
        }

        public void ColourButton(Player plr, Coordinate coord)
        {
            if (plr.Name == "Computer")
            {
                if (Game.IsHit(plr, coord))
                {
                    FindBtn(coord, GW.PlayerBoxGrid.Children).Background = Brushes.Red;
                    return;
                }
                FindBtn(coord, GW.PlayerBoxGrid.Children).Background = Brushes.Blue;
                return;
            }
            if (Game.IsHit(plr, coord))
            {
                FindBtn(coord, GW.PlayerGuessBoxGrid.Children).Background = Brushes.Red;
                return;
            }
            FindBtn(coord, GW.PlayerGuessBoxGrid.Children).Background = Brushes.Blue;
        }

        private ButtonExtended FindBtn(Coordinate coord, UIElementCollection gridChildren)
        {
            List<ButtonExtended> btns = new List<ButtonExtended>();
            foreach(var btn in gridChildren)
            {
                btns.Add((ButtonExtended)btn);
            }

            return btns.FirstOrDefault(b => b.Coordinate.Equals(coord));
        }
    }
}
