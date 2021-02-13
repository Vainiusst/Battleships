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
        public void FillWithButtons(Grid grid, double buttonOpacity, bool addContent)
        {
            int x = 10;
            int y = 10;

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    ButtonExtended btnToAdd = new ButtonExtended(i, j);
                    btnToAdd.Height = 30;
                    btnToAdd.Width = 30;
                    btnToAdd.Opacity = buttonOpacity;
                    if (addContent) btnToAdd.Content = $"{CoordToLetterDict.coordToLetter[i]}{j+1}";
                    btnToAdd.Click += OnBtnClickTest;
                    if (buttonOpacity == 0) btnToAdd.MouseEnter += BtnToAdd_MouseEnter;
                    if (buttonOpacity == 0) btnToAdd.MouseLeave += BtnToAdd_MouseLeave;
                    Grid.SetColumn(btnToAdd, i);
                    Grid.SetRow(btnToAdd, j);
                    grid.Children.Add(btnToAdd);
                }
            }
        }

        private static void BtnToAdd_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Opacity = 0; //min opacity
        }

        private static void BtnToAdd_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Opacity = 1; //max opacity
        }

        private static void OnBtnClickTest(object sender, RoutedEventArgs e)
        {
            //Testing method to see whether the ButtonExtended class is working as it should
            ButtonExtended btn = (ButtonExtended)sender;
            MessageBox.Show($"Coordinates are X:{btn.Coordinate.Column}, Y:{btn.Coordinate.Row}");
        }
    }
}
