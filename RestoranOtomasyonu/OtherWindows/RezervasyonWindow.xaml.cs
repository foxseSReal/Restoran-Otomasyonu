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
    /// RezervasyonWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class RezervasyonWindow : Window
    {

        public int SeciliMasaId { get; set; }

        // 1. EKLENEN KISIM: Diğer pencerelerin saati okuyabilmesi için public değişkenimiz
        public string SecilenSaat { get; set; }

        public RezervasyonWindow(int masaId)
        {
            InitializeComponent();
            SeciliMasaId = masaId;

            // Masa numarasını başlığa yazdıralım (İsteğe bağlı, txtBaslik adında bir TextBlock varsa)
            // txtBaslik.Text = $"Masa {masaId} Rezervasyonu"; 

            // 1. Saatleri ComboBox'a dolduruyoruz (00'dan 23'e kadar)
            for (int i = 0; i < 24; i++)
            {
                cmbSaat.Items.Add(i.ToString("00")); // "00", "01", "02" formatında ekler
            }

            // 2. Dakikaları ComboBox'a dolduruyoruz (15'er dakikalık periyotlar)
            for (int i = 0; i < 60; i += 15)
            {
                cmbDakika.Items.Add(i.ToString("00")); // "00", "15", "30", "45"
            }

            // 3. Ekran açıldığında kutular boş görünmesin diye varsayılan bir saat seçiyoruz
            cmbSaat.SelectedIndex = 12; // 12'yi seçer
            cmbDakika.SelectedIndex = 0;  // 00'ı seçer (Yani ilk açılışta 12:00 görünür)
        }

        private void btnOnayla_Click(object sender, RoutedEventArgs e)
        {
            // 1. ComboBox'lardan saati metin olarak alıyoruz (Örn: "14:30")
            string secilenSaat = $"{cmbSaat.SelectedItem}:{cmbDakika.SelectedItem}";

            using (RESTORANDBEntities tazeDb = new RESTORANDBEntities())
            {
                var secilenMasa = tazeDb.TblMASA.FirstOrDefault(x => x.MasaId == SeciliMasaId);
                if (secilenMasa != null)
                {
                    secilenMasa.Statu = "R"; // Masayı rezerve yapıyoruz

                    // İŞTE YENİ SÜTUNUMUZ BURADA DEVREYE GİRİYOR!
                    secilenMasa.RezervasyonSaati = secilenSaat;

                    tazeDb.SaveChanges();
                }
            }

            // Pencereyi kapatıyoruz
            this.DialogResult = true;
        }



        private void btnIptal_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }


    }
}
