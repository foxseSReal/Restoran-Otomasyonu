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
using RestoranOtomasyonu.OtherWindows;

namespace RestoranOtomasyonu.userControls
{
    /// <summary>
    /// Firmalar.xaml etkileşim mantığı
    /// </summary>
    public partial class Firmalar : UserControl
    {
        public Firmalar()
        {
            InitializeComponent();
        }

        private void FirmaDetaylar(object sender, RoutedEventArgs e)
        {
            FirmaDetaylar FirmaDetaylar = new FirmaDetaylar();
            FirmaDetaylar.ShowDialog();
        }

        private void musteriFirma_Ara_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void musteri_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Modeme_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void musteri_ekleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void musteri_silButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void musteri_guncelleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void musteri_temizleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBorcListele_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBorcEkle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBorcGuncelle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBorcSil_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DetaylarClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
