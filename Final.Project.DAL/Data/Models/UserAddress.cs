namespace Final.Project.DAL;
public class UserAddress
{
    public int Id { get; set; }
    public string UserId { get; set; }=string.Empty;
    public User User { get; set; } = null!;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool DefaultAddress { get; set; } = false;
    public IEnumerable<Order> Orders { get; set; } = new HashSet<Order>(); 

}
