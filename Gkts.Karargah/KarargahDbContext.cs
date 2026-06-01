using Microsoft.EntityFrameworkCore;

namespace Gkts.Karargah
{
    public class KarargahDbContext : DbContext
    {
        // Yapıcı metod: Ayarları (şifre, port vb.) dışarıdan almamızı sağlar
        public KarargahDbContext(DbContextOptions<KarargahDbContext> options) : base(options)
        {
        }

        // Veritabanındaki tablomuzun adı "TaktikVeriler" olacak
        public DbSet<TaktikVeriPaketi> TaktikVeriler { get; set; }
    }
}