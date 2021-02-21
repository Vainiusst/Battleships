using Battleships.Business.Models;

namespace Battleships.Business.Services
{
    public interface ICoordinateTranslationService
    {
        string Translate(Coordinate coord);
    }
}