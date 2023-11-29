using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.DAL;

public class ContactUsRepo:GenericRepo<ContactUs>, IContactUsRepo
{
    private readonly ECommerceContext _context;

    public ContactUsRepo(ECommerceContext context):base(context)
    {
        _context = context;
    }
}
