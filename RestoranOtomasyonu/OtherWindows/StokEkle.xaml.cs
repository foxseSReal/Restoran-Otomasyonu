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
            var firma = db.TblFIRMA.Find(firmaid);
            if (firma == null)
            {
                MessageBox.Show("Firma bulunamadı: " + firmaid);
                return;
            }

            TblFIRMAHAREKET uekle = new TblFIRMAHAREKET();
            uekle.UrunId = urunid;
            uekle.FirmaId = cbxUrun_Firma.SelectedIndex;
            uekle.Adet = Convert.ToInt16(urun_Adet.Value);
            uekle.Tarih = DateTime.Now;
            uekle.Tutar = uekle.Adet * db.TblURUN.Find(urunid).Fiyat;
            db.TblFIRMAHAREKET.Add(uekle);

            //2 kasaya gider kaydı oluşturma
            TblGIDER gider = new TblGIDER();
            gider.FirmaId = cbxUrun_Firma.SelectedIndex ;
            gider.Aciklama = db.TblFIRMA.Find(firmaid).FirmaAdi + " Firmasından" + db.TblURUN.Find(urunid).UrunAdi + " Ürünü İçin Ödeme";
            gider.Tarih = DateTime.Now;
            gider.Tutar = (decimal)(uekle.Tutar);
            gider.GiderTuru = "Stok Alım";
           

           
            MessageBox.Show("Stok Ekleme İşlemi Başarılı");
            
            db.TblGIDER.Add(gider);
            db.SaveChanges();
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
            //firmahareket doldur
            firmaid = (int)(firma_DataGrid.SelectedItem as dynamic).ID;
            var firmahareket = db.TblFIRMA.Find(firmaid);
            cbxUrun_Firma.Text = firmahareket.FirmaAdi;

        }

        private void urun_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //urun doldur
            urunid = (int)(urun_DataGrid.SelectedItem as dynamic).ID;
            var urun = db.TblURUN.Find(urunid);
            urun_isim.Text = urun.UrunAdi;
            urunFiyat.Text = urun.Fiyat.ToString();
            cbxUrun_Kategori.Text = urun.TblKATEGORI.KategoriAdi;
        }
    }
}
