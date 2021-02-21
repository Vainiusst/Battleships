using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Battleships.Presentation.Services
{
    //Class that checks the input in the register forms
    public class InputCheckingService : IInputCheckingService
    {
        public bool UsernameCheck(string username)
        {
            //Username should be between 1-20 charcters long
            //and may contain any alphanumeric characters as well as "-", "_" or ".".
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
            //Pasword must be at least 8 characters long and must contain 
            //at least 1 lowercase letter, 1 uppercase leter and 1 digit.
            Regex rx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[\w]{8,}$");
            return rx.IsMatch(pass);
        }
    }
}
