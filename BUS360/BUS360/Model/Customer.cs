namespace BUS360.Model;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string PickupPoint { get; set; } // Điểm đón khách
    public string DropoffPoint { get; set; } // Điểm trả khách
    public bool IsWalkIn { get; set; } // Đánh dấu nếu là khách vãng lai do tài xế thêm dọc đường
}