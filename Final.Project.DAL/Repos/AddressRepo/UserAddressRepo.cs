using Microsoft.EntityFrameworkCore;

namespace Final.Project.DAL;
public class UserAddressRepo : GenericRepo<UserAddress>, IUserAddressRepo
{
    private readonly ECommerceContext _context;

    public UserAddressRepo(ECommerceContext context) : base(context)
    {
        _context = context;
    }

    public void deleteByUId(string uId)
    {
        _context.Set<UserAddress>().Where(u => u.UserId == uId).ExecuteDelete();
    }

    public IEnumerable<UserAddress> GetAllUserAddresses(string userIdFromToken)
    {
        return _context.Set<UserAddress>()
                .Where(u => u.UserId == userIdFromToken);
    }

    public void ResetDefaultAddress(string userIdFromToken)
    {
        _context.Set<UserAddress>()
            .Where(u => u.UserId == userIdFromToken)
            .ExecuteUpdate(setters =>
                setters.SetProperty(u => u.DefaultAddress, false));
    }
}
