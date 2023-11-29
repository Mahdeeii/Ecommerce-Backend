using Final.Project.API.Controllers;
using Final.Project.API.Responses;
using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Final.Project.API;
public class UserService : IUserService
{
    private readonly IConfiguration configuration;
    private readonly UserManager<User> manager;
    private readonly IMailingService mailingService;
    private readonly IUnitOfWork unitOfWork;


    public UserService(IConfiguration configuration,
                                 UserManager<User> manager,
                                 IMailingService mailingService,
                                 IUnitOfWork unitOfWork)
    {
        this.configuration = configuration;
        this.manager = manager;
        this.mailingService = mailingService;
        this.unitOfWork = unitOfWork;
    }


    public async Task<UserManagerResponse> Login(LoginDto loginCredientials)
    {
        try
        {
            // Search by Email and check if user found or Not 
            User? user = await manager.FindByEmailAsync(loginCredientials.Email);
            if (user is null)
            {
                return new UserManagerResponse
                {
                    Message = "User Not Found",
                    IsSuccess = false,

                };
            }

            // Check On Password
            bool isValiduser =  manager.CheckPasswordAsync(user, loginCredientials.Password).GetAwaiter().GetResult();
            if (!isValiduser)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid Password!",
                    IsSuccess = false,
                };
            }

            // Get claims
            var claims = await manager.GetClaimsAsync(user);
            //List<Claim> claims = manager.GetClaimsAsync(user).GetAwaiter().GetResult().ToList();

            // get Secret Key
            string? secretKey = configuration.GetValue<string>("SecretKey");
            byte[] keyAsBytes = Encoding.ASCII.GetBytes(secretKey!);
            SymmetricSecurityKey key = new(keyAsBytes);

            SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256Signature);

            DateTime exp = DateTime.Now.AddMinutes(30);//expire after 20days
            JwtSecurityToken jwtSecurity = new(claims: claims, signingCredentials: signingCredentials, expires: exp);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurity);

            return new UserManagerResponse
            {
                Message = "User Loggedin Successfully",
                IsSuccess = true,
                Data = new TokenDto
                {
                    Token = token,
                    Role = user.Role.ToString(),
                },
            };
        }
        catch (Exception ex)
        {
            return new UserManagerResponse
            {
                Message = ex.Message,
                IsSuccess = false,
            };
        }
    }

    public async Task<UserManagerResponse> Register(RegisterDto registerCredientials)
    {
        try
        {
            User user = new()
            {
                FName = registerCredientials.FName,
                LName = registerCredientials.LName,
                Email = registerCredientials.Email,
                UserName = registerCredientials.Email,
                Role = Role.Customer,

            };

            var result = await manager.CreateAsync(user, registerCredientials.Password);
            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Errors = result.Errors,
                    IsSuccess = false,
                };
            }

            List<Claim> claims = new()
             {
                 new Claim(ClaimTypes.NameIdentifier, user.Id),
                 new Claim(ClaimTypes.Role, user.Role.ToString()),
             };
            var claimsResult = await manager.AddClaimsAsync(user, claims);
            if (!claimsResult.Succeeded)
            {
                return new UserManagerResponse
                {
                    Errors = claimsResult.Errors,
                    IsSuccess = false,
                };
            }

            string? secretKey = configuration.GetValue<string>("SecretKey");
            byte[] keyAsBytes = Encoding.ASCII.GetBytes(secretKey!);
            SymmetricSecurityKey key = new(keyAsBytes);

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            DateTime exp = DateTime.Now.AddMinutes(1);//expire after 20days
            JwtSecurityToken jwtSecurity = new(claims: claims, signingCredentials: signingCredentials, expires: exp);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurity);

            return new UserManagerResponse
            {
                Message = "Register Successfully",
                IsSuccess = true,
                Data = new TokenDto
                {
                    Token = token,
                    Role = user.Role.ToString(),
                },
            };
        }
        catch (Exception ex)
        {
            return new UserManagerResponse
            {
                Message = ex.Message,
                IsSuccess = false,
            };
        }
    }

    public async Task<UserManagerResponse> Forget_Password(string email)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                return new UserManagerResponse
                {
                    Message = "Please Enter Email",
                    IsSuccess = false,
                };
            }

            User? user = await manager.FindByEmailAsync(email);
            if (user is null)
            {
                return new UserManagerResponse
                {
                    Message = "User not found with the given email.",
                    IsSuccess = false,
                };
            }

            var random = new Random();
            var code = random.Next(10000, 99999).ToString();
            user.Code = code;
            var result = unitOfWork.Savechanges();
            if (result <= 0)
            {
                return new UserManagerResponse
                {
                    Message = "Code can Not be Set !!!!",
                    IsSuccess = false,
                };
            }

            await mailingService.SendEmailAsync(email, "Reset Password", $"Your Code is {code}");
            return new UserManagerResponse
            {
                Message = "Reset Password Email has been Sent Successfully!!!",
                IsSuccess = true,
            };
        }
        catch (Exception ex)
        {
            return new UserManagerResponse
            {
                Message = ex.Message,
                IsSuccess = false,
            };
        }
    }

    public async Task<UserManagerResponse> Check_Code(ConfirmCodeDto confirmCodeDto)
    {
        try
        {
            if (string.IsNullOrEmpty(confirmCodeDto.Email))
            {
                return new UserManagerResponse {
                    Message = "Email is Invalid!!!!",
                    IsSuccess = false,
                };
            }

            User? user = await manager.FindByEmailAsync(confirmCodeDto.Email);
            if (user is null)
            {
                return new UserManagerResponse
                {
                    Message = "User not found with the given email.",
                    IsSuccess = false,
                };
            }

            if (user.Code != confirmCodeDto.Code)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid Code!!!",
                    IsSuccess = true,  
                };
            }

            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = "Code Is Valid!"
            };
        }
        catch (Exception ex)
        {
            return new UserManagerResponse
            {
                Message = ex.Message,
                IsSuccess = false,
            };
        }
    }

    public async Task<UserManagerResponse> Reset_Password(UserResetPasswordDto userResetPasswordDto)
    {
        try
        {
            User? user = await manager.FindByEmailAsync(userResetPasswordDto.Email);
            if (user is null)
            {
                return new UserManagerResponse
                {
                    Message = "User Not Found With This Email!!!",
                    IsSuccess = false,
                };
            }

            if (userResetPasswordDto.NewPassword != userResetPasswordDto.ConfirmNewPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Passwored Dosen't Match Confirmation!!!",
                    IsSuccess = false,
                };
            }

            var token = await manager.GeneratePasswordResetTokenAsync(user);

            var result = await manager.ResetPasswordAsync(user, token, userResetPasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Errors = result.Errors,
                    IsSuccess = false
                };
            }

            return new UserManagerResponse
            {
                Message = "Reset Password Successfully!!!",
                IsSuccess = true,
            };
        }
        catch (Exception ex)
        {
            return new UserManagerResponse
            {
                Message = ex.Message,
                IsSuccess = false,
            };
        }
    }
}
