using Battleships.Business.Models;
using Battleships.Business.Services;
using Battleships.Presentation.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.Presentation.Services
{
    public class ButtonFillingService : IButtonFillingService
    {
        public void FillWithButtons(Grid grid, GameGrid gameGrid, bool addContent, RoutedEventHandler eh)
        {
            CoordinateTranslationService CTS = new CoordinateTranslationService();

            int x = gameGrid.Width;
            int y = gameGrid.Height;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    ButtonExtended btnToAdd = new ButtonExtended(i, j);
                    btnToAdd.Name = $"bg{i}{j}";
                    btnToAdd.Height = 30;
                    btnToAdd.Width = 30;
                    if (addContent)
                    {
                        btnToAdd.Content = $"{CTS.Translate(new Coordinate(i, j))}";
                        btnToAdd.IsEnabled = false;
                    }
                    if (!addContent)
                    {
                        btnToAdd.Background = Brushes.LightGray;
                        btnToAdd.Click += eh;
                    }

                    Grid.SetColumn(btnToAdd, i);
                    Grid.SetRow(btnToAdd, j);
                    grid.Children.Add(btnToAdd);
                }
            }
        }

        public void AddHandlerAndEnableButtons(UIElementCollection gridChildren, RoutedEventHandler eh)
        {
            foreach (ButtonExtended btn in gridChildren)
            {
                btn.IsEnabled = true;
                btn.Click += eh;
            }
        }
    }
}
