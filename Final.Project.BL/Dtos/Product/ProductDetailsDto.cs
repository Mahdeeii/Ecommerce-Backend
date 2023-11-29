

using Final.Project.DAL;
using System.ComponentModel;

namespace Final.Project.BL;

public class ProductDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public IEnumerable<string> Images { get; set; } = new HashSet<string>();
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal Discount { get; set; }
    public decimal PriceAfter => Math.Round(Price - (Price * Discount / 100), 0);
    public IEnumerable<ReviewDto> Reviews { get; set; } = new HashSet<ReviewDto>();
    public decimal AvgRating { get; set; }
    public decimal AvgRatingRounded => Math.Round(AvgRating, 1);
    public int ReviewCount { get; set; }


}
