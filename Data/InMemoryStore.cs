using EquipmentBase = APBD_Cw1_s29820.Domain.Equipment.Equipment;
using APBD_Cw1_s29820.Domain.Rentals;
using APBD_Cw1_s29820.Domain.Users;

namespace APBD_Cw1_s29820.Data;

public class InMemoryStore
{
    public List<User> Users { get; } = new();
    public List<EquipmentBase> EquipmentItems { get; } = new();
    public List<Rental> Rentals { get; } = new();
}
