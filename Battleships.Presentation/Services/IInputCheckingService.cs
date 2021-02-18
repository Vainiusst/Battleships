namespace Battleships.Presentation.Services
{
    public interface IInputCheckingService
    {
        bool EmailCheck(string email);
        bool PasswordCheck(string pass);
        bool UsernameCheck(string username);
    }
}