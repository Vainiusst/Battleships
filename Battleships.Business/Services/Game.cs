using Battleships.Business.Models;
using Battleships.Business.Models.GameModels;
using System;
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
        public RandomShotService RSS { get; set; }

        public Game(Player player, Player computerPlayer)
        {
            Player = player;
            ComputerPlayer = computerPlayer;
            CTS = new CoordinateTranslationService();
            RSS = new RandomShotService(ComputerPlayer, new Random());
        }

        public Move PlayerMove(Coordinate shootingCoord)
        {
            Player.ShotsTaken.Add(shootingCoord);
            return new Move(shootingCoord, CTS.Translate(shootingCoord));
        }

        public Move ComputerMove()
        {
            var shootingCoord = RSS.Shoot();
            return new Move(shootingCoord, CTS.Translate(shootingCoord));
        }
    }
}
