using Battleships.Business.Models;
using Battleships.Business.Models.GameModels;
using System.Windows.Controls;

namespace Battleships.Presentation.Services
{
    public interface IHitOutputService
    {
        bool IsHit(Player plr, Coordinate coordShotAt);
        void OutputTheHit(TextBlock lbl, Move mv, Player plr, Player opponent);
    }
}