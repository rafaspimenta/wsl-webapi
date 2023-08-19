using CloudCustomers.API.Models;

namespace CloudCustomers.API.Services;

public class UsersService : IUsersService
{
    private readonly Random _random;
    private readonly IList<User> _users;

    public UsersService()
    {
        _random = new Random(999);
        _users = new List<User>();
    }
    public async Task<User> AddUser(User user)
    {
        var rdi = _random.Next();
        user.Id = rdi;

        _users.Add(user);

        return await Task.FromResult(user);
    }

    public async Task<IList<User>> GetAllUsers()
    {
        return await Task.FromResult(_users);
    }

    public async Task<User> GetUser(int userId)
    {
        return await Task.FromResult(_users.FirstOrDefault(x => x.Id == userId));
    }
}
