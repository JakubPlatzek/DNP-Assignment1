using System;
using System.Collections.Generic;
using System.Linq;
using Assignment1.Persistence;
using Models;

namespace Assignment1.Data
{
    public class UserService : IUserService
    {
        private IList<User> users;
        private FileContext fileContext;

        public UserService()
        {
            fileContext = new FileContext();
            users = fileContext.Users;
        }
        
        public User ValidateUser(string userName, string password)
        {
            User first = users.FirstOrDefault(user => user.UserName.Equals(userName) && user.Password.Equals(password));
            if (first == null)
            {
                throw new Exception("User not found");
            }

            if (!first.Password.Equals(password))
            {
                throw new Exception("Incorrect password");
            }
            return first;
        }

        public void AddUser(User user)
        {
            users.Add(user);
            fileContext.SaveChanges();
        }

        public void SetLogged(string username, string password, bool status)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].UserName.Equals(username) && users[i].Password.Equals(password))
                {
                    users[i].Logged = status;
                }
            }
        }
    }
}