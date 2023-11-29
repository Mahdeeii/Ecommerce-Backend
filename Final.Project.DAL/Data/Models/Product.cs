using Final.Project.DAL.Data.Models;
using System.Drawing;

namespace Final.Project.DAL;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<ProductImages> ProductImages { get; set; } = new List<ProductImages>();
    public string Model { get; set; } = string.Empty;
    public int CategoryID { get; set; }
    public Category Category { get; set; } = null!;
    public IEnumerable<UserProductsCart> UsersProductsCarts { get; set; } = new HashSet<UserProductsCart>();
    public IEnumerable<OrderProductDetails> OrdersProductDetails { get; set; } = new HashSet<OrderProductDetails>();
    public IEnumerable<Review> Reviews { get; set; } = new HashSet<Review>();
    public IEnumerable<WishList> WishLists { get; set; } = new HashSet<WishList>();

}

