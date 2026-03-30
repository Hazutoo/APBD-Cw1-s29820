using APBD_Cw1_s29820.Domain.Users;

namespace APBD_Cw1_s29820.Services;

public interface IUserService
{
    void AddUser(User user);
    IReadOnlyCollection<User> GetAllUsers();
    User GetById(int id);
}
