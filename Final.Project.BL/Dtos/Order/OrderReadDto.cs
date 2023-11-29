using Final.Project.DAL;

namespace Final.Project.BL;

public class OrderReadDto
{
    public int Id { get; set; }
    public string OrderStatus { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
    public string FormattedCreationDate => OrderDate.ToString("dd-MM-yyyy");
}

