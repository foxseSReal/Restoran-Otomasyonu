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

            var listele = from x in db.TblFIRMA.OrderByDescending(x => x.FirmaId).ToList()
                          where x.Unvan == "Firma"
                          select new
                          {
                              ID = x.FirmaId,
                              MüşteriFirma = x.FirmaAdi,
                              Adres = x.Adres,
                              Telefon = x.Telefon,
                              Telefonİki = x.Telefonİki,
                              WebSitesi = x.WebSitesi,
                              VergiDairesi = x.VergiDairesi,
                              HesapNo = x.HesapNo

                          };
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
        
    }
}
