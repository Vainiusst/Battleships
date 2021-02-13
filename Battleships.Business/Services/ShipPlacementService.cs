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
        public List<List<Coordinate>> OccupiedCoordinates { get; set; }
        public int GridHeight { get; }
        public int GridWidth { get; }

        public ShipPlacementService(int gridHeight, int gridWidth)
        {
            OccupiedCoordinates = new List<List<Coordinate>>();
            GridHeight = gridHeight;
            GridWidth = gridWidth;
        }

        public void PlaceShip(string orientation, Coordinate coord, Ship ship)
        {
            if (orientation == "Horizontal")
            {
                HorizontalPlacement(coord, ship);
            }
            else if (orientation == "Vertical")
            {
                VerticalPlacement(coord, ship);
            }
            else
            {
                MessageBox.Show("An error has occured!");
            }
        }

        private void HorizontalPlacement(Coordinate coord, Ship ship)
        {
            List<Coordinate> coords = new List<Coordinate>();

            if ((GridWidth - 1 - coord.Column) >= ship.Size)
            //-1 is used to adjust between measurment from 0 and measurment from 1
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column + i, coord.Row);
                    coords.AddRange(CoordinateValidator(newCoord).ToList());
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column - i, coord.Row);
                    coords.AddRange(CoordinateValidator(newCoord).ToList());
                }
            }

            if (coords.Count == ship.Size) PlaceCoordinates(ship, coords);
        }

        private void VerticalPlacement(Coordinate coord, Ship ship)
        {
            List<Coordinate> coords = new List<Coordinate>();

            if ((GridHeight - 1 - coord.Row) >= ship.Size)
            //-1 is used to adjust between measurment from 0 and measurment from 1
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column, coord.Row + i);
                    coords.AddRange(CoordinateValidator(newCoord));
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column, coord.Row - i);
                    coords.AddRange(CoordinateValidator(newCoord));
                }
            }

            if (coords.Count == ship.Size) PlaceCoordinates(ship, coords);
        }

        private IEnumerable<Coordinate> CoordinateValidator(Coordinate coord)
        {
            List<Coordinate> returnCoords = new List<Coordinate>();

            if (!OccupiedCoordinates.Any(l => l.Contains(coord)))
            {
                returnCoords.Add(coord);
                return returnCoords;
            }
            else
            {
                MessageBox.Show("This coordinate choice is invalid!");
                Console.WriteLine("This coordinate choice is invalid!");
                return returnCoords;
            }
        }

        private void PlaceCoordinates(Ship ship, IEnumerable<Coordinate> coords)
        {
            ship.Placement.AddRange(coords);
            OccupiedCoordinates.Add(coords.ToList());
        }
    }
}
