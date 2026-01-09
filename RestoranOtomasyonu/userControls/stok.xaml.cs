using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.OtherWindows;
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
    /// stok.xaml etkileşim mantığı
    /// </summary>
    public partial class stok : UserControl
    {
        RESTORANDBEntities1 db = new RESTORANDBEntities1();
        public stok()
        {
            InitializeComponent();
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await StokListeleAsync();
        }
        public async Task StokListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities1())
                    {
                        return db.TblFIRMAHAREKET
                                 .OrderByDescending(x => x.ID)
                                 .ToList()
                                 .Select(s => new
                                 {
                                     ID = s.ID,
                                     FirmaAdı = s.TblFIRMA != null ? s.TblFIRMA.FirmaAdi : "Firma Silinmiş",
                                     ÜrünAdı = s.TblURUN != null ? s.TblURUN.UrunAdi : "Ürün Silinmiş",
                                     Miktarı = s.Adet ?? 0,
                                     Fiyat = s.Tutar ?? 0,
                                     Aciklama = s.Aciklama,
                                     Tarih = s.Tarih.HasValue ? s.Tarih.Value.ToString("dd.MM.yyyy") : "-"
                                 })
                                 .ToList();
                    }
                });

                stokDataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Stok listesi yüklenirken hata: " + ex.Message);
            }
        }
        int secilenid, urunid;
        private void stokDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilen = stokDataGrid.SelectedItem;
            if (secilen == null)
                return;
            var secilenid = (int)(secilen as dynamic).ID;
            var secilenStok = db.TblFIRMAHAREKET.Find(secilenid);

            if (secilenStok != null)
            {
                firma_adi.Text = secilenStok.TblFIRMA.FirmaAdi;
                urun_adi.Text = secilenStok.TblURUN.UrunAdi;
                stokMiktari.Text = secilenStok.Adet.ToString();
                stokFiyat.Text = secilenStok.Tutar.ToString();
                stokAciklama.Text = secilenStok.Aciklama;
                stokTarih.Text = secilenStok.Tarih.ToString();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {//stok ekle
         //    TblSTOKHAREKET stok = new TblSTOKHAREKET();
         //    stok.TblURUN = db.TblURUN.FirstOrDefault(x => x.UrunAdi == stokUrun.Text);
         //    stok.Miktar = decimal.Parse(stokMiktari.Text);
         //    stok.BirimTuru = cbxBirim.Text;
         //    stok.BirimFiyat = decimal.Parse(stokFiyat.Text);
         //    stok.Aciklama = stokAciklama.Text;
         //    stok.Tarih = DateTime.Parse(stokTarih.Text);
         //    stok.Saat = TimeSpan.Parse(stokSaat.Text);
         //    db.TblSTOKHAREKET.Add(stok);
         //    db.SaveChanges();
         //    MessageBox.Show("Stok Ekleme İşlemi Başarılı", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
         //    Stoklistele();

            // *** Stok Ekleme Komutu artik baska bir windowda yapilacak *** //

            StokEkle stokEkle = new StokEkle();
            stokEkle.ShowDialog();
            db = new RESTORANDBEntities1();
            StokListeleAsync();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var secilen = stokDataGrid.SelectedItem;
            if (secilen == null) return;

            var secilenid = (int)(stokDataGrid.SelectedItem as dynamic).ID;
            var secilenStok = db.TblFIRMAHAREKET.Find(secilenid);

            if (secilenStok != null)
            {
                int yeniAdet = 0;
                int.TryParse(stokMiktari.Text, out yeniAdet);
                var urun = db.TblURUN.Find(secilenStok.UrunId);
                decimal birimFiyat = 0;
                if (urun != null)
                {
                    birimFiyat = (decimal)urun.Fiyat;
                }
                secilenStok.Adet = yeniAdet;
                secilenStok.Tutar = yeniAdet * birimFiyat;
                secilenStok.Aciklama = stokAciklama.Text;
                secilenStok.Tarih = DateTime.Parse(stokTarih.Text);
                stokFiyat.Text = secilenStok.Tutar.ToString();
                db.SaveChanges();
                var kasa = db.TblGIDER.FirstOrDefault(x => x.GiderId == secilenStok.ID);

                if (kasa != null)
                {
                    kasa.FirmaId = secilenStok.FirmaId;
                    kasa.GiderTuru = "Stok Gideri";
                    kasa.Aciklama = secilenStok.Aciklama;
                    kasa.Tutar = (decimal)secilenStok.Tutar;
                    kasa.Tarih = secilenStok.Tarih;
                    db.SaveChanges();
                }

                MessageBox.Show("Adet güncellendi ve tutar yeniden hesaplandı.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                StokListeleAsync();
            }
        }
        private void stokAra_TextChanged(object sender, TextChangedEventArgs e)
        {
            var firmaara = stokAra.Text;
            var filtreliListe = db.TblFIRMAHAREKET.OrderByDescending(x => x.ID)
                .Where(x => x.TblFIRMA.FirmaAdi.ToLower().Contains(firmaara))
                .Select(x => new
                {
                    ID = x.ID,
                    FirmaAdı = x.TblFIRMA.FirmaAdi,
                    ÜrünAdı = x.TblURUN.UrunAdi,
                    Miktarı = x.Adet,
                    Fiyat = x.Tutar,
                    Aciklama = x.Aciklama,
                    Tarih = x.Tarih,
                })
                .ToList();
            stokDataGrid.ItemsSource = filtreliListe;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            DateTime ilktarih = stokAralık.SelectedDate.HasValue ? stokAralık.SelectedDate.Value : DateTime.MinValue;
            DateTime sontarih = stokAralık2.SelectedDate.HasValue ? stokAralık2.SelectedDate.Value : DateTime.MinValue;
            var liste = from s in db.TblFIRMAHAREKET.OrderByDescending(x => x.FirmaId)
                        .Where(x => x.Tarih >= ilktarih && x.Tarih <= sontarih)
                        select new
                        {
                            ID = s.ID,
                            FirmaAdı = s.TblFIRMA.FirmaAdi,
                            ÜrünAdı = s.TblURUN.UrunAdi,
                            Miktarı = s.Adet,
                            Fiyat = s.Tutar,
                            Aciklama = s.Aciklama,
                            Tarih = s.Tarih,

                        };
            stokDataGrid.ItemsSource = liste.ToList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            firma_adi.Text = "";
            urun_adi.Text = "";
            stokAciklama.Text = "";
            stokFiyat.Text = "";
            stokMiktari.Text = "";
            stokAra.Text = "";
            stokTarih.Text = "";
            stokAralık.Text = "";
            stokAralık2.Text = "";
            StokListeleAsync();
        }

    }
}
