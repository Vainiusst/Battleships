using System;

namespace Battleships.Business.Services
{
    public static class CoinToss
    {
        public enum Players
        {
            Player,
            Computer
        }

        public static Players Toss()
        {
            Random rand = new Random();
            Players[] plrs = new Players[] { Players.Player, Players.Computer };

            return plrs[rand.Next(plrs.Length)];
        }
    }
}
