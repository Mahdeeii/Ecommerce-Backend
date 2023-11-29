namespace Final.Project.DAL;
public interface IUnitOfWork
{
    public IUserRepo UserRepo { get; }
    public IProductRepo ProductRepo { get; }
    public ICategoryRepo CategoryRepo { get; }
    public IOrderRepo OrderRepo { get; }
    public IOrdersDetailsRepo OrdersDetailsRepo  { get; }
    public IUserProductsCartRepo UserProdutsCartRepo { get; }
    public IUserAddressRepo UserAddressRepo { get; }
    public IReviewRepo ReviewRepo { get; }
    public IDashboardUserRepo DashboardUserRepo { get; }
    public IWishListRepo WishListRepo { get; }
    public IContactUsRepo ContactUsRepo { get; }





    int Savechanges();
}
