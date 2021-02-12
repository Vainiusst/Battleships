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
        List<List<Coordinate>> OccupiedCoordinates { get; set; }
        int GridHeight { get; }
        int GridWidth { get; }

        public ShipPlacementService(int gridHeight, int gridWidth)
        {
            OccupiedCoordinates = new List<List<Coordinate>>();
            GridHeight = gridHeight;
            GridWidth = gridWidth;
        }

        public IEnumerable<Coordinate> PlaceShip(string orientation, Coordinate coord, Ship ship)
        {
            if (orientation == "Horizontal")
            {
                List<Coordinate> coords = HorizontalPlacement(coord, ship).ToList();
                if (coords.Count == ship.Size) PlaceCoordinates(ship, coords);
                return coords;
            }
            else if (orientation == "Vertical")
            {
                List<Coordinate> coords = VerticalPlacement(coord, ship).ToList();
                if (coords.Count == ship.Size) PlaceCoordinates(ship, coords);
                return coords;
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
                    Coordinate newCoord = new Coordinate(coord.Column + i, coord.Row);
                    
                    if (!OccupiedCoordinates.Any(l => l.Contains(newCoord)))
                    {
                        coords.Add(newCoord);
                    }
                    else
                    {
                        Console.WriteLine("This coordinate choice is invalid!");
                        return new List<Coordinate>();
                    }
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column - i, coord.Row);

                    if (!OccupiedCoordinates.Any(l => l.Contains(newCoord)))
                    {
                        coords.Add(newCoord);
                    }
                    else
                    {
                        Console.WriteLine("This coordinate choice is invalid!");
                        return new List<Coordinate>();
                    }
                }
            }

            return coords;
        }

        private IEnumerable<Coordinate> VerticalPlacement(Coordinate coord, Ship ship)
        {
            List<Coordinate> coords = new List<Coordinate>();

            if ((GridHeight - 1 - coord.Row) >= ship.Size)
            //-1 is used to adjust between measurment from 0 and measurment from 1
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column, coord.Row + i);

                    if (!OccupiedCoordinates.Any(l => l.Contains(newCoord)))
                    {
                        coords.Add(newCoord);
                    }
                    else
                    {
                        Console.WriteLine("This coordinate choice is invalid!");
                        return new List<Coordinate>();
                    }
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column, coord.Row - i);

                    if (!OccupiedCoordinates.Any(l => l.Contains(newCoord)))
                    {
                        coords.Add(newCoord);
                    }
                    else
                    {
                        Console.WriteLine("This coordinate choice is invalid!");
                        return new List<Coordinate>();
                    }
                }
            }

            return coords;
        }

        private void PlaceCoordinates(Ship ship, IEnumerable<Coordinate> coords)
        {
            ship.Placement.AddRange(coords);
            OccupiedCoordinates.Add(coords.ToList());
        }
    }
}
