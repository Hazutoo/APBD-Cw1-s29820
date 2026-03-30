namespace APBD_Cw1_s29820.Domain.Equipment;

public class Projector : Equipment
{
    public int BrightnessLumens { get; private set; }
    public string Resolution { get; private set; }

    public Projector(string name, int brightnessLumens, string resolution) : base(name)
    {
        if (brightnessLumens <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(brightnessLumens), "Brightness must be greater than 0.");
        }

        if (string.IsNullOrWhiteSpace(resolution))
        {
            throw new ArgumentException("Resolution cannot be empty.", nameof(resolution));
        }

        BrightnessLumens = brightnessLumens;
        Resolution = resolution;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Projector | Brightness: {BrightnessLumens} lm, Resolution: {Resolution}";
    }
}
