using System.Reflection.Metadata.Ecma335;

namespace Final.Project.DAL;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<Product> Products { get; set; }= new HashSet<Product>();

}
