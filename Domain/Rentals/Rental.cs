using EquipmentBase = APBD_Cw1_s29820.Domain.Equipment.Equipment;
using APBD_Cw1_s29820.Domain.Users;

namespace APBD_Cw1_s29820.Domain.Rentals;

public class Rental
{
    private static int _nextId = 1;

    public int Id { get; }
    public User User { get; }
    public EquipmentBase Equipment { get; }
    public DateTime BorrowedAt { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnedAt { get; private set; }
    public decimal PenaltyAmount { get; private set; }
    public bool IsReturned => ReturnedAt.HasValue;

    public Rental(User user, EquipmentBase equipment, DateTime borrowedAt, DateTime dueDate)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        Equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));

        if (dueDate < borrowedAt)
        {
            throw new ArgumentException("Due date cannot be earlier than borrowed date.", nameof(dueDate));
        }

        Id = _nextId++;
        BorrowedAt = borrowedAt;
        DueDate = dueDate;
        ReturnedAt = null;
        PenaltyAmount = 0m;
    }

    public void Return(DateTime returnedAt, decimal penaltyAmount)
    {
        if (IsReturned)
        {
            throw new InvalidOperationException("This rental has already been returned.");
        }

        if (returnedAt < BorrowedAt)
        {
            throw new ArgumentException("Return date cannot be earlier than borrowed date.", nameof(returnedAt));
        }

        if (penaltyAmount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(penaltyAmount), "Penalty amount cannot be negative.");
        }

        ReturnedAt = returnedAt;
        PenaltyAmount = penaltyAmount;
    }

    public override string ToString()
    {
        string returnedInfo = IsReturned
            ? $"Returned at: {ReturnedAt:yyyy-MM-dd HH:mm}, Penalty: {PenaltyAmount:C}"
            : "Not returned";

        return $"[{Id}] {User.FullName} -> {Equipment.Name} | Borrowed: {BorrowedAt:yyyy-MM-dd HH:mm} | Due: {DueDate:yyyy-MM-dd HH:mm} | {returnedInfo}";
    }
}