using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Business.Models.GameModels
{
    public class Move
    {
        public string MoveStr { get; set; }
        public Coordinate MoveCoord { get; set; }

        public Move(Coordinate moveCoord, string moveStr)
        {
            MoveStr = moveStr;
            MoveCoord = moveCoord;
        }
    }
}
