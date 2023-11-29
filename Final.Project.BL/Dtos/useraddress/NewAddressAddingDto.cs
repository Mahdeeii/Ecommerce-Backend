using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class NewAddressAddingDto
{
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public bool DefaultAddress { get; set; } = false;

}
