using System;

namespace Battleships.Business.Services
{
    public static class CoinToss
    {
        //Class to determine who starts the game
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
