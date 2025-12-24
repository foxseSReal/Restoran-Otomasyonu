using Microsoft.SqlServer.Management.Common; // ServerConnection için gerekli
using Microsoft.SqlServer.Management.Smo; // NuGet ile gelen kütüphane
using Microsoft.Win32;
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
                string ServerName = @".\SQLEXPRESS";
                string dbName = "RESTORANDB";
                ServerConnection connection = new ServerConnection(ServerName);
                Server dbserver = new Server(connection);
                Backup dbBackup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = dbName,
                    Incremental = false,
                    Initialize = true,
                };
                string klasoryolu = @"C:\RestoranOtomasyonu";
                if (!Directory.Exists(klasoryolu)) Directory.CreateDirectory(klasoryolu);
                string dosyaadi = System.IO.Path.Combine(klasoryolu, $"RESTORANDB{DateTime.Now:ddMMyyyy_HHmm}.bak");
                dbBackup.Devices.AddDevice(dosyaadi, DeviceType.File);
                dbBackup.SqlBackup(dbserver);
                MessageBox.Show("Yedekleme işlemi tamamlandı\n" + dosyaadi, "BİLGİ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                string hataMesaji = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Hata oluştu:\n" + hataMesaji, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void _DBYedekle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ServerName = @".\SQLEXPRESS";
                string dbName = "RESTORANDB";

                ServerConnection connection = new ServerConnection(ServerName);
                Server dbserver = new Server(connection);
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "YEDEK DOSYASINI SEÇ (.bak) ",
                    Filter = "Yedek Dosyası (*.bak) | *.bak"
                };
                if (openFileDialog.ShowDialog() != true)
                    return;
                string bakdosyayolu = openFileDialog.FileName;
                Restore dbRestore = new Restore
                {
                    Database = dbName,  
                    Action = RestoreActionType.Database,
                    ReplaceDatabase = true   
                };
                dbRestore.Devices.AddDevice(bakdosyayolu, DeviceType.File);
                string sqlDisconnectUsers = $@"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                dbserver.ConnectionContext.ExecuteNonQuery(sqlDisconnectUsers);
                dbRestore.SqlRestore(dbserver);
                string sqlMultiUser = $@"ALTER DATABASE [{dbName}] SET MULTI_USER;";
                dbserver.ConnectionContext.ExecuteNonQuery(sqlMultiUser);
                MessageBox.Show("Geri yükleme işlemi tamamlandı.", "BİLGİ", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Hata Oluştu:\n" + ex.Message,
                    "HATA",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    
    }
}
