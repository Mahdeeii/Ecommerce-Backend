using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class UserDashboardReadDto
{
    public string Id { get; set; } = string.Empty;
    public string FName { get; set; } = string.Empty;
    public string LName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public IEnumerable<UserDashboardOrderDto> Orders { get; set; } = new HashSet<UserDashboardOrderDto>();
    public IEnumerable<UserDashboardAddressDto> UserAddresses { get; set; } = new HashSet<UserDashboardAddressDto>();
}

public class UserDashboardAddressDto
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool DefaultAddress { get; set; } = false;
}

public class UserDashboardOrderDto
{
    public int Id { get; set; }
    public string OrderStatus { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? DeliverdDate { get; set; } = null;

}