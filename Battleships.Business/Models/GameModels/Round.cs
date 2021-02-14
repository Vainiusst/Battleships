using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models.GameModels
{
    public class Round
    {
        public Move PlayerMove { get; set; }
        public Move ComputerMove { get; set; }

        public Round(Move plrMove, Move pcMove)
        {
            PlayerMove = plrMove;
            ComputerMove = pcMove;
        }
    }
}
