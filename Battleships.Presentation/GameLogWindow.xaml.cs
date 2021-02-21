using Battleships.Business.Models;
using Battleships.Data.Database;
using Battleships.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Battleships.Presentation
{
    /// <summary>
    /// Interaction logic for GameLogWindow.xaml
    /// </summary>
    public partial class GameLogWindow : Window
    {
        public List<DbGame> Games { get; set; }
        public User User { get; set; }
        public GameLogWindow(User user)
        {
            InitializeComponent();
            User = user;

            Games = new List<DbGame>();
            FetchGames();
            GameLogGrid.ItemsSource = Games;
        }

        private void FetchGames()
        {
            using (var ctx = new MyDbContext())
            {
                Games.AddRange(ctx.Games.Where(g => g.UserId == User.UserId).ToList());
            }

            GameLogGrid.Items.Refresh();
        }
    }
}
