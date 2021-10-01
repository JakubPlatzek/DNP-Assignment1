using Models;

namespace Assignment1.Data
{
    public interface IUserService
    {
        User ValidateUser(string userName, string Password);
        void AddUser(User user);
        void SetLogged(string username, string password, bool status);
    }
}