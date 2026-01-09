using BUS360.Model;

namespace BUS360.Hubs
{
    public class MockBusService
    {
        // Lấy danh sách ghế của một chuyến đi
        public List<TripSeat> GetSeatsByTrip(int tripId)
            => MockDataStore.TripSeats.Where(s => s.TripId == tripId).ToList();

        // Tài xế thêm khách vãng lai (Logic test nhanh)
        public bool AddWalkInPassenger(int tripId, string seatNumber, string customerName, string phone)
        {
            var seat = MockDataStore.TripSeats.FirstOrDefault(s => s.TripId == tripId && s.SeatNumber == seatNumber);

            if (seat == null || seat.Status != SeatStatus.Available) return false;

            // Tạo khách hàng mới
            var newCustomer = new Customer
            {
                Id = MockDataStore.Customers.Count + 1,
                FullName = customerName,
                PhoneNumber = phone,
                IsWalkIn = true
            };
            MockDataStore.Customers.Add(newCustomer);

            // Cập nhật ghế
            seat.Status = SeatStatus.Booked;
            seat.CustomerId = newCustomer.Id;
            return true;
        }

        // CSKH hoặc Khách hàng khóa ghế tạm thời (Pessimistic Locking giả lập)
        public bool TryLockSeat(int tripId, string seatNumber, string userId)
        {
            var seat = MockDataStore.TripSeats.FirstOrDefault(s => s.TripId == tripId && s.SeatNumber == seatNumber);

            if (seat != null && (seat.Status == SeatStatus.Available ||
               (seat.Status == SeatStatus.Pending && seat.LockedUntil < DateTime.Now)))
            {
                seat.Status = SeatStatus.Pending;
                seat.LockedByUserId = userId;
                seat.LockedUntil = DateTime.Now.AddSeconds(30); // Test nhanh: khóa 30 giây
                return true;
            }
            return false;
        }
    }
}
