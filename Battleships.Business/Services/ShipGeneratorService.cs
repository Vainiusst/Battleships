using Battleships.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Services
{
    public static class ShipGeneratorService
    {
        public static IEnumerable<Ship> ShipGenerator(IEnumerable<int> ShipSizes)
        {
            List<Ship> ships = new List<Ship>();

            foreach (int size in ShipSizes)
            {
                ships.Add(new Ship(size));
            }

            return ships.ToArray();
        }
    }
}
