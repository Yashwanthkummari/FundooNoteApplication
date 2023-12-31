﻿using BusinessLayer.Interface;
using Common_Layer.Models;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness:IUserBusiness
    {
        private readonly IUserRepo _userRepo ;

        public UserBusiness(IUserRepo userRepo )
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

        public UserEntity UserLogin(UserLoginModel model)
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


    }
}
