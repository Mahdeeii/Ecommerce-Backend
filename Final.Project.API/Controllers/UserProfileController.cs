using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly IUsersManager? _UsersManager;
        private readonly UserManager<User> _Usermanager;

        public UserProfileController(IUsersManager userManager, UserManager<User> manager)
        {
            _UsersManager = userManager;
            _Usermanager = manager;
        }

        #region profile

        #region getuser

        [HttpGet]
        [Route("profile")]
        public ActionResult<UserReadDto> getUserProfile()
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;
            //UserReadDto? user = _UsersManager.GetUserReadDto(currentUser.Id);
            UserReadDto? user = _UsersManager.GetUserReadDto(currentUser);

            if (user is null)
            {
                return NotFound();
            }


            return Ok(user);
        }
        #endregion

        #region EditUserName

        [HttpPut]
        [Route("Edit")]
        public async Task<ActionResult> Edit(UserUpdateDto updateDto)
        {
            var currentUser = await _Usermanager.GetUserAsync(User);
            
            //updateDto.Id=currentUser.Id;
            if (currentUser is null)
            {
                return NotFound();
            }

            // _UsersManager.Edit(updateDto,currentUser.Id);
             _UsersManager.Edit(updateDto, currentUser);

            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete]
        [Route("DeleteUser")]
        public ActionResult Delete()
        {
            var currentUser = _Usermanager.GetUserAsync(User).Result;

            //id = currentUser.Id;

            // if (!result) { return NotFound(); }
            var isfound = _UsersManager.delete(currentUser.Id);

            if (!isfound) { return NotFound(); }

            return Ok("Deleted  Successfully");
        }
        #endregion
        
        #region ChangePassword

        [HttpPost]
        [Route("Change_Password")]
        public async Task<ActionResult> ChangePassword(UserChangepassDto passwordDto)
        {
            //get the user
            User? currentUser = await _Usermanager.GetUserAsync(User);
            if (currentUser is null)
            {
                return NotFound();
            }

            //confirm old password
            var isValiduser = await _Usermanager.CheckPasswordAsync(currentUser!, passwordDto.OldPassword);
            if (!isValiduser)
            {
                return NotFound(); 
            }

            if (passwordDto.NewPassword != passwordDto.ConfirmNewPassword)
            {
                return NoContent();
            }

            //change password
            await _Usermanager.ChangePasswordAsync(currentUser!, passwordDto.OldPassword, passwordDto.NewPassword);

            return Ok();
        }


        #endregion

        #endregion

        #region GetUserOrders
        [HttpGet]
        [Route("orders")]
        public ActionResult<List<UserOrderDto>> GetUserOrders()
        {

            var currentUser = _Usermanager.GetUserAsync(User).Result;

            List<UserOrderDto>? userOrder = _UsersManager.GetUserOrdersDto(currentUser.Id) as List<UserOrderDto>;

            if (userOrder is null)

            { return null; }

            return Ok(userOrder);

        }
        //[HttpGet]
        //[Route("{id}/Products")]

        //public ActionResult Userorder(int id)
        //{
        //    IEnumerable<UserProductDto>? Userpro = (IEnumerable<UserProductDto>?)_UsersManager.UserOrders();
        //    if (Userpro == null) { return NotFound(); }
        //    return Ok(Userpro);
        //}
        #endregion

        #region getUserOrderDetails
        [HttpGet]
        [Route("orderDetails/{orderId}")]
        public ActionResult<UserOrderDetailsDto> GetOrderDetails(int orderId)
        {
            //var currentUser = _Usermanager.GetUserAsync(User).Result;
            // var order = _UsersManager.GetUserOrderDto(currentUser.Id);
            var orderDetails = _UsersManager.GetUserOrderDetailsDto(orderId);
            if (orderDetails is null)
            {
                return NotFound();
            }

            return Ok(orderDetails); //200 OK
        }
        #endregion region 
    }
}


