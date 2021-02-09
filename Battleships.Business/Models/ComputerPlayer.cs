using Battleships.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models
{
    public class ComputerPlayer
    {
        public GameGrid PlayerGrid { get; set; }
        public GameGrid GuessGrid { get; set; }
        public List<Ship> Ships { get; set; }

        public ComputerPlayer()
        {
            PlayerGrid = new GameGrid();
            GuessGrid = new GameGrid();
            Ships = ShipGeneratorService.ShipGenerator().ToList();
        }
    }
}
