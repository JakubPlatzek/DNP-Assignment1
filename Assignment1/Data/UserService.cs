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
            user.Registered = "true";
            users.Add(user);
            fileContext.SaveChanges();
        }
        
    }
}