using APBD_Cw1_s29820.Domain.Enums;

namespace APBD_Cw1_s29820.Domain.Users;

public class Student : User
{
    public string StudentNumber { get; private set; }

    public Student(string firstName, string lastName, string studentNumber)
        : base(firstName, lastName, UserType.Student)
    {
        if (string.IsNullOrWhiteSpace(studentNumber))
        {
            throw new ArgumentException("Student number cannot be empty.", nameof(studentNumber));
        }

        StudentNumber = studentNumber.Trim();
    }

    public void ChangeStudentNumber(string studentNumber)
    {
        if (string.IsNullOrWhiteSpace(studentNumber))
        {
            throw new ArgumentException("Student number cannot be empty.", nameof(studentNumber));
        }

        StudentNumber = studentNumber.Trim();
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Student number: {StudentNumber}";
    }
}
