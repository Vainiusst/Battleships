namespace Battleships.Data.Services
{
    public interface IPassHashingService
    {
        bool CheckPass(string passInput, string hashedPass);
        string HashedPass(string pass, string salt);
        string Salt();
    }
}