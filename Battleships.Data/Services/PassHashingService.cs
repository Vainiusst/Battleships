using Scrypt;
using System;
using System.Security.Cryptography;

namespace Battleships.Data.Services
{
    public class PassHashingService
    {
        public string Salt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[128];
            rng.GetBytes(buffer);
            return BitConverter.ToString(buffer);
        }

        public string HashedPass(string pass, string salt)
        {
            ScryptEncoder encoder = new ScryptEncoder();

            return encoder.Encode($"{pass}{salt}");
        }

        public bool CheckPass(string passInput, string hashedPass)
        {
            ScryptEncoder encoder = new ScryptEncoder();

            return encoder.Compare(passInput, hashedPass);
        }
    }
}
