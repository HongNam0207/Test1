namespace BUS.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public string SeatNumber { get; set; }
        public string PassengerName { get; set; }
        public string PassengerPhone { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
