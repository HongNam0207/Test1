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