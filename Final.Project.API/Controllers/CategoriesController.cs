using Final.Project.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Final.Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesManager _categoriesManager;

        public CategoriesController(ICategoriesManager categoriesManager)
        {
            _categoriesManager = categoriesManager;
        }

        #region Get All Categories
        
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetAllCategories()
        {
            IEnumerable<CategoryDto> categories = _categoriesManager.GetAllCategoriesDto();
            return Ok(categories);
        }
        #endregion

        #region Get Category by id
        
        [HttpGet]
        [Route("{id}")]
        public ActionResult<CategoryDto> GetCategoryById(int id)
        {
            CategoryDto? category = _categoriesManager.GetCategoryById(id);
            if (category is null) { return NotFound(); }

            return Ok(category);
        }
        #endregion

        #region Get Category By id With Products
        
        [HttpGet]
        [Route("{id}/Products")]
        public ActionResult CategoryDetails(int id)
        {
            IEnumerable<ProductChildDto>? categoryDetailDto = _categoriesManager.GetCategoryWithProducts(id);
            if (categoryDetailDto == null) { return NotFound(); }
            return Ok(categoryDetailDto);
        }
        #endregion

        #region Get all Categories Dashboard
        [HttpGet]
        [Route("Dashboard/GetAllCategories")]
        public ActionResult<IEnumerable<CategoryReadDto>> GetAllCategoriesDashBoard()
        {
            IEnumerable<CategoryReadDto> categoryReadDtos = _categoriesManager.GetAllCategories();
            if (categoryReadDtos is null)
            {
                return BadRequest();
            }

            return Ok(categoryReadDtos);
        }

        #endregion

        #region Add Category Dashboard

        [HttpPost]
        [Route("Dashboard/AddCategory")]
        public ActionResult Add(CategoryAddDto categoryAddDto)
        {
            bool isAdded = _categoriesManager.AddCategory(categoryAddDto);
            return isAdded ? NoContent() : BadRequest();
        }

        #endregion

        #region Edit Category Dashboard

        [HttpPut]
        [Route("Dashboard/EditCategory")]
        public ActionResult Edit(CategoryEditDto categoryEditDto)
        {
            bool isEdited = _categoriesManager.UpdateCategory(categoryEditDto);

            return isEdited ? NoContent() : BadRequest();
        }

        #endregion

        #region Delete Category Dashboard 

        [HttpDelete]
        [Route("Dashboard/DeleteCategory/{Id}")]
        public ActionResult Delete(int Id)
        {
            bool isDeleted = _categoriesManager.DeleteCategory(Id);

            return isDeleted ? NoContent() : BadRequest();
        }

        #endregion

    }
}
