using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static RepositoryLayer.Services.UserRepo;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity UserReg(UserRegModel model);
        public UserLoginResult UserLogin(UserLoginModel model);

        public string ForgetPassword(ForgetPassword model);
        public bool ResetPassword(string Email, string NewPassword, string ConfirmPassword);

        public List<UserEntity> GetAllUsers();
        public List<UserEntity> GetUserbyID(int UserId);


        

    }
}
