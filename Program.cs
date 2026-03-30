using APBD_Cw1_s29820.ConsoleUI;
using APBD_Cw1_s29820.Data;
using APBD_Cw1_s29820.Services;

namespace APBD_Cw1_s29820;

public class Program
{
    public static void Main(string[] args)
    {
        var store = new InMemoryStore();

        IUserLimitPolicy userLimitPolicy = new UserLimitPolicy();
        IPenaltyPolicy penaltyPolicy = new SimplePenaltyPolicy();

        IEquipmentService equipmentService = new EquipmentService(store);
        IUserService userService = new UserService(store);
        IRentalService rentalService = new RentalService(store, userLimitPolicy, penaltyPolicy);
        IReportService reportService = new ReportService(store);

        var demoScenario = new DemoScenario(
            equipmentService,
            userService,
            rentalService,
            reportService);

        demoScenario.Run();
    }
}
