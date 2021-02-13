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
            Coordinate coordBL = new Coordinate(5, 4);
            Coordinate coordBR = new Coordinate(1, 3);
            Ship ship = new Ship(4);

            ShipPlacementService sps = new ShipPlacementService(10, 10);

            sps.PlaceShip("Vertical", coordTL, ship);
            sps.PlaceShip("Vertical", coordTR, ship);
            sps.PlaceShip("Horizontal", coordBL, ship);
            sps.PlaceShip("Horizontal", coordBR, ship);

            foreach (var listCoor in sps.OccupiedCoordinates)
            {
                foreach(var coor in listCoor)
                {
                    Console.WriteLine($"{coor.Column}, {coor.Row}");
                }
                Console.WriteLine("-----------------");
            }
            
            Console.Read();
        }
    }
}
