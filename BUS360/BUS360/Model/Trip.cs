namespace BUS360.Model;

public class Trip
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int DriverId { get; set; } // ID tài xế được phân công
    public string Route { get; set; } // Tuyến đường
    public DateTime DepartureTime { get; set; } // Giờ xuất phát
    public string Status { get; set; } // Scheduled, Departed, Completed, Cancelled

    // Tích hợp GPS thời gian thực
    public double? CurrentLat { get; set; }
    public double? CurrentLng { get; set; }
    public DateTime? LastGpsUpdate { get; set; }
}