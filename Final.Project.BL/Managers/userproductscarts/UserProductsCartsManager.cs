
using Final.Project.DAL;

namespace Final.Project.BL;

public class UserProductsCartsManager : IUserProductsCartsManager
{
    private readonly IUnitOfWork _unitOfWork;
    //test by abdo
    public UserProductsCartsManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public string AddProductToCart(productToAddToCartDto product, string userId)
    {
        try
        {
            string status = "Product Added";// we will use it in controller to send the message to user
            //check if product exist in user cart 
            UserProductsCart? productFromDB = _unitOfWork.UserProdutsCartRepo.GetByCompositeId(product.ProductId, userId);
            if (productFromDB is null) // if not exist ==>  add this product in user cart
            {
                //check the quantity is > 10  ===> make the quantity 10 it is our limit
                if (product.Quantity > 10)
                {
                    product.Quantity = 10;
                    status = " Product Added only 10 pieces Max";
                }
                var productToAddToCart = new UserProductsCart
                {
                    ProductId = product.ProductId,
                    UserId = userId,
                    Quantity = product.Quantity
                };
                _unitOfWork.UserProdutsCartRepo.Add(productToAddToCart);
            }
            else // if product exist in userCart before , edit the quantity only 
            {
                productFromDB.Quantity += product.Quantity;
                //check the quantity is > 10  ===> make the quantity 10 it is our limit
                if (productFromDB.Quantity > 10)
                {
                    productFromDB.Quantity = 10;
                    status = "Product Exist in cart so ,Quantity Updated to only 10 pieces Max";
                }
                else
                {
                    status = "Product Exist in cart so ,Quantity Updated";
                }
            }
            _unitOfWork.Savechanges();

            return status;
        }
        catch (Exception)
        {
            return "Please Login Again!";
        }
    }

    public string UpdateProductQuantityInCart(ProductQuantityinCartUpdateDto product, string userId)
    {
        string status = "product Quantity Edited in userCart";// we will use it in controller to send the message to user

        UserProductsCart? productToEdit = _unitOfWork.UserProdutsCartRepo.GetByCompositeId(product.ProductId, userId);

        //check the quantity is > 10  ===> make the quantity 10 it is our limit
        if (product.Quantity > 10)
        {
            product.Quantity = 10;
            status = "product Quantity Edited in userCart to 10 pieces Max";
        }

        productToEdit.Quantity = product.Quantity;
        _unitOfWork.Savechanges();
        return status;
    }



    public void DeleteProductFromCart(int id, string userId)
    {
        UserProductsCart productToRemove = new UserProductsCart
        {
            ProductId = id,
            UserId = userId,
        };
        _unitOfWork.UserProdutsCartRepo.Delete(productToRemove);
        _unitOfWork.Savechanges();

    }



    public IEnumerable<AllProductsReadDto> GetAllUserProductsInCart(string userId)
    {
        IEnumerable<UserProductsCart> ProductsFromDB = _unitOfWork.UserProdutsCartRepo.GetAllProductsByUserId(userId);
        IEnumerable<AllProductsReadDto> products = ProductsFromDB.Select(p => new AllProductsReadDto
        {
            Id = p.ProductId,
            Quantity = p.Quantity,
            Name = p.Product.Name,
            Price = p.Product.Price,
            Discount = p.Product.Discount,
            Image = p.Product.ProductImages.FirstOrDefault()?.ImageUrl ?? ""
        });

        return products;
    }

    public int CartCounter(string userIdFromToken)
    {
        int counter = _unitOfWork.UserProdutsCartRepo.GetCartCounter(userIdFromToken);
        return counter;
    }
}
