using Battleships.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Battleships.Business.Services
{
    public class ComputerShootingService : IRandomShotService
    {
        //This is not implemented yet. After it is completed, this should replace RandomShotService
        //As this should be more advanced and use calculations for targeted shots instead of pure random ones.
        public Player Computer { get; set; }
        public Player Opponent { get; set; }
        public Random Rand { get; set; }
        public List<Coordinate> PotentialShots { get; set; }
        public List<Coordinate> CurrentHits { get; set; }
        public Orientation Orientation { get; set; }


        public ComputerShootingService(Player computer, Player opponent)
        {
            Computer = computer;
            Opponent = opponent;
            Rand = new Random();
            CurrentHits = new List<Coordinate>();
            PotentialShots = new List<Coordinate>();
        }

        public Coordinate Shoot()
        {
            if (PotentialShots.Count == 0)
            {
                return IfHit(RandomShot());
            }
            else
            {
                var shot = IfHit(PotentialShots[0]);
                PotentialShots.RemoveAt(0);
                IfFatal(shot);
                return shot;
            }
        }

        private void IfFatal(Coordinate coord)
        {
            var shipSize = FindShipSize(coord);
            if (CurrentHits.Count == shipSize) ClearCurrentAndPotentials();
        }

        private void ClearCurrentAndPotentials()
        {
            CurrentHits.Clear();
            PotentialShots.Clear();
        }

        //private Coordinate ShootInDirection()
        //{
        //    //Implement directionality
        //}

        //private Orientation CalculateOrientation(IEnumerable<Coordinate> coords)
        //{
        //    if (coords.ToList()[0].Column != coords.ToList()[1].Column)
        //    {
        //        return Orientation.Horizontal;
        //    }

        //    return Orientation.Vertical;
        //}

        private void GeneratePotentials(Coordinate hitShot)
        {
            PotentialShots = new List<Coordinate>();

            //Potential next shots can go 4 ways: up, down, left and right.
            PotentialShots.Add(new Coordinate(hitShot.Column, hitShot.Row - 1));
            PotentialShots.Add(new Coordinate(hitShot.Column, hitShot.Row + 1));
            PotentialShots.Add(new Coordinate(hitShot.Column - 1, hitShot.Row));
            PotentialShots.Add(new Coordinate(hitShot.Column + 1, hitShot.Row));

            RemoveMadeShots();
        }

        private void RemoveMadeShots()
        {
            foreach(Coordinate shot in Computer.ShotsTaken)
            {
                if (PotentialShots.Contains(shot)) PotentialShots.Remove(shot);
            }
        }

        private Coordinate IfHit(Coordinate coord)
        {
            if (Opponent.Ships.Where(s => !s.IsSunk).SelectMany(s => s.Placement).Contains(coord))
            {
                CurrentHits.Add(coord);
                GeneratePotentials(coord);
            }
            else
            {
                ClearCurrentAndPotentials();
            }

            return coord;
        }

        private int FindShipSize(Coordinate coord)
        {
            foreach(var ship in Opponent.Ships)
            {
                if (ship.Placement.Contains(coord)) return ship.Size;
            }

            return -1;
        }



        //Random shot mechanism
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
