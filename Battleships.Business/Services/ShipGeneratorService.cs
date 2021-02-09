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
        //Default game has 5 ships in these sizes
        private static int[] ShipSizes = { 5, 4, 3, 3, 2 };

        public static IEnumerable<Ship> ShipGenerator()
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
