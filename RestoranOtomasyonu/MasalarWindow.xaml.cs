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
                           where x.Durum == true && x.MasaId != 999
                           select new
                           {
                               x.MasaId,
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
                tb.Text = "Masa " + item.MasaId;
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

            Adisyon adisyonPenceresi = new Adisyon(masaId);

            // İŞTE KESİN ÇÖZÜM: Pencerenin "Kapandı" (Closed) olayına kanca atıyoruz!
            // Bu kod sayesinde, Adisyon penceresi (Onayla veya Çarpı ile) TAMAMEN kapandığı an 
            // MasaRenklendir metodu otomatik olarak tetiklenecek.
            adisyonPenceresi.Closed += (s, args) =>
            {
                // Arayüzün nefes alıp güncellenmesi için ufak bir Dispatcher (Kuyruk) içine alıyoruz
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MasaRenklendir();
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
                var tumMasalar = tazeDb.TblMASA.AsNoTracking().Where(x => x.Durum == true && x.MasaId != 999).ToList();

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
                                tb.Text = "Masa " + masaId; // Dolu olsa da numara yazsın
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
                                tb.Text = "Masa " + masaId; // Standart yazı
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
    }
}
