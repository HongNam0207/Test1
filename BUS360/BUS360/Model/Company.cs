namespace BUS360.Model;

public class Company
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
    public string HotLine { get; set; } = "";
}