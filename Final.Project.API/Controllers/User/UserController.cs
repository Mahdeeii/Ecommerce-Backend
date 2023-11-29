using Final.Project.API.Responses;
using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMailingService mailingService;
        private readonly IUserService userService;

        public UserController(   IMailingService mailingService, IUserService userService)
        {
            this.mailingService = mailingService;
            this.userService = userService;
        }
          
        

                
        
        
        

        #region New Login Version
        [HttpPost("Login")]
        public async Task<ActionResult<UserManagerResponse>> Login(LoginDto loginCredientials)
        {
            var result = await userService.Login(loginCredientials);
            return result.IsSuccess ? Ok(result) : BadRequest(result);    
        }
        #endregion

        #region New Register Version

        [HttpPost("Register")]
        public async Task<ActionResult<UserManagerResponse>> Register([FromBody] RegisterDto credentials)
        {
            var result = await userService.Register(credentials);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion

        #region New Forget Password Version
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<UserManagerResponse>> ForgetPassword([FromForm] string email)
        {
            var result = await userService.Forget_Password(email);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion

        #region New Check Code For User Version
        [HttpPost("CheckCode")]
        public async Task<ActionResult<UserManagerResponse>> CheckCode([FromBody] ConfirmCodeDto confirmCodeDto)
        {
            var result = await userService.Check_Code(confirmCodeDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        #endregion

        #region New Reset Password Version

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<UserManagerResponse>> ResetPassword(UserResetPasswordDto userResetPasswordDto)
        {
            var result = await userService.Reset_Password(userResetPasswordDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        #endregion

        #region Sending Email

        [HttpPost]
        [Route("SendEmail")]
        public async Task<ActionResult<UserManagerResponse>> SendEmail([FromForm] MailRequestDto mailRequestDto)
        {
            var result = await mailingService.SendEmailAsync(mailRequestDto.ToEmail, mailRequestDto.Subject, mailRequestDto.Body) ;
            return result.IsSuccess ? Ok(result.Message) : BadRequest(result.Message);
        }

        #endregion

    }
}