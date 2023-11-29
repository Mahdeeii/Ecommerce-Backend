using Final.Project.Bl;
using Final.Project.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAddressesController : ControllerBase
    {
        private readonly IUserAddressesManager _userAddressesManager;

        public UserAddressesController(IUserAddressesManager userAddressesManager)
        {
            _userAddressesManager = userAddressesManager;
        }

        #region Get All User Addresses
        [HttpGet]
        public ActionResult GetAllUserAddresses()
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }

            var addresses = _userAddressesManager.GetAllUserAddresses(userIdFromToken);

            return Ok(addresses);
        }

        #endregion
        #region Get User Addresses  by Id
        [HttpGet]
        [Route("address/{id}")]
        public ActionResult<AllUserAddressesReadDto> GetAddressById( int id)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }

            var addresses = _userAddressesManager.GetAddressById(id);

            return Ok(addresses);
        }

        #endregion

        #region Add New Address

        [HttpPost("AddNewAddress")]
        public ActionResult AddNewAddress(NewAddressAddingDto newAddress)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest();
            }
            _userAddressesManager.AddNewAddress(userIdFromToken, newAddress);

            return Ok();

        }

        #endregion

        #region Edit Address
        [HttpPut]
        [Route("Edit")]
        public ActionResult EditAddressDetails(AddressEditDto address)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }

            _userAddressesManager.EditAddress(userIdFromToken, address);

            return Ok();
        }

        #endregion

        #region Set Address Default
        [HttpPut]
        [Route("SetDefault/{AddressId}")]
        public ActionResult SetDefaultAddress(int AddressId)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }
            _userAddressesManager.SetDefaultAddress(userIdFromToken, AddressId);
            return Ok();
        }

        #endregion

        #region Delete Address
        [HttpDelete]
        [Route("Delete/{addressId}")]
        public ActionResult DeleteAddress(int addressId)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest();
            }

            _userAddressesManager.Delete(addressId);

            return Ok();
        } 
        #endregion

    }
}
