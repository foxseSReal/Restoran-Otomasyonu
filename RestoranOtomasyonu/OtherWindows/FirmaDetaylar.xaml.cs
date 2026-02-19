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
using System.Windows.Shapes;

namespace RestoranOtomasyonu.OtherWindows
{
    /// <summary>
    /// FirmaDetaylar.xaml etkileşim mantığı
    /// </summary>
    public partial class FirmaDetaylar : Window
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public FirmaDetaylar()
        {
            InitializeComponent();
        }
        private int _seciliFirmaId = 0;
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await FirmaListeleAsync();
        }

        private async void firma_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilmis = FirmaD_DataGrid.SelectedItem as dynamic;
            if (secilmis != null)
            {
                int id = secilmis.ID;
                _seciliFirmaId = id;
                await DetayListeleAsync2(id);
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public async Task FirmaListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblFIRMA.OrderByDescending(x => x.FirmaId).Where(x => x.Unvan == "Firma")
                                 .Select(x => new
                                 {
                                     ID = x.FirmaId,
                                     MüşteriFirma = x.FirmaAdi,
                                     Adres = x.Adres,
                                     Telefon = x.Telefon,
                                     Telefonİki = x.Telefonİki,
                                     WebSitesi = x.WebSitesi,
                                     VergiDairesi = x.VergiDairesi,
                                     HesapNo = x.HesapNo
                                 }).ToList();
                    }
                });

                FirmaD_DataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }
        public async Task DetayListeleAsync2(int id)
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblFIRMAHAREKET
                                 .OrderByDescending(x => x.ID).Where(x => x.TblFIRMA.Unvan == "Firma").Where(x => x.FirmaId == id)
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

                Detay_DataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Stok listesi yüklenirken hata: " + ex.Message);
            }
        }

    }
}
