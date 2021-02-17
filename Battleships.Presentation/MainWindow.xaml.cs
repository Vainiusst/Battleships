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
using Battleships.Data.Database;
using Battleships.Data.Models;

namespace Battleships.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User User { get; set; }
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
            btnLoginLogin.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF707070"));

            var user = DbManager.LoginUser(tbLoginUsername.Text, tbLoginPassword.Password.ToString());

            if (user != null)
            {
                LoginPanel.Visibility = Visibility.Hidden;
                UserPanel.Visibility = Visibility.Visible;
                lblUserGreeting.Content = $"Hello, {user.Username}!";
            }
            else
            {
                btnLoginLogin.BorderBrush = Brushes.Red;
                lblLoginInfo.Content = "No such user in the database!";
            }
        }

        private void btnRegRegister_Click(object sender, RoutedEventArgs e)
        {
            AllBordersGrey();
            InputCheckingService ips = new InputCheckingService();

            if (!ips.UsernameCheck(tbRegUsername.Text))
            {
                tbRegUsername.BorderBrush = Brushes.Red;
                lblRegInfo.Text =
                    "Username must be between 1 and 20 characters long and may include alphanumeric characters or certain special characters like . _ and -.";
                return;
            }

            if (!ips.EmailCheck(tbRegEmail.Text))
            {
                tbRegEmail.BorderBrush = Brushes.Red;
                lblRegInfo.Text = "Email has been entered incorrectly.";
                return;
            }

            if (!ips.PasswordCheck(tbRegPassword.Password.ToString()))
            {
                tbRegPassword.BorderBrush = Brushes.Red;
                lblRegInfo.Text =
                    "Pasword has to be at least 8 characters long and must include an uppercase letter, a lowercase letter and a digit.";
                return;
            }

            if (CheckInput())
            {
                DbUser regUser = DbManager.RegisterUser(tbRegUsername.Text, tbRegEmail.Text, tbRegPassword.Password.ToString());
                RegAttempt(regUser);
            }
            else
            {
                lblRegInfo.Text = "Something went wrong. Please restart the application.";
            }
        }

        public bool CheckInput()
        {
            InputCheckingService ips = new InputCheckingService();

            if (!ips.UsernameCheck(tbRegUsername.Text))
            {
                tbRegUsername.BorderBrush = Brushes.Red;
                lblRegInfo.Text =
                    "Username must be between 1 and 20 characters long and may include alphanumeric characters or certain special characters like . _ and -.";
                return false;
            }

            if (!ips.EmailCheck(tbRegEmail.Text))
            {
                tbRegEmail.BorderBrush = Brushes.Red;
                lblRegInfo.Text = "Email has been entered incorrectly.";
                return false;
            }

            if (!ips.PasswordCheck(tbRegPassword.Password.ToString()))
            {
                tbRegPassword.BorderBrush = Brushes.Red;
                lblRegInfo.Text =
                    "Pasword has to be at least 8 characters long and must include an uppercase letter, a lowercase letter and a digit.";
                return false;
            }

            return true;
        }

        private void RegAttempt(DbUser user)
        {
            if (user != null)
            {
                RegisterPanel.Visibility = Visibility.Hidden;
                UserPanel.Visibility = Visibility.Visible;
                lblUserGreeting.Content = $"Hello, {user.Username}!";
            }
            else
            {
                btnRegRegister.BorderBrush = Brushes.Red;
                lblRegInfo.Text = "Such user already exists in the database.";
            }
        }

        private void AllBordersGrey()
        {
            var grey = (Brush)(new BrushConverter().ConvertFrom("#FF707070"));
            tbRegUsername.BorderBrush = grey;
            tbRegEmail.BorderBrush = grey;
            tbRegPassword.BorderBrush = grey;
            btnRegRegister.BorderBrush = grey;
        }
    }
}
