namespace APBD_Cw1_s29820.Services;

public interface IPenaltyPolicy
{
    decimal CalculatePenalty(DateTime dueDate, DateTime returnedAt);
}
