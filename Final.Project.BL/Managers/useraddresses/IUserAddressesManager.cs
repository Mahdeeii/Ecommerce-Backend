using Final.Project.Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public interface IUserAddressesManager
{
    void AddNewAddress(string userId, NewAddressAddingDto newAddress);
    void Delete(int addressId);
    void EditAddress(string userIdFromToken, AddressEditDto address);
    AllUserAddressesReadDto GetAddressById(int id);
    IEnumerable<AllUserAddressesReadDto> GetAllUserAddresses(string userIdFromToken);
    void SetDefaultAddress(string userIdFromToken, int addressId);
}
