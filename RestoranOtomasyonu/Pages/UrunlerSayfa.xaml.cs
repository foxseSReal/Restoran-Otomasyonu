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
                btn.Click += UrunEkle_Click;

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

        private void UrunEkle_Click(object sender, RoutedEventArgs e)
        {
            Button secilenUrun = (Button)sender;
            int tiklananUrunId = Convert.ToInt32(secilenUrun.Tag);

            Adisyon anaPencere = (Adisyon)Window.GetWindow(this);
            if (anaPencere == null) return;

            // Ürün adını ve fiyatını öğreniyoruz
            var urunBilgisi = db.TblURUN.FirstOrDefault(u => u.UrunId == tiklananUrunId);

            // Acaba sepette bu üründen (YENİ eklenmiş olarak) var mı?
            var sepettekiUrun = anaPencere.GuncelSepet.FirstOrDefault(x => x.UrunId == tiklananUrunId && x.YeniEklendiMi == true);

            if (sepettekiUrun != null)
            {
                // Varsa sadece geçici sepetteki adeti artır
                sepettekiUrun.Adet += 1;
                // ListView'in yeni adeti ekranda göstermesi için yeniliyoruz
                anaPencere.SiparisList.Items.Refresh();
            }
            else
            {
                // Yoksa sepete yepyeni bir ürün olarak atıyoruz
                anaPencere.GuncelSepet.Add(new SepetItem
                {
                    UrunId = tiklananUrunId,
                    UrunAdi = urunBilgisi.UrunAdi,
                    Adet = 1,
                    Fiyat = urunBilgisi.Fiyat,
                    EkstraNot = "",
                    YeniEklendiMi = true // Bu ürün henüz onaylanmadı, DB'de yok!
                });
            }
            anaPencere.GenelToplamiHesapla();
        }
    }
}
