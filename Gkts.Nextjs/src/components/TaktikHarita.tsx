"use client"; // Next.js'e bunun tarayıcıda çalışacağını söylüyoruz

import { useEffect, useState } from 'react';
import { MapContainer, TileLayer, Popup, CircleMarker } from 'react-leaflet';
import 'leaflet/dist/leaflet.css'; // Leaflet'in iskelet CSS'i zorunlu!

// Karargah'tan gelecek verinin şeması
interface TaktikVeri {
  id: number;
  birlikAdi: string;
  enlem: number;
  boylam: number;
  saglik: number;
  kayitZamani: string;
}

export default function TaktikHarita() {
  const [birlikler, setBirlikler] = useState<TaktikVeri[]>([]);

  // Telsiz Bağlantısı: Karargahı sürekli dinleyen kulaklık
  useEffect(() => {
    const veriCek = async () => {
      try {
        // DİKKAT: Port numarasını (7098) Karargah terminaline göre ayarla!
        const res = await fetch('https://localhost:7098/api/TaktikVeri/guncel-konumlar');
        const data = await res.json();
        setBirlikler(data);
      } catch (error) {
        console.error("Karargah ile iletişim koptu!", error);
      }
    };

    // Sayfa açılır açılmaz ilk veriyi çek
    veriCek();
    
    // Askeri radar mantığı: Her 2 saniyede bir ekranı güncelle
    const radarDöngüsü = setInterval(veriCek, 2000); 
    
    return () => clearInterval(radarDöngüsü);
  }, []);

  return (
    // Haritayı Macunköy/Ankara (Aselsan bölgesi) civarına ortalıyoruz
    <MapContainer 
      center={[39.9612, 32.7661]} 
      zoom={16} 
      style={{ height: '100vh', width: '100%' }}
      zoomControl={false} // Default butonları kapatıyoruz, daha sinematik olsun
    >
      {/* Esri World Imagery - Gerçekçi Uydu Katmanı */}
      <TileLayer
        url="https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}"
        attribution='Tiles &copy; Esri &mdash; Source: GeoEye, Earthstar Geographics, CNES/Airbus DS, USDA, USGS, AeroGRID, IGN, and the GIS User Community'
      />
      
      {/* Sahadan gelen her bir taktik veri için haritaya kırmızı bir radar izi bırakıyoruz */}
      {birlikler.map((birlik) => (
        <CircleMarker 
          key={birlik.id} 
          center={[birlik.enlem, birlik.boylam]}
          pathOptions={{ 
            color: birlik.saglik > 20 ? '#10b981' : '#ef4444', // Sağlık iyiyse dış çember yeşil, kötüyse kırmızı
            fillColor: '#ef4444', // İçi her zaman taktik kırmızı
            fillOpacity: 0.8 
          }}
          radius={8}
        >
          {/* Üstüne tıklayınca açılacak komuta penceresi */}
          <Popup>
            <div className="font-mono text-slate-900 font-bold p-1">
              <p className="border-b border-slate-300 pb-1 mb-1">🛡️ BİRLİK: {birlik.birlikAdi}</p>
              <p>❤️ Sağlık: %{birlik.saglik}</p>
              <p className="text-xs mt-1 text-slate-500">
                LAT: {birlik.enlem.toFixed(4)} | LNG: {birlik.boylam.toFixed(4)}
              </p>
            </div>
          </Popup>
        </CircleMarker>
      ))}
    </MapContainer>
  );
}