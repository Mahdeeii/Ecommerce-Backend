using Final.Project.DAL;
using System.Collections.Generic;

namespace Final.Project.BL;

public class UsersManager : IUsersManager
{
    private readonly IUnitOfWork _unitOfWork;
    

    public UsersManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region delete user's account
    public bool delete(string id)
    {
        User? user = _unitOfWork.UserRepo.GetById(id);
        
        _unitOfWork.UserAddressRepo.deleteByUId(user.Id);

        _unitOfWork.UserRepo.Delete(user);

        _unitOfWork.Savechanges();

        return true;
    }
    #endregion

   
    public bool Edit(UserUpdateDto updateDto, User user)
    {
        user.FName = updateDto.FName;

        user.LName = updateDto.LName;


        _unitOfWork.Savechanges();
        return true;

    }

    #region UserOrderDetails
    public UserOrderDetailsDto? GetUserOrderDetailsDto(int id)
    {

        List<OrderProductDetails> OrderProductDetails = _unitOfWork.UserRepo.GetUsersOrderDetails(id).ToList();
        if (OrderProductDetails == null)
        {
            return null;
        }

        IEnumerable<UserOrderProductsDetailsDto> products = OrderProductDetails.Select(p => new UserOrderProductsDetailsDto
        {
            product_Id = p.ProductId,
            Image = p.Product.ProductImages.FirstOrDefault()?.ImageUrl??"",
            Price = p.Product.Price,
            Discount=p.Product.Discount,
            Quantity = p.Quantity,
            title = p.Product.Name,
            IsReviewed=p.IsReviewed,
            ProductPriceAtThisTime=p.ProductPriceAtThisTime
            
          
        });

        UserOrderAddressDetailsDto orderAddress = new UserOrderAddressDetailsDto
        {
            Id = OrderProductDetails[0].Order.UserAddress?.Id,
            City = OrderProductDetails[0].Order.UserAddress?.City,
            Street = OrderProductDetails[0].Order.UserAddress?.Street,
            Phone = OrderProductDetails[0].Order.UserAddress?.Phone,
            DefaultAddress = OrderProductDetails[0].Order.UserAddress?.DefaultAddress,

        };

        //check order status is delievered or not 
        bool OrderStatus = ((int)OrderProductDetails.FirstOrDefault().Order.OrderStatus)==3;


        UserOrderDetailsDto orderDetails = new UserOrderDetailsDto
        {
            OrderProducts = products,
            OrderAddress = orderAddress,
            IsOrderDelieverd= OrderStatus
        };


        return orderDetails;

    }
    #endregion

    #region UserOrder
    public IEnumerable<UserOrderDto> GetUserOrdersDto(string id)
    {
        IEnumerable<Order>? ordersFromDB = _unitOfWork.UserRepo.GetUserOrders(id);
        if (ordersFromDB == null) { return null; }
        IEnumerable<UserOrderDto> ordersDto= ordersFromDB.Select(order=> new UserOrderDto
        {
            Id = order.Id,
            OrderStatus = Enum.GetName(typeof(OrderStatus), order.OrderStatus),
            DeliverdDate = order.DeliverdDate,
            Products = order.OrdersProductDetails.Select(ip => new UserProductDto
            {
                
                Image = ip.Product.ProductImages.FirstOrDefault()?.ImageUrl??"",
                title = ip.Product.Name,
            }

            )

        });
        return ordersDto.ToList();
    }

    #endregion

    #region View user Profile
    
    public UserReadDto GetUserReadDto(User user)
    {

        return new UserReadDto
        {
            Fname = user.FName,
            Lname = user.LName,
            Email = user.Email
        };

    }

    #endregion
}
