using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.DAL;

public class WishList
{
    public string UserId { get; set; }
    public int ProductId { get; set; }
    public User User { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
