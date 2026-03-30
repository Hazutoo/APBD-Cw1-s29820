using APBD_Cw1_s29820.Domain.Enums;
using APBD_Cw1_s29820.Domain.Users;

namespace APBD_Cw1_s29820.Services;

public class UserLimitPolicy : IUserLimitPolicy
{
    public int GetMaxActiveRentals(User user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return user.UserType switch
        {
            UserType.Student => 2,
            UserType.Employee => 5,
            _ => throw new ArgumentOutOfRangeException(nameof(user), "Unsupported user type.")
        };
    }
}
