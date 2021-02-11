using Battleships.Business.Models;
using Battleships.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business
{
    public class Program
    {
        static void Main(string[] args)
        {
            Coordinate coordTL = new Coordinate(5, 2);
            Coordinate coordTR = new Coordinate(3, 8);
            Coordinate coordBL = new Coordinate(4, 9);
            Coordinate coordBR = new Coordinate(1, 3);
            Ship ship = new Ship(4);

            ShipPlacementService sps = new ShipPlacementService(10, 10);

            List<Coordinate> lctl = sps.PlaceShip(coordTL, "Vertical", ship).ToList();
            List<Coordinate> lctr = sps.PlaceShip(coordTR, "Vertical", ship).ToList();
            List<Coordinate> lcbl = sps.PlaceShip(coordBL, "Vertical", ship).ToList();
            List<Coordinate> lcbr = sps.PlaceShip(coordBR, "Vertical", ship).ToList();

            foreach (var item in lctl)
            {
                Console.WriteLine($"{item.Column}, {item.Row}");
            }
            Console.WriteLine("-----------------");
            foreach (var item in lctr)
            {
                Console.WriteLine($"{item.Column}, {item.Row}");
            }
            Console.WriteLine("-----------------");
            foreach (var item in lcbl)
            {
                Console.WriteLine($"{item.Column}, {item.Row}");
            }
            Console.WriteLine("-----------------");
            foreach (var item in lcbr)
            {
                Console.WriteLine($"{item.Column}, {item.Row}");
            }

            Console.Read();
        }
    }
}
