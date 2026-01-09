namespace BUS.Model
{
    public class Bus
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; } // Biển số xe
        public string BusType { get; set; } // Ghế ngồi, giường nằm, Limousine
        public int TotalSeats { get; set; } // Tổng số ghế (vd: 16, 29, 34, 45)
        public bool IsActive { get; set; } = true;
    }
}
