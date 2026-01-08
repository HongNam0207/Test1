namespace BUS360.Model;

public class Trip
{
    public string Id { get; set; } = "";
    public string RouteName { get; set; } = ""; // Tuyến đường
    public DateTime DepartureTime { get; set; } // Giờ xuất phát

    // Quan hệ với các Model khác
    public Vehicle Bus { get; set; } = new();
    public Staff Driver { get; set; } = new();
    public List<TripSeat> Seats { get; set; } = new();
}