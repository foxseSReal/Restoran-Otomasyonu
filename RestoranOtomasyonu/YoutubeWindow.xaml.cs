using Microsoft.Web.WebView2.Core;
using RestoranOtomasyonu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RestoranOtomasyonu
{
    /// <summary>
    /// YouTubeWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class YouTubeWindow : Window
    {

        private MasalarWindow _mainWindow;

        public YouTubeWindow(MasalarWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            InitializeBrowser();
        }

        // Önceki Sayfaya (Geri) Gitme Metodu
        private void BtnPageBack_Click(object sender, RoutedEventArgs e)
        {
            // Önce WebView2'nin hazır olup olmadığını ve "Geri" gidilecek bir sayfa olup olmadığını kontrol ediyoruz
            if (YTWebView.CoreWebView2 != null && YTWebView.CoreWebView2.CanGoBack)
            {
                YTWebView.CoreWebView2.GoBack();
            }
        }

        // Sonraki Sayfaya (İleri) Gitme Metodu
        private void BtnPageForward_Click(object sender, RoutedEventArgs e)
        {
            // Aynı şekilde "İleri" gidilecek bir sayfa var mı diye kontrol ediyoruz
            if (YTWebView.CoreWebView2 != null && YTWebView.CoreWebView2.CanGoForward)
            {
                YTWebView.CoreWebView2.GoForward();
            }
        }
        public async System.Threading.Tasks.Task<string> GetVideoProgressJson()
        {
            if (YTWebView.CoreWebView2 != null)
            {
                // Video nesnesinden anlık saniye (current) ve toplam süreyi (total) alıyoruz
                string script = @"(function() {
            var video = document.querySelector('video');
            if (video) {
                return JSON.stringify({
                    current: video.currentTime,
                    total: video.duration
                });
            }
            return null;
        })()";

                return await YTWebView.CoreWebView2.ExecuteScriptAsync(script);
            }
            return null;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // Pencerenin tamamen yok edilmesini iptal et
            this.Hide();     // Sadece görünmez yap (Böylece müzik çalmaya devam eder)
        }

        private async void InitializeBrowser()
        {
            await YTWebView.EnsureCoreWebView2Async(null);
            YTWebView.CoreWebView2.Navigate("https://www.youtube.com");

            // URL değiştiğinde (Yeni videoya tıklandığında) resmi güncelle
            YTWebView.CoreWebView2.SourceChanged += CoreWebView2_SourceChanged;

            // Sayfa başlığı değiştiğinde (Şarkı adı) yazıyı güncelle
            YTWebView.CoreWebView2.DocumentTitleChanged += CoreWebView2_DocumentTitleChanged;
        }

        private void CoreWebView2_SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {
            string url = YTWebView.CoreWebView2.Source;
            // URL'den Video ID'sini (v=...) çekip YouTube Thumbnail linkini oluşturuyoruz
            Match match = Regex.Match(url, @"[?&]v=([^&]+)");
            if (match.Success)
            {
                string videoId = match.Groups[1].Value;
                _mainWindow.UpdateThumbnail($"https://img.youtube.com/vi/{videoId}/hqdefault.jpg");
            }
        }

        private void CoreWebView2_DocumentTitleChanged(object sender, object e)
        {
            string title = YTWebView.CoreWebView2.DocumentTitle;
            // Başlıktaki "- YouTube" yazısını temizliyoruz
            title = title.Replace(" - YouTube", "");
            _mainWindow.UpdateTitle(title);
        }

        // Ana sayfadan gelen Oynat/Durdur komutu
        public async void TogglePlayPause()
        {
            if (YTWebView.CoreWebView2 != null)
                await YTWebView.CoreWebView2.ExecuteScriptAsync("document.querySelector('.ytp-play-button').click();");
        }

        // Ana sayfadan gelen Sonraki Şarkı komutu
        public async void NextSong()
        {
            if (YTWebView.CoreWebView2 != null)
                await YTWebView.CoreWebView2.ExecuteScriptAsync("document.querySelector('.ytp-next-button').click();");
        }
        // "Arayüze Dön" butonuna basıldığında pencereyi gizler, müzik çalmaya devam eder
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

    }
}