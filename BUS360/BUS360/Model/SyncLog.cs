namespace BUS360.Model;

public class SyncLog
{
    public string TripId { get; set; } = "";
    public string SeatNumber { get; set; } = "";
    public SeatStatus NewStatus { get; set; }
    public DateTime EventTime { get; set; } // T_event: thời điểm tài xế thao tác
    public bool IsProcessed { get; set; } = false;
}