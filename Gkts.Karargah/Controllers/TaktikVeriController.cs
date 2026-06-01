using Microsoft.AspNetCore.Mvc;

namespace Gkts.Karargah.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaktikVeriController : ControllerBase
    {
        private readonly KarargahDbContext _context;

        // Veritabanı bağlantımızı Controller'a enjekte ediyoruz
        public TaktikVeriController(KarargahDbContext context)
        {
            _context = context;
        }

        [HttpPost("rapor-ilet")]
        public IActionResult RaporAl([FromBody] TaktikVeriPaketi gelenVeri)
        {
            // Veriyi Entity Framework aracılığıyla tabloya ekliyoruz
            _context.TaktikVeriler.Add(gelenVeri);

            // Değişiklikleri veritabanına mühürlüyoruz (SQL INSERT çalışıyor)
            _context.SaveChanges();

            Console.WriteLine($"[KARARGAH DB] Birlik: {gelenVeri.BirlikAdi} başarıyla kaydedildi.");

            return Ok("Veri merkeze ulaştı ve veritabanına işlendi.");
        }

        [HttpGet("guncel-konumlar")]
        public IActionResult GuncelKonumlariGetir()
        {
            // Tüm veritabanını çekmek intihardır. 
            // Sadece en son gelen 50 taktik veriyi, zamana göre yeninden eskiye sıralayarak çekiyoruz.
            var sonVeriler = _context.TaktikVeriler
                .OrderByDescending(veri => veri.KayitZamani)
                .Take(50)
                .ToList(); // Veritabanından (PostgreSQL) veriyi liste olarak koparır.

            // Çekilen verileri JSON formatında harita arayüzüne (istek atana) paketleyip gönderiyoruz
            return Ok(sonVeriler);
        }
    }
}