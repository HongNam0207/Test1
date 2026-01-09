namespace BUS.Model
{
    public enum SeatStatus
    {
        Available, // Trống
        Booked,    // Đã đặt (chưa thanh toán)
        Sold,      // Đã bán (đã thanh toán)
        Occupied   // Khách đã lên xe (Tài xế xác nhận)
    }

    public class Seat
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public string SeatNumber { get; set; } // Ví dụ: A1, A2, B1...
        public SeatStatus Status { get; set; } = SeatStatus.Available;
    }
}
