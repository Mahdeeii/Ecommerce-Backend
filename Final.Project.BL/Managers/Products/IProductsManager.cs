using Final.Project.DAL;

namespace Final.Project.BL;

public interface IProductsManager
{
   public ProductDetailsDto GetProductByID(int id);
    public ProducttoeditdashboardDto GetProductByIDdashboard(int id);

    //IEnumerable<ProductChildDto> GetAllProductsWithAvgRating();

    public IEnumerable<ProductChildDto> GetAllProductWithDiscount();

    IEnumerable<ProductReadDto> GetAllProducts();
    public bool AddProduct(ProductAddDto productDto);
    bool EditProduct(ProductEditDto productEditDto);
    bool DeleteProduct(int Id);

    public IEnumerable<RelatedProductDto> GetRelatedProducts(string brand);

    IEnumerable<ProductFilterationResultDto> ProductAfterFilteration(ProductQueryDto queryDto);

    ProductPaginationDto GetAllProductsInPagnation(int page, int countPerPage);
    ProductFilterationPaginationResultDto ProductAfterFilterationInPagination(ProductQueryDto queryDto, int page, int countPerPage);

    IEnumerable<ProductChildDto> GetNewProducts();
   ProductReadPaginationDto GetAllPaginationDashboardProducts(int page, int countPerPage);

}
