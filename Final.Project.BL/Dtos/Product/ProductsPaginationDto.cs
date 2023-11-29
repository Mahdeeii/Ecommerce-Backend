

namespace Final.Project.BL;

public class ProductPaginationDto
{
   public IEnumerable<ProductChildDto> products { get; set; }=new List<ProductChildDto>();
    public int TotalCount { get; set; }
}
