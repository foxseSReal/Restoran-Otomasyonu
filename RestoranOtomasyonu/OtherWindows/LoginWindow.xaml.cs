using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.userControls;
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
using System.Windows.Threading;

namespace RestoranOtomasyonu.OtherWindows
{
    /// <summary>
    /// LoginWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        RESTORANDBEntities1 db = new RESTORANDBEntities1();
        public LoginWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void btnGiris_Click(object sender, RoutedEventArgs e)
        {
            string girilenKadi = txtUsername.Text;
            string girilenSifre = PasswordBox.Password;

            try
            {
                btnGiris.IsEnabled = false;
                prgBar.Visibility = Visibility.Visible;
                var user = await Task.Run(() =>
                {
                    return db.TBLKULLANICI.FirstOrDefault(k => k.KullaniciAdi == girilenKadi);
                });
                if (user != null && user.Sifre.Equals(girilenSifre, StringComparison.Ordinal))
                {
                    AktifKullanici.KullaniciID = user.KullaniciId;
                    AktifKullanici.AdSoyad = user.KullaniciAdSoyad;
                    AktifKullanici.Yetki = user.Yetki;
                    AktifKullanici.GunlukHarcamaYetki = user.GUNLUKHARCAMA;
                    AktifKullanici.MuhasebeYetki = user.MUHASEBE;
                    AktifKullanici.CekSenetYetki = user.CEKSENET;
                    AktifKullanici.SatisDurumuYetki = user.SATISDURUMU;
                    AktifKullanici.MusteriFirmaYetki = user.MUSTERIFIRMA;
                    AktifKullanici.PersonelYetki = user.PERSONEL;
                    AktifKullanici.RezarvasyonYetki = user.REZERVASYON;
                    AktifKullanici.StokYetki = user.STOK;
                    AktifKullanici.UrunlerYetki = user.URUNLER;

                    await Task.Delay(2500);

                    MasalarWindow MasalarWindow = new MasalarWindow();
                    MasalarWindow.Show();
                    this.Close();
                }
                else MessageBox.Show("Hatalı kullanıcı adı veya şifre.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş sırasında bir hata oluştu: " + ex.Message);
            }
            finally
            {
                btnGiris.IsEnabled = true;
                prgBar.Visibility = Visibility.Collapsed;
            }
        }

        private void AppClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Saat.Text = DateTime.Now.ToString("HH:mm");
        }
    }
}
