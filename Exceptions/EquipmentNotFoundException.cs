namespace APBD_Cw1_s29820.Exceptions;

public class EquipmentNotFoundException : BusinessRuleException
{
    public EquipmentNotFoundException()
    {
    }

    public EquipmentNotFoundException(string message) : base(message)
    {
    }

    public EquipmentNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
