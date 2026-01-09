using Microsoft.AspNetCore.SignalR;
using BUS.Services;
using BUS.Model;

namespace BUS.Hubs
{
    // Kế thừa từ Hub của SignalR
    public class TripHub : Hub
    {
        private readonly BusDataService _dataService;

        // Inject BusDataService vào để lưu lại trạng thái mới nhất
        public TripHub(BusDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// TÀI XẾ gọi hàm này để cập nhật vị trí xe
        /// </summary>
        public async Task SendLocation(int tripId, double lat, double lng)
        {
            // 1. Lưu vào bộ nhớ tạm
            _dataService.UpdateLocation(tripId, lat, lng);

            // 2. Phát tín hiệu cho tất cả mọi người (Khách, CSKH) để cập nhật bản đồ
            await Clients.All.SendAsync("ReceiveGps", tripId, lat, lng);
        }

        /// <summary>
        /// KHÁCH HÀNG hoặc TÀI XẾ gọi hàm này để đặt/xác nhận ghế
        /// </summary>
        public async Task UpdateSeatStatus(int tripId, string seatNumber)
        {
            // 1. Thực hiện đặt ghế trong Service
            bool success = _dataService.BookSeat(tripId, seatNumber);

            if (success)
            {
                // 2. Nếu thành công, báo cho tất cả các máy khác đổi màu ghế này sang "Sold"
                await Clients.All.SendAsync("ReceiveSeatUpdate", tripId, seatNumber, "Sold");
            }
        }
    }
}