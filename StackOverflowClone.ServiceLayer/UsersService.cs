using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackOverflowClone.DomainModels;
using StackOverflowClone.ViewModels;
using StackOverflowClone.Repositories;

using AutoMapper;
using AutoMapper.Configuration;


namespace StackOverflowClone.ServiceLayer
{

    public interface IUsersService
    {
        int InsertUser(RegisterViewModel userViewModel);
        void UpdateUser(EditUserDetailsViewModel userViewModel);
        void UpdateUserPassword(EditUserPasswordViewModel userViewModel);
        void DeleteUser(int userID);

        List<UserViewModel> GetAllUsers();
        UserViewModel GetUsersByEmailAndPassword(string Email, string Password);
        UserViewModel GetUsersByEmail(string Email);
        UserViewModel GetUsersByUserID(int userID);

    }
    public class UsersService : IUsersService
    {
        IUsersRepository ur;

        public UsersService()
        {
            ur = new UsersRepository();
        }

        public void DeleteUser(int userID)
        {
            ur.DeleteUser(userID);
        }

        public List<UserViewModel> GetAllUsers()
        {

            List<User> usersList = ur.GetAllUsers();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> users = mapper.Map<List<User>, List<UserViewModel>>(usersList);

            return users;
        }

        public UserViewModel GetUsersByEmail(string Email)
        {
            User existingUser = ur.GetUsersByEmail(Email).FirstOrDefault();
            UserViewModel user = null;
            if (existingUser != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                user = mapper.Map<User, UserViewModel>(existingUser);
            }
            return user;
        }


        public UserViewModel GetUsersByEmailAndPassword(string Email, string Password)
        {
            User existingUser = ur.GetUsersByEmailAndPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            UserViewModel user = null;
            if (existingUser != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                user = mapper.Map<User, UserViewModel>(existingUser);
            }
            return user;
        }

        public UserViewModel GetUsersByUserID(int userID)
        {
            User existingUser = ur.GetUserByUserID(userID).FirstOrDefault();
            UserViewModel user = null;
            if (existingUser != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                user = mapper.Map<User, UserViewModel>(existingUser);
            }
            return user;
        }

        public int InsertUser(RegisterViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<RegisterViewModel, User>(userViewModel);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(userViewModel.Password);
            ur.InsertUser(u);
            int uid = ur.GetLatestUserID();
            return uid;
        }

        public void UpdateUser(EditUserDetailsViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserDetailsViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<EditUserDetailsViewModel, User>(userViewModel);
            ur.UpdateUserDetails(u);
        }

        public void UpdateUserPassword(EditUserPasswordViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<EditUserPasswordViewModel, User>(userViewModel);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(userViewModel.Password);

            ur.UpdateUserPassword(u);
        }
    }
}
