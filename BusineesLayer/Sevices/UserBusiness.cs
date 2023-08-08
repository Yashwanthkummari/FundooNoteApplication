using BusineesLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static RepositoryLayer.Services.UserRepo;

namespace BusineesLayer.Sevices
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepo _userRepo;

    
        public UserBusiness(IUserRepo userRepo)
        {
            _userRepo = userRepo;

        }

        public UserEntity UserReg(UserRegModel model)

        {
            try
            {
                return _userRepo.UserReg(model);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

      UserRepo.UserLoginResult IUserBusiness.UserLogin(UserLoginModel model)
        {

            try
            {
                return _userRepo.UserLogin(model);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public string ForgetPassword(ForgetPassword model)
        {
            try
            {
                return _userRepo.ForgetPassword(model);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        public bool ResetPassword(string Email, string NewPassword, string ConfirmPassword)
        {

        
        
            try
            {
                return _userRepo.ResetPassword(Email,NewPassword,ConfirmPassword);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public List<UserEntity> GetAllUsers() 
        {
            try
            {
                return _userRepo.GetAllUsers();
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        public List<UserEntity> GetUserbyID(int UserId) 
        {
            try
            {
                return _userRepo.GetUserbyID(UserId);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


    }
}

