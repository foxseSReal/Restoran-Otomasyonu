using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RestoranOtomasyonu
{
    /// <summary>
    /// App.xaml etkileşim mantığı
    /// </summary>
    public partial class App : Application
    {
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Şu an sadece programı kapatır.
            // İleride buraya başka menü işlemleri eklenecek.
            Application.Current.Shutdown();
        }


        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.ContextMenu != null)
            {
                // Menüyü butonun tam altına konumlandır
                btn.ContextMenu.PlacementTarget = btn;
                btn.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;

                // Menüyü aç
                btn.ContextMenu.IsOpen = true;
            }
        }
    }

}
