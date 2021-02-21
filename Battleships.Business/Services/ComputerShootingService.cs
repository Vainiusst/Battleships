using Battleships.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Battleships.Business.Services
{
    public class ComputerShootingService
    {
        //This is not implemented yet. After it is completed, this should replace RandomShotService
        //As this should be more advanced and use calculations for targeted shots instead of pure random ones.
        public Player Computer { get; set; }
        public Player Opponent { get; set; }
        public Random Rand { get; set; }
        public Coordinate HitShot { get; }
        public List<Coordinate> PotentialShots { get; set; }
        public List<Coordinate> Hits { get; set; }
        public Orientation Orientation { get; set; }

        public ComputerShootingService(Player computer, Player opponent, Coordinate hitShot)
        {
            Computer = computer;
            Opponent = opponent;
            Rand = new Random();
            Hits = new List<Coordinate>();
        }

        public Coordinate Shot()
        {
            return IsHit(RandomShot());
        }

        private IEnumerable<Coordinate> GeneratePotentials(Coordinate hitShot)
        {
            List<Coordinate> potentials = new List<Coordinate>();

            //Potential next shots can go 4 ways: up, down, left and right.
            potentials.Add(new Coordinate(hitShot.Column, hitShot.Row - 1));
            potentials.Add(new Coordinate(hitShot.Column, hitShot.Row + 1));
            potentials.Add(new Coordinate(hitShot.Column - 1, hitShot.Row));
            potentials.Add(new Coordinate(hitShot.Column + 1, hitShot.Row));

            return potentials;
        }

        private Coordinate IsHit(Coordinate coord)
        {
            if (Opponent.Ships.Where(s => s.IsSunk).SelectMany(s => s.Placement).Contains(coord))
            {
                Hits.Add(coord);
                return coord;
            }
            return coord;
        }

        private Coordinate RandomShot()
        {
            Coordinate shotToTake = RandomCoord();

            if (!Computer.ShotsTaken.Contains(shotToTake))
            {
                Computer.ShotsTaken.Add(shotToTake);
                return shotToTake;
            }
            else
            {
                return RandomShot();
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
