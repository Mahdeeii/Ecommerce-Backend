using Final.Project.DAL;

namespace Final.Project.BL;

public class CategoryReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int NoOfProducts { get; set; }
}
