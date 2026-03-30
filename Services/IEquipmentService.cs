using EquipmentBase = APBD_Cw1_s29820.Domain.Equipment.Equipment;

namespace APBD_Cw1_s29820.Services;

public interface IEquipmentService
{
    void AddEquipment(EquipmentBase item);
    IReadOnlyCollection<EquipmentBase> GetAllEquipment();
    IReadOnlyCollection<EquipmentBase> GetAvailableEquipment();
    EquipmentBase GetById(int id);
    void MarkAsUnavailable(int equipmentId);
}
