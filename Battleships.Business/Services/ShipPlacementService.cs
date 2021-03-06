﻿using Battleships.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Battleships.Business.Services
{
    public class ShipPlacementService : IShipPlacementService
    {
        //Class for placing ships on the grid. Involves Random placements for computer and manual placement for player.
        public List<List<Coordinate>> OccupiedCoordinates { get; set; }
        public int GridHeight { get; }
        public int GridWidth { get; }

        public ShipPlacementService(int gridHeight, int gridWidth)
        {
            OccupiedCoordinates = new List<List<Coordinate>>();
            GridHeight = gridHeight;
            GridWidth = gridWidth;
        }

        public void PlaceShipRandom(Ship ship)
        {
            Random rand = new Random();
            Orientation[] orientations = new Orientation[] { Orientation.Horizontal, Orientation.Vertical };
            int minCoord = 0;
            int maxCoord = 9;

            PlaceShip(
                orientations[rand.Next(orientations.Length)],
                new Coordinate(rand.Next(minCoord, maxCoord + 1), rand.Next(minCoord, maxCoord + 1)),
                ship);
        }

        public void PlaceShip(Orientation orientation, Coordinate coord, Ship ship)
        {
            if (orientation == Orientation.Horizontal)
            {
                HorizontalPlacement(coord, ship);
            }
            else if (orientation == Orientation.Vertical)
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

            if ((GridWidth - coord.Column) >= ship.Size)
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column + i, coord.Row);
                    if (CoordinateValidator(newCoord) == null) return;
                    coords.Add(newCoord);
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column - i, coord.Row);
                    if (CoordinateValidator(newCoord) == null) return;
                    coords.Add(newCoord);
                }
            }

            if (coords.Count == ship.Size) SetCoordinates(ship, coords);
        }

        private void VerticalPlacement(Coordinate coord, Ship ship)
        {
            List<Coordinate> coords = new List<Coordinate>();

            if ((GridHeight - coord.Row) >= ship.Size)
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column, coord.Row + i);
                    if (CoordinateValidator(newCoord) == null) return;
                    coords.Add(newCoord);
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    Coordinate newCoord = new Coordinate(coord.Column, coord.Row - i);
                    if (CoordinateValidator(newCoord) == null) return;
                    coords.Add(newCoord);
                }
            }

            if (coords.Count == ship.Size) SetCoordinates(ship, coords);
        }

        private Coordinate CoordinateValidator(Coordinate coord)
        {
            if (!OccupiedCoordinates.Any(l => l.Contains(coord)))
            {
                return coord;
            }
            else
            {
                MessageBox.Show("This coordinate choice is invalid!");
                return null;
            }
        }

        private void SetCoordinates(Ship ship, IEnumerable<Coordinate> coords)
        {
            ship.Placement.AddRange(coords);
            OccupiedCoordinates.Add(coords.ToList());
        }
    }
}
