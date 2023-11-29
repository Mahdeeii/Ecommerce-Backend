
using Final.Project.DAL;

namespace Final.Project.BL;

public interface IOrderDetailsManager
{
    public IEnumerable<OrderProductDetailsDto> GetTopProducts();

}
