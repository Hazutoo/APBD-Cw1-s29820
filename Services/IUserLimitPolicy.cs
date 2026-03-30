using APBD_Cw1_s29820.Domain.Users;

namespace APBD_Cw1_s29820.Services;

public interface IUserLimitPolicy
{
    int GetMaxActiveRentals(User user);
}
