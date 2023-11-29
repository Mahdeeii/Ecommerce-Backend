namespace Final.Project.BL;

public class ProductReadPaginationDto
{
    public IEnumerable<ProductReadDto> Products { get; set; } = new List<ProductReadDto>();
    public int TotalCount { get; set; }
}


