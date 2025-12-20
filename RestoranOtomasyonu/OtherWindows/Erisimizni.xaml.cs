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

namespace RestoranOtomasyonu.OtherWindows
{
    /// <summary>
    /// Erisimizni.xaml etkileşim mantığı
    /// </summary>
    public partial class Erisimizni : Window
    {
        RESTORANDBEntities1 db = new RESTORANDBEntities1();
        public Erisimizni()
        {
            InitializeComponent();
        }

        private void thisClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void kaydetButtonu_Click(object sender, RoutedEventArgs e)
        {
            var newUser = db.TBLKULLANICI;
            TBLKULLANICI userAdd = new TBLKULLANICI();
            userAdd.KullaniciAdSoyad = kullanici_adSoyad.Text;
            userAdd.KullaniciEmail = kullanici_email.Text;
            userAdd.KullaniciAdi = kullanici_Adi.Text;
            userAdd.Sifre = PasswordBox.Password;

            /***********************************/
#warning    //Resim kaydi yapilmadi simdilik
            /***********************************/

            //Erişim Yetkileri
            userAdd.GUNLUKHARCAMA = toggle_gunlukHarcama.IsChecked == true;
            userAdd.MUHASEBE = toggle_muhasebe.IsChecked == true;
            userAdd.CEKSENET = toggle_cekSenet.IsChecked == true;
            userAdd.SATISDURUMU = toggle_satisDurumu.IsChecked == true;
            userAdd.PERSONEL = toggle_personel.IsChecked == true;
            userAdd.MUSTERIFIRMA = toggle_musteriFirma.IsChecked == true;
            userAdd.STOK = toggle_stok.IsChecked == true;
            userAdd.URUNLER = toggle_urunler.IsChecked == true;
            userAdd.REZERVASYON = toggle_rezervasyon.IsChecked == true;
            userAdd.VERITABANI = toggle_veriTabani.IsChecked == true;
            userAdd.YETKILENDIRMEYAP = toggle_yetkilendirmeYap.IsChecked == true;

            db.TBLKULLANICI.Add(userAdd);
            db.SaveChanges();
            Temizle();
        }

        private void Temizle()
        {
            kullanici_adSoyad.Clear();
            kullanici_email.Clear();
            kullanici_Adi.Clear();
            PasswordBox.Clear();
            toggle_gunlukHarcama.IsChecked = false;
            toggle_muhasebe.IsChecked = false;
            toggle_cekSenet.IsChecked = false;
            toggle_satisDurumu.IsChecked = false;
            toggle_personel.IsChecked = false;
            toggle_musteriFirma.IsChecked = false;
            toggle_stok.IsChecked = false;
            toggle_veriTabani.IsChecked = false;
        }

    }
}
