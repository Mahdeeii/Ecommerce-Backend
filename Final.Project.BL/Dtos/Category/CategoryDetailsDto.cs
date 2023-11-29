

namespace Final.Project.BL;

public class CategoryDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<ProductChildDto> Products { get; set; } = new HashSet<ProductChildDto>();
}
