using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IProductsManager _productsManager;
        private readonly ICategoriesManager _categoriesManager;
        private readonly IOrderDetailsManager _orderDetailsManager;



        public HomeController(IProductsManager productsManager, ICategoriesManager categoriesManager ,IOrderDetailsManager orderDetailsManager)
        {
            _productsManager = productsManager;
            _categoriesManager = categoriesManager;

            _orderDetailsManager = orderDetailsManager;
        }



        #region Get All Categories With All Products
        [HttpGet]
        [Route("CategoriesWithProducts")]
        public ActionResult<IEnumerable<CategoryDetailsDto>> GetAllCategoriesWithProducts()
        {
            IEnumerable<CategoryDetailsDto> categories = _categoriesManager.GetAllCategoriesWithProducts();

            return Ok(categories);
        }

        #endregion

        #region Get All Products have dicounts

        [HttpGet]
        [Route("SpecialOffers")]

        public ActionResult<IEnumerable<ProductChildDto>> GetAllProductWithDiscount()
        {
            IEnumerable<ProductChildDto> products = _productsManager.GetAllProductWithDiscount();
            if (products == null) { return NotFound(); }
            return Ok(products);
        }
        #endregion

        #region Get Top Products
        [HttpGet]
        [Route("TopProducts")]
        public ActionResult<IEnumerable<OrderProductDetailsDto>> GetTopProducts()
        {
            IEnumerable<OrderProductDetailsDto> TopProducts=_orderDetailsManager.GetTopProducts();
            return Ok(TopProducts);

        }
        #endregion


        #region Get New Products
        [HttpGet]
        [Route("NewProducts")]
        public ActionResult<IEnumerable<ProductChildDto>> GetNewProducts()
        {
            IEnumerable<ProductChildDto> NewProducts = _productsManager.GetNewProducts();
            return Ok(NewProducts);

        }
        #endregion
    }
}
