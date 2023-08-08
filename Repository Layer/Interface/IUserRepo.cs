using Common_Layer.Models;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IUserRepo
    {
        public UserEntity UserReg(UserRegModel model);

        public UserEntity UserLogin(UserLoginModel model);

    }
}
