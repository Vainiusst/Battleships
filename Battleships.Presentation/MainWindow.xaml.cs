using Battleships.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battleships.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FillWithButtons()
        {
            int x = 10;
            int y = 10;

            for(int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    ButtonExtended btnToAdd = new ButtonExtended(i, j);
                    btnToAdd.Height = 30;
                    btnToAdd.Width = 30;
                    btnToAdd.Content = $"{i},{j}";
                    btnToAdd.Click += OnBtnClickTest;
                    Grid.SetColumn(btnToAdd, i);
                    Grid.SetRow(btnToAdd, j);

                    PlayerGuessBoxGrid.Children.Add(btnToAdd);
                }
            }
        }

        private void btnSubmitAction_Click(object sender, RoutedEventArgs e)
        {
            FillWithButtons();
        }

        private void OnBtnClickTest(object sender, RoutedEventArgs e)
        {
            //Testing method to see whether the ButtonExtended class is working as it should
            ButtonExtended btn = (ButtonExtended)sender;
            MessageBox.Show($"Coordinates are X:{btn.Coordinate.Column}, Y:{btn.Coordinate.Row}");
        }
    }
}
