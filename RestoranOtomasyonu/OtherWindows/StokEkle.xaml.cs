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
    /// StokEkle.xaml etkileşim mantığı
    /// </summary>
    public partial class StokEkle : Window
    {
        public StokEkle()
        {
            InitializeComponent();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void urun_temizleButton_Click(object sender, RoutedEventArgs e)
        {
            urun_isim.Clear();
            urunFiyat.Clear();
            urun_Adet.Value= 0;
            cbxUrun_Firma.Text="";
            cbxUrun_Kategori.Text="";
        }
    }
}
