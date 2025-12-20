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
using System.Windows.Threading;

namespace RestoranOtomasyonu.OtherWindows
{
    /// <summary>
    /// LoginWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        RESTORANDBEntities1 db = new RESTORANDBEntities1();
        public LoginWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void btnGiris_Click(object sender, RoutedEventArgs e)
        {
            var user = db.TBLKULLANICI.FirstOrDefault(k => k.KullaniciAdi == txtUsername.Text && k.Sifre == PasswordBox.Password);

            if (user != null)
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Hide();
            }
            else MessageBox.Show("Hatalı kullanıcı adı veya şifre.");
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
