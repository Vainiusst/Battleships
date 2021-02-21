using Battleships.Business.Models;
using Battleships.Business.Models.GameModels;

namespace Battleships.Business.Services
{
    public interface IGame
    {
        Player ComputerPlayer { get; set; }
        CoordinateTranslationService CTS { get; set; }
        Player Player { get; set; }
        RandomShotService RSS { get; set; }

        Move ComputerMove();
        Move PlayerMove(Coordinate shootingCoord);
    }
}