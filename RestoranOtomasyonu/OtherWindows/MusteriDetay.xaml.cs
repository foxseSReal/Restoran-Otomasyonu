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
    /// MusteriDetay.xaml etkileşim mantığı
    /// </summary>
    public partial class MusteriDetay : Window
    {
        public MusteriDetay()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await MusteriListeleAsync();
        }

        public async Task MusteriListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblFIRMA.OrderByDescending(x => x.FirmaId).Where(x => x.Unvan == "Müşteri")
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

                MusteriD_DataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }

        private void Musteri_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
