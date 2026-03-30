using APBD_Cw1_s29820.Data;
using APBD_Cw1_s29820.Domain.Enums;
using APBD_Cw1_s29820.Domain.Rentals;
using APBD_Cw1_s29820.Domain.Users;
using APBD_Cw1_s29820.Exceptions;
using EquipmentBase = APBD_Cw1_s29820.Domain.Equipment.Equipment;

namespace APBD_Cw1_s29820.Services;

public class RentalService : IRentalService
{
    private readonly InMemoryStore _store;
    private readonly IUserLimitPolicy _userLimitPolicy;
    private readonly IPenaltyPolicy _penaltyPolicy;

    public RentalService(
        InMemoryStore store,
        IUserLimitPolicy userLimitPolicy,
        IPenaltyPolicy penaltyPolicy)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _userLimitPolicy = userLimitPolicy ?? throw new ArgumentNullException(nameof(userLimitPolicy));
        _penaltyPolicy = penaltyPolicy ?? throw new ArgumentNullException(nameof(penaltyPolicy));
    }

    public Rental RentEquipment(int userId, int equipmentId, int days)
    {
        if (days <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(days), "Rental days must be greater than 0.");
        }

        User user = GetUserById(userId);
        EquipmentBase equipment = GetEquipmentById(equipmentId);

        if (equipment.Status != EquipmentStatus.Available)
        {
            throw new EquipmentNotAvailableException(
                $"Equipment with id {equipmentId} is not available.");
        }

        int activeRentalsCount = _store.Rentals.Count(r => r.User.Id == userId && !r.IsReturned);
        int maxAllowed = _userLimitPolicy.GetMaxActiveRentals(user);

        if (activeRentalsCount >= maxAllowed)
        {
            throw new UserLimitExceededException(
                $"User with id {userId} has reached the active rentals limit ({maxAllowed}).");
        }

        DateTime borrowedAt = DateTime.Now;
        DateTime dueDate = borrowedAt.AddDays(days);

        Rental rental = new Rental(user, equipment, borrowedAt, dueDate);

        _store.Rentals.Add(rental);
        equipment.MarkAsRented();

        return rental;
    }

    public void ReturnEquipment(int rentalId, DateTime returnedAt)
    {
        Rental rental = _store.Rentals.FirstOrDefault(r => r.Id == rentalId)
                        ?? throw new RentalNotFoundException($"Rental with id {rentalId} was not found.");

        if (rental.IsReturned)
        {
            throw new BusinessRuleException($"Rental with id {rentalId} has already been returned.");
        }

        decimal penaltyAmount = _penaltyPolicy.CalculatePenalty(rental.DueDate, returnedAt);

        rental.Return(returnedAt, penaltyAmount);
        rental.Equipment.MarkAsAvailable();
    }

    public IReadOnlyCollection<Rental> GetAllRentals()
    {
        return _store.Rentals.AsReadOnly();
    }

    public IReadOnlyCollection<Rental> GetActiveRentalsForUser(int userId)
    {
        GetUserById(userId);

        return _store.Rentals
            .Where(r => r.User.Id == userId && !r.IsReturned)
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyCollection<Rental> GetOverdueRentals(DateTime now)
    {
        return _store.Rentals
            .Where(r => !r.IsReturned && r.DueDate < now)
            .ToList()
            .AsReadOnly();
    }

    private User GetUserById(int userId)
    {
        return _store.Users.FirstOrDefault(u => u.Id == userId)
               ?? throw new UserNotFoundException($"User with id {userId} was not found.");
    }

    private EquipmentBase GetEquipmentById(int equipmentId)
    {
        return _store.EquipmentItems.FirstOrDefault(e => e.Id == equipmentId)
               ?? throw new EquipmentNotFoundException($"Equipment with id {equipmentId} was not found.");
    }
}
