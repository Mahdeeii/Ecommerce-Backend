using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class AddReviewDto
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}
