namespace APBD_Cw1_s29820.Exceptions;

public class UserLimitExceededException : BusinessRuleException
{
    public UserLimitExceededException()
    {
    }

    public UserLimitExceededException(string message) : base(message)
    {
    }

    public UserLimitExceededException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
