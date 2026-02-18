using RestoranOtomasyonu.Entity;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RestoranOtomasyonu.userControls
{
    /// <summary>
    /// muhasebe.xaml etkileşim mantığı
    /// </summary>
    public partial class muhasebe : UserControl
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public muhasebe()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await GelirListeleAsync();
            await GiderListeleAsync();
            await FinansDurumuHesaplaAsync();
        }
        public async Task GelirListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblGELIR
                                 .OrderByDescending(x => x.GelirId)
                                 .ToList()
                                 .Select(x => new
                                 {
                                     ID = x.GelirId,
                                     Personel = x.TblPERSONELLER != null ? x.TblPERSONELLER.Ad + " " + x.TblPERSONELLER.Soyad : "Bilinmiyor",
                                     Müşteri = x.TblMUSTERILER != null ? x.TblMUSTERILER.Ad + " " + x.TblMUSTERILER.Soyad : "Bilinmiyor",
                                     Tarih = x.Tarih,
                                     Açıklama = x.Aciklama,
                                     Tutar = x.Tutar
                                 })
                                 .ToList();
                    }
                });

                muhasebeGelir_DataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gelirler yüklenirken hata: " + ex.Message);
            }
        }
        public async Task GiderListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblGIDER
                                 .OrderByDescending(x => x.GiderId)
                                 .ToList()
                                 .Select(x => new
                                 {
                                     ID = x.GiderId,
                                     Firma = x.TblFIRMA != null ? x.TblFIRMA.FirmaAdi : "Bilinmiyor",
                                     GiderTürü = x.GiderTuru,
                                     Personel = x.TblPERSONELLER != null ? x.TblPERSONELLER.Ad + " " + x.TblPERSONELLER.Soyad : "Bilinmiyor",
                                     Tarih = x.Tarih,
                                     Açıklama = x.Aciklama,
                                     Tutar = x.Tutar
                                 })
                                 .ToList();
                    }
                });

                muhasebeGider_DataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giderler yüklenirken hata: " + ex.Message);
            }
        }
        public async Task FinansDurumuHesaplaAsync()
        {
            try
            {
                var finansOzeti = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        decimal toplamGelir = db.TblGELIR.Sum(x => (decimal?)x.Tutar) ?? 0;
                        decimal toplamGider = db.TblGIDER.Sum(x => (decimal?)x.Tutar) ?? 0;
                        decimal netKar = toplamGelir - toplamGider;

                        return new { Gelir = toplamGelir, Gider = toplamGider, Net = netKar };
                    }
                });
                muhasebe_ToplamGelir.Text = finansOzeti.Gelir.ToString("C2");
                muhasebe_ToplamGider.Text = finansOzeti.Gider.ToString("C2");
                muhasebe_NetKar.Text = finansOzeti.Net.ToString("C2");

                // Net kar durumuna göre renk değişimi (Opsiyonel Güzellik)
                if (finansOzeti.Net < 0)
                    muhasebe_NetKar.Foreground = System.Windows.Media.Brushes.Red;
                else
                    muhasebe_NetKar.Foreground = System.Windows.Media.Brushes.Green;
            }
            catch (Exception)
            {
                muhasebe_ToplamGelir.Text = "₺0,00";
                muhasebe_ToplamGider.Text = "₺0,00";
                muhasebe_NetKar.Text = "₺0,00";
            }
        }
        private void btnGiderFitrele_Click(object sender, RoutedEventArgs e)
        {
            DateTime? baslangic = muhasebe_BaslanicTarih.SelectedDate;
            DateTime? bitis = muhasebe_BitisTarih.SelectedDate;
            var listele = db.TblGIDER.OrderByDescending(x => x.GiderId).AsQueryable();
            if (baslangic.HasValue)
            {
                listele = listele.Where(x => x.Tarih >= baslangic.Value);
            }
            if (bitis.HasValue)
            {
                listele = listele.Where(x => x.Tarih <= bitis.Value);
            }
            var sonuc = listele.ToList()
                          .Select(x => new
                          {
                              ID = x.GiderId,
                              Firma = x.TblFIRMA != null ? x.TblFIRMA.FirmaAdi : "Bilinmiyor",
                              GiderTürü =x.GiderTuru,
                             
                              Personel = x.TblPERSONELLER != null ? x.TblPERSONELLER.Ad + " " + x.TblPERSONELLER.Soyad : "Bilinmiyor",
                              Tarih = x.Tarih,
                              Açıklama = x.Aciklama,
                              Tutar = x.Tutar,
                          }
                          );
            muhasebeGider_DataGrid.ItemsSource = sonuc;
        }
        private void btnGelirFitrele_Click(object sender, RoutedEventArgs e)
        {
            DateTime? baslangic = muhasebe_BaslanicTarih.SelectedDate;
            DateTime? bitis = muhasebe_BitisTarih.SelectedDate;
            var listele = db.TblGELIR.OrderByDescending(x => x.GelirId).AsQueryable();
            if (baslangic.HasValue)
            {
                listele = listele.Where(x => x.Tarih >= baslangic.Value);
            }
            if (bitis.HasValue)
            {
                listele = listele.Where(x => x.Tarih <= bitis.Value);
            }
            var sonuc = listele.ToList()
                          .Select(x => new
                          {
                              ID = x.GelirId,
                              Personel = x.TblPERSONELLER != null ? x.TblPERSONELLER.Ad + " " + x.TblPERSONELLER.Soyad : "Bilinmiyor",
                              Müşteri = x.TblMUSTERILER != null ? x.TblMUSTERILER.Ad + " " + x.TblMUSTERILER.Soyad : "Bilinmiyor",
                              Tarih = x.Tarih,
                              Açıklama = x.Aciklama,
                              Tutar = x.Tutar,

                          }
                          );
            muhasebeGelir_DataGrid.ItemsSource = sonuc;
        }

    }
}
