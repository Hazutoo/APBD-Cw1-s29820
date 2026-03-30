namespace APBD_Cw1_s29820.Exceptions;

public class RentalNotFoundException : BusinessRuleException
{
    public RentalNotFoundException()
    {
    }

    public RentalNotFoundException(string message) : base(message)
    {
    }

    public RentalNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
