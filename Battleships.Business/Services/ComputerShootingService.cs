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
        public int CurrentShipSize { get; set; }
        public List<Coordinate> PotentialShots { get; set; }
        public List<Coordinate> CurrentHits { get; set; }
        public Orientation CurrentOrientation { get; set; }


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

            var shot = PotentialShots[Rand.Next(PotentialShots.Count)];

            if (PotentialShots.Count > 0 && CurrentHits.Count == 1)
            {
                IfHit(AimedShot(shot));
                if (CurrentHits.Count == 2)
                {
                    CurrentOrientation = CalculateOrientation();
                    PotentialShots.Clear();
                    GenerateDirectionPotentials();
                }
                return PostShotRoutine(shot);
            }

            IfHitAfter2Hits(AimedShot(shot));
            return PostShotRoutine(shot);
        }

        private Coordinate PostShotRoutine(Coordinate shot)
        {
            PotentialShots.Remove(shot);
            if (CurrentHits.Count == CurrentShipSize) ClearCurrentAndPotentials();
            return shot;
        }

        private void ClearCurrentAndPotentials()
        {
            CurrentHits.Clear();
            PotentialShots.Clear();
        }

        private Orientation CalculateOrientation()
        {
            if (CurrentHits.ToList()[0].Column != CurrentHits.ToList()[1].Column)
            {
                return Orientation.Horizontal;
            }

            return Orientation.Vertical;
        }

        private void GenerateDirectionPotentials()
        {
            //Once the direction is set we can generate potential shots based on the direction
            if (CurrentOrientation == Orientation.Horizontal)
            {
                var columns = CurrentHits.Select(c => c.Column).ToList();
                PotentialShots.Add(new Coordinate(columns.Max() + 1, CurrentHits[0].Row));
                PotentialShots.Add(new Coordinate(columns.Min() - 1, CurrentHits[0].Row));
            }
            else
            {
                var rows = CurrentHits.Select(c => c.Row).ToList();
                PotentialShots.Add(new Coordinate(CurrentHits[0].Column, rows.Max() + 1));
                PotentialShots.Add(new Coordinate(CurrentHits[0].Column, rows.Min() - 1));
            }

            RemoveBadCoords();
            RemoveMadeShots();
        }

        private void GenerateAllPotentials(Coordinate hitShot)
        {
            //Potential next shots can go 4 ways: up, down, left and right.
            PotentialShots.Add(new Coordinate(hitShot.Column, hitShot.Row - 1));
            PotentialShots.Add(new Coordinate(hitShot.Column, hitShot.Row + 1));
            PotentialShots.Add(new Coordinate(hitShot.Column - 1, hitShot.Row));
            PotentialShots.Add(new Coordinate(hitShot.Column + 1, hitShot.Row));

            RemoveBadCoords();
            RemoveMadeShots();
        }

        private void RemoveBadCoords()
        {
            for(int i = 0; i < PotentialShots.Count; i++)
            {
                if (PotentialShots[i].Column > 9 ||
                    PotentialShots[i].Column < 0 ||
                    PotentialShots[i].Row > 9 ||
                    PotentialShots[i].Row < 0) PotentialShots.Remove(PotentialShots[i]);
            }
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
                GenerateAllPotentials(coord);
                CurrentShipSize = FindShipSize(coord);
            }

            return coord;
        }

        private void IfHitAfter2Hits(Coordinate coord)
        {
            if (Opponent.Ships.Where(s => !s.IsSunk).SelectMany(s => s.Placement).Contains(coord))
            {
                CurrentHits.Add(coord);
                GenerateDirectionPotentials();
                CurrentShipSize = FindShipSize(coord);
            }
        }

        private int FindShipSize(Coordinate coord)
        {
            foreach(var ship in Opponent.Ships)
            {
                if (ship.Placement.Contains(coord)) return ship.Size;
            }

            return -1;
        }

        private Coordinate AimedShot(Coordinate shotToTake)
        {
            Computer.ShotsTaken.Add(shotToTake);
            return shotToTake;
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

            return RandomShot();
        }

        private Coordinate RandomCoord()
        {
            int maxColumn = Computer.Grid.Width;
            int maxRow = Computer.Grid.Height;

            return new Coordinate(Rand.Next(maxColumn), Rand.Next(maxRow));
        }
    }
}
