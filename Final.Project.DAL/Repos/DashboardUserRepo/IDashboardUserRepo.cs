using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.DAL;

public interface IDashboardUserRepo : IGenericRepo<User>
{
    User GetByUserId(string userId);
}
