using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class WishListManager : IWishListManager
{
    private readonly IUnitOfWork _unitOfWork;

    public WishListManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public bool AddtoWishList(string userIdFromToken, int productId)
    {
        
        //check if it exist 
        WishList? checkItExist = _unitOfWork.WishListRepo.CheckItExistInWishList(userIdFromToken, productId);
        _unitOfWork.Savechanges();
        if (checkItExist is null)
        {
            WishList wishlist = new WishList
            {
                UserId = userIdFromToken,
                ProductId = productId
            };
         _unitOfWork.WishListRepo.AddToWishList(wishlist);

        }
        else
        {
            _unitOfWork.WishListRepo.Delete(checkItExist);
        }
        return _unitOfWork.Savechanges() > 0;
    }

    public int GetWishListCount(string userIdFromToken)
    {
       int counter= _unitOfWork.WishListRepo.count(userIdFromToken);
        return counter;
    }

    public IEnumerable<wishListProductDto> GetWishListProducts(string userIdFromToken)
    {
        IEnumerable<WishList> wishListFromDB = _unitOfWork.WishListRepo.GetuserWishList(userIdFromToken);
        IEnumerable<wishListProductDto> wishLishProducts = wishListFromDB.Select(p => new wishListProductDto
        {
            Id = p.Product.Id,
            Name = p.Product.Name,
            Price = p.Product.Price,
            Image = p.Product.ProductImages.FirstOrDefault()?.ImageUrl??"",
            Discount = p.Product.Discount,
            AvgRating = p.Product.Reviews.Any() ? (decimal)p.Product.Reviews.Average(r => r.Rating) : 0,
            ReviewCount = p.Product.Reviews.Count()
        });
        return wishLishProducts;
    }

    public IEnumerable<wishListProductIdsDto> GetuserWishListProductIds(string userId)
    {
        var wishList = _unitOfWork.WishListRepo.GetuserWishListProductIds(userId);
        IEnumerable<wishListProductIdsDto> wishlistProductsId = wishList.Select(w => new wishListProductIdsDto
        {
            Id = w.ProductId
        });
        return wishlistProductsId;
    }

}
