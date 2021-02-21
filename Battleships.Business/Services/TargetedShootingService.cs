using Battleships.Business.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Battleships.Business.Services
{
    public class TargetedShootingService
    {
        public Coordinate HitShot { get; }
        public List<Coordinate> PotentialShots { get; set; }
        public List<Coordinate> ShotsTaken { get; set; }
        public Orientation Orientation { get; set; }
        public TargetedShootingService(Coordinate hitShot)
        {
            HitShot = hitShot;
            PotentialShots = GeneratePotentials(hitShot).ToList();
            ShotsTaken = new List<Coordinate>();
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


    }
}
