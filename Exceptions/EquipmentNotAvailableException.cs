namespace APBD_Cw1_s29820.Exceptions;

public class EquipmentNotAvailableException : BusinessRuleException
{
    public EquipmentNotAvailableException()
    {
    }

    public EquipmentNotAvailableException(string message) : base(message)
    {
    }

    public EquipmentNotAvailableException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
