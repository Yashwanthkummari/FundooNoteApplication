using Common_Layer.Models;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Repository_Layer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly WebApplication2Context webApplication2Context;

        public UserRepo(WebApplication2Context webApplication2Context)
        {
            this.webApplication2Context = webApplication2Context;
        }


        public UserEntity UserReg(UserRegModel model)

        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.Email = model.Email;
                userEntity.DateOfBirth = model.DateOfBirth;
                userEntity.Password = model.Password;

                webApplication2Context.User.Add(userEntity);

                webApplication2Context.SaveChanges();

                if (userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
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
                var userEntity = webApplication2Context.User.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (userEntity != null)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /*public string GenerateJwtToken(string Email, long UserId)
        {


            var claims = new List<Claim>
      {
               new Claim("UserId", UserId.ToString()),
               new Claim(ClaimTypes.Email, Email),
                // Add any other claims you want to include in the token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("JwtSettings:Issuer", "JwtSettings:Audience", claims, DateTime.Now, DateTime.Now.AddHours(1), creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }*/
    }
}
    

