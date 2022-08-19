using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowClone.DomainModels;

namespace StackOverflowClone.Repositories
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
            List<User> users = db.Users.Where(temp => temp.IsAdmin == false).ToList();
            return users;
        }

        public int GetLatestUserID()
        {
            int lastUserID = db.Users.Select(temp => temp.UserID).Max();

            return lastUserID;
        }

        public List<User> GetUserByUserID(int UserID)
        {
            List<User> usersById = db.Users.Where(temp => temp.UserID == UserID).ToList();
            return usersById;
        }

        public List<User> GetUsersByEmail(string Email)
        {
            List<User> usersByEmail = db.Users.Where(temp => temp.Email == Email).ToList();

            return usersByEmail;
        }

        public List<User> GetUsersByEmailAndPassword(string Email, string Password)
        {
            List<User> usersByEmail = db.Users.Where(temp => temp.Email == Email && temp.PasswordHash == Password).ToList();

            return usersByEmail;
        }

        public void InsertUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User u)
        {
            User existingUser = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            existingUser.Name = u.Name;
            db.SaveChanges();
        }

        public void UpdateUserPassword(User u)
        {
            User existingUser = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (u != null)
            {
                existingUser.PasswordHash = u.PasswordHash;
            }
            db.SaveChanges();
        }
    }

}
