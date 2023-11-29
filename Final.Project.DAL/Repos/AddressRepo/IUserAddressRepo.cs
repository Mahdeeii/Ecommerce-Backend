namespace Final.Project.DAL;
public interface IUserAddressRepo : IGenericRepo<UserAddress>
{
    public void deleteByUId(string uId);
    IEnumerable<UserAddress> GetAllUserAddresses(string userIdFromToken);
    void ResetDefaultAddress(string userIdFromToken);
}
