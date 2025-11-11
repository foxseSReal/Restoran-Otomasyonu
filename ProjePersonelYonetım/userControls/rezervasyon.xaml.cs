using ProjePersonelYonetım.Entity;
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

namespace ProjePersonelYonetım.userControls
{
    /// <summary>
    /// rezervasyon.xaml etkileşim mantığı
    /// </summary>
    public partial class rezervasyon : UserControl
    {
        RESTORANDBEntities2 db = new RESTORANDBEntities2();
        public rezervasyon()
        {
            InitializeComponent();
            RezarvasyonListele();
            var rezarvasyon = db.TblREZARVASYON.OrderByDescending(x => x.RezarvasyonId).FirstOrDefault();
            if (rezarvasyon != null)
            {
                rezervasyon_Adsoyad.Text = rezarvasyon.TblMUSTERILER.Ad + " " + rezarvasyon.TblMUSTERILER.Soyad;
                rezervasyon_MasaNo.Text = rezarvasyon.MasaNoId.ToString();
                rezervasyonKisi_Sayisi.Value = rezarvasyon.KisiSayisi;
                rezervasyonAciklama.Text = rezarvasyon.Aciklama.ToString();
                rezervasyonTarih.Text = rezarvasyon.Tarih.ToString("dd.MM.yyyy");
                rezervasyonSaat.Text = rezarvasyon.Saat.ToString(@"hh\:mm");

            }
        }
        //          OLUŞTURULAN METHODLAR         //    

        public void RezarvasyonListele()
        {
            var listele = db.TblREZARVASYON.OrderByDescending(x=>x.RezarvasyonId).ToList()
                 .Select(x => new
                 {
                     ID = x.RezarvasyonId,
                     Müşteri = x.TblMUSTERILER.Ad + " " + x.TblMUSTERILER.Soyad,
                     MasaNo = x.MasaNoId,
                     KişiS = x.KisiSayisi,
                     Tarih = x.Tarih.ToString("dd.MM.yyyy"),
                     Saat = x.Saat.ToString(@"hh\:mm"),
                     Açıklama = x.Aciklama
                 });
            rezervasyon_DataGrid.ItemsSource = listele;
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
            RezarvasyonListele();
        }

        private void rezervasyon_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RezarvasyonListele();

            // Seçili satırdan RezarvasyonId alınmalı
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

            // 2. Müşteri bulundu mu diye KONTROL ET
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
            RezarvasyonListele();
        }

        private void rezervasyonButton_Sil_Click(object sender, RoutedEventArgs e)
        {
            var silinecek = db.TblREZARVASYON.Find(((dynamic)rezervasyon_DataGrid.SelectedItem).ID);
            if (silinecek == null) return;
            db.TblREZARVASYON.Remove(silinecek);
            MessageBox.Show("Rezarvasyon Başarıyla Silinmiştir", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            db.SaveChanges();
            AlanlariTemizle();
            RezarvasyonListele();
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
