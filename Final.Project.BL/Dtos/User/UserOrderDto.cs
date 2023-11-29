using Final.Project.BL;
using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project;

public class UserOrderDto
{
    public int Id { get; set; }
    public string OrderStatus { get; set; }
    public DateTime? DeliverdDate { get; set; } = null;
    //public string UserId { get; set; } = string.Empty;
    public IEnumerable<UserProductDto> Products { get; set; } = new HashSet<UserProductDto>();

}
