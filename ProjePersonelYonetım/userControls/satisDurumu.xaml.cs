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
    /// satisDurumu.xaml etkileşim mantığı
    /// </summary>
    public partial class satisDurumu : UserControl
    {
        RESTORANDBEntities2 db = new RESTORANDBEntities2();
        
        public satisDurumu()
        {
            InitializeComponent();
            SatisListele();
            var terslistele=db.TblURUN.OrderByDescending(x=>x.UrunId).FirstOrDefault();
        }

        public void SatisListele()
        {
            var listele =db.TblURUN.OrderByDescending(x=>x.UrunId).ToList()
                          .Select(x => new
                          {
                              ID = x.UrunId,
                              ÜrünAdı= x.UrunAdi,
                              Tutar = x.Fiyat,
                              Kategori=x.TblKATEGORI.KategoriAdi,
                              SatılanAdet= x.StokMiktari,
                              Birim=x.Birim,
                              Tarih = x.EklenmeTarihi,
                          });
            satis_DataGrid.ItemsSource = listele;

        }

    

        private void satisara_TextChanged(object sender, TextChangedEventArgs e)
        {
          
            var satisara = txtatisara.Text;
            var listele = db.TblURUN.OrderByDescending(x => x.UrunId).Where(x => x.UrunAdi.ToLower().Contains(satisara))
                         .Select(x => new
                         {
                             ID = x.UrunId,
                             ÜrünAdı = x.UrunAdi,
                             Tutar = x.Fiyat,
                             Kategori = x.TblKATEGORI.KategoriAdi,
                             SatılanAdet = x.StokMiktari,
                             Birim = x.Birim,
                             Tarih = x.EklenmeTarihi,
                         }).ToList();
            satis_DataGrid.ItemsSource = listele;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Tarih Fitrele
            DateTime ilktarih = satis_BaslanicTarih.SelectedDate.HasValue ? satis_BaslanicTarih.SelectedDate.Value : DateTime.MinValue;
            DateTime sontarih = satis_BitisTarih.SelectedDate.HasValue ? satis_BitisTarih.SelectedDate.Value : DateTime.MinValue;
            var liste = from x in db.TblURUN.OrderByDescending(x => x.UrunId)
                        .Where(x => x.EklenmeTarihi >= ilktarih && x.EklenmeTarihi <= sontarih)
                        select new
                        {
                            ID = x.UrunId,
                            ÜrünAdı = x.UrunAdi,
                            Tutar = x.Fiyat,
                            Kategori = x.TblKATEGORI.KategoriAdi,
                            SatılanAdet = x.StokMiktari,
                            Birim = x.Birim,
                            Tarih = x.EklenmeTarihi

                        };
            satis_DataGrid.ItemsSource = liste.ToList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            satis_BaslanicTarih.Text = "";
            satis_BitisTarih.Text = "";
            txtatisara.Text = "";
            SatisListele();
        }
    }   
}
