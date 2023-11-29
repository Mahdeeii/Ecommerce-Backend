using Final.Project.DAL;

namespace Final.Project.BL;

public class OrderDetailsManager:IOrderDetailsManager
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderDetailsManager(IUnitOfWork unitOfWork)
    {
       _unitOfWork = unitOfWork;
    }

    public IEnumerable<OrderProductDetailsDto> GetTopProducts()
    {
        IEnumerable<OrderProductDetails> orderProductsFromDb = _unitOfWork.OrdersDetailsRepo.GetTopProducts();

        IEnumerable<OrderProductDetailsDto> productDtos = orderProductsFromDb
            .Select(p => new OrderProductDetailsDto
            {
                Id = p.Product.Id,
                Name = p.Product.Name,
                Price = p.Product.Price,
                Discount = p.Product.Discount,
                AvgRating = p.Product.Reviews.Any() ? (decimal)p.Product.Reviews.Average(r => r.Rating) : 0,
                ReviewCount = p.Product.Reviews.Count(),
                Image=p.Product.ProductImages.FirstOrDefault()?.ImageUrl??""
            });
        return productDtos;
    }

}
