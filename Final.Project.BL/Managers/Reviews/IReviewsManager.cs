namespace Final.Project.BL;

public interface IReviewsManager
{
    void AddReview(string userId, int productId, string comment, int rating);

    public double GetAverageRating(int productId);

    IEnumerable<ReviewReadDto> GetAllReviews();

    bool DeleteReview(ReviewKeyDto reviewKey);
    bool AddReviewToProduct(string userIdFromToken, AddReviewDto review);
}
