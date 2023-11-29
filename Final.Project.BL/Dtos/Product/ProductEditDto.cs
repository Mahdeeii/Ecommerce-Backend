using Microsoft.AspNetCore.Http;

namespace Final.Project.BL;

public class ProductEditDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public IEnumerable<string> ImagesURLs { get; set; } = new HashSet<string>();
    public string Model { get; set; } = string.Empty;
    public decimal Discount { get; set; }

    public int CategoryID { get; set; }
    //public string StockStatus { get; set; } = string.Empty;
}
