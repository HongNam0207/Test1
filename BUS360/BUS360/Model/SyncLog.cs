namespace BUS360.Model;

public class SyncLog
{
    public int Id { get; set; }
    public string ActionType { get; set; } // "UPDATE_GPS", "PICKUP_CUSTOMER", "ADD_WALKIN"
    public string Payload { get; set; } // Dữ liệu dạng JSON của hành động
    public DateTime ClientTimestamp { get; set; } // Thời gian thực tế tại thiết bị tài xế
    public bool IsSynced { get; set; } // Trạng thái đã đẩy lên server chưa
    public int TripId { get; set; }
}