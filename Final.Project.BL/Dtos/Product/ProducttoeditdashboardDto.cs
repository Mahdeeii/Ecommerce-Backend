using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class ProducttoeditdashboardDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public IEnumerable<string> Image { get; set; } = new HashSet<string>();
    public int CategoryId { get; set; }
    public decimal Discount { get; set; }
   
}
