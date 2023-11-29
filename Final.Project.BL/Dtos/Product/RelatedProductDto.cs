
namespace Final.Project.BL;

public class RelatedProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ReviewCount { get; set; }

    public decimal Discount { get; set; }
    public decimal PriceAfter => Math.Round(Price - (Price * Discount / 100), 0);
    public decimal AvgRating { get; set; }
    public decimal AvgRatingRounded => Math.Round(AvgRating, 1);
}
