using RestoranOtomasyonu.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using RestoranOtomasyonu.Pages;
using System.ComponentModel;

namespace RestoranOtomasyonu.OtherWindows
{
    /// <summary>
    /// Adisyon.xaml etkileşim mantığı
    /// </summary>
    /// 
    public class SepetItem
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Toplam { get { return Adet * Fiyat; } }
        public string EkstraNot { get; set; }

        // Bu çok kritik: Bu ürün veritabanında zaten var mı, yoksa demin butona basılarak YENİ mi eklendi?
        public bool YeniEklendiMi { get; set; }
    }

    public partial class Adisyon : Window
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public int SeciliMasaId { get; set; }
        public ObservableCollection<SepetItem> GuncelSepet = new ObservableCollection<SepetItem>();
        public Adisyon(int SecilenMasa)
        {
            InitializeComponent();
            SeciliMasaId = SecilenMasa;
            this.DataContext = SecilenMasa;
            RESTORANDBEntities _context = new RESTORANDBEntities();
            var secilenMasa = _context.TblMASA.FirstOrDefault(m => m.MasaNo == SecilenMasa);

            if (secilenMasa != null)
            {
                this.DataContext = secilenMasa;
            }
            SiparisleriGetir();
            // Adisyon sayfası açıldığında KategoriSayfa'ya yönlendirme yap
            MenuFrame.Navigate(new KategoriSayfa());
        }

        private void Adisyon_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        public void SiparisleriGetir()
        {
            GuncelSepet.Clear(); // Sepeti sıfırla

            var aktifAdisyon = db.TblADISYON.FirstOrDefault(x => x.MasaId == SeciliMasaId && x.Durum == true);

            if (aktifAdisyon != null)
            {
                // Masanın eski siparişlerini DB'den çekiyoruz
                var eskiSiparisler = (from d in db.TblADISYON_DETAY
                                      join u in db.TblURUN on d.UrunId equals u.UrunId
                                      where d.AdisyonId == aktifAdisyon.AdisyonId
                                      select new SepetItem
                                      {
                                          UrunId = u.UrunId,
                                          UrunAdi = u.UrunAdi,
                                          Adet = (int)d.Adet,
                                          Fiyat = (decimal)d.Fiyat,
                                          EkstraNot = "",
                                          YeniEklendiMi = false // Bunlar zaten DB'de var!
                                      }).ToList();

                // Çekilenleri bizim geçici listemize ekliyoruz
                foreach (var item in eskiSiparisler)
                {
                    GuncelSepet.Add(item);
                }
            }

            // XAML'daki ListView'e veri kaynağı olarak bu Dinamik Listeyi veriyoruz
            SiparisList.ItemsSource = GuncelSepet;
            GenelToplamiHesapla();
        }
        private void btnOnayla_Click(object sender, RoutedEventArgs e)
        {
            // Sadece "YeniEklendiMi == true" olanları filtrele
            var onaylanacakSiparisler = GuncelSepet.Where(x => x.YeniEklendiMi == true).ToList();

            if (onaylanacakSiparisler.Count == 0)
            {
                MessageBox.Show("Onaylanacak yeni bir sipariş yok!");
                return;
            }

            // Masa adisyonu yoksa aç
            var aktifAdisyon = db.TblADISYON.FirstOrDefault(x => x.MasaId == SeciliMasaId && x.Durum == true);
            if (aktifAdisyon == null)
            {
                aktifAdisyon = new TblADISYON();
                aktifAdisyon.MasaId = SeciliMasaId;
                aktifAdisyon.AcilisZamani = DateTime.Now;
                aktifAdisyon.Durum = true;
                db.TblADISYON.Add(aktifAdisyon);
                db.SaveChanges(); // ID almak için kaydet
            }

            // Bekleyen siparişleri veritabanına ekle
            foreach (var item in onaylanacakSiparisler)
            {
                // Belki bu adisyonda önceden aynı üründen vardır, varsa adetini güncelle
                var varOlanSiparis = db.TblADISYON_DETAY.FirstOrDefault(d => d.AdisyonId == aktifAdisyon.AdisyonId && d.UrunId == item.UrunId);

                if (varOlanSiparis != null)
                {
                    varOlanSiparis.Adet += item.Adet;
                }
                else
                {
                    TblADISYON_DETAY yeniSiparis = new TblADISYON_DETAY();
                    yeniSiparis.AdisyonId = aktifAdisyon.AdisyonId;
                    yeniSiparis.UrunId = item.UrunId;
                    yeniSiparis.Adet = item.Adet;
                    yeniSiparis.Fiyat = item.Fiyat;
                    db.TblADISYON_DETAY.Add(yeniSiparis);
                }
            }
            var secilenMasa = db.TblMASA.FirstOrDefault(x => x.MasaId == SeciliMasaId);

            if (secilenMasa != null)
            {
                // 2. Masanın SADECE "Statu" sütununu "D" (Dolu) olarak güncelliyoruz.
                // (Eğer veritabanındaki sütununuzun adı 'Statu' değil de başka bir şeyse burayı ona göre değiştirin)
                secilenMasa.Statu = "D";

                // 3. Değişikliği SQL'e fırlatıyoruz!
            }
            db.SaveChanges(); // Tüm yenilikleri tek seferde SQL'e çak!

            // İşlem bitince listeyi tazelemek için baştan yükle (Artık hepsi YeniEklendiMi = false olacak)
            SiparisleriGetir();

            MessageBox.Show("Siparişler başarıyla onaylandı ve mutfağa iletildi!", "Sipariş Alındı", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        public void GenelToplamiHesapla()
        {
            // Sepetteki (GuncelSepet) tüm ürünlerin "Toplam" özelliklerini LINQ ile topluyoruz
            decimal genelToplam = GuncelSepet.Sum(x => x.Toplam);

            // Çıkan sonucu x:Name verdiğimiz TextBlock'a formatlı şekilde yazdırıyoruz
            txtGenelToplam.Text = $"₺ {genelToplam:N2}";
        }

        private void RezervasyonButton_Click(object sender, RoutedEventArgs e)
        {
            // Rezervasyon penceresini oluştur ve aç
            RezervasyonWindow rezPenceresi = new RezervasyonWindow(SeciliMasaId);

            // Eğer garson Rezervasyon penceresinde "İptal" demez de "Onayla" (True) derse...
            if (rezPenceresi.ShowDialog() == true)
            {
                MessageBox.Show("Masa başarıyla rezerve edildi!", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);

                // Adisyon penceresini de kapat ki Masalar ekranı rengi güncellesin!
                this.Close();
            }
        }
    }
}
