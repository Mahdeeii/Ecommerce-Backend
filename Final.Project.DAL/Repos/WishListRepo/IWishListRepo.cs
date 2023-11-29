using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.DAL;

public interface IWishListRepo:IGenericRepo<WishList>
{
    void AddToWishList(WishList wishList);
    WishList CheckItExistInWishList(string userIdFromToken, int productId);
    int count(string userIdFromToken);
    IEnumerable<WishList> GetuserWishList(string userId);
    IEnumerable<WishList> GetuserWishListProductIds(string userId);

}
