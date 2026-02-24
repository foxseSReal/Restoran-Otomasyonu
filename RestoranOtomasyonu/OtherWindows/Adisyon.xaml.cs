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
    /// Adisyon.xaml etkileşim mantığı
    /// </summary>
    public partial class Adisyon : Window
    {
        public Adisyon(int SecilenMasa)
        {
            InitializeComponent();
            this.DataContext = SecilenMasa;
            RESTORANDBEntities _context = new RESTORANDBEntities();
            var secilenMasa = _context.TblMASA.FirstOrDefault(m => m.MasaNo == SecilenMasa);

            if (secilenMasa != null)
            {
                this.DataContext = secilenMasa;
            }
        }

        private void Adisyon_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
