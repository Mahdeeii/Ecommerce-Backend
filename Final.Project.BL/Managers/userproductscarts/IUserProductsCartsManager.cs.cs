
namespace Final.Project.BL;

public interface IUserProductsCartsManager
{
    string AddProductToCart(productToAddToCartDto product,string userId);
    string UpdateProductQuantityInCart(ProductQuantityinCartUpdateDto product, string userId);
    void DeleteProductFromCart(int id, string userId);
    IEnumerable<AllProductsReadDto> GetAllUserProductsInCart(string userId);
    int CartCounter(string userIdFromToken);
}
