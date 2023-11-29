namespace Final.Project.DAL;
public interface ICategoryRepo : IGenericRepo<Category>
{
    public IEnumerable<Product>? GetByIdWithProducts(int id);
    public IEnumerable<Category>? GetAllCategoriesWithAllProducts();

}
