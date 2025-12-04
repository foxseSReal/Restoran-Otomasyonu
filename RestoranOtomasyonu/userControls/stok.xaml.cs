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
        RESTORANDBEntities db = new RESTORANDBEntities();
        public stok()
        {
            InitializeComponent();
            Stoklistele();
            var stok = db.TblFIRMAHAREKET.OrderByDescending(x => x.FirmaId).FirstOrDefault();
            if (stok != null)
            {
                firma_adi.Text = stok.TblFIRMA.FirmaAdi;
                urun_adi.Text = stok.TblURUN.UrunAdi;
                stokMiktari.Text = stok.Adet.ToString();
                stokFiyat.Text = stok.Tutar.ToString();
                stokAciklama.Text = stok.Aciklama;
                stokTarih.Text = stok.Tarih.ToString();
                
            }
        }
        public void Stoklistele()
        {
            var liste = from s in db.TblFIRMAHAREKET.OrderByDescending(x => x.FirmaId)
                        select new
                        {
                            ID = s.ID,
                            FirmaAdı = s.TblFIRMA.FirmaAdi,
                            ÜrünAdı = s.TblURUN.UrunAdi,
                            Miktarı = s.Adet,
                            Fiyat=s.Tutar,
                            Aciklama = s.Aciklama,
                            Tarih = s.Tarih,
                           

                        };
            stokDataGrid.ItemsSource = liste.ToList();
        }
        int secilenid;
        private void stokDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilen = stokDataGrid.SelectedItem;
            if (secilen == null)
                return; // veya uygun bir kullanıcı mesajı gösterin

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

            //ShowDialog metodu ile pencereyi modal olarak açıyoruz Kullici kapatmadan altta kalan forma gecis yapamayacak

            stokEkle.ShowDialog();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {// güncelle
            var secilen = stokDataGrid.SelectedItem;
            var secilenid = (int)(stokDataGrid.SelectedItem as dynamic).ID;

            var secilenStok = db.TblFIRMAHAREKET.Find(secilenid);
            if (secilenStok != null)
            {
                
                var firma = db.TblFIRMA.FirstOrDefault(x => x.FirmaAdi == firma_adi.Text);
                var urun = db.TblURUN.FirstOrDefault(x => x.UrunAdi == urun_adi.Text);

                secilenStok.Aciklama = stokAciklama.Text;
                secilenStok.Adet = int.Parse(stokMiktari.Text);
                secilenStok.Tutar = decimal.Parse(stokFiyat.Text);
                secilenStok.Tarih = DateTime.Parse(stokTarih.Text);

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
                else
                {
                   
                }

                MessageBox.Show("Stok Güncelleme İşlemi Başarılı", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                Stoklistele();
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
            //Tarih Fitrele
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
            //Temizle
            firma_adi.Text = "";
            urun_adi.Text = "";
            stokAciklama.Text = "";
            stokFiyat.Text = "";
            stokMiktari.Text = "";
            stokAra.Text = "";
            stokTarih.Text = "";
            stokAralık.Text = "";
            stokAralık2.Text = "";
            
            Stoklistele();
        }
        int urunid;
        private void stokMiktari_TextChanged(object sender, TextChangedEventArgs e)
        {
            
          
        }
    }
}
