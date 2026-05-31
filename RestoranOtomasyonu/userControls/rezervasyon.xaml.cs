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

namespace RestoranOtomasyonu.userControls
{
    /// <summary>
    /// rezervasyon.xaml etkileşim mantığı
    /// </summary>
    public partial class rezervasyon : UserControl
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public rezervasyon()
        {
            InitializeComponent();
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await RezarvasyonListeleAsync();
            await SonRezervasyonuGetirAsync();
            MusteriComboBoxDoldur();
        }

        public async Task RezarvasyonListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblREZARVASYON
                                 .Include("TblMUSTERILER")
                                 .OrderByDescending(x => x.RezarvasyonId)
                                 .ToList()
                                 .Select(x => new
                                 {
                                     ID = x.RezarvasyonId,
                                     MusteriId = x.MusteriId,
                                     Müşteri = x.TblMUSTERILER != null ? x.TblMUSTERILER.Ad + " " + x.TblMUSTERILER.Soyad : "Bilinmiyor",
                                     MasaNo = x.MasaNoId,
                                     KişiS = x.KisiSayisi,
                                     Tarih = x.Tarih.ToString("dd.MM.yyyy"),
                                     Saat = x.Saat.ToString(@"hh\:mm"),
                                     Açıklama = x.Aciklama
                                 })
                                 .ToList();
                    }
                });

                rezervasyon_DataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }
        private async Task SonRezervasyonuGetirAsync()
        {
            try
            {
                var sonRezervasyon = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        var kayit = db.TblREZARVASYON
                                      .Include("TblMUSTERILER")
                                      .OrderByDescending(x => x.RezarvasyonId)
                                      .FirstOrDefault();

                        if (kayit != null)
                        {
                            // Veriyi UI'a taşımak için güvenli bir pakete koyuyoruz
                            return new
                            {
                                AdSoyad = kayit.TblMUSTERILER != null ? kayit.TblMUSTERILER.Ad + " " + kayit.TblMUSTERILER.Soyad : "Müşteri Silinmiş",
                                MasaNo = kayit.MasaNoId.ToString(),
                                KisiSayisi = kayit.KisiSayisi,
                                Aciklama = kayit.Aciklama,
                                Tarih = kayit.Tarih.ToString("dd.MM.yyyy"),
                                Saat = kayit.Saat.ToString(@"hh\:mm")
                            };
                        }
                        return null;
                    }
                });

                // Veri geldiyse ekrana bas
                if (sonRezervasyon != null)
                {
                    rezervasyon_Adsoyad.Text = sonRezervasyon.AdSoyad;
                    rezervasyon_MasaNo.Text = sonRezervasyon.MasaNo;
                    rezervasyonKisi_Sayisi.Value = sonRezervasyon.KisiSayisi;
                    rezervasyonAciklama.Text = sonRezervasyon.Aciklama;
                    rezervasyonTarih.Text = sonRezervasyon.Tarih;
                    rezervasyonSaat.Text = sonRezervasyon.Saat;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Son kayıt getirilirken hata: " + ex.Message);
            }
        }

        public void RezarvasyonBilgileriDoldur(int rezarvasyonId)
        {
            var bukim = db.TblREZARVASYON.Find(rezarvasyonId);
            if (bukim == null) return;
            rezervasyon_Adsoyad.SelectedValue = bukim.MusteriId;
            rezervasyon_MasaNo.Text = bukim.MasaNoId.ToString();
            rezervasyonKisi_Sayisi.Value = bukim.KisiSayisi;
            rezervasyonAciklama.Text = bukim.Aciklama?.ToString() ?? "";
            rezervasyonTarih.Text = bukim.Tarih.ToString("dd.MM.yyyy");
            rezervasyonSaat.Text = bukim.Saat.ToString(@"hh\:mm");
        }
        private void MusteriComboBoxDoldur()
        {
            // Müşteri bilgilerini çekip ComboBox'a dolduruyoruz
            var musteriler = db.TblMUSTERILER
                                   .Select(x => new
                                   {
                                       Id = x.MusteriId,
                                       AdSoyad = x.Ad + " " + x.Soyad
                                   }).ToList();

            rezervasyon_Adsoyad.ItemsSource = musteriler;
            rezervasyon_Adsoyad.DisplayMemberPath = "AdSoyad";
            rezervasyon_Adsoyad.SelectedValuePath = "Id";
            rezervasyon_Adsoyad.SelectedIndex = -1;
        }
        private void rezervasyonButton_Ekle_Click(object sender, RoutedEventArgs e)
        {
            if (rezervasyon_Adsoyad.SelectedValue == null)
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(rezervasyon_MasaNo.Text) || !int.TryParse(rezervasyon_MasaNo.Text, out int girilenMasaId))
            {
                MessageBox.Show("Lütfen geçerli bir masa numarası (sayı) giriniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var kontrolEdilenMasa = db.TblMASA.Find(girilenMasaId);
            if (kontrolEdilenMasa == null)
            {
                MessageBox.Show("Girilen numaraya ait bir masa veritabanında bulunamadı!", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (kontrolEdilenMasa.Statu == "D")
            {
                MessageBox.Show("Bu masa şu anda salonda aktif olarak DOLU! Dolu bir masaya rezervasyon yapılamaz.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var yeniRezervasyon = new TblREZARVASYON
                {
                    MusteriId = (int)rezervasyon_Adsoyad.SelectedValue,
                    MasaNoId = girilenMasaId,
                    KisiSayisi = (int)rezervasyonKisi_Sayisi.Value,
                    Tarih = DateTime.Parse(rezervasyonTarih.Text),
                    Saat = TimeSpan.Parse(rezervasyonSaat.Text),
                    Aciklama = rezervasyonAciklama.Text
                };
                db.TblREZARVASYON.Add(yeniRezervasyon);
                kontrolEdilenMasa.Statu = "R";
                db.SaveChanges();
                MessageBox.Show("Yeni Rezervasyon Başarıyla Eklendi ve Masa Rezerve Durumuna Getirildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);

                RezarvasyonListeleAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt esnasında bir hata oluştu! Lütfen alanları kontrol edin.\nHata Detayı: {ex.Message}", "Sistem Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void rezervasyon_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilmis = rezervasyon_DataGrid.SelectedItem;
            if (secilmis != null)
            {
                dynamic item = secilmis;
                int rezarvasyonId = item.ID;
                RezarvasyonBilgileriDoldur(rezarvasyonId);
                if (item.MusteriId != null)
                {
                    rezervasyon_Adsoyad.SelectedValue = item.MusteriId;
                }
            }
        }

        private void rezervasyonButton_Guncelle_Click(object sender, RoutedEventArgs e)
        {
            if (rezervasyon_DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz rezervasyonu tablodan seçiniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (rezervasyon_Adsoyad.SelectedValue == null)
            {
                MessageBox.Show("Lütfen geçerli bir müşteri seçiniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(rezervasyon_MasaNo.Text) || !int.TryParse(rezervasyon_MasaNo.Text, out int yeniMasaId))
            {
                MessageBox.Show("Lütfen geçerli bir masa numarası giriniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var guncellenecek = db.TblREZARVASYON.Find(((dynamic)rezervasyon_DataGrid.SelectedItem).ID);
                if (guncellenecek == null) return;
                int eskiMasaId = guncellenecek.MasaNoId;
                if (eskiMasaId != yeniMasaId)
                {
                    var yeniMasaKontrol = db.TblMASA.Find(yeniMasaId);
                    if (yeniMasaKontrol != null && yeniMasaKontrol.Statu == "D")
                    {
                        MessageBox.Show("Geçiş yapmak istediğiniz yeni masa şu anda DOLU!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var eskiMasa = db.TblMASA.Find(eskiMasaId);
                    if (eskiMasa != null) eskiMasa.Statu = "B";
                    if (yeniMasaKontrol != null) yeniMasaKontrol.Statu = "R";
                }
                else
                {
                    var mevcutMasa = db.TblMASA.Find(yeniMasaId);
                    if (mevcutMasa != null) mevcutMasa.Statu = "R";
                }
                guncellenecek.MusteriId = (int)rezervasyon_Adsoyad.SelectedValue;
                guncellenecek.MasaNoId = yeniMasaId;
                guncellenecek.KisiSayisi = (int)rezervasyonKisi_Sayisi.Value;
                guncellenecek.Tarih = DateTime.Parse(rezervasyonTarih.Text);
                guncellenecek.Saat = TimeSpan.Parse(rezervasyonSaat.Text);
                guncellenecek.Aciklama = rezervasyonAciklama.Text;
                db.SaveChanges();
                MessageBox.Show("Rezervasyon ve Masa Durumu Başarıyla Güncellendi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);

                RezarvasyonListeleAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void rezervasyonButton_Sil_Click(object sender, RoutedEventArgs e)
        {
            if (rezervasyon_DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz rezervasyonu tablodan seçiniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult sonuc = MessageBox.Show("Bu rezervasyonu silmek istediğinize emin misiniz?", "Onay", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (sonuc == MessageBoxResult.Yes)
            {
                try
                {
                    int silinecekId = ((dynamic)rezervasyon_DataGrid.SelectedItem).ID;
                    var silinecekRezervasyon = db.TblREZARVASYON.Find(silinecekId);

                    if (silinecekRezervasyon != null)
                    {
                        var bağlıMasa = db.TblMASA.Find(silinecekRezervasyon.MasaNoId);
                        if (bağlıMasa != null)
                        {
                            bağlıMasa.Statu = "B";
                        }
                        db.TblREZARVASYON.Remove(silinecekRezervasyon);

                        db.SaveChanges();
                        MessageBox.Show("Rezervasyon iptal edildi ve masa boşa çıkarıldı.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);

                        RezarvasyonListeleAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Silme işlemi esnasında hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void AlanlariTemizle()
        {
            rezervasyon_Adsoyad.Text = "";
            rezervasyon_MasaNo.Text = "";
            rezervasyonKisi_Sayisi.Value = 0;
            rezervasyonAciklama.Text = "";
            rezervasyonTarih.Text = "";
            rezervasyonSaat.Text = "";
        }

        private void rezervasyonAra_TextChanged(object sender, TextChangedEventArgs e)
        {
            var rezarvasyonara = rezervasyonAra.Text;
            var listele=db.TblMUSTERILER.Where(x => x.Ad.ToLower().Contains(rezarvasyonara) ||
                            x.Soyad.ToLower().Contains(rezarvasyonara))
                .Select(x => new
                {
                    ID = x.MusteriId,
                    ÜrünAdı = x.Ad,
                    Tutar = x.Soyad,
                    Telefon = x.Telefon,
                    Açıklama=x.Aciklama
                })
                .ToList();
            rezervasyon_DataGrid.ItemsSource = listele;


        }

        private void rezervasyonBtn_Filtrele_Click(object sender, RoutedEventArgs e)
        {
            DateTime ilktarih = RezarvasyonAralik.SelectedDate.HasValue ? RezarvasyonAralik.SelectedDate.Value : DateTime.MinValue;
            DateTime sontarih = RezarvasyonAralik2.SelectedDate.HasValue ? RezarvasyonAralik2.SelectedDate.Value : DateTime.MinValue;

           
            var veritabaniSonuclari = db.TblREZARVASYON
                .OrderByDescending(x => x.RezarvasyonId)
                .Where(x => x.Tarih >= ilktarih && x.Tarih <= sontarih)
                .Select(x => new
                {
                    ID = x.RezarvasyonId,
                    Müşteri = x.TblMUSTERILER.Ad + " " + x.TblMUSTERILER.Soyad,
                    MasaNo = x.MasaNoId,
                    KişiS = x.KisiSayisi,
                    Açıklama = x.Aciklama,
                    Tarih_Orjinal = x.Tarih,
                    Saat_Orjinal = x.Saat
                })
                .ToList();
            var formatlanmisSonuclar = veritabaniSonuclari.Select(x => new
            {
                ID = x.ID,
                Müşteri = x.Müşteri,
                MasaNo = x.MasaNo,
                KişiS = x.KişiS,
                Açıklama = x.Açıklama,
                Tarih = x.Tarih_Orjinal.ToString("dd.MM.yyyy"),
                Saat = x.Saat_Orjinal.ToString(@"hh\:mm")

            }).ToList();
            rezervasyon_DataGrid.ItemsSource = formatlanmisSonuclar;
        }

        private void rezervasyonBtn_Temizle_Click(object sender, RoutedEventArgs e)
        {
            rezervasyon_Adsoyad.SelectedValue = null;
            rezervasyon_Adsoyad.Text = string.Empty;
            rezervasyon_MasaNo.Text = string.Empty;
            rezervasyonAciklama.Text = string.Empty;
            rezervasyonTarih.Text = string.Empty;
            rezervasyonSaat.Text = string.Empty;
            rezervasyonKisi_Sayisi.Value = 1;
            rezervasyon_DataGrid.SelectedItem = null;
        }
        private void btnHizliMusteriEkle_Click(object sender, RoutedEventArgs e)
        {
            RestoranOtomasyonu.OtherWindows.MusteriEklemeWindow mstPencere = new RestoranOtomasyonu.OtherWindows.MusteriEklemeWindow();
            if (mstPencere.ShowDialog() == true)
            {
                var guncelMusteriler = db.TblMUSTERILER
                                   .Select(x => new
                                   {
                                       Id = x.MusteriId,
                                       AdSoyad = x.Ad + " " + x.Soyad
                                   }).ToList();

                rezervasyon_Adsoyad.ItemsSource = guncelMusteriler;

                MessageBox.Show("Müşteri listesi güncellendi. Yeni eklediğiniz kişiyi seçebilirsiniz.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
