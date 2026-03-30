namespace APBD_Cw1_s29820.Domain.Equipment;

public class Camera : Equipment
{
    public int Megapixels { get; private set; }
    public bool InterchangeableLens { get; private set; }

    public Camera(string name, int megapixels, bool interchangeableLens) : base(name)
    {
        if (megapixels <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(megapixels), "Megapixels must be greater than 0.");
        }

        Megapixels = megapixels;
        InterchangeableLens = interchangeableLens;
    }

    public override string ToString()
    {
        string lensInfo = InterchangeableLens ? "Yes" : "No";
        return $"{base.ToString()} | Camera | MP: {Megapixels}, Interchangeable lens: {lensInfo}";
    }
}
