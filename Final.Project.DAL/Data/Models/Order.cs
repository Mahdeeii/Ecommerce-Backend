using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Project.DAL;
public class Order
{
    public int Id { get; set; }
    
    public OrderStatus OrderStatus  { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? DeliverdDate { get; set; } = null;
    public string? UserId { get; set; }=string.Empty; 
    public User? User { get; set; } 
    public int? UserAddressId { get; set; }
    public UserAddress? UserAddress { get; set; } 
    public IEnumerable<OrderProductDetails> OrdersProductDetails { get; set; } = new HashSet<OrderProductDetails>();
    public IEnumerable<Review> Reviews { get; set; } = new HashSet<Review>();

}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}