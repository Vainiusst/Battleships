using Battleships.Business.Models;
using Battleships.Business.Services;
using Battleships.Presentation.Services;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Battleships.Presentation.Controls;

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

        //private void Login()
        //{
        //    //check if username matches anything from DB
        //    //if it does - check the password
        //    //if everything matches - change of window
        //}

        //private void Register()
        //{
        //    //check if username matches standards (up to 20 chars, no unusual chars)
        //    //check if email is valid
        //    //check if password meets requirements
        //    //hash the password
        //    //send everything to the DB
        //    //log the user in
        //}

        private void btnRegisterLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Hidden;
            RegisterPanel.Visibility = Visibility.Visible;
        }

        private void btnLoginLogin_Click(object sender, RoutedEventArgs e)
        {
            //Login();
        }

        private void btnRegRegister_Click(object sender, RoutedEventArgs e)
        {
            //Register();
        }
    }
}
