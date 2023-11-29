namespace Final.Project.DAL;
public interface IOrderRepo : IGenericRepo<Order>
{
    public int GetLastUserOrder(string userId);

    public ICollection<Order> GetOrdersWithData();
    Order GetOrderWithProducts(int OrderId);
}
