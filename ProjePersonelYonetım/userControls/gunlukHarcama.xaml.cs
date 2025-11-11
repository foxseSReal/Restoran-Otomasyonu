using ProjePersonelYonetım.Entity;
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

namespace ProjePersonelYonetım.userControls
{
    /// <summary>
    /// gunlukHarcama.xaml etkileşim mantığı
    /// </summary>
    public partial class gunlukHarcama : UserControl
    {
        RESTORANDBEntities2 db = new RESTORANDBEntities2();
        public gunlukHarcama()
        {
            InitializeComponent();
            HarcamaListele();
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
            //günlük harcama toplamını bugünke göre al



            harcamaGunlukToplam.Text = db.TblGUNLUKHARCAMA.Sum(x => x.Tutar).ToString();
            
        }
        private void harcamaButton_Click(object sender, RoutedEventArgs e)
        {
           //harcama ekle
            TblGUNLUKHARCAMA harcama = new TblGUNLUKHARCAMA();
            harcama.HarcananYer = harcamaYer.Text;
            harcama.Tarih = harcamaTarih.SelectedDate.Value;
            TimeSpan saat;
            if (TimeSpan.TryParse(harcamaSaat.Text, out saat))
            {
                harcama.Saat = saat;
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir saat formatı girin (örneğin: 14:30).");
                return;
            }
            harcama.Aciklama = haracamaAciklama.Text;
            decimal tutar;
            if (decimal.TryParse(harcamaTutari.Text, out tutar))
            {
                harcama.Tutar = tutar;
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir tutar girin.");
                return;
            }
            db.TblGUNLUKHARCAMA.Add(harcama);
            db.SaveChanges();
            MessageBox.Show("Günlük Harcama Eklendi.");
            HarcamaListele();


        }

        private void harcamalar_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//harcama doldur
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

            // Önce temel sorgu
            var listele = db.TblGUNLUKHARCAMA
                .OrderByDescending(x => x.GunlukId)
                .AsQueryable();

            // Tarih aralığı filtreleri
            if (baslangic.HasValue)
            {
                listele = listele.Where(x => x.Tarih >= baslangic.Value);
            }
            if (bitis.HasValue)
            {
                listele = listele.Where(x => x.Tarih <= bitis.Value);
            }

            // Veritabanından çek ve formatla
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

        }
    }
}
