using BUS360.Model;
using BUS360.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BUS360.Services
{
    public class BusService
    {
        private readonly IHubContext<BusHub> _hubContext;
        private readonly List<TripSeat> _seats = new();

        public BusService(IHubContext<BusHub> hubContext)
        {
            _hubContext = hubContext;
            // Tạo dữ liệu mẫu
            for (int i = 1; i <= 10; i++)
            {
                _seats.Add(new TripSeat { Id = i, TripId = 1, SeatNumber = "A" + i, Status = SeatStatus.Available });
            }
        }

        public List<TripSeat> GetSeats(int tripId) => _seats.Where(s => s.TripId == tripId).ToList();

        public async Task<bool> TryLockSeat(int tripId, string seatNumber, string userId)
        {
            var seat = _seats.FirstOrDefault(s => s.TripId == tripId && s.SeatNumber == seatNumber);
            if (seat != null && seat.Status == SeatStatus.Available)
            {
                seat.Status = SeatStatus.Pending;
                // Gửi tín hiệu Real-time
                await _hubContext.Clients.Group(tripId.ToString()).SendAsync("ReceiveSeatUpdate", seatNumber, "Pending");
                return true;
            }
            return false;
        }

        public async Task<bool> AddWalkInPassenger(int tripId, string seatNumber, Customer customer)
        {
            var seat = _seats.FirstOrDefault(s => s.TripId == tripId && s.SeatNumber == seatNumber);
            if (seat != null)
            {
                seat.Status = SeatStatus.Booked;
                await _hubContext.Clients.Group(tripId.ToString()).SendAsync("ReceiveSeatUpdate", seatNumber, "Booked");
                return true;
            }
            return false;
        }
    }
}