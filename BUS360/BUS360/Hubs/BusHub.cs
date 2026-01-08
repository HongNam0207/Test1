using Microsoft.AspNetCore.SignalR;

namespace BUS360.Hubs;

public class BusHub : Hub
{
    // Thông báo cập nhật trạng thái ghế cho toàn hệ thống
    public async Task NotifySeatUpdate(string seatNumber, string status)
    {
        await Clients.All.SendAsync("ReceiveSeatUpdate", seatNumber, status);
    }
}