using System;

namespace Battleships.Business.Services
{
    public static class CoinToss
    {
        private enum Players
        {
            Player,
            Computer
        }

        public static string Toss()
        {
            Random rand = new Random();
            Players[] plrs = new Players[] { Players.Player, Players.Computer };

            return plrs[rand.Next(plrs.Length)].ToString();
        }
    }
}
