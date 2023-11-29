using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.DAL;

public  class WishListRepo: GenericRepo<WishList>, IWishListRepo
{
    private readonly ECommerceContext _context;

    public WishListRepo( ECommerceContext context):base(context)
    {
        _context = context;
    }

    public void AddToWishList(WishList wishList)
    {
        _context.Set<WishList>().Add(wishList);

    }

   

    public WishList? CheckItExistInWishList(string userIdFromToken, int productId)
    {
        return _context.Set<WishList>().FirstOrDefault(x => x.ProductId == productId&& x.UserId == userIdFromToken);
    }

    public int count(string userIdFromToken)
    {
        return _context.Set<WishList>()
                        .Where(u => u.UserId == userIdFromToken)
                        .Count();
    }

    public IEnumerable<WishList> GetuserWishList(string userId)
    {
        return _context.Set<WishList>()
                .Include(u => u.Product)
                    .ThenInclude(u=>u.Reviews)
                .Include(u => u.Product)
                    .ThenInclude(u => u.ProductImages)
                .Where(u => u.UserId == userId);

    }

    public IEnumerable<WishList> GetuserWishListProductIds(string userId)
    {
        return _context.Set<WishList>()
                .Include(u => u.Product)
                .Where(u => u.UserId == userId);

    }


}
