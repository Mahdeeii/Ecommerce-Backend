using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public interface IWishListManager
{
    bool AddtoWishList(string userIdFromToken, int productId);
    int GetWishListCount(string userIdFromToken);
    IEnumerable<wishListProductDto> GetWishListProducts(string userIdFromToken);
    IEnumerable<wishListProductIdsDto> GetuserWishListProductIds(string userId);

}
