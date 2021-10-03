using Models;

namespace Assignment1.Data
{
    public interface IUserService
    {
        User ValidateUser(string userName, string Password);
        void AddUser(User user);
    }
}