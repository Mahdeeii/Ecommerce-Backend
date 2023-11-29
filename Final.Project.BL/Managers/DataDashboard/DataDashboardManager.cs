using Final.Project.DAL;

namespace Final.Project.BL;

public class DataDashboardManager : IDataDashboardManager
{
    private readonly IUnitOfWork _unitOfWork;

    public DataDashboardManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public DataReadDto GetDataDashboard()
    {
        var orderFromDb = _unitOfWork.OrderRepo.GetOrdersWithData();
        var sell = orderFromDb
            .Select(o => new OrderReadDto
            {
                TotalPrice = o.OrdersProductDetails.Sum(op => Math.Round((op.Product.Price - (op.Product.Price * (op.Product.Discount / 100))) * op.Quantity, 0)),
            });

        int users = _unitOfWork.DashboardUserRepo.GetAll().Count();
        int products = _unitOfWork.ProductRepo.GetAll().Count();
        int orders = _unitOfWork.OrderRepo.GetAll().Count();

        DataReadDto data = new DataReadDto
        {
            TotalUsers = users,
            TotalProducts = products,
            TotalOrders = orders,
            TotalSell = sell.Sum(o => o.TotalPrice)
        };

        return data;
    }
}
