using Newtonsoft.Json;
using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.OtherWindows;
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
using Microsoft.Web.WebView2.Core;

namespace RestoranOtomasyonu
{
    /// <summary>
    /// MasalarWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MasalarWindow : Window
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        DispatcherTimer timer = new DispatcherTimer();
        private readonly CultureInfo _tr = new CultureInfo("tr-TR");
        private YouTubeWindow _ytWindow;
        private DispatcherTimer _timer;
        public MasalarWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            _tarih.Text = DateTime.Now.ToString("D", _tr);
            SetupTimer();
            DashboardBilgileriniGuncelle();
        }
        public enum AdisyonTipi
        {
            Masa,
            Paket
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _saat.Text = DateTime.Now.ToString("HH:mm");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MasaGoster();
            MasaRenklendir();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult cevap = MessageBox.Show(
                "Uygulamayi Kapatmak İstiyor musunuz?",
                "Uygulamayi Kapatacak mısın?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );
            if (cevap == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void MasaGoster()
        {
            MasaPanel.Children.Clear();
            var masalar = (from x in db.TblMASA
                           where x.Durum == true && x.NESNE_DURUMU != "Paket"
                           select new
                           {
                               x.MasaId,
                               x.NESNE_DURUMU,
                               x.MasaNo,
                               x.Durum
                           }).ToList();

            foreach (var item in masalar)
            {
                Button btn = new Button();
                btn.Style = (Style)FindResource("MasaButton");
                btn.Margin = new Thickness(10);
                var converter = new System.Windows.Media.BrushConverter();
                btn.Background = (System.Windows.Media.Brush)converter.ConvertFromString("#85dcdcdc");
                btn.Tag = item.MasaId;
                btn.Click += Masa_Click;

                StackPanel sp = new StackPanel();

                TextBlock tb = new TextBlock();
                tb.Text = item.NESNE_DURUMU + " " + item.MasaId;
                tb.FontSize = 28;

                tb.FontFamily = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/"), "/NewFonts/Modak-Regular.ttf#Modak");

                sp.Children.Add(tb);
                btn.Content = sp;
                MasaPanel.Children.Add(btn);
            }
        }

        private void Masa_Click(object sender, RoutedEventArgs e)
        {
            Button secilenMasa = (Button)sender;
            int masaId = Convert.ToInt32(secilenMasa.Tag);

            Adisyon adisyonPenceresi = new Adisyon(masaId, AdisyonTipi.Masa);

            adisyonPenceresi.Closed += (s, args) =>
            {
                // Arayüzün nefes alıp güncellenmesi için ufak bir Dispatcher (Kuyruk) içine alıyoruz
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MasaRenklendir();
                    DashboardBilgileriniGuncelle();
                });
            };

            // Pencereyi açıyoruz. (ShowDialog olması çok önemli!)
            adisyonPenceresi.ShowDialog();
        }
        public void MasaRenklendir()
        {
            // Veritabanını tazeleyerek en güncel statüleri alıyoruz
            using (RESTORANDBEntities tazeDb = new RESTORANDBEntities())
            {
                var tumMasalar = tazeDb.TblMASA.AsNoTracking().Where(x => x.Durum == true && x.NESNE_DURUMU != "Paket").ToList();

                var converter = new System.Windows.Media.BrushConverter();
                var standartArkaplan = (Brush)converter.ConvertFromString("#85dcdcdc");

                foreach (var child in MasaPanel.Children)
                {
                    if (child is Button btn)
                    {
                        int masaId = Convert.ToInt32(btn.Tag);
                        var masaVerisi = tumMasalar.FirstOrDefault(x => x.MasaId == masaId);

                        // Butonun içindeki StackPanel ve TextBlock'a ulaşıyoruz
                        StackPanel sp = (StackPanel)btn.Content;
                        TextBlock tb = (TextBlock)sp.Children[0];

                        if (masaVerisi != null)
                        {
                            if (masaVerisi.Statu == "D") // DOLU
                            {
                                btn.Background = (Brush)FindResource("DoluMasaBrush");
                                tb.Foreground = Brushes.AntiqueWhite;
                                tb.Text = masaVerisi.NESNE_DURUMU + " " + masaVerisi.MasaNo; // Dolu olsa da numara yazsın
                            }
                            else if (masaVerisi.Statu == "R") // REZERVE
                            {
                                btn.Background = (Brush)FindResource("AmberBrush");
                                tb.Foreground = Brushes.Black;
                                tb.Text = !string.IsNullOrEmpty(masaVerisi.RezervasyonSaati)
                                          ? $"Rezerve {masaVerisi.RezervasyonSaati}"
                                          : "Rezerve";
                            }
                            else // BOŞ (Senin MasaGoster'deki standart halin)
                            {
                                btn.Background = standartArkaplan; // #85dcdcdc rengi
                                btn.BorderBrush = Brushes.Transparent;
                                tb.Foreground = Brushes.Black; // Varsayılan yazı rengi
                                tb.Text = masaVerisi.NESNE_DURUMU + " " + masaVerisi.MasaNo; // Standart yazı
                            }
                        }
                    }
                }
            }
        }

        private void YonetimClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void UrunEkle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MasaEkle_Click(object sender, RoutedEventArgs e)
        {
            MasaEkleWindow win = new MasaEkleWindow();
            win.Owner = this; // Ana pencerenin üzerinde kalması için önemli
            bool? result = win.ShowDialog(); // Show() yerine ShowDialog() kullan
        }

        //Paketleme işlemi için gerekli olan kodlar
        public void PaketGoster(object sender, RoutedEventArgs e)
        {
            MasaPanel.Children.Clear();
            var masalar = (from x in db.TblMASA
                           where x.Durum == true && x.NESNE_DURUMU != "Masa"
                           select new
                           {
                               x.MasaId,
                               x.NESNE_DURUMU,
                               x.MasaNo,
                               x.Durum
                           }).ToList();

            foreach (var item in masalar)
            {
                Button btn = new Button();
                btn.Style = (Style)FindResource("MasaButton");
                btn.Margin = new Thickness(10);
                var converter = new System.Windows.Media.BrushConverter();
                btn.Background = (System.Windows.Media.Brush)converter.ConvertFromString("#85dcdcdc");
                btn.Tag = item.MasaId;
                btn.Click += Paket_Click;

                StackPanel sp = new StackPanel();

                TextBlock tb = new TextBlock();
                tb.Text = item.NESNE_DURUMU + " " + item.MasaId;
                tb.FontSize = 28;

                tb.FontFamily = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/"), "/NewFonts/Modak-Regular.ttf#Modak");

                sp.Children.Add(tb);
                btn.Content = sp;
                MasaPanel.Children.Add(btn);
            }

            PaketRenklendir();

        }


        private void Paket_Click(object sender, RoutedEventArgs e)
        {
            Button secilenMasa = (Button)sender;
            int masaId = Convert.ToInt32(secilenMasa.Tag);

            Adisyon adisyonPenceresi = new Adisyon(masaId, AdisyonTipi.Paket);

            adisyonPenceresi.Closed += (s, args) =>
            {
                // Arayüzün nefes alıp güncellenmesi için ufak bir Dispatcher (Kuyruk) içine alıyoruz
                Application.Current.Dispatcher.Invoke(() =>
                {
                    PaketRenklendir();
                    DashboardBilgileriniGuncelle();
                });
            };

            // Pencereyi açıyoruz. (ShowDialog olması çok önemli!)
            adisyonPenceresi.ShowDialog();
        }

        public void PaketRenklendir()
        {
            // Veritabanını tazeleyerek en güncel statüleri alıyoruz
            using (RESTORANDBEntities tazeDb = new RESTORANDBEntities())
            {
                var tumMasalar = tazeDb.TblMASA.AsNoTracking().Where(x => x.Durum == true && x.NESNE_DURUMU != "Masa").ToList();

                var converter = new System.Windows.Media.BrushConverter();
                var standartArkaplan = (Brush)converter.ConvertFromString("#85dcdcdc");

                foreach (var child in MasaPanel.Children)
                {
                    if (child is Button btn)
                    {
                        int masaId = Convert.ToInt32(btn.Tag);
                        var masaVerisi = tumMasalar.FirstOrDefault(x => x.MasaId == masaId);

                        // Butonun içindeki StackPanel ve TextBlock'a ulaşıyoruz
                        StackPanel sp = (StackPanel)btn.Content;
                        TextBlock tb = (TextBlock)sp.Children[0];

                        if (masaVerisi != null)
                        {
                            if (masaVerisi.Statu == "D") // DOLU
                            {
                                btn.Background = (Brush)FindResource("DoluMasaBrush");
                                tb.Foreground = Brushes.AntiqueWhite;
                                tb.Text = masaVerisi.NESNE_DURUMU + " " + masaVerisi.MasaNo; // Dolu olsa da numara yazsın
                            }
                            else if (masaVerisi.Statu == "R") // REZERVE
                            {
                                btn.Background = (Brush)FindResource("AmberBrush");
                                tb.Foreground = Brushes.Black;
                                tb.Text = !string.IsNullOrEmpty(masaVerisi.RezervasyonSaati)
                                          ? $"Rezerve {masaVerisi.RezervasyonSaati}"
                                          : "Rezerve";
                            }
                            else // BOŞ (Senin MasaGoster'deki standart halin)
                            {
                                btn.Background = standartArkaplan; // #85dcdcdc rengi
                                btn.BorderBrush = Brushes.Transparent;
                                tb.Foreground = Brushes.Black; // Varsayılan yazı rengi
                                tb.Text = AdisyonTipi.Paket + " " + masaVerisi.MasaNo; // Standart yazı
                            }
                        }
                    }
                }
            }
        }

        public void DashboardBilgileriniGuncelle()
        {
            try
            {
                using (RESTORANDBEntities db = new RESTORANDBEntities())
                {
                    // 1. Açık Masa Sayısını Hesapla
                    // Statu "D" (Dolu) olan ve sistemde aktif (Durum == true) olanları sayıyoruz
                    int acikMasaSayisi = db.TblMASA.Count(x => x.Statu == "D" && x.Durum == true);

                    // UI Güncelleme
                    txtAcikMasaSayisi.Text = acikMasaSayisi.ToString();

                    // 2. Günlük Ciroyu Hesapla (Önceki konuştuğumuz kısım)
                    DateTime bugun = DateTime.Today;
                    decimal gunlukToplam = db.TblADISYON_ODEME
                        .Where(x => x.Tarih >= bugun)
                        .Sum(x => (decimal?)x.OdenenTutar) ?? 0;

                    txtGunlukCiro.Text = string.Format("₺ {0:N2}", gunlukToplam);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda 0 yazdır ki uygulama çökmesin
                txtAcikMasaSayisi.Text = "0";
                txtGunlukCiro.Text = "₺ 0,00";
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GunSonuRaporu gunSonuRaporu = new GunSonuRaporu();
            gunSonuRaporu.Owner = this;
            bool? result = gunSonuRaporu.ShowDialog();
        }

        /*Youtube*/

        private void SetupTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_TickY;
            _timer.Start();
        }

        private async void Timer_TickY(object sender, EventArgs e)
        {
            if (MusicSlider.IsMouseCaptureWithin) return;

            if (_ytWindow != null && _ytWindow.IsLoaded)
            {
                string rawJson = await _ytWindow.GetVideoProgressJson();

                if (!string.IsNullOrEmpty(rawJson) && rawJson != "null")
                {
                    try
                    {
                        string cleanJson = JsonConvert.DeserializeObject<string>(rawJson);
                        var data = JsonConvert.DeserializeObject<dynamic>(cleanJson);

                        double current = (double)data.current;
                        double total = (double)data.total;

                        if (!double.IsNaN(current) && !double.IsNaN(total))
                        {
                            MusicSlider.Maximum = total;
                            MusicSlider.Value = current;

                            CurrentTimeText.Text = FormatTime(current);
                            TotalTimeText.Text = FormatTime(total);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Hata durumunda loglama veya kullanıcıya bildirim yapabilirsiniz
                        Console.WriteLine($"Hata: {ex.Message}");
                    }
                }
            }
        }

        private void MusicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MusicSlider.IsMouseCaptureWithin && _ytWindow != null && _ytWindow.YTWebView.CoreWebView2 != null)
            {
                string seekScript = $"document.querySelector('video').currentTime = {MusicSlider.Value};";
                _ytWindow.YTWebView.CoreWebView2.ExecuteScriptAsync(seekScript);
            }
        }

        private string FormatTime(double seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
        }

        private void MusicPlayer_Click(object sender, MouseButtonEventArgs e)
        {
            if (_ytWindow == null)
            {
                _ytWindow = new YouTubeWindow(this);
                _ytWindow.Owner = this;
                _ytWindow.Show();
            }
            else
            {
                _ytWindow.Show();
                _ytWindow.WindowState = WindowState.Normal;
                _ytWindow.WindowState = WindowState.Maximized;
                _ytWindow.Activate();
            }
        }

        // Önceki Şarkı Butonu
        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (_ytWindow != null && _ytWindow.IsLoaded && _ytWindow.YTWebView.CoreWebView2 != null)
            {
                _ytWindow.YTWebView.CoreWebView2.ExecuteScriptAsync("window.history.back();");
            }
        }

        // Oynat/Durdur Butonu
        private void BtnPlayPause_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (_ytWindow != null && _ytWindow.IsLoaded)
            {
                _ytWindow.TogglePlayPause();
                if (PlayPauseIcon.Kind == MaterialDesignThemes.Wpf.PackIconKind.Pause)
                {
                    PlayPauseIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
                }
                else
                {
                    PlayPauseIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
                }
            }
        }

        // Sonraki Şarkı Butonu
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (_ytWindow != null && _ytWindow.IsLoaded)
            {
                _ytWindow.NextSong();
            }
        }

        // YouTube penceresinden çağrılacak metotlar (Resim ve Başlık güncelleme)
        public void UpdateTitle(string title)
        {
            SongTitleText.Text = title;
            ArtistText.Text = "YouTube";
        }

        public void UpdateThumbnail(string imageUrl)
        {
            try
            {
                SongThumbnail.Source = new BitmapImage(new Uri(imageUrl));
            }
            catch { }
        }

    }
}
