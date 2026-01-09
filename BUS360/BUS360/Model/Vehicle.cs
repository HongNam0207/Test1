namespace BUS360.Model;

public class Vehicle
{
    public int Id { get; set; }
    public string LicensePlate { get; set; } // Biển số xe
    public string VehicleType { get; set; } // Ghế ngồi, giường nằm
    public int Capacity { get; set; } // Số lượng ghế
    public string Status { get; set; } // Hoạt động, Bảo trì, Ngưng hoạt động
    public int CompanyId { get; set; }
}