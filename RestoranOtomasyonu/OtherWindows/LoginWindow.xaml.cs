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
using System.Windows.Threading;

namespace RestoranOtomasyonu.OtherWindows
{
    /// <summary>
    /// LoginWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public LoginWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void btnGiris_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "a" && PasswordBox.Password == "a")
            {
                // Ana pencereyi aç
                MainWindow main = new MainWindow();
                main.Show();

                // Login penceresini kapat
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı giriş");
            }
        }

        private void AppClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Saat.Text = DateTime.Now.ToString("HH:mm");
        }
    }
}
