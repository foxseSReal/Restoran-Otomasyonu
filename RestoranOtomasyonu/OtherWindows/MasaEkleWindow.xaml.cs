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
    /// MasaEkleWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MasaEkleWindow : Window
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public MasaEkleWindow()
        {
            InitializeComponent();
        }

        private void BtnKaydet_Click(object sender, RoutedEventArgs e)
        {
            string yeniMasaNoRaw = txtMasaNo.Text.Trim();

            // 1. Sayısal kontrolü TryParse ile yapıyoruz
            // Eğer girdi sayı değilse 'isSayı' false döner, sayıysa 'masaSayisi' değişkenine atanır.
            if (!int.TryParse(yeniMasaNoRaw, out int masaSayisi))
            {
                MessageBox.Show("Lütfen sadece sayısal bir masa numarası giriniz!", "Giriş Hatası", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var db = new RESTORANDBEntities())
            {
                try
                {
                    // Mükerrer Kayıt Kontrolü (Opsiyonel ama önerilir)
                    if (db.TblMASA.Any(m => m.MasaNo == masaSayisi))
                    {
                        MessageBox.Show("Bu masa numarası zaten kullanımda!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Stop);
                        return;
                    }

                    var yeniMasa = new TblMASA
                    {
                        MasaNo = masaSayisi, // Parse hatası riski ortadan kalktı
                        Aciklama = masaSayisi.ToString(),
                        Tutar = 0, // Decimal cast işlemine gerek kalmadan 0 yazabilirsin
                        Statu = "B",
                        Durum = true,
                        RezervasyonSaati = null
                    };

                    db.TblMASA.Add(yeniMasa);
                    db.SaveChanges();

                    MessageBox.Show($"{masaSayisi} numaralı masa sisteme eklendi.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    // InnerException kontrolü gerçek hatayı görmeni sağlar
                    string mesaj = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    MessageBox.Show("Veritabanı hatası: " + mesaj, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // İptal butonuna basıldığında pencere kapanır
        }
    }
}
