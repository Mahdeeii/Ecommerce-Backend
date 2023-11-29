namespace Final.Project.BL;

public class ProductFilterationPaginationResultDto
{
   public IEnumerable<ProductFilterationResultDto> filteredProducts { get; set; } = new List<ProductFilterationResultDto>();
   public int TotalCount { get; set; }
}
