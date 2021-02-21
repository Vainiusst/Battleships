using Battleships.Business.Models;
using Battleships.Data.Models;
using Battleships.Data.Services;
using System;
using System.Linq;

namespace Battleships.Data.Database
{
    public class DbManager
    {
        public DbUser RegisterUser(string username, string email, string pass)
        {
            PassHashingService phs = new PassHashingService();

            string salt = phs.Salt();
            string passwordToDb = phs.HashedPass(pass, salt);

            using (var ctx = new MyDbContext())
            {
                if (CheckIfUserUnique(username, email, ctx))
                {
                    DbUser newUser = new DbUser { Username = username, Email = email, Password = passwordToDb, Salt = salt };
                    DbScore newScore = new DbScore { User = newUser, Wins = 0, Losses = 0 };
                    ctx.Users.Add(newUser);
                    ctx.Scores.Add(newScore);
                    ctx.SaveChanges();
                    return newUser;
                }

                return null;
            }
        }

        public DbUser LoginUser(string username, string password)
        {
            using (var ctx = new MyDbContext())
            {
                var user = ctx.Users.Where(u => u.Username == username).FirstOrDefault();

                if (user != null)
                {
                    PassHashingService phs = new PassHashingService();
                    return phs.CheckPass($"{password}{user.Salt}", user.Password) ? user : null;
                }

                return null;
            }
        }

        private bool CheckIfUserUnique(string username, string email, MyDbContext ctx)
        {
            var usernames = ctx.Users.Select(u => u.Username).ToList();
            var emails = ctx.Users.Select(u => u.Email).ToList();

            if (!usernames.Contains(username) && !emails.Contains(email))
            {
                return true;
            }

            return false;
        }

        public void CreateGame(Player plr, MyDbContext ctx, string gameLog)
        {
            var user = ctx.Users.FirstOrDefault(u => u.UserId == plr.UserId);
            var gameToAdd = new DbGame { User = user, GameTime = DateTime.Now, GameMoves = gameLog };
            ctx.Games.Add(gameToAdd);
            ctx.SaveChanges();
        }

        public void AddWin(Player plr, MyDbContext ctx)
        {
            var score = ctx.Scores.FirstOrDefault(s => s.UserId == plr.UserId);
            score.Wins++;
            ctx.SaveChanges();
        }

        public void AddLoss(Player plr, MyDbContext ctx)
        {
            var score = ctx.Scores.FirstOrDefault(s => s.UserId == plr.UserId);
            score.Losses++;
            ctx.SaveChanges();
        }
    }
}
