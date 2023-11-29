namespace Final.Project.DAL;
public interface IUserProductsCartRepo : IGenericRepo<UserProductsCart>
{
    void DeleteAllProductsFromUserCart(string userId);
    IEnumerable<UserProductsCart> GetAllProductsByUserId(string userId);
    UserProductsCart GetByCompositeId(int ProductID, string userID);
    int GetCartCounter(string userIdFromToken);
}
