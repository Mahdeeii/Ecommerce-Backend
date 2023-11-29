using Final.Project.API.Responses;
using Final.Project.BL;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API;
public interface IUserService
{
    Task<UserManagerResponse> Login(LoginDto loginCredientials);
    Task<UserManagerResponse> Register(RegisterDto registerCredientials);
    Task<UserManagerResponse> Forget_Password(string email);
    Task<UserManagerResponse> Check_Code(ConfirmCodeDto confirmCodeDto);
    Task<UserManagerResponse> Reset_Password(UserResetPasswordDto userResetPasswordDto);

}
