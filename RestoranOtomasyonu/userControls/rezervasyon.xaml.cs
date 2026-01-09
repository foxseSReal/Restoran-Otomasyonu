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
        RESTORANDBEntities1 db = new RESTORANDBEntities1();
        public rezervasyon()
        {
            InitializeComponent();
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await RezarvasyonListeleAsync();
            await SonRezervasyonuGetirAsync();
        }

        public async Task RezarvasyonListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities1())
                    {
                        return db.TblREZARVASYON
                                 .Include("TblMUSTERILER") 
                                 .OrderByDescending(x => x.RezarvasyonId)
                                 .ToList()
                                 .Select(x => new
                                 {
                                     ID = x.RezarvasyonId,
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
                    using (var db = new RESTORANDBEntities1())
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
            rezervasyon_Adsoyad.Text = bukim.TblMUSTERILER.Ad + " " + bukim.TblMUSTERILER.Soyad;
            rezervasyon_MasaNo.Text = bukim.MasaNoId.ToString();
            rezervasyonKisi_Sayisi.Value = bukim.KisiSayisi;
            rezervasyonAciklama.Text = bukim.Aciklama.ToString();
            rezervasyonTarih.Text = bukim.Tarih.ToString("dd.MM.yyyy");
            rezervasyonSaat.Text = bukim.Saat.ToString(@"hh\:mm");
        }
        private void rezervasyonButton_Ekle_Click(object sender, RoutedEventArgs e)
        {
            var yeniRezervasyon = new TblREZARVASYON
            {
                TblMUSTERILER = db.TblMUSTERILER.FirstOrDefault(x => x.Ad + " " + x.Soyad == rezervasyon_Adsoyad.Text),
                MasaNoId = int.Parse(rezervasyon_MasaNo.Text),
                KisiSayisi = (int)rezervasyonKisi_Sayisi.Value,
                Tarih = DateTime.Parse(rezervasyonTarih.Text),
                Saat = TimeSpan.Parse(rezervasyonSaat.Text),
                Aciklama = rezervasyonAciklama.Text
            };
            db.TblREZARVASYON.Add(yeniRezervasyon);
            MessageBox.Show("Yeni Rezarvasyon Başarıyla Eklendi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            db.SaveChanges();
            RezarvasyonListeleAsync();
        }

        private void rezervasyon_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RezarvasyonListeleAsync();
            var secilmis = rezervasyon_DataGrid.SelectedItem;
            if (secilmis != null)
            {
                dynamic item = secilmis;
                int rezarvasyonId = item.ID;
                RezarvasyonBilgileriDoldur(rezarvasyonId);
            }
        }

        private void rezervasyonButton_Guncelle_Click(object sender, RoutedEventArgs e)
        {
            var guncellenecek = db.TblREZARVASYON.Find(((dynamic)rezervasyon_DataGrid.SelectedItem).ID);
            if (guncellenecek == null) return;
            var musteri = db.TblMUSTERILER.FirstOrDefault(x => x.Ad + " " + x.Soyad == rezervasyon_Adsoyad.Text);
            if (musteri == null)
            {
                MessageBox.Show("Bu isimde bir müşteri bulunamadı!", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }

           
            guncellenecek.MusteriId = musteri.MusteriId;

            guncellenecek.MasaNoId = int.Parse(rezervasyon_MasaNo.Text);
            guncellenecek.KisiSayisi = (int)rezervasyonKisi_Sayisi.Value;
            guncellenecek.Tarih = DateTime.Parse(rezervasyonTarih.Text);
            guncellenecek.Saat = TimeSpan.Parse(rezervasyonSaat.Text);
            guncellenecek.Aciklama = rezervasyonAciklama.Text;

            db.SaveChanges();
            MessageBox.Show("Rezervasyon Güncelleme İşlemi Başarılı", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            RezarvasyonListeleAsync();
        }

        private void rezervasyonButton_Sil_Click(object sender, RoutedEventArgs e)
        {
            var silinecek = db.TblREZARVASYON.Find(((dynamic)rezervasyon_DataGrid.SelectedItem).ID);
            if (silinecek == null) return;
            db.TblREZARVASYON.Remove(silinecek);
            MessageBox.Show("Rezarvasyon Başarıyla Silinmiştir", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            db.SaveChanges();
            AlanlariTemizle();
            RezarvasyonListeleAsync();
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
    }
}
