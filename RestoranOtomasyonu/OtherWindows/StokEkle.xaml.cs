using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.userControls;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace RestoranOtomasyonu.OtherWindows
{
    /// <summary>
    /// StokEkle.xaml etkileşim mantığı
    /// </summary>

    public partial class StokEkle : Window
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public StokEkle()
        {
            InitializeComponent();
            UrunListele();
            FirmaListele();

            urunFiyat.IsEnabled=false;

        }
        void FirmaListele()
        {
                var listele = (from x in db.TblFIRMA.OrderByDescending(x => x.FirmaId).ToList()
                               where x.Unvan == "Firma"
                               select new
                               {
                                   ID = x.FirmaId,
                                   MusteriFirma = x.FirmaAdi,
                                   Adres = x.Adres,
                                   Telefon = x.Telefon,
                                   TelefonIki = x.Telefonİki,
                                   WebSitesi = x.WebSitesi,
                                   VergiDairesi = x.VergiDairesi,
                                   HesapNo = x.HesapNo
                               }).ToList();
                firma_DataGrid.ItemsSource = listele;
        }
        void UrunListele()
        {

            var listele = db.TblURUN.OrderByDescending(x => x.UrunId)
                            .Select(x => new
                            {
                                ID = x.UrunId,
                                ÜrünAdı = x.UrunAdi,
                                Tutar = x.Fiyat,
                                Kategori = x.TblKATEGORI.KategoriAdi,

                            })
                            .ToList();
            urun_DataGrid.ItemsSource = listele;
        }
        void temizle()
        {
            urun_isim.Clear();
            urunFiyat.Clear();
            urun_Adet.Value = 0;
            cbxUrun_Firma.Text = "";
            cbxUrun_Kategori.Text = "";
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void urun_temizleButton_Click(object sender, RoutedEventArgs e)
        {
            temizle();
        }
        int firmaid;
        int urunid;
   
        private void urun_ekleButton_Click(object sender, RoutedEventArgs e)
        {
            string secilenFirmaAdi = cbxUrun_Firma.Text;
            var firma = db.TblFIRMA.FirstOrDefault(x => x.FirmaAdi == secilenFirmaAdi);

            if (firma == null)
            {
                MessageBox.Show("Lütfen geçerli bir firma seçiniz.");
                return;
            }
            var urun = db.TblURUN.Find(urunid);
            if (urun == null) return;
            TblFIRMAHAREKET uekle = new TblFIRMAHAREKET();
            uekle.UrunId = urunid;
            uekle.FirmaId = firma.FirmaId;
            uekle.Adet = Convert.ToInt16(urun_Adet.Value);
            uekle.Tarih = DateTime.Now;
            uekle.Tutar = uekle.Adet * urun.Fiyat;
            db.TblFIRMAHAREKET.Add(uekle);
            TblGIDER gider = new TblGIDER();
            gider.FirmaId = firma.FirmaId;
            var mudur = db.TblPERSONELLER.FirstOrDefault(x => x.Pozisyon == "Müdür");
            gider.PersonelId = (mudur != null) ? mudur.PersonelID : 1;
            gider.Aciklama = $"{firma.FirmaAdi} firmasından {urun.UrunAdi} alımı.";
            gider.Tarih = DateTime.Now;
            gider.Tutar =(decimal) uekle.Tutar; 
            gider.GiderTuru = "Stok Alım";
            db.TblGIDER.Add(gider);
            db.SaveChanges();
            MessageBox.Show("Stok Ekleme İşlemi Başarılı ve Gidere İşlendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            UrunListele();
            FirmaListele();
            temizle();
        }

        private void urun_Adet_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            urunFiyat.Text = (Convert.ToDecimal(db.TblURUN.Find(urunid).Fiyat * urun_Adet.Value).ToString());
        }

        private void firma_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            firmaid = (int)(firma_DataGrid.SelectedItem as dynamic).ID;
            var firmahareket = db.TblFIRMA.Find(firmaid);
            cbxUrun_Firma.Text = firmahareket.FirmaAdi;

        }

        private void urun_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            urunid = (int)(urun_DataGrid.SelectedItem as dynamic).ID;
            var urun = db.TblURUN.Find(urunid);
            urun_isim.Text = urun.UrunAdi;
            urunFiyat.Text = urun.Fiyat.ToString();
            cbxUrun_Kategori.Text = urun.TblKATEGORI.KategoriAdi;
        }
    }
}
