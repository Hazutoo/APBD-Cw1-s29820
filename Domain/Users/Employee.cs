using APBD_Cw1_s29820.Domain.Enums;

namespace APBD_Cw1_s29820.Domain.Users;

public class Employee : User
{
    public string Department { get; private set; }

    public Employee(string firstName, string lastName, string department)
        : base(firstName, lastName, UserType.Employee)
    {
        if (string.IsNullOrWhiteSpace(department))
        {
            throw new ArgumentException("Department cannot be empty.", nameof(department));
        }

        Department = department.Trim();
    }

    public void ChangeDepartment(string department)
    {
        if (string.IsNullOrWhiteSpace(department))
        {
            throw new ArgumentException("Department cannot be empty.", nameof(department));
        }

        Department = department.Trim();
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Department: {Department}";
    }
}
