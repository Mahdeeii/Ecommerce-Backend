using Final.Project.DAL;

namespace Final.Project.BL;

public class ReviewReadDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public string FormattedCreationDate => CreationDate.ToString("dd-MM-yyyy");
    public int Rating { get; set; }
}
