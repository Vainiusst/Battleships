using Battleships.Data.Models;
using Battleships.Data.Services;
using System.Linq;

namespace Battleships.Data.Database
{
    public static class DbManager
    {
        public static DbUser RegisterUser(string username, string email, string pass)
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

        public static DbUser LoginUser(string username, string password)
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

        private static bool CheckIfUserUnique(string username, string email, MyDbContext ctx)
        {
            var usernames = ctx.Users.Select(u => u.Username).ToList();
            var emails = ctx.Users.Select(u => u.Email).ToList();

            if (!usernames.Contains(username) && !emails.Contains(email))
            {
                return true;
            }

            return false;
        }
    }
}
