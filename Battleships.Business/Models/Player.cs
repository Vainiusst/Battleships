using Battleships.Business.Services;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Business.Models
{
    public class Player : User
    {
        public List<Ship> Ships { get; set; }
        public GameGrid Grid { get; }
        public GameGrid GuessGrid { get; }
        public List<Coordinate> ShotsTaken { get; set; }

        public Player()
        {
            Ships = ShipGeneratorService.ShipGenerator(ShipSizes.DefaultSizes).ToList();
            Grid = new GameGrid();
            GuessGrid = new GameGrid();
            ShotsTaken = new List<Coordinate>();
        }
    }
}
