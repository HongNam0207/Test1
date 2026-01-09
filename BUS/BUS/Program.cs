using BUS.Components;
using BUS.Services; // Thêm namespace này
using BUS.Hubs;     // Thêm namespace này

var builder = WebApplication.CreateBuilder(args);

// --- 1. ĐĂNG KÝ DỊCH VỤ (SERVICES) ---

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Đăng ký kho dữ liệu tạm (Singleton để dùng chung cho tất cả các máy)
builder.Services.AddSingleton<BusDataService>();

// Đăng ký SignalR để làm tính năng Real-time
builder.Services.AddSignalR();

var app = builder.Build();

// --- 2. CẤU HÌNH PIPELINE ---

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Cấu hình đường dẫn cho Hub SignalR (Nơi các máy kết nối với nhau)
app.MapHub<TripHub>("/triphub");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();