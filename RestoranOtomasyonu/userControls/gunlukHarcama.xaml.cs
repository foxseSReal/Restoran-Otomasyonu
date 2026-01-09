using RestoranOtomasyonu.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoranOtomasyonu.userControls
{
    /// <summary>
    /// gunlukHarcama.xaml etkileşim mantığı
    /// </summary>
    public partial class gunlukHarcama : UserControl
    {
        RESTORANDBEntities1 db = new RESTORANDBEntities1();
        public gunlukHarcama()
        {
            InitializeComponent();
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await HarcamaListeleAsync();
            await HarcamaGetirAsync();
        }
        public async Task HarcamaListeleAsync()
        {
            try
            {
                DateTime bugun = DateTime.Now.Date;

                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities1())
                    {
                        return db.TblGUNLUKHARCAMA
                                 .Where(x => x.Tarih == bugun)
                                 .OrderByDescending(x => x.GunlukId)
                                 .ToList()
                                 .Select(x => new
                                 {
                                     ID = x.GunlukId,
                                     Açıklama = x.Aciklama,
                                     Tarih = x.Tarih.ToString("dd.MM.yyyy"),
                                     Saat = x.Saat.ToString(@"hh\:mm"),
                                     Tutar = x.Tutar
                                 })
                                 .ToList();
                    }
                });

                harcamalar_DataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }
        public async Task HarcamaGetirAsync()
        {
            try
            {
                DateTime baslangic = gunlukDataGridAralik.SelectedDate ?? DateTime.Now.Date;
                DateTime bitis = gunlukDataGridAralik2.SelectedDate ?? DateTime.Now.Date;

                decimal toplam = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities1())
                    {
                        return db.TblGUNLUKHARCAMA
                                 .Where(x => x.Tarih >= baslangic && x.Tarih <= bitis)
                                 .Sum(x => (decimal?)x.Tutar) ?? 0;
                    }
                });
                harcamaGunlukToplam.Text = toplam.ToString("C2");
            }
            catch (Exception)
            {
                harcamaGunlukToplam.Text = "₺0,00";
            }
        }
        private async void harcamaButton_Click(object sender, RoutedEventArgs e)
        {
            if (harcamaTarih.SelectedDate == null)
            {
                MessageBox.Show("Lütfen bir tarih seçin.");
                return;
            }

            decimal tutar;
            if (!decimal.TryParse(harcamaTutari.Text, out tutar))
            {
                MessageBox.Show("Lütfen geçerli bir tutar girin.");
                return;
            }

            TimeSpan saat;
            if (!TimeSpan.TryParse(harcamaSaat.Text, out saat))
            {
                MessageBox.Show("Lütfen geçerli bir saat formatı girin (örneğin: 14:30).");
                return;
            }
            string harcananYer = harcamaYer.Text;
            DateTime secilenTarih = harcamaTarih.SelectedDate.Value;
            string aciklama = haracamaAciklama.Text;

            try
            {
                using (var db = new RESTORANDBEntities1())
                {
                    TblGUNLUKHARCAMA harcama = new TblGUNLUKHARCAMA();
                    harcama.HarcananYer = harcananYer;
                    harcama.Tarih = secilenTarih;
                    harcama.Saat = saat;
                    harcama.Aciklama = aciklama;
                    harcama.Tutar = tutar;
                    db.TblGUNLUKHARCAMA.Add(harcama);
                    TblGIDER yeniGider = new TblGIDER();
                    yeniGider.Tarih = secilenTarih;
                    yeniGider.Tutar = tutar;
                    yeniGider.Aciklama = $"{harcananYer} - {aciklama}";
                    yeniGider.GiderTuru = "Günlük Harcama";
                    var mudur = db.TblPERSONELLER.FirstOrDefault(x => x.Pozisyon == "Müdür");
                    yeniGider.PersonelId = (mudur != null) ? mudur.PersonelID : 1;
                    db.TblGIDER.Add(yeniGider);
                    await db.SaveChangesAsync();
                }

                MessageBox.Show("Günlük Harcama eklendi ve Gider tablosuna işlendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                await HarcamaListeleAsync();
                await HarcamaGetirAsync();
                harcamaYer.Clear();
                harcamaTutari.Clear();
                haracamaAciklama.Clear();
                harcamaSaat.SelectedTime = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        private void harcamalar_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilenHarcama = harcamalar_DataGrid.SelectedItem;
            if (secilenHarcama != null)
            {
                dynamic secim = secilenHarcama;
                int harcamaId = secim.ID;
                var harcama = db.TblGUNLUKHARCAMA.Find(harcamaId);
                if (harcama != null)
                {
                    harcamaYer.Text = harcama.HarcananYer;
                    harcamaTarih.SelectedDate = harcama.Tarih;
                    harcamaSaat.Text = harcama.Saat.ToString(@"hh\:mm");
                    haracamaAciklama.Text = harcama.Aciklama;
                    harcamaTutari.Text = harcama.Tutar.ToString();
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime? baslangic = gunlukDataGridAralik.SelectedDate;
            DateTime? bitis = gunlukDataGridAralik2.SelectedDate;
            var listele = db.TblGUNLUKHARCAMA
                .OrderByDescending(x => x.GunlukId)
                .AsQueryable();
            if (baslangic.HasValue)
            {
                listele = listele.Where(x => x.Tarih >= baslangic.Value);
            }
            if (bitis.HasValue)
            {
                listele = listele.Where(x => x.Tarih <= bitis.Value);
            }

            var sonuc = listele
                .ToList()
                .Select(x => new
                {
                    ID = x.GunlukId,
                    Açıklama = x.Aciklama,
                    Tarih = x.Tarih.ToString("dd.MM.yyyy"),
                    Saat = x.Saat.ToString(@"hh\:mm"),
                    Tutar = x.Tutar
                })
                .ToList();

            harcamalar_DataGrid.ItemsSource = sonuc;
            HarcamaGetirAsync();
        }
    }
}
