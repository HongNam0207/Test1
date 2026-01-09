namespace BUS.Model
{
    public enum TripStatus
    {
        Scheduled,  // Mới lên lịch
        Departing,  // Đang chạy
        Completed,  // Đã về bến
        Cancelled   // Đã hủy
    }

    public class Trip
    {
        public int Id { get; set; }
        public string RouteName { get; set; } // Ví dụ: Hà Nội - Hải Phòng
        public DateTime DepartureTime { get; set; } // Giờ xuất phát
        public int BusId { get; set; }
        public int DriverId { get; set; } // ID của tài xế
        public TripStatus Status { get; set; } = TripStatus.Scheduled;

        // GPS real-time lưu tại đây hoặc một bảng riêng
        public double CurrentLatitude { get; set; }
        public double CurrentLongitude { get; set; }
    }
}
