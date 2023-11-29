using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class OrdersManager : IOrdersManager
{
    private readonly IUnitOfWork _unitOfWork;

    public OrdersManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region Add Order

    public void AddNewOrder(string userId, int addressId)
    {
        //1-Add new order in order table
        Order newOrder = new Order
        {
            OrderStatus = OrderStatus.Pending,
            OrderDate = DateTime.Now,
            UserId = userId,
            UserAddressId = addressId
        };
        _unitOfWork.OrderRepo.Add(newOrder);
        _unitOfWork.Savechanges();
        //2-we need the orderId of the new order to use it in orderdetails table
        //so lets get that 
        int LastOrderId = _unitOfWork.OrderRepo.GetLastUserOrder(userId);

        //3-transfer products from cart to order 
        IEnumerable<UserProductsCart> productsFromCart = _unitOfWork.UserProdutsCartRepo.GetAllProductsByUserId(userId);

        //4-we need  productId,OrderId,Quantity for each row in orderDetails table
        //so lets extract this data from products we got from cart and insert them
        // to userProductsDetails Table

        var orderProducts = productsFromCart.Select(p => new OrderProductDetails
        {
            OrderId = LastOrderId,
            ProductId = p.ProductId,
            Quantity = p.Quantity,
            ProductPriceAtThisTime=p.Product.Price*(1- (p.Product.Discount/100))
        });

        _unitOfWork.OrdersDetailsRepo.AddRange(orderProducts);

        //5-Make The UserCart Empty

        _unitOfWork.UserProdutsCartRepo.DeleteAllProductsFromUserCart(userId);
        _unitOfWork.Savechanges();
    }

    #endregion

    #region Get all Orders

    public IEnumerable<OrderReadDto> GetAllOrders()
    {
        var orderFromDb = _unitOfWork.OrderRepo.GetOrdersWithData();
        if(orderFromDb is null)
        {
            return null;
        }
        var orderReadDto = orderFromDb
            .Select(o => new OrderReadDto
            {
                Id = o.Id,
                OrderStatus = Enum.GetName(typeof(OrderStatus), o.OrderStatus),
                OrderDate = o.OrderDate,
                UserId = o.User.Id,
                UserName = (o.User.FName + " " + o.User.LName),
                ProductCount = o.OrdersProductDetails.Count(),
                TotalPrice = o.OrdersProductDetails.Sum(op => Math.Round( (op.Product.Price - (op.Product.Price * (op.Product.Discount/100))) * op.Quantity, 0)),
            });

        return orderReadDto;
    }

    #endregion

    #region Get Order Details

    public OrderDetailsDto GetOrderDetails(int OrderId)
    {
        Order order = _unitOfWork.OrderRepo.GetOrderWithProducts(OrderId);

        if(order is null)
        {
            return null;
        }

        OrderDetailsDto orderDetails = new OrderDetailsDto
        {
            Id = order.Id,
            OrderStatus = Enum.GetName(typeof(OrderStatus), order.OrderStatus),
            OrderDate = order.OrderDate,
            DeliverdDate = order.DeliverdDate,
            UserId = order.User.Id,
            UserName = (order.User.FName + " " + order.User.LName),
            ProductCount = order.OrdersProductDetails.Count(),
            TotalPrice = order.OrdersProductDetails.Sum(op => Math.Round((op.Product.Price - (op.Product.Price * (op.Product.Discount / 100))) * op.Quantity, 0)),
            ProductsInOrder = order.OrdersProductDetails.Select(op => new ProductsInOrder
            {
                Quantity = op.Quantity,
                ProductName = op.Product.Name,
                ProductPrice = op.Product.Price,
                ProductImage = op.Product.ProductImages.FirstOrDefault()?.ImageUrl??"",
                Discount = op.Product.Discount,
                ProductId = op.ProductId
            }),
            City = order.UserAddress.City,
            Street = order.UserAddress.Street,
            Phone = order.UserAddress.Phone

        };

        return orderDetails;
    }

    #endregion

    #region Update Order

    public bool UpdateOrder(OrderEditDto orderEdit)
    {
        var order = _unitOfWork.OrderRepo.GetById(orderEdit.Id);
        if (order is null)
        {
            return false;
        }

        order.OrderStatus = (OrderStatus)Enum.ToObject(typeof(OrderStatus), orderEdit.OrderStatus);

        if ((OrderStatus)Enum.ToObject(typeof(OrderStatus), orderEdit.OrderStatus) == OrderStatus.Delivered)
        {
            order.DeliverdDate = DateTime.Now;
        }

        return _unitOfWork.Savechanges() > 0;
    }

    #endregion

    #region Delete Order

    public bool DeleteOrder(int Id)
    {
        var order = _unitOfWork.OrderRepo.GetById(Id);
        if (order is null)
        {
            return false;
        }

        _unitOfWork.OrderRepo.Delete(order);
        return _unitOfWork.Savechanges() > 0;
    }

    #endregion
}
