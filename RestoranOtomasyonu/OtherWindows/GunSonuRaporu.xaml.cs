using LiveCharts;
using LiveCharts.Wpf;
using RestoranOtomasyonu.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace RestoranOtomasyonu.OtherWindows
{
    public partial class GunSonuRaporu : Window
    {
        private RESTORANDBEntities db = new RESTORANDBEntities();

        // X eksenindeki tarih etiketleri için property
        public string[] GrafikEtiketleri { get; set; }

        // Grafiklerdeki para formatı (₺) için property
        public Func<double, string> Formatter { get; set; }

        public GunSonuRaporu()
        {
            InitializeComponent();

            // 1. ADIM: Sayfa ilk açıldığında tarihleri bugüne ayarla
            dtBaslangic.SelectedDate = DateTime.Today;
            dtBitis.SelectedDate = DateTime.Today;

            // Para formatlayıcıyı tanımla
            Formatter = value => "₺" + value.ToString("N2");

            // Binding işlemlerinin çalışması için DataContext'i bu sayfaya bağla
            this.DataContext = this;

            // 2. ADIM: İlk açılış verilerini (bugün) yükle
            RaporuYukle(DateTime.Today, DateTime.Today);
        }

        private void RaporuYukle(DateTime baslangic, DateTime bitis)
        {
            try
            {
                DateTime baslangicSorgu = baslangic.Date;
                DateTime bitisSorgu = bitis.Date.AddDays(1).AddTicks(-1);

                // db yerine metodun içinde yeni bir context açıyoruz (using ile)
                using (var tazeDb = new RESTORANDBEntities())
                {
                    // 1. TÜM ÖDEMELERİ VE İNDİRİMLERİ VERİTABANINDAN ÇEK
                    var tumKayitlar = tazeDb.TblADISYON_ODEME
                        .Where(x => x.Tarih >= baslangicSorgu && x.Tarih <= bitisSorgu)
                        .AsNoTracking()
                        .ToList();

                    // Ödeme Türlerine Göre Filtrele (Büyük/Küçük Harf Duyarsız)
                    decimal nakit = tumKayitlar
                        .Where(x => x.OdemeTuru != null && x.OdemeTuru.Trim().Equals("Nakit", StringComparison.OrdinalIgnoreCase))
                        .Sum(x => x.OdenenTutar);

                    decimal kart = tumKayitlar
                        .Where(x => x.OdemeTuru != null && x.OdemeTuru.Trim().Equals("Kart", StringComparison.OrdinalIgnoreCase))
                        .Sum(x => x.OdenenTutar);

                    // OdemeTuru kolonunda 'İndirim' yazan kayıtların toplamı
                    decimal toplamIndirim = tumKayitlar
                        .Where(x => x.OdemeTuru != null && x.OdemeTuru.Trim().Equals("İNDİRİM", StringComparison.OrdinalIgnoreCase))
                        .Sum(x => x.OdenenTutar);

                    // Net Kasaya Giren Toplam Ciro (Nakit + Kart)
                    decimal toplamCiro = nakit + kart;

                    // UI Elementlerini Güncelle
                    txtToplamCiro.Text = toplamCiro.ToString("C2");
                    txtNakit.Text = nakit.ToString("C2");
                    txtKart.Text = kart.ToString("C2");
                    txtToplamIndirim.Text = toplamIndirim.ToString("C2");


                    // 2. GRAFİKLER VE DATA GRID İÇİN ADİSYON ID LİSTESİ
                    // Tip uyuşmazlığı hatasını (int? - int) engellemek için listeyi int? yapıyoruz
                    var adisyonIdListesi = tumKayitlar.Select(o => (int?)o.AdisyonId).Distinct().ToList();


                    // 3. ÜRÜN DAĞILIMI (Pasta Grafiği)
                    var urunSatisSorgu = tazeDb.TblADISYON_DETAY
                        .Where(x => adisyonIdListesi.Contains(x.AdisyonId))
                        .GroupBy(x => x.TblURUN.UrunAdi)
                        .Select(g => new { Isim = g.Key, Adet = g.Sum(s => s.Adet) ?? 0 })
                        .OrderByDescending(o => o.Adet)
                        .Take(5)
                        .ToList();

                    SeriesCollection pieSeries = new SeriesCollection();
                    foreach (var item in urunSatisSorgu)
                    {
                        pieSeries.Add(new PieSeries { Title = item.Isim, Values = new ChartValues<int> { item.Adet }, DataLabels = true });
                    }
                    ChartUrunler.Series = pieSeries;


                    // 4. SATIŞ TRENDİ (Grafik)
                    // Trend grafiğinde indirim ödemelerini hariç tutarak gerçek ciroyu gösteriyoruz
                    var gunlukVeriler = tumKayitlar
                        .Where(x => x.OdemeTuru != null && !x.OdemeTuru.Trim().Equals("İNDİRİM", StringComparison.OrdinalIgnoreCase))
                        .GroupBy(x => x.Tarih.Value.Date)
                        .Select(g => new {
                            TarihEtiketi = g.Key.ToString("dd.MM.yyyy"),
                            ToplamTutar = g.Sum(s => s.OdenenTutar)
                        })
                        .OrderBy(o => DateTime.ParseExact(o.TarihEtiketi, "dd.MM.yyyy", null))
                        .ToList();

                    GrafikEtiketleri = gunlukVeriler.Select(x => x.TarihEtiketi).ToArray();

                    ChartTrend.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Günlük Net Ciro",
                    Values = new ChartValues<decimal>(gunlukVeriler.Select(x => x.ToplamTutar)),
                    Fill = (Brush)new BrushConverter().ConvertFromString("#FF00FF7F"),
                    LabelPoint = point => $"{point.Y:C2}"
                }
            };

                    // LiveCharts kütüphanesinin arayüzü tetiklemesi için DataContext tazeleme
                    this.DataContext = null;
                    this.DataContext = this;


                    // 5. DATA GRID VERİSİ
                    var gridVerisi = tazeDb.TblADISYON_DETAY
                        .Where(x => adisyonIdListesi.Contains(x.AdisyonId))
                        .GroupBy(x => new { x.TblURUN.UrunAdi, x.TblURUN.TblKATEGORI.KategoriAdi, x.Fiyat })
                        .Select(g => new UrunRaporuModel
                        {
                            UrunAd = g.Key.UrunAdi,
                            Kategori = g.Key.KategoriAdi,
                            Adet = g.Sum(s => s.Adet) ?? 0,
                            BirimFiyat = g.Key.Fiyat ?? 0,
                            ToplamTutar = g.Sum(s => (s.Adet ?? 0) * (s.Fiyat ?? 0))
                        })
                        .OrderByDescending(o => o.ToplamTutar)
                        .ToList();

                    dgUrunRaporu.ItemsSource = gridVerisi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        // --- BUTON EVENTLERİ ---

        private void btnSorgula_Click(object sender, RoutedEventArgs e)
        {
            if (dtBaslangic.SelectedDate.HasValue && dtBitis.SelectedDate.HasValue)
            {
                // Seçilen tarihlere göre tüm sayfayı yeniden yükle
                RaporuYukle(dtBaslangic.SelectedDate.Value, dtBitis.SelectedDate.Value);
            }
            else
            {
                MessageBox.Show("Lütfen tarih aralığı seçiniz.");
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void ChartUrunler_DataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show($"Ürün: {chartPoint.SeriesView.Title}\nAdet: {chartPoint.Y}", "Hızlı Bilgi");
        }

        public class UrunRaporuModel
        {
            public string UrunAd { get; set; }
            public string Kategori { get; set; }
            public int Adet { get; set; }
            public decimal BirimFiyat { get; set; }
            public decimal ToplamTutar { get; set; }
        }
    }
}