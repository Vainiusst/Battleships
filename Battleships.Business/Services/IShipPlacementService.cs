using Battleships.Business.Models;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Battleships.Business.Services
{
    public interface IShipPlacementService
    {
        int GridHeight { get; }
        int GridWidth { get; }
        List<List<Coordinate>> OccupiedCoordinates { get; set; }

        void PlaceShip(Orientation orientation, Coordinate coord, Ship ship);
        void PlaceShipRandom(Ship ship);
    }
}