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

namespace ProjePersonelYonetım.userControls
{
    /// <summary>
    /// muhasebe.xaml etkileşim mantığı
    /// </summary>
    public partial class muhasebe : UserControl
    {
        RESTORANDBEntities1 db = new RESTORANDBEntities1();
        public muhasebe()
        {
            InitializeComponent();
            GelirListele();
            GiderListele();
        }
        public void GelirListele()
        {
            var listele = db.TblGELIR.OrderByDescending(x => x.GelirId).ToList()
                          .Select(x => new
                          {
                              ID = x.GelirId,
                              Personel = x.TblPERSONELLER != null ? x.TblPERSONELLER.Ad + " " + x.TblPERSONELLER.Soyad : "Bilinmiyor",
                              Müşteri = x.TblMUSTERILER != null ? x.TblMUSTERILER.Ad + " " + x.TblMUSTERILER.Soyad : "Bilinmiyor",
                              Tarih = x.Tarih,
                              Açıklama = x.Aciklama,
                              Tutar = x.Tutar,


                          });
            muhasebeGelir_DataGrid.ItemsSource = listele;
            muhasebe_ToplamGelir.Text = db.TblGELIR.Sum(x => x.Tutar).ToString();
           muhasebe_ToplamGider.Text = db.TblGIDER.Sum(x => x.Tutar).ToString();
           muhasebe_NetKar.Text = (db.TblGELIR.Sum(x => x.Tutar) - db.TblGIDER.Sum(x => x.Tutar)).ToString();
        }
        public void GiderListele()
        {
            var listele = db.TblGIDER.OrderByDescending(x => x.GiderId).ToList()
                          .Select(x => new
                          {
                              ID = x.GiderId,
                              Personel = x.TblPERSONELLER != null ? x.TblPERSONELLER.Ad + " " + x.TblPERSONELLER.Soyad : "Bilinmiyor",
                              Firma = x.TblFIRMA != null ? x.TblFIRMA.FirmaAdi : "Bilinmiyor",
                              Tarih = x.Tarih,
                              Açıklama = x.Aciklama,
                              Tutar = x.Tutar,
                          });
            muhasebeGider_DataGrid.ItemsSource = listele;
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
                              Personel = x.TblPERSONELLER != null ? x.TblPERSONELLER.Ad + " " + x.TblPERSONELLER.Soyad : "Bilinmiyor",
                              Firma = x.TblFIRMA != null ? x.TblFIRMA.FirmaAdi : "Bilinmiyor",
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
