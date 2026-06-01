using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

Console.WriteLine("G.K.T.S Taktik Simülatörü Başlatılıyor...");
Console.WriteLine("Birlik_Alpha sahaya iniyor. Veri akışı başlıyor...\n");

double enlem = 39.9612;
double boylam = 32.7661;
int saglik = 100;

// Telsiz cihazımızı (HttpClient) kuruyoruz
using var client = new HttpClient();

// Karargahın frekansı (Senin terminaldeki port numaran)
string karargahUrl = "https://localhost:7098/api/taktikveri/rapor-ilet";

while (true)
{
    enlem += 0.0001;
    boylam += 0.0001;

    // Karargahın beklediği veri formatında paketi hazırlıyoruz (JSON'a dönüşecek)
    var veriPaketi = new
    {
        BirlikAdi = "Birlik_Alpha",
        Enlem = enlem,
        Boylam = boylam,
        Saglik = saglik
    };

    try
    {
        // Veriyi Karargaha fırlatıyoruz (HTTP POST işlemi)
        var response = await client.PostAsJsonAsync(karargahUrl, veriPaketi);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"[TX BAŞARILI] Birlik_Alpha | Enlem: {enlem:F4} - Boylam: {boylam:F4} Karargaha İletildi.");
        }
        else
        {
            Console.WriteLine($"[TX HATA] Karargah frekansı reddetti! Hata Kodu: {response.StatusCode}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[BAĞLANTI KOPTU] Karargaha ulaşılamıyor. Sunucu kapalı olabilir. Detay: {ex.Message}");
    }

   
    await Task.Delay(1000);
}