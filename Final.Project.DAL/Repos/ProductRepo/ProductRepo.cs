using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Final.Project.DAL;
public class ProductRepo : GenericRepo<Product>, IProductRepo
{
    private readonly ECommerceContext _context;

    public ProductRepo(ECommerceContext context) : base(context)
    {
        _context = context;

    }

    #region Get Product Details with its Category
    public Product? GetProductByIdWithCategory(int id)
    {
        return _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Reviews)
                        .ThenInclude(p => p.User)
                    .Include(p => p.ProductImages)
                    .FirstOrDefault(p => p.Id == id);
    }

    #endregion

    #region Get Product Details with images
    public Product? GetProductByIdWithimages(int id)
    {
        return _context.Set<Product>()
                    .Include(p => p.ProductImages)
                    .FirstOrDefault(p => p.Id == id);



    }

    #endregion

    #region Get All Products have Discount
    public IEnumerable<Product> GetAllProductWithDiscount()
    {
        return _context.Products
            .Include(p => p.Reviews)
            .Include(p => p.ProductImages)
            .Where(p => p.Discount > 0)
            .OrderByDescending(p => p.Discount)
            .Take(8);
    }
    #endregion

    #region Get all With Category

    public IEnumerable<Product> GetAllWithCategory()
    {
        return _context.Set<Product>()
                       .Include(x => x.Category)
                       .Include(p => p.ProductImages);

    }
    #endregion

    #region Get Related Products By brand
    public IEnumerable<Product> GetRelatedProductsByCategoryName(string brand)
    {
        return _context.Set<Product>()
            .Include(x => x.Category)
            .Include(x => x.Reviews)
            .Include(p => p.ProductImages)

            .Where(x => x.Category.Name == brand)
            .Take(5);

    }

    #endregion

    #region Get Product After Filteration

    public IEnumerable<Product> GetProductFiltered(QueryParametars parametars)
    {

        var products = _context.Products.Include(p => p.Reviews).Include(p => p.ProductImages).AsQueryable();

        if (parametars.CategotyId > 0)
        {
            products = products.Where(q => q.CategoryID == parametars.CategotyId);
        }

        if (parametars.ProductName != null || parametars.ProductName != "")
        {
            products = products.Where(q => q.Name.Contains(parametars.ProductName));
        }

        if (parametars.MaxPrice > 0)
        {
            products = products.Where(q => q.Price - (q.Price * q.Discount / 100) <= parametars.MaxPrice);
        }
        if (parametars.MinPrice > 0)
        {
            products = products.Where(q => q.Price - (q.Price * q.Discount / 100) >= parametars.MinPrice);
        }

        if (parametars.Rating > 0)
        {
            products = products.Where(q => q.Reviews.Average(r => r.Rating) >= parametars.Rating);
        }

        return products;
    }

    #endregion

    #region Get All Products With Pagination
    public IEnumerable<Product> GetAllProductsInPagnation(int page, int countPerPage)
    {
        return _context.Products
            .Include(p => p.Reviews)
            .Include(p => p.ProductImages)
            .Skip((page - 1) * countPerPage)
            .Take(countPerPage);


    }

    public int GetCount()
    {
        return _context.Products.Count();
    }
    #endregion

    #region Get Product After Filteration with Pagination

    public IEnumerable<Product> GetProductFilteredInPagination(QueryParametars parametars, int page, int countPerPage)
    {

        var products = _context.Products.Include(p => p.Reviews).AsQueryable();
        products = products.Include(p => p.ProductImages);

        if (parametars.CategotyId > 0)
        {
            products = products.Where(q => q.CategoryID == parametars.CategotyId);
        }

        if (parametars.ProductName != null || parametars.ProductName != "")
        {
            products = products.Where(q => q.Name.Contains(parametars.ProductName));
        }

        if (parametars.MaxPrice > 0)
        {
            products = products.Where(q => q.Price - (q.Price * q.Discount / 100) <= parametars.MaxPrice);
        }
        if (parametars.MinPrice > 0)
        {
            products = products.Where(q => q.Price - (q.Price * q.Discount / 100) >= parametars.MinPrice);
        }

        if (parametars.Rating > 0)
        {
            products = products.Where(q => q.Reviews.Average(r => r.Rating) >= parametars.Rating);
        }

        return products

;
    }

    #endregion

    #region Get last new products
    public IEnumerable<Product> GetNewProducts()
    {
        return _context.Products
            .Include(p => p.Reviews)
            .Include(p=>p.ProductImages)
            .OrderByDescending(p => p.Id)
            .Take(8);
    }


    #endregion

    #region Get All Dashboard Products with pagination
    public IEnumerable<Product> GetAllPaginationDashboardProducts(int page, int countPerPage)
    {
        return _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .Skip((page - 1) * countPerPage)
            .Take(countPerPage);
    }
    #endregion
}
