namespace APBD_Cw1_s29820.Domain.Equipment;

public class Laptop : Equipment
{
    public string Processor { get; private set; }
    public int RamGb { get; private set; }

    public Laptop(string name, string processor, int ramGb) : base(name)
    {
        if (string.IsNullOrWhiteSpace(processor))
        {
            throw new ArgumentException("Processor cannot be empty.", nameof(processor));
        }

        if (ramGb <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(ramGb), "RAM must be greater than 0.");
        }

        Processor = processor;
        RamGb = ramGb;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Laptop | CPU: {Processor}, RAM: {RamGb} GB";
    }
}
