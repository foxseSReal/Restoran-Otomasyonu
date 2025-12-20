using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOtomasyonu.OtherWindows
{
    public  static class AktifKullanici
    {
        public static int KullaniciID { get; set; }
        public static string AdSoyad { get; set; }
        public static string Yetki { get; set; } // "a" veya "k"

        // Veritabanındaki izin sütunları (Nullable bool yaptık çünkü db'de null olabilir)
        public static bool? GunlukHarcamaYetki { get; set; }
        public static bool? MuhasebeYetki { get; set; }
        public static bool? CekSenetYetki { get; set; }
        public static bool? SatisDurumuYetki { get; set; }
        public static bool? PersonelYetki { get; set; }
        public static bool? MusteriFirmaYetki { get; set; }
        public static bool? StokYetki { get; set; }
        public static bool? UrunlerYetki { get; set; }
        public static bool? RezarvasyonYetki { get; set; }
    }
}
