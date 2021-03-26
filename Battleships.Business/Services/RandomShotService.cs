using Battleships.Business.Models;
using System;

namespace Battleships.Business.Services
{
    //Class that fires random shots. USed by the computer player. To be replaced by the ComputerShootingService in the future.
    public class RandomShotService : IRandomShotService
    {
        public Player Computer { get; set; }
        public Random Rand { get; set; }

        public RandomShotService(Player computer, Random rand)
        {
            Computer = computer;
            Rand = rand;
        }

        public Coordinate Shoot()
        {
            Coordinate shotToTake = RandomCoord();

            if (!Computer.ShotsTaken.Contains(shotToTake))
            {
                Computer.ShotsTaken.Add(shotToTake);
                return shotToTake;
            }
            else
            {
                return Shoot();
            }
        }

        private Coordinate RandomCoord()
        {
            int maxColumn = Computer.Grid.Width;
            int maxRow = Computer.Grid.Height;

            return new Coordinate(Rand.Next(maxColumn), Rand.Next(maxRow));
        }
    }
}
