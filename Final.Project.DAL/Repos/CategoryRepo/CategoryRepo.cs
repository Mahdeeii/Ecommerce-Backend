using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;
public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
{
    private readonly ECommerceContext _context;

    public CategoryRepo(ECommerceContext context) : base(context)
    {
        _context = context;

    }

    #region Get Category By ID  With  Products
    public IEnumerable<Product>? GetByIdWithProducts(int id)
    {
        return _context.Products
               .Include(p => p.Reviews)
               .Include(p => p.Category)
               .Where(c => c.CategoryID == id).ToList();

    }
    #endregion

    #region Get All Categories With all products
    public IEnumerable<Category>? GetAllCategoriesWithAllProducts()
    {
        return _context.Categories
              .Include(c => c.Products)
              .ThenInclude(c => c.Reviews);
    }
    #endregion
}













