using Battleships.Business.Models;
using System.Linq;
using System.Collections.Generic;
using Battleships.Data.Database;
using Battleships.Data.Models;
using Battleships.Presentation.Services;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Battleships.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User CurrentUser { get; set; }
        private List<User> Scores { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Scores = new List<User>();
            ScoreGrid.ItemsSource = Scores;
        }

        private void btnRegisterLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Hidden;
            RegisterPanel.Visibility = Visibility.Visible;
        }

        private void btnLoginReg_Click(object sender, RoutedEventArgs e)
        {
            RegisterPanel.Visibility = Visibility.Hidden;
            LoginPanel.Visibility = Visibility.Visible;
        }

        private void btnLoginLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLoginLogin.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF707070"));
            var user = DbManager.LoginUser(tbLoginUsername.Text, tbLoginPassword.Password.ToString());
            UponLogin(user);
            ScoreGridInfoUpdate();
            tbLoginPassword.Clear();
            tbLoginUsername.Clear();
        }

        private void UponLogin(DbUser user)
        {
            if (user != null)
            {
                LoginPanel.Visibility = Visibility.Hidden;
                UserPanel.Visibility = Visibility.Visible;
                CurrentUser = CreateUser(user);
                lblUserGreeting.Content = $"Hello, {CurrentUser.Name}!";
            }
            else
            {
                btnLoginLogin.BorderBrush = Brushes.Red;
                tbLoginInfo.Text = "No such user in the database!";
            }

        }

        private void btnRegRegister_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput()) RegisterUser();
            ScoreGridInfoUpdate();
            tbRegPassword.Clear();
            tbRegUsername.Clear();
            tbRegEmail.Clear();
        }

        private User CreateUser(DbUser user)
        {
            using (var ctx = new MyDbContext())
            {
                return new User(
                user.UserId,
                user.Username,
                ctx.Scores.Where(u => u.UserId == user.UserId).Select(w => w.Wins).FirstOrDefault(),
                ctx.Scores.Where(u => u.UserId == user.UserId).Select(l => l.Losses).FirstOrDefault()
                );
            }
        }

        private bool CheckInput()
        {
            InputCheckingService ips = new InputCheckingService();

            if (tbRegUsername.Text.Length == 0 || !ips.UsernameCheck(tbRegUsername.Text))
            {
                tbRegUsername.BorderBrush = Brushes.Red;
                lblRegInfo.Text =
                    "Username must be between 1 and 20 characters long and may include alphanumeric characters or certain special characters like . _ and -.";
                return false;
            }

            if (tbRegEmail.Text.Length == 0 || !ips.EmailCheck(tbRegEmail.Text))
            {
                tbRegEmail.BorderBrush = Brushes.Red;
                lblRegInfo.Text = "Email has been entered incorrectly.";
                return false;
            }

            if (tbRegPassword.Password.Length == 0 || !ips.PasswordCheck(tbRegPassword.Password.ToString()))
            {
                tbRegPassword.BorderBrush = Brushes.Red;
                lblRegInfo.Text =
                    "Pasword has to be at least 8 characters long and must include an uppercase letter, a lowercase letter and a digit.";
                return false;
            }

            return true;
        }

        private void RegisterUser()
        {
            DbUser regUser = DbManager.RegisterUser(tbRegUsername.Text, tbRegEmail.Text, tbRegPassword.Password.ToString());
            UponRegistration(regUser);
        }

        private void UponRegistration(DbUser user)
        {
            if (user != null)
            {
                RegisterPanel.Visibility = Visibility.Hidden;
                UserPanel.Visibility = Visibility.Visible;
                CurrentUser = CreateUser(user);
                lblUserGreeting.Content = $"Hello, {CurrentUser.Name}!";
            }
            else
            {
                btnRegRegister.BorderBrush = Brushes.Red;
                lblRegInfo.Text = "Such user already exists in the database.";
            }
        }

        private void ScoreGridInfoUpdate()
        {
            using(var ctx = new MyDbContext())
            {
                var scores = ctx.Scores.ToList();

                foreach (var sc in scores)
                {
                    Scores.Add(new User(sc.UserId,
                        ctx.Users.Where(i => i.UserId == sc.UserId).Select(u => u.Username).FirstOrDefault(),
                        sc.Wins,
                        sc.Losses));
                }

                ScoreGrid.Items.Refresh();
            }
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF707070"));
        }

        private void pass_TextChanged(object sender, RoutedEventArgs e)
        {
            var tb = (PasswordBox)sender;
            tb.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FF707070"));
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            UserPanel.Visibility = Visibility.Hidden;
            CurrentUser = null;
            LoginPanel.Visibility = Visibility.Visible;
        }
    }
}
