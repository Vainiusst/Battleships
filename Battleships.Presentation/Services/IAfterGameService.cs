using Battleships.Business.Models;
using Battleships.Data.Database;

namespace Battleships.Presentation.Services
{
    public interface IAfterGameService
    {
        DbManager DbM { get; set; }
        Player PlayerHum { get; set; }
        Player PlayerPC { get; set; }

        void AfterGame(GameWindow gw);
    }
}