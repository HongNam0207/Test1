namespace BUS360.Model;

public class TripSeat
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public string SeatNumber { get; set; } // Tên ghế (A1, A2...)
    public SeatStatus Status { get; set; } // Available, Pending, Booked

    // Phục vụ Pessimistic Locking (Khóa bi quan)
    public string? LockedByUserId { get; set; } // ID nhân viên CSKH hoặc Khách hàng đang giữ ghế
    public DateTime? LockedUntil { get; set; } // Thời gian hết hạn khóa (TTL)

    public int? CustomerId { get; set; } // Thông tin khách hàng nếu đã đặt
    public bool IsPickedUp { get; set; } // Trạng thái tài xế đã đón khách hay chưa
}

public enum SeatStatus { Available, Pending, Booked }