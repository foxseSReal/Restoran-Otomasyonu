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
    /// DB.xaml etkileşim mantığı
    /// </summary>
    public partial class DB : Window
    {
        public DB()
        {
            InitializeComponent();
        }

        private void thisClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
