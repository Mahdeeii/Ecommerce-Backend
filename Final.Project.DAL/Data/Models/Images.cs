using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.DAL.Data.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }=string.Empty;
        public Product Product { get; set; }
    }
}
