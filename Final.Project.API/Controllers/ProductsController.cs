using Final.Project.BL;
using Final.Project.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsManager _productsManager;
        private readonly ECommerceContext context;
        private readonly IHelper helper;

        public ProductsController(IProductsManager productsManager, ECommerceContext context,IHelper Helper)
        {
            _productsManager = productsManager;
            this.context = context;
            helper = Helper;
        }

        #region Get Product By Id
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProductDetailsDto> GetProductbyid(int id)
        {
            ProductDetailsDto product = _productsManager.GetProductByID(id);
            if (product == null) { return NotFound(); }
            return Ok(product);
        }
       
        #endregion

        #region Get Product By Id in dashboard for edit
        [HttpGet]
        [Route("dashboard/{id}")]
        public ActionResult<ProducttoeditdashboardDto> GetProductDetails(int id)
        {
            ProducttoeditdashboardDto product = _productsManager.GetProductByIDdashboard(id);
            if (product == null) { return NotFound(); }
            return Ok(product);
        }

        #endregion

        

        #region Products Filteration
        [HttpPost]
        [Route("Filter")]
        public ActionResult GetAll(ProductQueryDto productQueryDto)
        {
            var result = _productsManager.ProductAfterFilteration(productQueryDto);

            return Ok(result.ToList());
        }
        #endregion

        #region Get all Products
        [HttpGet]
        [Route("Dashboard/GetAllProducts")]

        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            IEnumerable<ProductReadDto> products = _productsManager.GetAllProducts();

            if (products is null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        #endregion

        #region Add Product
        [HttpPost]
        [Route("Dashboard/addProduct")]
        public ActionResult Add( ProductAddDto product)
        {
            if (product == null) return BadRequest();
            _productsManager.AddProduct(product);
            return NoContent();
          
        }


    [HttpPost]
        [Route("Dashboard/uploadImages")]
        public ActionResult<List<string>> Upload(List<IFormFile> files)

        {
            List<string> urls = new List<string>();
            foreach (IFormFile file in files)
            {
                helper.ImageValidation(file);
                var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var imagesPath = Path.Combine(Environment.CurrentDirectory, "Images");
                var fulFilePath = Path.Combine(imagesPath, newFileName);
                using var stream = new FileStream(fulFilePath, FileMode.Create);
                file.CopyTo(stream);

                var url = $"{Request.Scheme}://{Request.Host}/Images/{newFileName}";
                urls.Add(url);


            }

            return urls;
        }
        #endregion

        #region Edit Product

        [HttpPatch]
        [Route("/dashboard/editproduct")]

        public ActionResult<Product> Edit(ProductEditDto product)
        {
            if (product==null)
            {
                return BadRequest();
            }
            _productsManager.EditProduct(product);
            return NoContent();

        }

        #endregion

        #region Delete Product
        [HttpDelete]
        [Route("Dashboard/DeleteProduct/{Id}")]

        public ActionResult Delete(int Id)
        {
            bool isDeleted = _productsManager.DeleteProduct(Id);

            return isDeleted ? NoContent() : BadRequest();
        }

        #endregion

        #region Get Related Products
        [HttpGet]
        [Route("RelatedProducts/{brand}")]
        public ActionResult<RelatedProductDto> GetRelatedProducts(string brand)
        {
            IEnumerable<RelatedProductDto> products = _productsManager.GetRelatedProducts(brand);

            if (products is null)
            {
                return NotFound("There is No Related Products");
            }

            return Ok(products);
        }
        #endregion

        #region Get All Products in Pagination 
        [HttpGet]
        [Route("{page}/{countPerPage}")]

        public ActionResult<ProductPaginationDto> GetAllProductsInPagination(int page,int countPerPage)
        {
            var products = _productsManager.GetAllProductsInPagnation(page, countPerPage);
            return Ok(products);
            
        }
        #endregion

        #region Filter With Pagination
        [HttpPost]
        [Route("PaginationFilter/{page}/{countPerPage}")]
        public ActionResult<ProductFilterationPaginationResultDto> GetAllProductAfterFilterationInPagination(ProductQueryDto productQueryDto, int page, int countPerPage)
        {
            var result = _productsManager.ProductAfterFilterationInPagination(productQueryDto, page, countPerPage);

            return Ok(result);
        }
        #endregion

        #region Get all Products in dashboard with pagination
        [HttpGet]
        [Route("Dashboard/AllProducts/{page}/{countPerPage}")]
        public ActionResult<ProductReadPaginationDto> GetAllPaginationDashboardProducts(int page, int countPerPage)
        {
            ProductReadPaginationDto products = _productsManager.GetAllPaginationDashboardProducts(page, countPerPage);

            if (products is null)
            {
                return NotFound();
            }

            return Ok(products);
        }
        #endregion
    }
}