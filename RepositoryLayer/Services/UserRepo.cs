using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooContext fundooContext;
       
        private readonly IConfiguration configuration;

        public UserRepo(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
             this.configuration = configuration;
        }
        public UserEntity UserReg(UserRegModel model)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.DateOfBirth = model.DateOfBirth;
                userEntity.Email = model.Email;
                userEntity.Password = model.Password;

                fundooContext.Users.Add(userEntity);

                fundooContext.SaveChanges();

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

        public string GenerateJwtToken(string Email, long UserId)
        {
           
          
            var claims = new List<Claim>
            {

               new Claim("UserId", UserId.ToString()),
               new Claim(ClaimTypes.Email, Email),
             
                // Add any other claims you want to include in the token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["JwtSettings:Issuer"], configuration["JwtSettings:Audience"], claims, DateTime.Now, DateTime.Now.AddHours(1), creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
      /*  public UserEntity UserLogin(UserLoginModel model)
        {
            try
            {
                var userEntity = fundooContext.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

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
        }*/


        public  UserLoginResult UserLogin(UserLoginModel model)
        {
            try
            {

                var userEntity = fundooContext.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (userEntity != null)
                {
                    string jwtToken = GenerateJwtToken(userEntity.Email, userEntity.UserId);
                    return new UserLoginResult
                    {
                        UserEntity = userEntity,
                        JwtToken = jwtToken
                    };
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

        public class UserLoginResult
        {
            public UserEntity UserEntity { get; set; }
            public string JwtToken { get; set; }
        }
        public string ForgetPassword(ForgetPassword model)
        {
           
            try
            {
                var EmailValidity = fundooContext.Users.FirstOrDefault(u => u.Email == model.Email);

                if (EmailValidity != null)
                {
                    var Token = GenerateJwtToken(EmailValidity.Email,EmailValidity.UserId);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(Token);
                    return Token;
                }
                else
                
                {
                    return null ;
                }
            }
            catch (Exception ex)
            {
                throw ex ;
            }
        }

         public List<UserEntity> GetAllUsers()
         {
            try
            {
                List<UserEntity> userList = new List<UserEntity>();
                userList = fundooContext.Users.ToList();
                return userList;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassword (string Email,string NewPassword,string ConfirmPassword)
        {
            try
            {
                if (NewPassword == ConfirmPassword)
                {
                    var Emailvalidity = fundooContext.Users.FirstOrDefault(u => u.Email == Email);
                    {
                        if (Emailvalidity != null)
                        {
                            Emailvalidity.Password = ConfirmPassword;
                            Emailvalidity.Password = EncryptionDecryption.EncryptPassword(ConfirmPassword);
                            fundooContext.Users.Update(Emailvalidity);
                            fundooContext.SaveChanges();
                            return true;

                        }
                       
                    }
                }
                return false;
            }
            catch(Exception Ex)
            {
            throw Ex ;
            }
        }
        public List<UserEntity> GetUserbyID(int UserId)
        {
            try
            {
                var userEntity = fundooContext.Users.FirstOrDefault(u => u.UserId == UserId);
                if (userEntity != null)
                {
                    List<UserEntity> userList = new List<UserEntity>();
                    userList.Add(userEntity);

                    return userList;
                }
                return null;
                
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        
    }

}
    
