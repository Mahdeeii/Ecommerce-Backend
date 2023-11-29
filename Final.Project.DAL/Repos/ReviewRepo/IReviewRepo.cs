

namespace Final.Project.DAL;

public interface IReviewRepo : IGenericRepo<Review>
{
    IEnumerable<Review> GetReviewsByProduct(int productId);
    IEnumerable<Review> GetReviewsWithProductAndUser();
    Review GetByCompositeId(int ProductID, string userID);
}
