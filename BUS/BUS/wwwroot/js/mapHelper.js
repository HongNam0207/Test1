// Biến toàn cục để lưu trữ bản đồ và các marker
let map;
let busMarker; // Marker cho xe đơn lẻ (trang Booking)
let busMarkers = {}; // Đối tượng lưu danh sách marker cho nhiều xe (trang Monitor)

// 1. Khởi tạo bản đồ
window.initMap = (lat, lng) => {
    // Nếu bản đồ đã tồn tại, chúng ta không khởi tạo lại mà chỉ cập nhật view
    if (map) {
        map.setView([lat, lng], 15);
        return;
    }

    // Kiểm tra xem phần tử 'map' có tồn tại trong DOM không
    const mapElement = document.getElementById('map');
    if (!mapElement) return;

    map = L.map('map').setView([lat, lng], 15);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    // Khởi tạo marker đơn lẻ mặc định (dùng cho trang Booking)
    busMarker = L.marker([lat, lng]).addTo(map).bindPopup('Xe BUS360').openPopup();
};

// 2. Cập nhật vị trí 1 xe duy nhất (Dùng cho Booking.razor)
window.updateBusLocation = (lat, lng) => {
    if (busMarker && map) {
        busMarker.setLatLng([lat, lng]);
        map.setView([lat, lng]); // Tự động cuộn bản đồ theo xe
    }
};

// 3. Cập nhật vị trí nhiều xe (Dùng cho MonitorMap.razor)
window.updateMultiBusLocation = (tripId, lat, lng, routeName) => {
    if (!map) return;

    // Nếu xe này đã có marker trên bản đồ thì chỉ cập nhật vị trí
    if (busMarkers[tripId]) {
        busMarkers[tripId].setLatLng([lat, lng]);
    } else {
        // Nếu chưa có, tạo marker mới với màu sắc hoặc icon riêng nếu cần
        busMarkers[tripId] = L.marker([lat, lng])
            .addTo(map)
            .bindPopup(`<b>Tuyến: ${routeName}</b><br>ID Chuyến: ${tripId}`)
            .openPopup();
    }
};

// 4. Lấy vị trí thực tế của thiết bị (GPS trình duyệt)
window.getRealLocation = (dotNetHelper) => {
    if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition((position) => {
            // Gửi tọa độ về hàm C# có tên 'UpdateRealLocation'
            dotNetHelper.invokeMethodAsync('UpdateRealLocation',
                position.coords.latitude,
                position.coords.longitude);
        }, (error) => {
            console.error("Lỗi lấy GPS: " + error.message);
        });
    } else {
        alert("Trình duyệt của bạn không hỗ trợ GPS.");
    }
};