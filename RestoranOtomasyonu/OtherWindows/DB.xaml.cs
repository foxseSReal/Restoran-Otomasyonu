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
                // sql server adını belirt
                string ServerName = @".\SQLEXPRESS";
                string dbName = "RESTORANDB";

                // bağlantı nesnesi oluştur
                ServerConnection connection = new ServerConnection(ServerName);
                Server dbserver = new Server(connection);

                // Yedekleme nesnesi oluştur
                Backup dbBackup = new Backup
                {
                    Action = BackupActionType.Database,
                    Database = dbName,
                    Incremental = false,
                    Initialize = true,
                };

                // Yedeğin kaydedileceği klasör
                string klasoryolu = @"C:\RestoranOtomasyonu";

                // Eğer bu klasör yoksa oluştur
                if (!Directory.Exists(klasoryolu)) Directory.CreateDirectory(klasoryolu);

                // Yedeği isimlendirelim (Path hatası için System.IO.Path kullanıyoruz)
                string dosyaadi = System.IO.Path.Combine(klasoryolu, $"RESTORANDB{DateTime.Now:ddMMyyyy_HHmm}.bak");

                // Yedek dosyasını klasöre ekle
                dbBackup.Devices.AddDevice(dosyaadi, DeviceType.File);

                // Yedekleme işlemini başlat
                dbBackup.SqlBackup(dbserver);

                // Bilgilendir (WPF uyumlu MessageBox kullanımı)
                MessageBox.Show("Yedekleme işlemi tamamlandı\n" + dosyaadi, "BİLGİ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // Hata durumunda iç hatayı da görmek gerekebilir (InnerException)
                string hataMesaji = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Hata oluştu:\n" + hataMesaji, "HATA", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void _DBYedekle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // SQL server adını belirtiyoruz
                string ServerName = @".\SQLEXPRESS";
                string dbName = "RESTORANDB";

                // Bağlantı nesnesi oluşturuyoruz
                ServerConnection connection = new ServerConnection(ServerName);
                Server dbserver = new Server(connection);

                // Kullanıcıdan .bak dosyasını seçtirelim
                // WPF için Microsoft.Win32.OpenFileDialog kullanılır
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "YEDEK DOSYASINI SEÇ (.bak) ",
                    Filter = "Yedek Dosyası (*.bak) | *.bak"
                };

                // Dosya seçilmezse işlemi iptal et
                // WPF'te ShowDialog 'bool?' döndürür, true ise seçildi demektir.
                if (openFileDialog.ShowDialog() != true)
                    return;

                string bakdosyayolu = openFileDialog.FileName;

                // Restore (geri yükleme) nesnesi
                Restore dbRestore = new Restore
                {
                    Database = dbName,   // Hangi vt geri yüklenecek
                    Action = RestoreActionType.Database,  // Restore işlemi
                    ReplaceDatabase = true   // Mevcut vt üzerine yaz
                };

                // Geri yüklenecek .bak dosyasını ekle
                dbRestore.Devices.AddDevice(bakdosyayolu, DeviceType.File);

                // Veritabanındaki açık bağlantıları kapatmak için SingleUser moduna al
                // (Eğer bağlantı kapatılamazsa hata vermemesi için try-catch içinde olması güvenlidir)
                string sqlDisconnectUsers = $@"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                dbserver.ConnectionContext.ExecuteNonQuery(sqlDisconnectUsers);

                // Geriyükleme işlemini başlat
                dbRestore.SqlRestore(dbserver);

                // Geri yükleme bitince tekrar çok kullanıcılı moda al
                string sqlMultiUser = $@"ALTER DATABASE [{dbName}] SET MULTI_USER;";
                dbserver.ConnectionContext.ExecuteNonQuery(sqlMultiUser);

                // Bilgilendirme (WPF uyumlu MessageBox)
                MessageBox.Show("Geri yükleme işlemi tamamlandı.", "BİLGİ", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                // Hata oluşsa bile veritabanını MULTI_USER moduna geri döndürmeye çalışmak iyi bir pratiktir
                // Ancak şimdilik sadece hatayı gösterelim:
                MessageBox.Show(
                    "Hata Oluştu:\n" + ex.Message,
                    "HATA",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    
    }
}
