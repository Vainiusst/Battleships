﻿using Battleships.Business.Models;
using Battleships.Business.Services;
using Battleships.Presentation.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleships.Presentation.Services
{
    //Class that fills grids with buttons that have required properties
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
                    btnToAdd.Height = 30;
                    btnToAdd.Width = 30;
                    if (addContent) btnToAdd.Content = $"{CTS.Translate(new Coordinate(i, j))}";
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

        public void AddHandlerToButtons(UIElementCollection gridChildren, RoutedEventHandler eh)
        {
            foreach (ButtonExtended btn in gridChildren)
            {
                btn.Click += eh;
            }
        }

        public void RemoveHandlerFromButtons(UIElementCollection gridChildren, RoutedEventHandler eh)
        {
            foreach (ButtonExtended btn in gridChildren)
            {
                btn.Click -= eh;
            }
        }
    }
}
