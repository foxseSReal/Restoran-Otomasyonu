using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.OtherWindows;
using RestoranOtomasyonu.userControls;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Threading;

namespace RestoranOtomasyonu
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            UserControls.Content = new gunlukHarcama();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            lblSaat.Content = DateTime.Now.ToString("HH:mm:ss");
            lblTarih.Content = DateTime.Now.ToString("dd dddd yyyy", new System.Globalization.CultureInfo("tr-TR"));

        }
        private void appClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult cevap = MessageBox.Show(
                "Uygulamayi Kapatmak İstiyor musunuz?",
                "Uygulamayi Kapatacak mısın?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );
            if (cevap == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void gunlukHarcama(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new gunlukHarcama());
        }
        private void muhasebe(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new muhasebe());
        }
        private void cekSenet(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new cekSenet());
        }
        private void satisDurumu(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new satisDurumu());
        }
        private void Personel(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new Personel());
        }
        private void musteri(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new musteriFirma());
        }
        private void stok(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new stok());
        }
        private void urun(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new urunler());
        }
        private void rezervasyon(object sender, RoutedEventArgs e)
        {
            ContentArea.Children.Clear();
            ContentArea.Children.Add(new rezervasyon());
        }

        //Admin Yetkindirme Formunu öne çıkarma komutu.

        private void yetkilendir(object sender, RoutedEventArgs e)
        {
            Erisimizni yetkilendirme = new Erisimizni();
            yetkilendirme.Show();
        }

        private void UserControls_Loaded(object sender, RoutedEventArgs e)
        { 

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (AktifKullanici.Yetki == "k")
            {
                menuItemYetkilendirme.Visibility = Visibility.Collapsed;

                resimButton.Visibility = (AktifKullanici.Yetki == "a") ? Visibility.Visible : Visibility.Collapsed;
             
                btnGunlukHarcama.Visibility = (AktifKullanici.GunlukHarcamaYetki == true)
                                              ? Visibility.Visible : Visibility.Collapsed;

                btnMuhasebe.Visibility = (AktifKullanici.MuhasebeYetki == true)
                                         ? Visibility.Visible : Visibility.Collapsed;

                btnCekSenet.Visibility = (AktifKullanici.CekSenetYetki == true)
                                         ? Visibility.Visible : Visibility.Collapsed;

                btnSatisDurumu.Visibility = (AktifKullanici.SatisDurumuYetki == true)
                                            ? Visibility.Visible : Visibility.Collapsed;

                btnPersonel.Visibility = (AktifKullanici.PersonelYetki == true)
                                         ? Visibility.Visible : Visibility.Collapsed;

                btnMusteri.Visibility = (AktifKullanici.MusteriFirmaYetki == true)
                                        ? Visibility.Visible : Visibility.Collapsed;

                btnStok.Visibility = (AktifKullanici.StokYetki == true)
                                     ? Visibility.Visible : Visibility.Collapsed;

                btnUrun.Visibility = (AktifKullanici.UrunlerYetki == true)
                                     ? Visibility.Visible : Visibility.Collapsed;

            }
            else if (AktifKullanici.Yetki == "a")
            { 
                menuItemYetkilendirme.Visibility = Visibility.Visible;
                btnGunlukHarcama.Visibility = Visibility.Visible;
                btnMuhasebe.Visibility = Visibility.Visible;
                btnCekSenet.Visibility = Visibility.Visible;
                btnSatisDurumu.Visibility = Visibility.Visible;
                btnPersonel.Visibility = Visibility.Visible;
                btnMusteri.Visibility = Visibility.Visible;
                btnStok.Visibility = Visibility.Visible;
                btnUrun.Visibility = Visibility.Visible;
                btnRezervasyon.Visibility = Visibility.Visible;
            }
            lblTarih.Content = DateTime.Now.ToString("dd MMMM yyyy");
            lblSaat.Content = DateTime.Now.ToString("HH:mm");
        }
    }
    
}
