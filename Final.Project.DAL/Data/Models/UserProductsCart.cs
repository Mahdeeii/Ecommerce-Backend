namespace Final.Project.DAL;
public class UserProductsCart
{
    public int ProductId { get; set; }
    public string UserId { get; set; }=string.Empty; 
    public int Quantity { get; set; }
    public Product Product { get; set; } = null!;
    public User User { get; set; } = null!;


}
