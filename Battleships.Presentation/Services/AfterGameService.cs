using Battleships.Business.Models;
using Battleships.Data.Database;
using System.Linq;
using System.Text;
using System.Windows;

namespace Battleships.Presentation.Services
{
    public class AfterGameService : IAfterGameService
    {
        public Player PlayerHum { get; set; }
        public Player PlayerPC { get; set; }
        public DbManager DbM { get; set; }

        public AfterGameService(Player playerHum, Player playerPC)
        {
            PlayerHum = playerHum;
            PlayerPC = playerPC;
            DbM = new DbManager();
        }

        public void AfterGame(GameWindow gw)
        {
            var winner = PickTheWinner();
            using (var ctx = new MyDbContext())
            {
                UpdateScoreDB(winner, ctx);
                UpdateGameDB(gw, ctx);
            }
        }

        private Player PickTheWinner()
        {
            var returnPlr = PlayerPC.Ships.Any(s => !s.IsSunk) ? PlayerPC : PlayerHum;
            MessageBox.Show($"{returnPlr.Name} won the game!");
            return returnPlr;
        }

        private void UpdateScoreDB(Player winner, MyDbContext ctx)
        {
            if (winner.Name == "Computer")
            {
                DbM.AddLoss(PlayerHum, ctx);
            }
            else
            {
                DbM.AddWin(PlayerHum, ctx);
            }
        }

        private void UpdateGameDB(GameWindow gw, MyDbContext ctx)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < gw.Rounds.Count; i++)
            {
                if (i == gw.Rounds.Count - 1)
                {
                    sb.Append($"Player: {gw.Rounds[i].PlayerMove.MoveStr}, Computer: {gw.Rounds[i].ComputerMove.MoveStr}.");
                }
                else
                {
                    sb.Append($"Player: {gw.Rounds[i].PlayerMove.MoveStr}, Computer: {gw.Rounds[i].ComputerMove.MoveStr}; ");
                }
            }

            DbM.CreateGame(PlayerHum, ctx, sb.ToString());
        }
    }
}
