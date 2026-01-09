using BUS360.Components;
using BUS360.Services; // Đảm bảo folder Services có file BusService.cs
using BUS360.Hubs;     // Đảm bảo folder Hubs có file BusHub.cs

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm dịch vụ giao diện
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. Đăng ký BusService là SINGLETON (Giả lập Database trong RAM)
builder.Services.AddSingleton<BusService>();

// 3. Đăng ký SignalR để đồng bộ Tài xế - Khách hàng - CSKH
builder.Services.AddSignalR();

var app = builder.Build();

// Cấu hình Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();

// 4. Kích hoạt Render Mode cho Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// 5. Cấu hình đường dẫn Hub cho SignalR
app.MapHub<BusHub>("/bushub");

app.Run();