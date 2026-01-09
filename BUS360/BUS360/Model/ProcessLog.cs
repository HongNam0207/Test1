namespace BUS360.Model;

public class ProcessLog
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public string LogType { get; set; } // GPS_Update, Status_Change, Incident
    public string Description { get; set; } // Nội dung sự cố hoặc thay đổi
    public DateTime LogTime { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}