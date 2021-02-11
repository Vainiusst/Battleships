using Battleships.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battleships.Business.Services
{
    public class ShipPlacementService
    {
        Random Rand { get; set; }
        List<Coordinate> OccupiedCoordinates { get; set; }
        int GridHeight { get; }
        int GridWidth { get; }

        public ShipPlacementService(int gridHeight, int gridWidth)
        {
            Rand = new Random();
            OccupiedCoordinates = new List<Coordinate>();
            GridHeight = gridHeight;
            GridWidth = gridWidth;
        }

        //private Coordinate ValidCoordinateGenerator()
        //{
        //    return new Coordinate(Rand.Next(GridHeight), Rand.Next(GridWidth));
        //}

        //public void RandomPlacement(Ship ship, string orientation)
        //{
        //    if (OccupiedCoordinates.Contains(CoordinateGenerator())
        //}

        public IEnumerable<Coordinate> PlaceShip(string orientation, Coordinate coord, Ship ship)
        {
            if (orientation == "Horizontal")
            {
                return HorizontalPlacement(coord, ship);
            }
            else if (orientation == "Vertical")
            {
                return VerticalPlacement(coord, ship);
            }
            else
            {
                MessageBox.Show("An error has occured!");
                return new List<Coordinate>();
            }
        }

        private IEnumerable<Coordinate> HorizontalPlacement(Coordinate coord, Ship ship)
        {
            List<Coordinate> coords = new List<Coordinate>();

            if ((GridWidth - 1 - coord.Column) >= ship.Size)
            //-1 is used to adjust between measurment from 0 and measurment from 1
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    coords.Add(new Coordinate(coord.Column + i, coord.Row));
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    coords.Add(new Coordinate(coord.Column - i, coord.Row));
                }
            }

            ship.Placement.AddRange(coords);
            return coords.ToArray();
        }

        private IEnumerable<Coordinate> VerticalPlacement(Coordinate coord, Ship ship)
        {
            List<Coordinate> coords = new List<Coordinate>();

            if ((GridHeight - 1 - coord.Row) >= ship.Size)
            //-1 is used to adjust between measurment from 0 and measurment from 1
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    coords.Add(new Coordinate(coord.Column, coord.Row + i));
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    coords.Add(new Coordinate(coord.Column, coord.Row - i));
                }
            }

            ship.Placement.AddRange(coords);
            return coords.ToArray();
        }
    }
}
