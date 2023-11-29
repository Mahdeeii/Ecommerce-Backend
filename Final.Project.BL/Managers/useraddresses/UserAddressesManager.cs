using Final.Project.Bl;
using Final.Project.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class UserAddressesManager : IUserAddressesManager
{
    private readonly IUnitOfWork _unitOfWork;

    public UserAddressesManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<AllUserAddressesReadDto> GetAllUserAddresses(string userIdFromToken)
    {
        var AddressesFromDb = _unitOfWork.UserAddressRepo
                                .GetAllUserAddresses(userIdFromToken);

        var addresses = AddressesFromDb.Select(a => new AllUserAddressesReadDto
        {
            Id = a.Id,
            City = a.City,
            Street = a.Street,
            Phone = a.Phone,
            DefaultAddress = a.DefaultAddress
        });

        return addresses;
    }

    public void AddNewAddress(string userIdFromToken,NewAddressAddingDto newAddress)
    {
        UserAddress address = new UserAddress
        {
            UserId= userIdFromToken,
            City=newAddress.City,
            Street=newAddress.Street,
            Phone=newAddress.Phone,
            DefaultAddress=newAddress.DefaultAddress

        };
        if(newAddress.DefaultAddress==true)
        {
            _unitOfWork.UserAddressRepo.ResetDefaultAddress(userIdFromToken);
        }
        _unitOfWork.UserAddressRepo.Add(address);
        _unitOfWork.Savechanges();
    }

    public void EditAddress(string userIdFromToken, AddressEditDto address)
    {

        if (address.DefaultAddress == true)
        {
            _unitOfWork.UserAddressRepo.ResetDefaultAddress(userIdFromToken);
        }

        UserAddress addressToEdit = _unitOfWork.UserAddressRepo.GetById(address.Id)!;
        addressToEdit.City = address.City;
        addressToEdit.Street = address.Street;
        addressToEdit.Phone = address.Phone;
        addressToEdit.DefaultAddress = address.DefaultAddress;
        _unitOfWork.Savechanges();
    }

    public void Delete(int addressId)
    {
        UserAddress addressToDelete= _unitOfWork.UserAddressRepo.GetById(addressId)!;
        _unitOfWork.UserAddressRepo.Delete(addressToDelete);
        _unitOfWork.Savechanges();
    }

    public void SetDefaultAddress(string userIdFromToken, int addressId)
    {
        _unitOfWork.UserAddressRepo.ResetDefaultAddress(userIdFromToken);
        UserAddress addressToEdit = _unitOfWork.UserAddressRepo.GetById(addressId)!;
        addressToEdit.DefaultAddress = true;
        _unitOfWork.Savechanges();

    }

    public AllUserAddressesReadDto GetAddressById(int id)
    {
        UserAddress userAddressFromDB= _unitOfWork.UserAddressRepo.GetById(id);
        AllUserAddressesReadDto userAddress = new AllUserAddressesReadDto
        {
            Id = userAddressFromDB.Id,
            City = userAddressFromDB.City,
            Street = userAddressFromDB.Street,
            DefaultAddress = userAddressFromDB.DefaultAddress,
            Phone = userAddressFromDB.Phone
        };

        return userAddress;
    }

    
}
