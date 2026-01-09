using BUS360.Model;

public static class MockDataStore
{
    // Danh sách tĩnh lưu trữ dữ liệu trong suốt vòng đời ứng dụng
    public static List<Trip> Trips = new List<Trip>();
    public static List<TripSeat> TripSeats = new List<TripSeat>();
    public static List<Customer> Customers = new List<Customer>();

    static MockDataStore()
    {
        // Khởi tạo 1 chuyến đi mẫu (TripId = 1)
        Trips.Add(new Trip { Id = 1, Route = "Hà Nội - Hải Phòng", Status = "Scheduled" });

        // Khởi tạo sơ đồ 10 ghế cho chuyến đi này
        for (int i = 1; i <= 10; i++)
        {
            TripSeats.Add(new TripSeat
            {
                Id = i,
                TripId = 1,
                SeatNumber = "A" + i,
                Status = SeatStatus.Available
            });
        }
    }
}