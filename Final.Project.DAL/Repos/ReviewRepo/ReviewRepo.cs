

using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;

public class ReviewRepo : GenericRepo<Review>, IReviewRepo
{
    private readonly ECommerceContext _context;

    public ReviewRepo(ECommerceContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Review> GetReviewsByProduct(int productId)
    {
        return _context.Set<Review>().Where(r => r.ProductId == productId).ToList();
    }

    public IEnumerable<Review> GetReviewsWithProductAndUser()
    {
        return _context.Set<Review>()
                .Include(r => r.Product)
                .Include(r => r.User)
                .ToList();
    }

    public Review? GetByCompositeId(int ProductId, string userId)
    {
        return _context.Set<Review>()
                        .Where(r => r.ProductId == ProductId && r.UserId == userId)
                        .FirstOrDefault();

    }
}
