using RestoranOtomasyonu;
using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static RestoranOtomasyonu.MasalarWindow;

namespace RestoranOtomasyonu.OtherWindows
{
    /// <summary>
    /// Adisyon.xaml etkileşim mantığı
    /// </summary>
    /// 
    public class SepetItem : INotifyPropertyChanged
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; } // Masadaki toplam adet
        public decimal Fiyat { get; set; }
        public decimal Toplam { get { return Adet * Fiyat; } }
        public string EkstraNot { get; set; }
        public bool YeniEklendiMi { get; set; }

        private int _secilenAdet;
        public int SecilenAdet
        {
            get => _secilenAdet;
            set { _secilenAdet = value; OnPropertyChanged("SecilenAdet"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public partial class Adisyon : Window
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public int SeciliMasaId { get; set; }
        public AdisyonTipi GelenTip;
        public ObservableCollection<SepetItem> GuncelSepet = new ObservableCollection<SepetItem>();
        public Adisyon(int SecilenMasaId, AdisyonTipi Tip)
        {
            InitializeComponent();
            this.SeciliMasaId = SecilenMasaId;
            this.GelenTip = Tip;

            using (RESTORANDBEntities _context = new RESTORANDBEntities())
            {
                // DÜZELTME: Sorguyu MasaId üzerinden yapıyoruz
                var secilenMasa = _context.TblMASA.FirstOrDefault(m => m.MasaId == SecilenMasaId);

                if (secilenMasa != null)
                {
                    this.DataContext = secilenMasa;

                    if (Tip == AdisyonTipi.Paket)
                    {
                        // Eğer paketse her zaman "Paket" yazsın
                        AdisyonMasaYazisi.Text = $"Paket - {secilenMasa.MasaNo}";
                    }
                    else
                    {
                        // Masa ise veritabanındaki durumu (Masa 1, Bahçe 2 vb.) yazsın
                        AdisyonMasaYazisi.Text = $"{secilenMasa.NESNE_DURUMU} - {secilenMasa.MasaNo}";
                    }
                }
            }

            SiparisleriGetir();
            MenuFrame.Navigate(new KategoriSayfa());
        }

        private void Adisyon_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();
        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }
        private void btnIptal_Click(object sender, RoutedEventArgs e)
        {
            // 1. Seçilen (rozetli) ürünleri filtrele
            var secilenItems = GuncelSepet.Where(x => x.SecilenAdet > 0).ToList();

            if (secilenItems.Count == 0)
            {
                MessageBox.Show("Lütfen iptal etmek istediğiniz adetleri ürünlerin üzerine tıklayarak belirleyin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 2. Mesaj içeriğini (Liste şeklinde) hazırla
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("Aşağıdaki ürünler iptal edilecek. Onaylıyor musunuz?");
            sb.AppendLine("-----------------------------------------");
            foreach (var item in secilenItems)
            {
                sb.AppendLine($"{item.SecilenAdet}x {item.UrunAdi}");
            }

            // 3. Kullanıcıya Onay Sor
            MessageBoxResult result = MessageBox.Show(sb.ToString(), "İptal Onayı", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var aktifAdisyon = db.TblADISYON.FirstOrDefault(x => x.MasaId == SeciliMasaId && x.Durum == true);

                    foreach (var item in secilenItems.ToList()) // ToList() kullanarak koleksiyon değişimi hatasını önlüyoruz
                    {
                        // A. VERİTABANI İŞLEMLERİ (Eskiden eklenmiş ürünler için)
                        if (item.YeniEklendiMi == false && aktifAdisyon != null)
                        {
                            var dbDetay = db.TblADISYON_DETAY.FirstOrDefault(d => d.AdisyonId == aktifAdisyon.AdisyonId && d.UrunId == item.UrunId);

                            if (dbDetay != null)
                            {
                                if (item.SecilenAdet >= item.Adet)
                                    db.TblADISYON_DETAY.Remove(dbDetay); // Satırı tamamen sil
                                else
                                    dbDetay.Adet -= item.SecilenAdet; // Sadece adedi düşür
                            }
                        }

                        // B. LOKAL LİSTE (GuncelSepet) GÜNCELLEMESİ
                        if (item.SecilenAdet >= item.Adet)
                        {
                            GuncelSepet.Remove(item);
                        }
                        else
                        {
                            item.Adet -= item.SecilenAdet;
                            item.SecilenAdet = 0; // Yeşil rozeti kapat
                        }
                    }

                    // Değişiklikleri SQL'e işle
                    db.SaveChanges();

                    // 4. MASA DURUM KONTROLÜ
                    if (GuncelSepet.Count == 0)
                    {
                        var secilenMasa = db.TblMASA.FirstOrDefault(x => x.MasaId == SeciliMasaId);
                        if (secilenMasa != null)
                        {
                            secilenMasa.Statu = "B"; // 'B'oş statüsüne çek
                        }

                        if (aktifAdisyon != null)
                        {
                            aktifAdisyon.Durum = false; // Adisyonu kapat
                        }

                        db.SaveChanges(); // Statü değişikliğini SQL'e gönder
                    }

                    // 5. ANA EKRAN RENK GÜNCELLEME (En Kritik Kısım)
                    // MasalarWindow'u bul ve içindeki MasaRenklendir'i tetikle
                    var mWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.GetType().Name == "MasalarWindow");

                    if (mWindow != null)
                    {
                        // Reflection ile 'MasaRenklendir' metoduna kanca atıyoruz
                        var method = mWindow.GetType().GetMethod("MasaRenklendir");
                        method?.Invoke(mWindow, null);
                    }

                    // 6. FİNAL İŞLEMLERİ
                    if (GuncelSepet.Count == 0)
                    {
                        MessageBox.Show("Masa boşaltıldı ve tüm siparişler iptal edildi.", "Bilgi");
                        this.DialogResult = true; // Pencereyi açan yere başarı sinyali gönder
                        this.Close();
                    }
                    else
                    {
                        // Hala ürün kaldıysa sadece sayfayı tazele
                        SiparisleriGetir();
                        GenelToplamiHesapla();
                        MessageBox.Show("Seçilen ürünler başarıyla iptal edildi.", "Başarılı");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata");
                }
            }
        }
        private void SiparisList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Tıklanan nesnenin bir ListViewItem (satır) olup olmadığını buluyoruz
            var item = VisualTreeHelper.HitTest(SiparisList, e.GetPosition(SiparisList))?.VisualHit;
            while (item != null && !(item is ListViewItem))
                item = VisualTreeHelper.GetParent(item);

            if (item is ListViewItem listViewItem)
            {
                var data = listViewItem.Content as SepetItem;
                if (data != null)
                {
                    // MANTIK: Eğer seçilen miktar toplam miktardan azsa 1 artır.
                    // Eğer hepsi seçiliyse sıfırla (Başa döner)
                    if (data.SecilenAdet < data.Adet)
                    {
                        data.SecilenAdet++;
                    }
                    else
                    {
                        data.SecilenAdet = 0;
                    }

                    // Seçim yapıldığı için standart ListView seçimini iptal edebiliriz 
                    // veya görsel olarak kalmasını sağlayabiliriz.
                    e.Handled = true;
                }
            }
        }

        public void SiparisleriGetir()
        {
            GuncelSepet.Clear();
            var aktif = db.TblADISYON.FirstOrDefault(x => x.MasaId == SeciliMasaId && x.Durum == true);
            if (aktif != null)
            {
                var detaylar = (from d in db.TblADISYON_DETAY
                                join u in db.TblURUN on d.UrunId equals u.UrunId
                                where d.AdisyonId == aktif.AdisyonId
                                select new SepetItem
                                {
                                    UrunId = u.UrunId,
                                    UrunAdi = u.UrunAdi,
                                    Adet = (int)d.Adet,
                                    Fiyat = (decimal)d.Fiyat,
                                    YeniEklendiMi = false
                                }).ToList();

                foreach (var item in detaylar) GuncelSepet.Add(item);
            }
            SiparisList.ItemsSource = GuncelSepet;
            GenelToplamiHesapla();
        }
        private void btnOnayla_Click(object sender, RoutedEventArgs e)
        {
            // 1. Sadece "YeniEklendiMi == true" olanları filtrele
            var onaylanacakSiparisler = GuncelSepet.Where(x => x.YeniEklendiMi == true).ToList();

            if (onaylanacakSiparisler.Count == 0)
            {
                MessageBox.Show("Onaylanacak yeni bir sipariş yok!");
                return;
            }

            // 2. Masanın verisini en başta çekiyoruz (Hem durumunu öğrenmek hem de adisyona tipini yazmak için)
            var secilenMasa = db.TblMASA.FirstOrDefault(x => x.MasaId == SeciliMasaId);

            // 3. Masa adisyonu yoksa yeni bir tane aç
            var aktifAdisyon = db.TblADISYON.FirstOrDefault(x => x.MasaId == SeciliMasaId && x.Durum == true);

            if (aktifAdisyon == null)
            {
                aktifAdisyon = new TblADISYON();
                aktifAdisyon.MasaId = SeciliMasaId;
                aktifAdisyon.AcilisZamani = DateTime.Now;
                aktifAdisyon.Durum = true;

                // --- YENİ EKLENEN KISIM: SİPARİŞ TİPİNİ BELİRLEME ---
                if (GelenTip == AdisyonTipi.Paket)
                {
                    aktifAdisyon.SiparisTipi = "Paket";
                }
                else
                {
                    // Eğer paket değilse, masanın NESNE_DURUMU'nu (Bahçe, Salon vb.) aynen aktar
                    aktifAdisyon.SiparisTipi = secilenMasa?.NESNE_DURUMU ?? "Masa";
                }
                // ----------------------------------------------------

                db.TblADISYON.Add(aktifAdisyon);
                db.SaveChanges(); // ID ve SiparisTipi'ni SQL'e gönder
            }

            // 4. Bekleyen siparişleri veritabanına ekle
            foreach (var item in onaylanacakSiparisler)
            {
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

            // 5. Masa durumunu güncelle (Dolu yap)
            if (secilenMasa != null)
            {
                secilenMasa.Statu = "D";
            }

            db.SaveChanges(); // Tüm detayları ve statü değişikliğini kaydet

            SiparisleriGetir();

            MessageBox.Show("Siparişler başarıyla onaylandı!", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        // Adisyon.xaml.cs içinde
        public void GenelToplamiHesapla()
        {
            // 1. Ürünlerin toplamı
            decimal urunlerToplami = GuncelSepet.Sum(x => x.Toplam);

            // 2. SQL'den bu adisyona yapılmış ödemeleri çek
            var aktifAdisyon = db.TblADISYON.FirstOrDefault(x => x.MasaId == SeciliMasaId && x.Durum == true);
            decimal odenenMiktar = 0;

            if (aktifAdisyon != null)
            {
                odenenMiktar = db.TblADISYON_ODEME
                                 .Where(x => x.AdisyonId == aktifAdisyon.AdisyonId)
                                 .Sum(x => (decimal?)x.OdenenTutar) ?? 0;
            }

            // 3. Kalan borcu yeşil etikete yaz (0'ın altına düşmesin)
            decimal kalan = urunlerToplami - odenenMiktar;
            txtGenelToplam.Text = string.Format("₺ {0:N2}", kalan > 0 ? kalan : 0);
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

        private void OdemeButtonu_Click(object sender, RoutedEventArgs e)
        {
            // 1. KONTROL: Eğer zaten ödeme sayfasındaysak tekrar açma (Hataları önler)
            if (MenuFrame.Content is OdemeSayfa)
                return;

            // 2. KRİTİK NOKTA: txtGenelToplam'ı (Kalanı) değil, ürünlerin NET toplamını gönderiyoruz.
            // Çünkü sayfa zaten SQL'den ödemeleri çekip kendi düşecek.
            decimal urunlerinNetToplami = GuncelSepet.Sum(x => x.Toplam);

            // Eğer sepette ürün yoksa ödeme sayfasını hiç açma
            if (urunlerinNetToplami <= 0)
            {
                MessageBox.Show("Ödeme almak için önce ürün eklemelisiniz!", "Uyarı");
                return;
            }

            string gonderilecekTutar = urunlerinNetToplami.ToString();
            int gonderilecekMasaId = SeciliMasaId;

            // Sayfayı tek bir sefer ve doğru tutarla açıyoruz
            MenuFrame.Navigate(new OdemeSayfa(gonderilecekTutar, gonderilecekMasaId, this));
        }
    }
}
