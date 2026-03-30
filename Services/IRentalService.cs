using APBD_Cw1_s29820.Domain.Rentals;

namespace APBD_Cw1_s29820.Services;

public interface IRentalService
{
    Rental RentEquipment(int userId, int equipmentId, int days);
    void ReturnEquipment(int rentalId, DateTime returnedAt);
    IReadOnlyCollection<Rental> GetAllRentals();
    IReadOnlyCollection<Rental> GetActiveRentalsForUser(int userId);
    IReadOnlyCollection<Rental> GetOverdueRentals(DateTime now);
}
