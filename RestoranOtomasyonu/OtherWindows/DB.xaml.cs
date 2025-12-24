using System;
using System.Collections.Generic;
using System.IO;
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

        private void _DB_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //sql server adını belirt
                string ServerName = @".\SQLEXPRESS";
                string dbName = "RESTORANDB";

                //bağlantı nesnesi oluştur
                ServerConnection connection = new ServerConnection(ServerName);
                Server dbserver = new Server(connection);

                //Yedekleme nesnesi oluştur
                Backup dbBackup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = dbName,
                    Incremental = false,
                    Initialize = true,
                };

                //Yedeğin kaydedileceği klasör
                string klasoryolu = @"D:\DBYedek";

                //Eğer bu klasör yoksa oluştur
                if (!Directory.Exists(klasoryolu)) Directory.CreateDirectory(klasoryolu);

                //Yedeği isimlendirelim
                string dosyaadi = Path.Combine(klasoryolu, $"RESTORANDB{DateTime.Now:ddMMyyyy_HHmm}.bak");

                //Yedek dosyasını klasöre ekle
                dbBackup.Devices.AddDevice(dosyaadi, DeviceType.File);

                //Yedekleme işlemini başlat
                dbBackup.SqlBackup(dbserver);

                //Bilgilendir
                MessageBox.Show("Yedekleme işlemi tamamlandı\n" + dosyaadi, "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu\n" + ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
