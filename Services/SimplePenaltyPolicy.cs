namespace APBD_Cw1_s29820.Services;

public class SimplePenaltyPolicy : IPenaltyPolicy
{
    private const decimal PenaltyPerStartedDay = 10m;

    public decimal CalculatePenalty(DateTime dueDate, DateTime returnedAt)
    {
        if (returnedAt <= dueDate)
        {
            return 0m;
        }

        TimeSpan delay = returnedAt - dueDate;
        decimal startedDaysLate = (decimal)Math.Ceiling(delay.TotalDays);

        return startedDaysLate * PenaltyPerStartedDay;
    }
}
