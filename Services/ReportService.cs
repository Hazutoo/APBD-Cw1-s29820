using APBD_Cw1_s29820.Data;
using APBD_Cw1_s29820.Domain.Enums;

namespace APBD_Cw1_s29820.Services;

public class ReportService : IReportService
{
    private readonly InMemoryStore _store;

    public ReportService(InMemoryStore store)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public string GenerateSummaryReport(DateTime now)
    {
        int allEquipmentCount = _store.EquipmentItems.Count;
        int availableEquipmentCount = _store.EquipmentItems.Count(e => e.Status == EquipmentStatus.Available);
        int rentedEquipmentCount = _store.EquipmentItems.Count(e => e.Status == EquipmentStatus.Rented);
        int unavailableEquipmentCount = _store.EquipmentItems.Count(e => e.Status == EquipmentStatus.Unavailable);

        int allRentalsCount = _store.Rentals.Count;
        int activeRentalsCount = _store.Rentals.Count(r => !r.IsReturned);
        int overdueRentalsCount = _store.Rentals.Count(r => !r.IsReturned && r.DueDate < now);

        return
$@"=== EQUIPMENT RENTAL REPORT ===
Generated at: {now:yyyy-MM-dd HH:mm:ss}

Equipment:
- Total equipment items: {allEquipmentCount}
- Available: {availableEquipmentCount}
- Rented: {rentedEquipmentCount}
- Unavailable: {unavailableEquipmentCount}

Rentals:
- Total rentals: {allRentalsCount}
- Active rentals: {activeRentalsCount}
- Overdue rentals: {overdueRentalsCount}";
    }
}
