using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services;

public interface IUsersService
{
    Task<IList<User>> GetAllUsers();
    Task<User> AddUser(User user);
    Task<User> GetUser(int userId);
}