using RestoranOtomasyonu.Entity;
using System;
using System.Linq;
using System.Windows;

namespace RestoranOtomasyonu.OtherWindows
{
    public partial class MusteriEklemeWindow : Window
    {
        public MusteriEklemeWindow()
        {
            InitializeComponent();
        }

        private async void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            // 1. Basit Boşluk Kontrolleri
            if (string.IsNullOrWhiteSpace(txtMusteriAd.Text) || string.IsNullOrWhiteSpace(txtMusteriSoyad.Text))
            {
                MessageBox.Show("Lütfen müşteri adını ve soyadını boş bırakmayınız!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2. Entity Framework Nesnesini Oluşturma ve Doldurma
                using (var db = new RESTORANDBEntities())
                {
                    var yeniMusteri = new TblMUSTERILER
                    {
                        Ad = txtMusteriAd.Text.Trim(),
                        Soyad = txtMusteriSoyad.Text.Trim(),
                        Telefon = txtMusteriTelefon.Text.Trim(),
                        MasaId = 1,                          // Artık dışarıdan alınmıyor, doğrudan 1 kaydediliyor
                        Aciklama = txtAciklama.Text.Trim(),
                        Tarih = DateTime.Now.Date,
                        Saat = DateTime.Now.TimeOfDay,
                        Durum = true
                    };

                    // Veritabanına ekle ve kaydet
                    db.TblMUSTERILER.Add(yeniMusteri);
                    await db.SaveChangesAsync();
                }

                MessageBox.Show("Müşteri Başarıyla Veritabanına Kaydedildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt esnasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnIptal_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}