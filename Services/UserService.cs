using APBD_Cw1_s29820.Data;
using APBD_Cw1_s29820.Domain.Users;

namespace APBD_Cw1_s29820.Services;

public class 
    UserService : IUserService
{
    private readonly InMemoryStore _store;

    public UserService(InMemoryStore store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public void AddUser(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _store.Users.Add(user);
    }

    public IReadOnlyCollection<User> GetAllUsers()
    {
        return _store.Users.AsReadOnly();
    }

    public User GetById(int id)
    {
        User? user = _store.Users.FirstOrDefault(u => u.Id == id);

        if (user is null)
        {
            throw new KeyNotFoundException($"User with id {id} was not found.");
        }

        return user;
    }
}
