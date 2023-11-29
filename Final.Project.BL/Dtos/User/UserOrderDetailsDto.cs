using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class UserOrderDetailsDto
{
    public IEnumerable<UserOrderProductsDetailsDto>? OrderProducts { get; set; } = null;
    public UserOrderAddressDetailsDto? OrderAddress { get; set; } = null;
    public bool IsOrderDelieverd { get; set; } = false;
}

public class UserOrderProductsDetailsDto
{
    public int product_Id { get; set; }
    public string title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal PriceAfter => Math.Round(Price - (Price * Discount / 100), 0);
    public decimal ProductPriceAtThisTime { get; set; }
    public bool IsReviewed { get; set; } = false;

}
public class UserOrderAddressDetailsDto
{
    public int? Id { get; set; }
    public string? City { get; set; } = string.Empty;
    public string? Street { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public bool? DefaultAddress { get; set; } = false;
}
