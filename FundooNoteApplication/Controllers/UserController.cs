using BusineesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System.Linq;
using static RepositoryLayer.Services.UserRepo;

namespace FundooNoteApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Registration(UserRegModel model)
        {
            model.Password = EncryptionDecryption.EncryptPassword(model.Password);

            var result = userBusiness.UserReg(model);
           
            if (result != null)
            {
                return this.Ok(new { Success = true, Message = "User Registration sucessfull", Data = result });
            }
            else
            {
                return this.BadRequest(new { Success = false, Message = "User Registration Unsucessfull", Data = result });
            }
        }

        [HttpPost]
        [Route("Login")]

        public IActionResult Login(UserLoginModel model)
        {
           //model.Password = EncryptionandDecryption.DecryptPassword(model.Password);
            var result = userBusiness.UserLogin(model);
            
            if (result != null)
            {
                return Ok(new { message ="Login Successful", Token = result });
            }
            else
            {
                return Unauthorized(new { message = "Login UnSuccessful"});
            }
        }

        [HttpPost]
        [Route("ResetPassword")]

        public IActionResult ResetPassword(ForgetPassword model)
        {
           

            var result = userBusiness.ForgetPassword(model);
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Password Updated sucessfull"});

            }
            else
            {
                return NotFound(new { Success = false, Message = "Password Updation Unsucessful" });

            }
        }
        [Authorize]
        [HttpPost]
        [Route("Reset")]

        public IActionResult ResetPassword(string NewPassword, string ConfirmPassword)
        {
            var email = User.Claims.First(data => data.Type == "Email").Value;
            var result = userBusiness.ResetPassword(email, NewPassword, ConfirmPassword);
            if (result != null)
            {
                return Ok(new { success = true, message = "Forgot Pass Email Send Successfully", data = result });
            }
            else
            {
                return NotFound(new { success = false, message = "Forgot pass email not send...", data = result });
            }
        }


        [HttpGet]
        [Route("GetUsers")]

        public IActionResult GetAllUsers()
        {
            var result = userBusiness.GetAllUsers();
            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get all users sucessfull", Data = result });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("GetallusersbyId")]

        public IActionResult GetallusersbyId(int UserId)
        {
            var result = userBusiness.GetUserbyID(UserId);

            if (result != null)
            {
                return Ok(new { Success = true, Message = "Get all users sucessfull", Data = result });

            }
            else
            {
                return NotFound();

            }
            
        }


    }
}
