using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Threading;

namespace RestoranOtomasyonu
{
    /// <summary>
    /// MasalarWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MasalarWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        private readonly CultureInfo _tr = new CultureInfo("tr-TR");

        public MasalarWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            _tarih.Text = DateTime.Now.ToString("D", _tr);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _saat.Text = DateTime.Now.ToString("HH:mm");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
