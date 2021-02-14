using Battleships.Business.Models;
using Battleships.Presentation.Controls;
using Battleships.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.Presentation.Services
{
    public class ButtonFillingService
    {
        public void FillWithButtons(Grid grid, GameGrid gameGrid, bool addContent, RoutedEventHandler eh)
        {
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
                    if (addContent) btnToAdd.Content = $"{CoordToLetterDict.coordToLetter[i]}{j+1}";
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
    }
}
