using BUS360.Model;

namespace BUS360.Services;

public class BusService
{
    public Trip CurrentTrip { get; set; }

    public BusService()
    {
        // Khởi tạo dữ liệu mẫu cho chuyến xe
        CurrentTrip = new Trip
        {
            Id = "TRIP-2026-001",
            RouteName = "Hà Nội - Sapa (Cao tốc)",
            DepartureTime = DateTime.Now.AddHours(3),
            Bus = new Vehicle { PlateNumber = "29B-888.88", Type = "Limousine 9 Chỗ", Brand = "Ford" },
            Driver = new Staff { FullName = "Nguyễn Văn Lái", PhoneNumber = "0912.345.678" }
        };

        // Khởi tạo sơ đồ 9 ghế cho xe Limousine
        for (int i = 1; i <= 9; i++)
        {
            CurrentTrip.Seats.Add(new TripSeat { SeatNumber = "L" + i });
        }
    }

    // Logic đặt vé: Cập nhật trạng thái ghế và thông tin khách hàng
    public bool BookSeat(string seatNumber, Customer customer)
    {
        var seat = CurrentTrip.Seats.FirstOrDefault(s => s.SeatNumber == seatNumber);
        if (seat != null && seat.Status == SeatStatus.Available)
        {
            seat.Status = SeatStatus.Booked;
            seat.Passenger = customer;
            return true;
        }
        return false;
    }

    public void CancelSeat(string seatNumber)
    {
        var seat = CurrentTrip.Seats.FirstOrDefault(s => s.SeatNumber == seatNumber);
        if (seat != null)
        {
            seat.Status = SeatStatus.Available;
            seat.Passenger = null;
        }
    }
}