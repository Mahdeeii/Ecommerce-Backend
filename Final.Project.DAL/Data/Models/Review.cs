

namespace Final.Project.DAL;

public class Review
{
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.Now;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = null!;
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

   
}
