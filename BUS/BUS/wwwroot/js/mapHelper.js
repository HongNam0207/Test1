// Biến toàn cục để lưu trữ bản đồ và các marker
let map;
let busMarker; // Marker cho xe đơn lẻ (trang Booking)
let busMarkers = {}; // Đối tượng lưu danh sách marker cho nhiều xe (trang Monitor)

// 1. Khởi tạo bản đồ
window.initMap = (lat, lng) => {
    if (map) {
        map.setView([lat, lng], 15);
        return;
    }

    const mapElement = document.getElementById('map');
    if (!mapElement) return;

    map = L.map('map').setView([lat, lng], 15);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '© OpenStreetMap contributors'
    }).addTo(map);

    busMarker = L.marker([lat, lng]).addTo(map).bindPopup('Xe BUS360').openPopup();
};

// 2. Cập nhật vị trí 1 xe duy nhất
window.updateBusLocation = (lat, lng) => {
    if (busMarker && map) {
        busMarker.setLatLng([lat, lng]);
        map.setView([lat, lng]);
    }
};

// 3. Cập nhật vị trí nhiều xe
window.updateMultiBusLocation = (tripId, lat, lng, routeName) => {
    if (!map) return;

    if (busMarkers[tripId]) {
        busMarkers[tripId].setLatLng([lat, lng]);
    } else {
        busMarkers[tripId] = L.marker([lat, lng])
            .addTo(map)
            .bindPopup(`<b>Tuyến: ${routeName}</b><br>ID Chuyến: ${tripId}`)
            .openPopup();
    }
};

// 4. Lấy vị trí thực tế của thiết bị (GPS trình duyệt) và theo dõi vị trí thay vì lấy một lần
window.startGPSLocation = (dotNetHelper) => {
    if ("geolocation" in navigator) {
        // Sử dụng watchPosition để theo dõi vị trí khi di chuyển
        navigator.geolocation.watchPosition(
            (position) => {
                // Gửi tọa độ về hàm C# có tên 'UpdateRealLocation'
                dotNetHelper.invokeMethodAsync('UpdateRealLocation',
                    position.coords.latitude,
                    position.coords.longitude);
            },
            (error) => {
                // Xử lý lỗi nếu không lấy được GPS
                console.error("Lỗi lấy GPS: " + error.message);
                alert("Không thể lấy GPS: " + error.message);
            },
            {
                enableHighAccuracy: true, // Cố gắng lấy tọa độ chính xác nhất
                timeout: 10000, // Thời gian chờ tối đa là 10 giây
                maximumAge: 0 // Không sử dụng dữ liệu GPS cũ
            }
        );
    } else {
        alert("Trình duyệt của bạn không hỗ trợ GPS.");
    }
};
