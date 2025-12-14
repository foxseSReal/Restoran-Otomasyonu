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
    /// LoginWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "a" && PasswordBox.Text == "a")
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


    }
}
