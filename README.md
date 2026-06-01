# 🎯 G.K.T.S - Gerçek Zamanlı Taktik Konum Takip Sistemi

![.NET]
![Next.js]
![PostgreSQL]
![TypeScript]

Bu proje; sahadan gelen anlık telemetri ve koordinat verilerini güvenli bir şekilde işleyen, izole veritabanı altyapısında kalıcı hale getiren ve dinamik bir harita üzerinde gerçek zamanlı olarak görselleştiren **uçtan uca dağıtık bir sistem (Distributed System)** mimarisidir.

## 🏗️ Mimari ve Kullanılan Teknolojiler

Proje 3 ana mikro-bileşenden oluşmaktadır:

1. **Sahadaki Birlik (Simülatör):** `C# Console App (.NET 10)`
   * Saniyede 10 veri paketi üreterek sahadaki birliğin hareketlerini (Enlem, Boylam, Sağlık Durumu) simüle eder.
   * `HttpClient` üzerinden HTTPS protokolü ile Karargah API'sine asenkron veri pompalar.

2. **Merkez Karargah (Backend API & Veritabanı):** `ASP.NET Core Web API & PostgreSQL`
   * Gelen verileri karşılayan, CORS politikaları ve şifreli veri aktarımı ile yapılandırılmış güvenli veri kabul noktasıdır.
   * **Entity Framework Core (EF Core)** kullanılarak, `Docker` üzerinde izole edilmiş `PostgreSQL` veritabanına milisaniyelik gecikmelerle (Connection Pooling) veri yazılır.

3. **Komuta Merkezi (Frontend UI):** `Next.js & Tailwind CSS & Leaflet.js`
   * Karargah API'sine periyodik olarak (Polling) bağlanıp en güncel 50 taktik veriyi çeker.
   * `Leaflet.js` ve *Esri World Imagery* kullanılarak oluşturulan karanlık temalı gerçekçi uydu haritası üzerinde radar izlerini dinamik olarak günceller.

---

## ⚙️ Kurulum ve Çalıştırma Rehberi

Sistemi kendi lokalinizde ayağa kaldırmak için bilgisayarınızda **Docker Desktop**, **.NET 10 SDK** ve **Node.js** kurulu olmalıdır.

### Adım 1: İzole Veritabanını Ayağa Kaldırma (Docker)
Terminali açın ve PostgreSQL sunucusunu arka planda başlatın:

// docker run --name gkts-db -e POSTGRES_USER=karargah -e POSTGRES_PASSWORD=Your_Password_Here -e POSTGRES_DB=TaktikSahaDB -p 5433:5432 -d postgres:latest

Adım 2: Karargah API'sini Başlatma
2-Karargah-API klasörüne gidin.

EF Core ile veritabanı tablolarını oluşturun: dotnet ef database update (Eğer Package Manager Console kullanıyorsanız: Update-Database)

Sunucuyu başlatın: dotnet run (Sistem varsayılan olarak https://localhost:7098 portunu dinleyecektir).

Adım 3: Simülatörü Ateşleme
Yeni bir terminalde 1-Simulator klasörüne gidin.

Simülatörü çalıştırın: dotnet run

Terminalde [TX BAŞARILI] loglarının aktığını doğrulayın.

Adım 4: Komuta Merkezini (Next.js) Açma
3-Komuta-Merkezi klasörüne gidin.

Bağımlılıkları yükleyin: npm install

Geliştirici sunucusunu başlatın: npm run dev

Tarayıcınızda http://localhost:3000 adresine giderek radar ekranını izleyin.

👨‍💻 Geliştirici
Tarık Aytuğ Özcan Full Stack Developer | tarikaytugozcan.com
