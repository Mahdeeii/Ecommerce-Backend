using Final.Project.Bl;
using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public interface IUsersManager
{
    //UserReadDto GetUserReadDto(string id);
    UserReadDto GetUserReadDto(User user);

    UserOrderDetailsDto GetUserOrderDetailsDto(int id);
    //bool Edit( UserUpdateDto updateDto,string id);
    bool Edit(UserUpdateDto updateDto, User user);

    bool delete(string id);
    IEnumerable<UserOrderDto> GetUserOrdersDto(string id);

}
