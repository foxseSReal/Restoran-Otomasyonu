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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoranOtomasyonu.Pages
{
    /// <summary>
    /// UrunlerSayfa.xaml etkileşim mantığı
    /// </summary>
    public partial class UrunlerSayfa : Page
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public UrunlerSayfa(int SecilenKategoriID)
        {
            InitializeComponent();
            UrunleriGoster(SecilenKategoriID);
        }
        public void UrunleriGoster(int SecilenKategoriID)
        {
            UrunlerPanel.Children.Clear();
            var urunler = (from x in db.TblURUN
                           where x.KategoriId == SecilenKategoriID
                           select new
                           {
                               x.UrunId,
                               x.UrunAdi
                           }).ToList();

            foreach (var item in urunler)
            {
                Button btn = new Button();
                btn.Style = (Style)FindResource("MasaButton");
                btn.Margin = new Thickness(10);
                var converter = new System.Windows.Media.BrushConverter();
                btn.Background = (System.Windows.Media.Brush)converter.ConvertFromString("#85dcdcdc");
                btn.Tag = item.UrunId;
                // btn.Click += UrunEkle_Click;

                StackPanel sp = new StackPanel();

                TextBlock tb = new TextBlock();
                tb.Text = item.UrunAdi;
                tb.FontSize = 28;

                tb.FontFamily = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/"), "/NewFonts/Modak-Regular.ttf#Modak");

                sp.Children.Add(tb);
                btn.Content = sp;
                UrunlerPanel.Children.Add(btn);
            }
        }

        // Urunler sayfasında ürünlere tıklanırsa yapılacak işlemler

        private void UrunEkle_Click(object sender, RoutedEventArgs e)
        {
           

        }
    }
}
