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

namespace ProjePersonelYonetım.userControls
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
            Stoklistele();
           var stok=db.TblSTOKHAREKET.OrderByDescending(x => x.StokHareketId).FirstOrDefault();
            if (stok != null)
            {
                stokUrun.Text = stok.TblURUN.UrunAdi;
                stokMiktari.Text = stok.Miktar.ToString();
                cbxBirim.Text = stok.BirimTuru;
                stokFiyat.Text = stok.BirimFiyat.ToString();
                stokAciklama.Text=stok.Aciklama;
                stokTarih.Text = stok.Tarih?.ToString("dd.MM.yyyy");
                stokSaat.Text = stok.Saat?.ToString(@"hh\:mm");
            }
        }
        public void Stoklistele()
        {
            var liste = from s in db.TblSTOKHAREKET.OrderByDescending(x=>x.StokHareketId)
                        select new
                        {
                            ID = s.StokHareketId,
                            ÜrünAdı = s.TblURUN.UrunAdi,
                            StokMiktarı = s.Miktar,
                            BirimTürü = s.BirimTuru,
                            BirimFiyat = s.BirimFiyat,
                            Aciklama = s.Aciklama,
                            Tarih = s.Tarih,
                            Saat = s.Saat

                        };
            stokDataGrid.ItemsSource = liste.ToList();
        }

        private void stokDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var urungetir = stokDataGrid.SelectedItem;
            if (urungetir != null)
            {
                stokUrun.Text = urungetir.GetType().GetProperty("ÜrünAdı").GetValue(urungetir, null).ToString();
                stokMiktari.Text = urungetir.GetType().GetProperty("StokMiktarı").GetValue(urungetir, null).ToString();
                cbxBirim.Text = urungetir.GetType().GetProperty("BirimTürü").GetValue(urungetir, null).ToString();
                stokFiyat.Text = urungetir.GetType().GetProperty("BirimFiyat").GetValue(urungetir, null).ToString();
                stokAciklama.Text = urungetir.GetType().GetProperty("Aciklama").GetValue(urungetir, null).ToString();
                stokTarih.Text = urungetir.GetType().GetProperty("Tarih").GetValue(urungetir, null).ToString();
                stokSaat.Text = urungetir.GetType().GetProperty("Saat").GetValue(urungetir, null).ToString();



            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//stok ekle
            TblSTOKHAREKET stok = new TblSTOKHAREKET();
            stok.TblURUN = db.TblURUN.FirstOrDefault(x => x.UrunAdi == stokUrun.Text);
            stok.Miktar = decimal.Parse(stokMiktari.Text);
            stok.BirimTuru = cbxBirim.Text;
            stok.BirimFiyat = decimal.Parse(stokFiyat.Text);
            stok.Aciklama = stokAciklama.Text;
            stok.Tarih = DateTime.Parse(stokTarih.Text);
            stok.Saat = TimeSpan.Parse(stokSaat.Text);
            db.TblSTOKHAREKET.Add(stok);
            db.SaveChanges();
            MessageBox.Show("Stok Ekleme İşlemi Başarılı", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            Stoklistele();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var stokguncelle = stokDataGrid.SelectedItem;
            if (stokguncelle != null)
            {
                var guncellenecek = db.TblSTOKHAREKET.Find(((dynamic)stokDataGrid.SelectedItem).ID);
                if (guncellenecek == null) return;
                guncellenecek.TblURUN = db.TblURUN.FirstOrDefault(x => x.UrunAdi == stokUrun.Text);
                guncellenecek.Miktar = decimal.Parse(stokMiktari.Text);
                guncellenecek.BirimTuru = cbxBirim.Text;
                guncellenecek.BirimFiyat = decimal.Parse(stokFiyat.Text);
                guncellenecek.Aciklama = stokAciklama.Text;
                guncellenecek.Tarih = DateTime.Parse(stokTarih.Text);
                guncellenecek.Saat = TimeSpan.Parse(stokSaat.Text);
                db.SaveChanges();
                MessageBox.Show("Stok Güncelleme İşlemi Başarılı", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                Stoklistele();
            }
        }

        private void stokAra_TextChanged(object sender, TextChangedEventArgs e)
        {
            var urunara = stokAra.Text;
            var filtreliListe = db.TblSTOKHAREKET.OrderByDescending(x=>x.UrunId)
                .Where(x => x.TblURUN.UrunAdi.ToLower().Contains(urunara))
                .Select(x => new
                {
                
                    ÜrünAdı = x.TblURUN.UrunAdi,
                    StokMiktarı = x.Miktar,
                    BirimTürü = x.BirimTuru,
                    BirimFiyat = x.BirimFiyat,
                    Aciklama = x.Aciklama,
                    Tarih = x.Tarih,
                    Saat = x.Saat
                })
                .ToList();
            stokDataGrid.ItemsSource = filtreliListe;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Tarih Fitrele
            DateTime ilktarih = stokAralık.SelectedDate.HasValue ? stokAralık.SelectedDate.Value : DateTime.MinValue;
            DateTime sontarih = stokAralık2.SelectedDate.HasValue ? stokAralık2.SelectedDate.Value : DateTime.MinValue;
            var liste = from s in db.TblSTOKHAREKET.OrderByDescending(x => x.StokHareketId)
                        .Where(x=>x.Tarih >= ilktarih && x.Tarih <= sontarih)
                        select new
                        {
                            ID = s.StokHareketId,
                            ÜrünAdı = s.TblURUN.UrunAdi,
                            StokMiktarı = s.Miktar,
                            BirimTürü = s.BirimTuru,
                            BirimFiyat = s.BirimFiyat,
                            Aciklama = s.Aciklama,
                            Tarih = s.Tarih,
                            Saat = s.Saat

                        };
            stokDataGrid.ItemsSource = liste.ToList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Temizle
            stokUrun.Text = "";
            stokAciklama.Text = "";
            stokFiyat.Text = "";
            stokMiktari.Text = "";
            stokAra.Text = "";
            stokTarih.Text = "";
            stokAralık.Text = "";
            stokAralık2.Text = "";
            stokSaat.Text = "";
            cbxBirim.Text = "";
            Stoklistele();
        }
    }
}
