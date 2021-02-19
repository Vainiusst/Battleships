using Battleships.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Services
{
    public class RandomShotService : IRandomShotService
    {
        public Player Player { get; set; }
        public Random Rand { get; set; }

        public RandomShotService(Player player, Random rand)
        {
            Player = player;
            Rand = rand;
        }

        public Coordinate Shoot()
        {
            Coordinate shotToTake = RandomCoord();

            if (!Player.ShotsTaken.Contains(shotToTake))
            {
                Player.ShotsTaken.Add(shotToTake);
                return shotToTake;
            }
            else
            {
                return Shoot();
            }
        }

        private Coordinate RandomCoord()
        {
            int maxColumn = Player.Grid.Width;
            int maxRow = Player.Grid.Height;

            return new Coordinate(Rand.Next(maxColumn), Rand.Next(maxRow));
        }
    }
}
