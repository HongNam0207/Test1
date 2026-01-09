using BUS.Model;

namespace BUS.Services
{
    public class BusDataService
    {
        // Danh sách lưu trữ trong bộ nhớ (In-memory)
        public List<Trip> Trips { get; set; } = new();
        public List<Seat> Seats { get; set; } = new();
        public List<Bus> Buses { get; set; } = new();

        public BusDataService()
        {
            // 1. Khởi tạo dữ liệu mẫu (Seed Data)
            SeedInitialData();
        }

        private void SeedInitialData()
        {
            // Tạo một chiếc xe mẫu
            var bus1 = new Bus { Id = 1, LicensePlate = "29B-123.45", BusType = "Giường nằm 34 chỗ", TotalSeats = 34 };
            Buses.Add(bus1);

            // Tạo một chuyến đi mẫu (Hà Nội -> Hải Phòng)
            var trip1 = new Trip
            {
                Id = 1,
                RouteName = "Hà Nội - Hải Phòng",
                DepartureTime = DateTime.Now.AddHours(3),
                BusId = 1,
                Status = TripStatus.Scheduled,
                CurrentLatitude = 21.0285, // Tọa độ HN mặc định
                CurrentLongitude = 105.8542
            };
            Trips.Add(trip1);

            // Tạo sơ đồ ghế cho chuyến đi số 1 (Ví dụ tạo 10 ghế để test nhanh)
            for (int i = 1; i <= 10; i++)
            {
                Seats.Add(new Seat
                {
                    Id = i,
                    TripId = 1,
                    SeatNumber = i < 6 ? $"A{i}" : $"B{i - 5}", // Ghế A1-A5, B1-B5
                    Status = SeatStatus.Available
                });
            }
        }

        // --- CÁC HÀM XỬ LÝ LOGIC ---

        // Lấy danh sách ghế của một chuyến cụ thể
        public List<Seat> GetSeatsByTrip(int tripId)
            => Seats.Where(s => s.TripId == tripId).ToList();

        // Cập nhật vị trí GPS (Dành cho Tài xế)
        public void UpdateLocation(int tripId, double lat, double lng)
        {
            var trip = Trips.FirstOrDefault(t => t.Id == tripId);
            if (trip != null)
            {
                trip.CurrentLatitude = lat;
                trip.CurrentLongitude = lng;
            }
        }

        // Đặt ghế (Dành cho Khách hàng/CSKH)
        public bool BookSeat(int tripId, string seatNumber)
        {
            var seat = Seats.FirstOrDefault(s => s.TripId == tripId && s.SeatNumber == seatNumber);
            if (seat != null && seat.Status == SeatStatus.Available)
            {
                seat.Status = SeatStatus.Sold;
                return true;
            }
            return false;
        }
    }
}