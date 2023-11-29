using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;
public interface IOrdersDetailsRepo : IGenericRepo<OrderProductDetails>
{
    void AddRange(IEnumerable<OrderProductDetails> orderProducts);
    public IEnumerable<OrderProductDetails> GetTopProducts();
    public OrderProductDetails? GetByCompositeId(int ProductId, int orderId);
    

}
