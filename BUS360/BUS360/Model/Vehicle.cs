namespace BUS360.Model;

public class Vehicle
{
    public string PlateNumber { get; set; } = ""; // Biển số xe
    public string Brand { get; set; } = ""; // Hãng xe (Thaco, Hyundai...)
    public string Type { get; set; } = ""; // Ghế ngồi, Giường nằm, Limousine
    public int Capacity { get; set; } // Tổng số ghế
}