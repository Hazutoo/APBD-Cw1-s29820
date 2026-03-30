using APBD_Cw1_s29820.Domain.Enums;

namespace APBD_Cw1_s29820.Domain.Equipment;

public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; private set; }
    public EquipmentStatus Status { get; private set; }

    protected Equipment(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Equipment name cannot be empty.", nameof(name));
        }

        Id = _nextId++;
        Name = name;
        Status = EquipmentStatus.Available;
    }

    public void MarkAsAvailable()
    {
        Status = EquipmentStatus.Available;
    }

    public void MarkAsRented()
    {
        Status = EquipmentStatus.Rented;
    }

    public void MarkAsUnavailable()
    {
        Status = EquipmentStatus.Unavailable;
    }

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            throw new ArgumentException("Equipment name cannot be empty.", nameof(newName));
        }

        Name = newName;
    }

    public override string ToString()
    {
        return $"[{Id}] {Name} - {Status}";
    }
}
