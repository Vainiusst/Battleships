using Battleships.Business.Models;
using Battleships.Business.Models.GameModels;
using System;

namespace Battleships.Business.Services
{
    public class Game : IGame
    {
        public Player Player { get; set; }
        public Player ComputerPlayer { get; set; }
        public CoordinateTranslationService CTS { get; set; }
        public IRandomShotService RSS { get; set; }

        public Game(Player player, Player computerPlayer)
        {
            Player = player;
            ComputerPlayer = computerPlayer;
            CTS = new CoordinateTranslationService();
            //RSS = new RandomShotService(ComputerPlayer, new Random());
            RSS = new ComputerShootingService(computerPlayer, player);
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
