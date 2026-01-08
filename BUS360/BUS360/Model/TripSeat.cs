namespace BUS360.Model;

public enum SeatStatus { Available, Pending, Booked }

public class TripSeat
{
    public string SeatNumber { get; set; } = "";
    public SeatStatus Status { get; set; } = SeatStatus.Available;

    // Liên kết với thông tin khách hàng
    public Customer? Passenger { get; set; }

    // Thông tin phục vụ Race Condition
    public string? LockedBy { get; set; } // ConnectionId
    public DateTime? Expiry { get; set; }
}