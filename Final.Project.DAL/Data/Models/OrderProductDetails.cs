using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final.Project.DAL;
//[PrimaryKey("ProductId", "OrderId")] another way to declare the composite primary key
public class OrderProductDetails
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPriceAtThisTime { get; set; }
    public bool IsReviewed { get; set; } = false;
    public Product Product { get; set; } = null!;
    public Order Order { get; set; } = null!;
}
