using LiveCharts;
using LiveCharts.Wpf;
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

namespace RestoranOtomasyonu.OtherWindows
{
    /// <summary>
    /// GunSonuRaporu.xaml etkileşim mantığı
    /// </summary>
    public partial class GunSonuRaporu : Window
    {

        public Func<double, string> Formatter { get; set; }
        public GunSonuRaporu()
        {
            InitializeComponent();
            OrnekVerileriYukle();
            UrunBazliRaporYukle();
        }

        private void OrnekVerileriYukle()
        {
            // 1. ÜST KARTLAR (Finansal Özet)
            txtToplamCiro.Text = "₺ 12.450,50";
            txtNakit.Text = "₺ 4.120,00";
            txtKart.Text = "₺ 8.330,50";

            // 2. SOL PANEL - EN ÇOK SATANLAR (Pie Chart)
            ChartUrunler.Series = new SeriesCollection
            {
                new PieSeries { Title = "Adana Kebap", Values = new ChartValues<int> { 45 }, DataLabels = true, Fill = Brushes.OrangeRed },
                new PieSeries { Title = "Lahmacun", Values = new ChartValues<int> { 82 }, DataLabels = true, Fill = Brushes.Goldenrod },
                new PieSeries { Title = "İskender", Values = new ChartValues<int> { 28 }, DataLabels = true, Fill = Brushes.Crimson },
                new PieSeries { Title = "Ayran", Values = new ChartValues<int> { 110 }, DataLabels = true, Fill = Brushes.DeepSkyBlue },
                new PieSeries { Title = "Sütlaç", Values = new ChartValues<int> { 35 }, DataLabels = true, Fill = Brushes.MediumPurple }
            };

            // 3. SAĞ PANEL - SAATLİK TREND (Column Chart)
            ChartTrend.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Satış Tutarı",
                    Values = new ChartValues<decimal> { 120, 250, 850, 1400, 900, 450, 600, 800, 1800, 2400, 1600, 1100, 500, 200 },
                    Fill = (Brush)new BrushConverter().ConvertFromString("#FF00FF7F") // Senin o meşhur yeşilin
                }
            };

            // Y ekseni için para birimi formatı
            Formatter = value => "₺" + value.ToString("N2");
            DataContext = this;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void CloseButton_Click(object sender, RoutedEventArgs e) => this.Close();

        // Grafik üzerine tıklandığında detay vermek istersen:
        private void ChartUrunler_DataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show($"Ürün: {chartPoint.SeriesView.Title}\nSatış Adedi: {chartPoint.Y}", "Ürün Detayı");
        }


        // Örnek bir Model Sınıfı
        public class UrunRaporuModel
        {
            public string UrunAd { get; set; }
            public string Kategori { get; set; }
            public int Adet { get; set; }
            public string BirimFiyat { get; set; }
            public string ToplamTutar { get; set; }
        }

        // DataGrid'i doldurmak için örnek kod (OrnekVerileriYukle içine ekleyebilirsin)
        private void UrunBazliRaporYukle()
        {
            List<UrunRaporuModel> liste = new List<UrunRaporuModel>
    {
        new UrunRaporuModel { UrunAd="Adana Kebap", Kategori="Kebaplar", Adet=42, BirimFiyat="₺240,00", ToplamTutar="₺10.080,00" },
        new UrunRaporuModel { UrunAd="Mercimek Çorbası", Kategori="Çorbalar", Adet=15, BirimFiyat="₺80,00", ToplamTutar="₺1.200,00" },
        new UrunRaporuModel { UrunAd="Ayran 200ml", Kategori="İçecekler", Adet=50, BirimFiyat="₺25,00", ToplamTutar="₺1.250,00" }
    };
            dgUrunRaporu.ItemsSource = liste;
        }

    }
}
