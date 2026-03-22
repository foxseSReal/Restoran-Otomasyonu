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
    /// OdemeSayfa.xaml etkileşim mantığı
    /// </summary>
    public partial class OdemeSayfa : Page
    {
        public OdemeSayfa()
        {
            InitializeComponent();
        }

        private void PaymentMethod_Click(object sender, RoutedEventArgs e)
        {
            // Tıklanan butonu yakalıyoruz
            Button tıklananButon = sender as Button;

            if (tıklananButon != null)
            {
                // Butonun Content bilgisini (KART, NAKIT vb.) TextBlock'a yazıyoruz
                txt_OdemeSekli.Text = tıklananButon.Content.ToString();

                // Bonus: Eğer görseldeki o yeşil noktayı da değiştirmek istersen:
                // (Noktanın adı 'dotIcon' varsayalım)
                // dotIcon.Fill = tıklananButon.Background; 
            }
        }

        // Girilen rakamları burada tutacağız
        private string girilenTutar = "";

        // Rakamlara ve Noktaya Basıldığında
        private void Num_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string deger = btn.Content.ToString();

            // Nokta kontrolü: Birden fazla nokta girilmesini engelle
            if (deger == "." && girilenTutar.Contains(".")) return;

            // İlk rakam 0 ise ve nokta değilse temizle (05 yerine 5 yazması için)
            if (girilenTutar == "0" && deger != ".") girilenTutar = "";

            girilenTutar += deger;
            TutarGuncelle();
        }

        // "C" (Clear) Butonu: Her şeyi sıfırlar
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            girilenTutar = "0";
            TutarGuncelle();
        }

        // "⌫" (Backspace) Butonu: Son karakteri siler
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (girilenTutar.Length > 0)
            {
                girilenTutar = girilenTutar.Substring(0, girilenTutar.Length - 1);
            }

            if (girilenTutar == "") girilenTutar = "0";
            TutarGuncelle();
        }

        // Metni Formatlayıp TextBlock'a Yazdıran Fonksiyon
        private void TutarGuncelle()
        {
            // Ekranda ₺ sembolü ile gösteriyoruz
            txt_Tutar.Text = "₺" + girilenTutar;
        }

        // "ENTER" Butonu: Ödemeyi onayla
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            // Burada ödeme işlemini veritabanına kaydedecek kodlar gelecek
            MessageBox.Show($"{txt_OdemeSekli.Text} ile {txt_Tutar.Text} tutarında ödeme alındı!");
        }

        private void QuickAmount_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                // Butonun içeriğindeki "₺" sembolünü temizleyip sadece rakamı alıyoruz
                // Örn: "₺50" -> "50"
                string secilenTutar = btn.Content.ToString().Replace("₺", "").Trim();

                // Arka plandaki değişkenimizi güncelliyoruz ki 
                // sonrasında numpad ile ekleme yapmak istersek kaldığı yerden devam etsin
                girilenTutar = secilenTutar;

                // Ekrana yansıt
                TutarGuncelle();
            }
        }

    }
}
