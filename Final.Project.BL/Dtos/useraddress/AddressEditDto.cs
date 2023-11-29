using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.Bl;

public class AddressEditDto
{
    public int Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool DefaultAddress { get; set; } = false;
}
