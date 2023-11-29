using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;
public class OrderRepo : GenericRepo<Order>, IOrderRepo
{
    private readonly ECommerceContext _context;
    public OrderRepo(ECommerceContext context) : base(context)
    {
        _context = context;
    }
    // select top(1) id from orders where userid=5 order by desc
    public int GetLastUserOrder(string userId)
    {
        return _context.Set<Order>()
                        .Where(o => o.UserId == userId)
                        .OrderByDescending(o => o.OrderDate)
                        .First().Id;

    }

    public ICollection<Order> GetOrdersWithData()
    {
        return _context.Set<Order>()
                .Include(o => o.User)
                .Include(o => o.OrdersProductDetails)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p=>p.ProductImages)
                .ToList();
    }

    #region Get Order With Product

    public Order GetOrderWithProducts(int OrderId)
    {
        return _context.Set<Order>()
                .Include(o => o.User)
                .Include(o => o.UserAddress)
                .Include(o => o.OrdersProductDetails)
                    .ThenInclude(op => op.Product)
                        .ThenInclude(p => p.ProductImages)

                .First(o => o.Id == OrderId);
    }

    #endregion
}

