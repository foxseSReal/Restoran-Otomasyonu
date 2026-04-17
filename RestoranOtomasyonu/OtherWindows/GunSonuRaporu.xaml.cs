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
        // Veritabanı bağlantısı
        private RESTORANDBEntities db = new RESTORANDBEntities();
        public string[] GrafikEtiketleri { get; set; }

        // Grafiklerde para birimi formatı için
        public Func<double, string> Formatter { get; set; }

        public GunSonuRaporu()
        {
            InitializeComponent();

            // ISSUE: Varsayılan Tarih Atama (Sayfa açıldığında bugünü getirir)
            dtBaslangic.SelectedDate = DateTime.Today;
            dtBitis.SelectedDate = DateTime.Today;

            // Grafik formatlayıcıyı ayarla
            Formatter = value => "₺" + value.ToString("N2");
            this.DataContext = this;

            // İlk verileri yükle
            RaporuYukle(DateTime.Today, DateTime.Today);
        }

        /// <summary>
        /// Belirtilen tarih aralığına göre tüm rapor verilerini günceller.
        /// </summary>
        private void RaporuYukle(DateTime baslangic, DateTime bitis)
        {
            try
            {
                // Tarih aralığını tam gün kapsayacak şekilde düzelt (00:00:00 ile 23:59:59 arası)
                DateTime baslangicSorgu = baslangic.Date;
                DateTime bitisSorgu = bitis.Date.AddDays(1).AddTicks(-1);

                // 1. FİNANSAL ÖZET (Üst Kartlar)
                // ---------------------------------------------------------
                var odemeler = db.TblADISYON_ODEME
                    .Where(x => x.Tarih >= baslangicSorgu && x.Tarih <= bitisSorgu)
                    .AsNoTracking()
                    .ToList();

                decimal nakit = odemeler.Where(x => x.OdemeTuru == "Nakit").Sum(x => (decimal?)x.OdenenTutar) ?? 0;
                decimal kart = odemeler.Where(x => x.OdemeTuru == "Kredi Kartı").Sum(x => (decimal?)x.OdenenTutar) ?? 0;
                decimal toplamCiro = nakit + kart;

                txtToplamCiro.Text = toplamCiro.ToString("C2");
                txtNakit.Text = nakit.ToString("C2");
                txtKart.Text = kart.ToString("C2");
                txtToplamIndirim.Text = "₺ 0,00"; // İndirim tablonuz varsa buraya bağlayabilirsiniz.


                // 2. ÜRÜN DAĞILIMI (Pie Chart - En Çok Satan 5 Ürün)
                // ---------------------------------------------------------
                var urunSatisSorgu = db.TblADISYON_DETAY
                    .Where(x => x.TblADISYON.KapanisZamani >= baslangicSorgu && x.TblADISYON.KapanisZamani <= bitisSorgu)
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


                // 3. SATIŞ TRENDİ (Günlük Ciro Analizi)
                // ---------------------------------------------------------
                var gunlukVeriler = odemeler
                    .GroupBy(x => x.Tarih.Value.Date) // Sadece güne göre grupla (saati görmezden gel)
                    .Select(g => new {
                        TarihEtiketi = g.Key.ToString("dd.MM.yyyy"), // İstediğin format: 17.04.2026
                        ToplamTutar = g.Sum(s => s.OdenenTutar)
                    })
                    .OrderBy(o => DateTime.ParseExact(o.TarihEtiketi, "dd.MM.yyyy", null)) // Tarih sırasına göre diz
                    .ToList();

                // Etiketleri grafiğin X eksenine gönder
                GrafikEtiketleri = gunlukVeriler.Select(x => x.TarihEtiketi).ToArray();

                ChartTrend.Series = new SeriesCollection
{
    new ColumnSeries
    {
        Title = "Günlük Toplam Satış",
        Values = new ChartValues<decimal>(gunlukVeriler.Select(x => x.ToplamTutar)),
        Fill = (Brush)new BrushConverter().ConvertFromString("#FF00FF7F"),
        LabelPoint = point => $"{point.Y:C2}" // Sütun üzerine gelince tutarı göster
    }
};

                // Binding'lerin güncellenmesi için DataContext tazeleme
                this.DataContext = null;
                this.DataContext = this;

                // 4. ÜRÜN BAZLI DETAYLAR (DataGrid)
                // ---------------------------------------------------------
                // ÖNEMLİ: ToString() hatasını almamak için formatlama XAML tarafına bırakıldı.
                var gridVerisi = db.TblADISYON_DETAY
                    .Where(x => x.TblADISYON.KapanisZamani >= baslangicSorgu && x.TblADISYON.KapanisZamani <= bitisSorgu)
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
            catch (Exception ex)
            {
                MessageBox.Show("Rapor yüklenirken kritik hata: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // --- BUTON EVENTLERİ ---

        private void btnSorgula_Click(object sender, RoutedEventArgs e)
        {
            if (dtBaslangic.SelectedDate.HasValue && dtBitis.SelectedDate.HasValue)
            {
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

        // --- MODEL SINIFI ---
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