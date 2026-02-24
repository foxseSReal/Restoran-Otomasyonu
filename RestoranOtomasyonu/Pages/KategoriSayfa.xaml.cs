using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.OtherWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoranOtomasyonu.Pages
{
    /// <summary>
    /// KategoriSayfa.xaml etkileşim mantığı
    /// </summary>
    public partial class KategoriSayfa : Page
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public KategoriSayfa()
        {
            InitializeComponent();
            KategoriGoster();
        }

        public void KategoriGoster()
        {
            KategoriPanel.Children.Clear();
            var masalar = (from x in db.TblKATEGORI
                           select new
                           {
                               x.KategoriId,
                               x.KategoriAdi
                           }).ToList();

            foreach (var item in masalar)
            {
                Button btn = new Button();
                btn.Style = (Style)FindResource("MasaButton");
                btn.Margin = new Thickness(10);
                var converter = new System.Windows.Media.BrushConverter();
                btn.Background = (System.Windows.Media.Brush)converter.ConvertFromString("#85dcdcdc");
                btn.Tag = item.KategoriId;
                btn.Click += Kategori_Click;

                StackPanel sp = new StackPanel();

                TextBlock tb = new TextBlock();
                tb.Text = item.KategoriAdi;
                tb.FontSize = 28;

                tb.FontFamily = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/"), "/NewFonts/Modak-Regular.ttf#Modak");

                sp.Children.Add(tb);
                btn.Content = sp;
                KategoriPanel.Children.Add(btn);
            }
        }

        private void Kategori_Click(object sender, RoutedEventArgs e)
        {
            Button SecilenKategori = (Button)sender;
            int kategoriId = Convert.ToInt32(SecilenKategori.Tag);

            // Frame içinde yeni sayfaya gidiyoruz ve ID'yi yolluyoruz
            NavigationService.Navigate(new UrunlerSayfa(kategoriId));
        }
    }
}
