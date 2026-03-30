using APBD_Cw1_s29820.Domain.Equipment;
using APBD_Cw1_s29820.Domain.Users;
using APBD_Cw1_s29820.Exceptions;
using APBD_Cw1_s29820.Services;

namespace APBD_Cw1_s29820.ConsoleUI;

public class DemoScenario
{
    private readonly IEquipmentService _equipmentService;
    private readonly IUserService _userService;
    private readonly IRentalService _rentalService;
    private readonly IReportService _reportService;

    public DemoScenario(
        IEquipmentService equipmentService,
        IUserService userService,
        IRentalService rentalService,
        IReportService reportService)
    {
        _equipmentService = equipmentService ?? throw new ArgumentNullException(nameof(equipmentService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _rentalService = rentalService ?? throw new ArgumentNullException(nameof(rentalService));
        _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
    }

    public void Run()
    {
        PrintHeader("APBD - Equipment Rental Demo");

        SeedUsers();
        SeedEquipment();

        PrintUsers();
        PrintEquipment("Initial equipment list");
        PrintAvailableEquipment("Available equipment at startup");

        ShowSuccessfulRental();
        ShowUnavailableEquipmentAttempt();

        ShowStudentLimitExceeded();
        ShowActiveRentalsForUser(3);

        ShowMarkAsUnavailable(4);
        PrintAvailableEquipment("Available equipment after marking one item as unavailable");

        ShowOnTimeReturn();
        ShowLateReturnWithPenalty();

        PrintEquipment("Equipment after scenario");
        PrintRentals();
        PrintReport();
    }

    private void SeedUsers()
    {
        _userService.AddUser(new Student("Jan", "Kowalski", "s12345"));
        _userService.AddUser(new Employee("Anna", "Nowak", "IT"));
        _userService.AddUser(new Student("Ola", "Wisniewska", "s54321"));
    }

    private void SeedEquipment()
    {
        _equipmentService.AddEquipment(new Laptop("Dell Latitude 5440", "Intel Core i5", 16));
        _equipmentService.AddEquipment(new Laptop("Lenovo ThinkPad T14", "AMD Ryzen 5", 16));
        _equipmentService.AddEquipment(new Projector("Epson EB-X49", 3600, "1024x768"));
        _equipmentService.AddEquipment(new Camera("Canon EOS 250D", 24, true));
        _equipmentService.AddEquipment(new Camera("Sony ZV-1", 20, false));
    }

    private void ShowSuccessfulRental()
    {
        PrintHeader("Correct rental");

        var rental = _rentalService.RentEquipment(userId: 1, equipmentId: 1, days: 7);

        Console.WriteLine("Rental created successfully.");
        Console.WriteLine(rental);
        Console.WriteLine();
    }

    private void ShowUnavailableEquipmentAttempt()
    {
        PrintHeader("Attempt to rent unavailable equipment");

        try
        {
            _rentalService.RentEquipment(userId: 2, equipmentId: 1, days: 3);
        }
        catch (EquipmentNotAvailableException ex)
        {
            Console.WriteLine($"Expected business error: {ex.Message}");
        }

        Console.WriteLine();
    }

    private void ShowStudentLimitExceeded()
    {
        PrintHeader("Attempt to exceed student limit");

        try
        {
            var first = _rentalService.RentEquipment(userId: 3, equipmentId: 2, days: 5);
            var second = _rentalService.RentEquipment(userId: 3, equipmentId: 3, days: 5);

            Console.WriteLine("Two rentals created for student:");
            Console.WriteLine(first);
            Console.WriteLine(second);
            Console.WriteLine();

            _rentalService.RentEquipment(userId: 3, equipmentId: 4, days: 5);
        }
        catch (UserLimitExceededException ex)
        {
            Console.WriteLine($"Expected business error: {ex.Message}");
        }

        Console.WriteLine();
    }

    private void ShowActiveRentalsForUser(int userId)
    {
        PrintHeader($"Active rentals for user {userId}");

        var rentals = _rentalService.GetActiveRentalsForUser(userId);

        if (rentals.Count == 0)
        {
            Console.WriteLine("No active rentals.");
        }
        else
        {
            foreach (var rental in rentals)
            {
                Console.WriteLine(rental);
            }
        }

        Console.WriteLine();
    }

    private void ShowMarkAsUnavailable(int equipmentId)
    {
        PrintHeader("Mark equipment as unavailable");

        _equipmentService.MarkAsUnavailable(equipmentId);

        Console.WriteLine("Equipment status changed to unavailable:");
        Console.WriteLine(_equipmentService.GetById(equipmentId));
        Console.WriteLine();
    }

    private void ShowOnTimeReturn()
    {
        PrintHeader("Return on time");

        var onTimeRental = _rentalService.RentEquipment(userId: 2, equipmentId: 5, days: 2);

        Console.WriteLine("Rental created for on-time return:");
        Console.WriteLine(onTimeRental);

        DateTime returnedAt = onTimeRental.DueDate.AddHours(-2);
        _rentalService.ReturnEquipment(onTimeRental.Id, returnedAt);

        Console.WriteLine("Returned before due date.");
        Console.WriteLine(onTimeRental);
        Console.WriteLine();
    }

    private void ShowLateReturnWithPenalty()
    {
        PrintHeader("Late return with penalty");

        var lateRental = _rentalService.RentEquipment(userId: 2, equipmentId: 5, days: 1);

        Console.WriteLine("Rental created for late return:");
        Console.WriteLine(lateRental);
        Console.WriteLine();

        ShowOverdueRentals(
            lateRental.DueDate.AddHours(1),
            "Overdue rentals before return");

        DateTime returnedAt = lateRental.DueDate.AddDays(2).AddHours(1);
        _rentalService.ReturnEquipment(lateRental.Id, returnedAt);

        Console.WriteLine("Returned after due date.");
        Console.WriteLine(lateRental);
        Console.WriteLine();
    }

    private void ShowOverdueRentals(DateTime now, string title)
    {
        PrintHeader(title);

        var overdueRentals = _rentalService.GetOverdueRentals(now);

        if (overdueRentals.Count == 0)
        {
            Console.WriteLine("No overdue rentals.");
        }
        else
        {
            foreach (var rental in overdueRentals)
            {
                Console.WriteLine(rental);
            }
        }

        Console.WriteLine();
    }

    private void PrintUsers()
    {
        PrintHeader("Users");

        foreach (var user in _userService.GetAllUsers())
        {
            Console.WriteLine(user);
        }

        Console.WriteLine();
    }

    private void PrintEquipment(string title)
    {
        PrintHeader(title);

        foreach (var item in _equipmentService.GetAllEquipment())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }

    private void PrintAvailableEquipment(string title)
    {
        PrintHeader(title);

        foreach (var item in _equipmentService.GetAvailableEquipment())
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }

    private void PrintRentals()
    {
        PrintHeader("All rentals");

        foreach (var rental in _rentalService.GetAllRentals())
        {
            Console.WriteLine(rental);
        }

        Console.WriteLine();
    }

    private void PrintReport()
    {
        PrintHeader("Final report");
        Console.WriteLine(_reportService.GenerateSummaryReport(DateTime.Now));
        Console.WriteLine();
    }

    private static void PrintHeader(string title)
    {
        Console.WriteLine(new string('=', 60));
        Console.WriteLine(title);
        Console.WriteLine(new string('=', 60));
    }
}
