namespace BUS360.Model;

public class ProcessLog
{
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string Source { get; set; } = ""; // Ví dụ: "Xe khách", "Server", "Web"
    public string Message { get; set; } = "";
    public string Type { get; set; } = "info"; // info, success, warning, danger
}