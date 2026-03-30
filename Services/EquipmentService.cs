using APBD_Cw1_s29820.Data;
using APBD_Cw1_s29820.Domain.Enums;
using EquipmentBase = APBD_Cw1_s29820.Domain.Equipment.Equipment;

namespace APBD_Cw1_s29820.Services;

public class EquipmentService : IEquipmentService
{
    private readonly InMemoryStore _store;

    public EquipmentService(InMemoryStore store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public void AddEquipment(EquipmentBase item)
    {
        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        _store.EquipmentItems.Add(item);
    }

    public IReadOnlyCollection<EquipmentBase> GetAllEquipment()
    {
        return _store.EquipmentItems.AsReadOnly();
    }

    public IReadOnlyCollection<EquipmentBase> GetAvailableEquipment()
    {
        return _store.EquipmentItems
            .Where(e => e.Status == EquipmentStatus.Available)
            .ToList()
            .AsReadOnly();
    }

    public EquipmentBase GetById(int id)
    {
        EquipmentBase? equipment = _store.EquipmentItems.FirstOrDefault(e => e.Id == id);

        if (equipment is null)
        {
            throw new KeyNotFoundException($"Equipment with id {id} was not found.");
        }

        return equipment;
    }

    public void MarkAsUnavailable(int equipmentId)
    {
        EquipmentBase equipment = GetById(equipmentId);
        equipment.MarkAsUnavailable();
    }
}
