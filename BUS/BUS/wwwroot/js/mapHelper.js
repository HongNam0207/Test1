// Nội dung cho wwwroot/js/mapHelper.js
let map, busMarker;

window.initMap = (lat, lng) => {
    if (map) return; // Tránh khởi tạo lại nhiều lần
    map = L.map('map').setView([lat, lng], 15);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);
    busMarker = L.marker([lat, lng]).addTo(map).bindPopup('Xe BUS360').openPopup();
};

window.updateBusLocation = (lat, lng) => {
    if (busMarker) {
        busMarker.setLatLng([lat, lng]);
        map.setView([lat, lng]); // Tự động cuộn bản đồ theo xe
    }
};

// Thêm vào file wwwroot/js/mapHelper.js của bạn
window.getRealLocation = (dotNetHelper) => {
    if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition((position) => {
            // Gửi tọa độ về hàm C# có tên 'UpdateRealLocation'
            dotNetHelper.invokeMethodAsync('UpdateRealLocation',
                position.coords.latitude,
                position.coords.longitude);
        }, (error) => {
            alert("Lỗi lấy GPS: " + error.message);
        });
    } else {
        alert("Trình duyệt của bạn không hỗ trợ GPS.");
    }
};

// Đối tượng lưu danh sách marker các xe: { "1": marker1, "2": marker2 }
let busMarkers = {};

window.updateMultiBusLocation = (tripId, lat, lng, routeName) => {
    if (!map) return;

    // Nếu xe này đã có marker trên bản đồ thì chỉ cập nhật vị trí
    if (busMarkers[tripId]) {
        busMarkers[tripId].setLatLng([lat, lng]);
    } else {
        // Nếu chưa có, tạo marker mới và thêm vào bản đồ
        busMarkers[tripId] = L.marker([lat, lng])
            .addTo(map)
            .bindPopup(`<b>Xe: ${routeName}</b><br>ID Chuyến: ${tripId}`)
            .openPopup();
    }
};

let busMarkers = {}; // Lưu trữ marker theo TripId

window.updateMultiBusLocation = (tripId, lat, lng, routeName) => {
    if (!map) return;

    if (busMarkers[tripId]) {
        // Cập nhật vị trí nếu xe đã tồn tại
        busMarkers[tripId].setLatLng([lat, lng]);
    } else {
        // Tạo mới marker nếu xe lần đầu xuất hiện
        busMarkers[tripId] = L.marker([lat, lng])
            .addTo(map)
            .bindPopup(`<b>Tuyến: ${routeName}</b><br>ID: ${tripId}`)
            .openPopup();
    }
};