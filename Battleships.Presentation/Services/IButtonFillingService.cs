using Battleships.Business.Models;
using System.Windows;
using System.Windows.Controls;

namespace Battleships.Presentation.Services
{
    public interface IButtonFillingService
    {
        void FillWithButtons(Grid grid, GameGrid gameGrid, bool addContent, RoutedEventHandler eh);
    }
}