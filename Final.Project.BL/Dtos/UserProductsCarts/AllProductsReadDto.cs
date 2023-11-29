using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class AllProductsReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal PriceAfter => Math.Round(Price - (Price * Discount / 100), 0);
    public string Image { get; set; } = string.Empty;
    public int Quantity { get; set; }


}
