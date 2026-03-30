namespace APBD_Cw1_s29820.Exceptions;

public class UserNotFoundException : BusinessRuleException
{
    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string message) : base(message)
    {
    }

    public UserNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
