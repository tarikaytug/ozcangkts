using System;

namespace Gkts.Karargah
{
    public class TaktikVeriPaketi
    {
        // Veritabanı için benzersiz kimlik (Primary Key)
        public int Id { get; set; }
        public string BirlikAdi { get; set; } = string.Empty;
        public double Enlem { get; set; }
        public double Boylam { get; set; }
        public int Saglik { get; set; }

        // Verinin merkeze düştüğü anı damgalıyoruz
        public DateTime KayitZamani { get; set; } = DateTime.UtcNow;
    }
}