namespace Final.Project.DAL;
public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceContext context;

    public IUserRepo UserRepo { get; }
    public IProductRepo ProductRepo { get; }
    public ICategoryRepo CategoryRepo { get; }
    public IOrderRepo OrderRepo { get; }
    public IOrdersDetailsRepo OrdersDetailsRepo { get; }
    public IUserProductsCartRepo UserProdutsCartRepo { get; }
    public IUserAddressRepo UserAddressRepo { get; }
    public IReviewRepo ReviewRepo { get; }
    public IDashboardUserRepo DashboardUserRepo { get; }
    public IWishListRepo WishListRepo { get; }

    public IContactUsRepo ContactUsRepo { get; }

    public UnitOfWork(ECommerceContext context, IUserRepo userRepo, IProductRepo productRepo, ICategoryRepo categoryRepo, IOrderRepo orderRepo, IOrdersDetailsRepo ordersDetailsRepo, IUserProductsCartRepo userProdutsCartRepo, IUserAddressRepo userAddressRepo, IReviewRepo reviewRepo, IDashboardUserRepo dashboardUserRepo,IWishListRepo wishListRepo, IContactUsRepo contactUsRepo)
    {
        this.context = context;
        UserRepo = userRepo;
        ProductRepo = productRepo;
        CategoryRepo = categoryRepo;
        OrderRepo = orderRepo;
        OrdersDetailsRepo = ordersDetailsRepo;
        UserProdutsCartRepo = userProdutsCartRepo;
        UserAddressRepo = userAddressRepo;
        ReviewRepo = reviewRepo;
        DashboardUserRepo = dashboardUserRepo;
        WishListRepo = wishListRepo;
        ContactUsRepo = contactUsRepo;
    }

    public int Savechanges()
    {
        return context.SaveChanges();
    }
}
