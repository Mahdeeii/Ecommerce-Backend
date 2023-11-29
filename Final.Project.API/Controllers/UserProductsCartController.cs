using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProductsCartController : ControllerBase
    {
        private readonly IUserProductsCartsManager _userProductsCartsManager;
        private readonly UserManager<User> _manager;

        public UserProductsCartController(IUserProductsCartsManager userProductsCartsManager,
            UserManager<User> manager)
        {
            _userProductsCartsManager = userProductsCartsManager;
            _manager = manager;
        }


        #region Get All Products In Cart

        [HttpGet]
        public ActionResult GetAllProductsInCart()
        {
            //temp user id until we use authentication and then
            // we will get that userId from token by this function
            //var currentUser = _userManager.GetUserAsync(User).Result;
            //then get user id from current user details
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentUser = _manager.GetUserAsync(User).Result;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }
            //string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            IEnumerable<AllProductsReadDto> Products = _userProductsCartsManager.GetAllUserProductsInCart(userIdFromToken);

            return Ok(Products);

        }


        #endregion

        #region Add Product To Cart

        [HttpPost]
        [Route("AddProduct")]
        public ActionResult AddProductToCart(productToAddToCartDto product)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentUser = _manager.GetUserAsync(User).Result;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");

            }
            //string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            string status = _userProductsCartsManager.AddProductToCart(product, userIdFromToken);

            return Ok(new
            {
                message = status
            });

        }

        #endregion

        #region Get Cart Products Counter
        [HttpGet]
        [Route("counter")]
        public ActionResult GetUserCartProductsCounter()
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");

            }
            int counter = _userProductsCartsManager.CartCounter(userIdFromToken);
            return Ok(counter);
        }

        #endregion

        #region Update Product Quantity In Cart
        [HttpPut]
        [Route("UpdateProduct")]

        public ActionResult UpdateProductQuantityInCart(ProductQuantityinCartUpdateDto product)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentUser = _manager.GetUserAsync(User).Result;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }
            // string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            string status = _userProductsCartsManager.UpdateProductQuantityInCart(product, userIdFromToken);
            return Ok(status);

            //return new JsonResult(new
            //{
            //    message = status,
            //    statuscode = 200,
            //    data = product,
            //    success = true
            //});
        }

        #endregion

        #region Delete Product
        [HttpDelete]
        [Route("DeleteProduct/{id}")]

        public ActionResult DeleteProduct(int id)
        {
            var userIdFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var currentUser = _manager.GetUserAsync(User).Result;
            if (userIdFromToken is null)
            {
                return BadRequest("not logged in");
            }

            //string? userId = "18c2ddd6-ec81-4e72-ab47-88958cd1e43a";
            _userProductsCartsManager.DeleteProductFromCart(id, userIdFromToken);
            return Ok("product Deleted from Cart");

            
            //this return json to frontend

            //return new JsonResult(new
            //{
            //    Message = "Product Deleted SuccessFully From user cart",
            //    StatusCode = 200,
            //    data = id,
            //    Success = true
            //});


            

        }

        #endregion

    }
}
