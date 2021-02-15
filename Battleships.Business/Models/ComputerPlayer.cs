using Battleships.Business.Services;
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Business.Models
{
    public class ComputerPlayer
    {
        public string Name { get; }
        public GameGrid Grid { get; set; }
        public GameGrid GuessGrid { get; set; }
        public List<Ship> Ships { get; set; }
        public List<Coordinate> ShotsTaken { get; set; }

        public ComputerPlayer()
        {
            Name = "Computer";
            Grid = new GameGrid();
            GuessGrid = new GameGrid();
            Ships = ShipGeneratorService.ShipGenerator(ShipSizes.DefaultSizes).ToList();
            ShotsTaken = new List<Coordinate>();
        }
    }
}
