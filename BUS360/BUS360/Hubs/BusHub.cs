using BUS360.Hubs;
using Microsoft.AspNetCore.SignalR;

public class BusHub : Hub
{
    private readonly MockBusService _busService;

    public BusHub(MockBusService busService)
    {
        _busService = busService;
    }

    // Tài xế/Khách/CSKH join vào nhóm của chuyến xe
    public async Task JoinTrip(int tripId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, tripId.ToString());
    }

    // Khi tài xế thêm khách vãng lai thành công, báo cho mọi người cập nhật SeatMap
    public async Task DriverAddPassenger(int tripId, string seatNumber, string name, string phone)
    {
        var success = _busService.AddWalkInPassenger(tripId, seatNumber, name, phone);
        if (success)
        {
            await Clients.Group(tripId.ToString()).SendAsync("NotifySeatUpdated", seatNumber, "Booked");
        }
    }

    // Khi CSKH click chọn ghế, khóa ghế và báo cho những người khác
    public async Task StaffLockSeat(int tripId, string seatNumber, string staffName)
    {
        var success = _busService.TryLockSeat(tripId, seatNumber, staffName);
        if (success)
        {
            await Clients.Group(tripId.ToString()).SendAsync("NotifySeatLocked", seatNumber, staffName);
        }
    }

    // Đồng bộ GPS cho khách hàng xem
    public async Task UpdateLocation(int tripId, double lat, double lng)
    {
        await Clients.Group(tripId.ToString()).SendAsync("ReceiveLocation", lat, lng);
    }
}