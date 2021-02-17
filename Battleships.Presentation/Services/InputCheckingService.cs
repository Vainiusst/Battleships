using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Battleships.Presentation.Services
{
    public class InputCheckingService
    {
        public bool UsernameCheck(string username)
        {
            Regex rx = new Regex(@"^[\w\.\-]{1,20}$");
            return rx.IsMatch(username);
        }

        public bool EmailCheck(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool PasswordCheck(string pass)
        {
            Regex rx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[\w]{8,}$");
            return rx.IsMatch(pass);
        }
    }
}
