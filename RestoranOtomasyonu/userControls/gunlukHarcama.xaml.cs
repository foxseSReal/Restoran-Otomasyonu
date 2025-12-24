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
        public void HarcamaListele()
        {
            var listele = db.TblGUNLUKHARCAMA.OrderByDescending(x => x.GunlukId).ToList()
                .Where(x => x.Tarih == DateTime.Now.Date)
                            .Select(x => new
                            {
                                ID = x.GunlukId,
                                Açıklama = x.Aciklama,
                                Tarih = x.Tarih.ToString("dd.MM.yyyy"),
                                Saat = x.Saat.ToString(@"hh\:mm"),

                                Tutar = x.Tutar
                            });
            harcamalar_DataGrid.ItemsSource = listele;
        
        }
        private void HarcamaGetir()
        {
            try
            {
                
                
                DateTime baslangic = gunlukDataGridAralik.SelectedDate ?? DateTime.Now.Date;
                DateTime bitis = gunlukDataGridAralik2.SelectedDate ?? DateTime.Now.Date;

                
               
                decimal toplam = db.TblGUNLUKHARCAMA
                                   .Where(x => x.Tarih >= baslangic && x.Tarih <= bitis)
                                   .Sum(x => (decimal?)x.Tutar) ?? 0;

             
                harcamaGunlukToplam.Text = toplam.ToString("C2");
            }
            catch (Exception)
            {
            
                harcamaGunlukToplam.Text = "₺0,00";
            }
        }
        private void harcamaButton_Click(object sender, RoutedEventArgs e)
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
            TblGUNLUKHARCAMA harcama = new TblGUNLUKHARCAMA();
            harcama.HarcananYer = harcamaYer.Text;
            harcama.Tarih = harcamaTarih.SelectedDate.Value;
            harcama.Saat = saat;
            harcama.Aciklama = haracamaAciklama.Text;
            harcama.Tutar = tutar;

            db.TblGUNLUKHARCAMA.Add(harcama);

            TblGIDER yeniGider = new TblGIDER();
            yeniGider.Tarih = harcamaTarih.SelectedDate.Value;
            yeniGider.Tutar = tutar;
            yeniGider.Aciklama = $"{harcamaYer.Text} - {haracamaAciklama.Text}";
            yeniGider.GiderTuru = "Günlük Harcama"; 
            var mudur = db.TblPERSONELLER.FirstOrDefault(x => x.Pozisyon == "Müdür");
            yeniGider.PersonelId = (mudur != null) ? mudur.PersonelID : 1;
            db.TblGIDER.Add(yeniGider);
            db.SaveChanges(); 
            MessageBox.Show("Günlük Harcama eklendi ve Gider tablosuna işlendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            HarcamaListele();
            HarcamaGetir();

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
                    Saat = x.Saat.ToString(@"hh\:mm") ,
                    Tutar = x.Tutar
                })
                .ToList();

            harcamalar_DataGrid.ItemsSource = sonuc;
            HarcamaGetir();

        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            HarcamaListele();
            HarcamaGetir();
        }
    }
}
