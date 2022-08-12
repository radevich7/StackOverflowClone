using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowClone.DomainModels;

namespace StockOverflowClone.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User u);
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int UserID);
        List<User> GetAllUsers();
        List<User> GetUsersByEmailAndPassword(string Email, string Password);
        List<User> GetUsersByEmail(string Email);
        List<User> GetUserByUserID(int UserID);
        int GetLatestUserID();
    }


    public class UsersRepository : IUsersRepository
    {
        StackOverflowDbContext db;

        public UsersRepository()
        {
            db = new StackOverflowDbContext();
        }

        public void DeleteUser(int UserID)
        {
            User existingUser = db.Users.Where(temp => temp.UserID == UserID).FirstOrDefault();
            db.Users.Remove(existingUser);
            db.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public int GetLatestUserID()
        {
            throw new NotImplementedException();
        }

        public List<User> GetUserByUserID(int UserID)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersByEmail(string Email)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersByEmailAndPassword(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public void InsertUser(User u)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserDetails(User u)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserPassword(User u)
        {
            throw new NotImplementedException();
        }
    }

}
