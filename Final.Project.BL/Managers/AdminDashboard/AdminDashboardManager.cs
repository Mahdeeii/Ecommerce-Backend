using Final.Project.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final.Project.BL;

public class AdminDashboardManager : IAdminDashboardManager
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminDashboardManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public bool DeleteUser(string userId)
    {
        User? user = _unitOfWork.UserRepo.GetById(userId);

        if (user is null) { return false; }

        _unitOfWork.UserAddressRepo.deleteByUId(user.Id);

        _unitOfWork.UserRepo.Delete(user);
        _unitOfWork.Savechanges();
        return true;
    }

    public IEnumerable<UserDashboardReadDto> GetAllUsers()
    {
        IEnumerable<User> UsersFromDB = _unitOfWork.DashboardUserRepo.GetAll();
        IEnumerable<UserDashboardReadDto> AllUsersDetails = UsersFromDB.Select(u => new UserDashboardReadDto
        {
            Id = u.Id,
            FName = u.FName,
            LName = u.LName,
            Email = u.Email,
            Role = u.Role.ToString()
        });
        return AllUsersDetails;
    }

    public UserDashboardReadDto GetUserById(string userId)
    {
        User userFromDB = _unitOfWork.DashboardUserRepo.GetByUserId(userId);
        UserDashboardReadDto user = new UserDashboardReadDto
        {
            Id = userFromDB.Id,
            Email = userFromDB.Email,
            FName = userFromDB.FName,
            LName = userFromDB.LName,
            Role = userFromDB.Role.ToString(),
            Orders = userFromDB.Orders.Select(o => new UserDashboardOrderDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                OrderStatus = Enum.GetName(typeof(OrderStatus), o.OrderStatus),
                DeliverdDate = o.DeliverdDate,
            }),
            UserAddresses = userFromDB.UserAddresses.Select(o => new UserDashboardAddressDto
            {
                Id = o.Id,
                City = o.City,
                Street = o.Street,
                Phone = o.Phone,
                DefaultAddress = o.DefaultAddress,
            })
        };
        return user;
    }


}
