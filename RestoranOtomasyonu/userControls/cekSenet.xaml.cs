using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.OtherWindows;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// cekSenet.xaml etkileşim mantığı
    /// </summary>
    public partial class cekSenet : UserControl
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        public cekSenet()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await cekSenetListeleAsync();

            try
            {
                var sonKayitVerisi = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        var sonKayit = db.TblCEKSENET
                                         .OrderByDescending(x => x.CeksenetId)
                                         .FirstOrDefault();

                        if (sonKayit != null)
                        {
                            return new
                            {
                                FirmaAdi = sonKayit.TblFIRMA != null ? sonKayit.TblFIRMA.FirmaAdi : "",
                                Tutar = sonKayit.Tutar,
                                OdemeTuru = sonKayit.OdemeTuru,
                                Aciklama = sonKayit.Aciklama,
                                YTarih = sonKayit.YTarih,
                                OTarih = sonKayit.OTarih
                            };
                        }
                        return null;
                    }
                });
                if (sonKayitVerisi != null)
                {
                    cbxCekSenet_Firma.Text = sonKayitVerisi.FirmaAdi;
                    cekSenet_Tutar.Text = sonKayitVerisi.Tutar.ToString();
                    cbxCekSenet.Text = sonKayitVerisi.OdemeTuru;
                    cekSenet_Aciklama.Text = sonKayitVerisi.Aciklama;

                    cekSenet_YazmaTarih.Text = sonKayitVerisi.YTarih.ToString("dd.MM.yyyy");
                    cekSenet_OdemeTarih.Text = sonKayitVerisi.OTarih?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Son kayıt getirilirken hata: " + ex.Message);
            }
        }
        public async Task cekSenetListeleAsync()
        {
            try
            {
                var liste = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblCEKSENET
                                 .OrderByDescending(x => x.CeksenetId)
                                 .ToList()
                                 .Select(x => new
                                 {
                                     ID = x.CeksenetId,
                                     FirmaAdı = x.TblFIRMA != null ? x.TblFIRMA.FirmaAdi : "Firma Yok",
                                     Tutar = x.Tutar,
                                     OdemeTuru = x.OdemeTuru,
                                     Açıklama = x.Aciklama,
                                     YazılmaTarihi = x.YTarih.ToString("dd.MM.yyyy"),
                                     OdemeTarihi = x.OTarih
                                 })
                                 .ToList();
                    }
                });
                cekSenet_DataGrid.ItemsSource = liste;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }
        private async void cekSenetOdeme_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await cekSenetListeleAsync();
        }

        private void cekSenet_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secimdoldur = cekSenet_DataGrid.SelectedItem;
            if (secimdoldur != null)
            {

                cekSenet_Tutar.Text = (cekSenet_DataGrid.SelectedItem as dynamic).Tutar.ToString();
                cbxCekSenet.Text = (cekSenet_DataGrid.SelectedItem as dynamic).OdemeTuru.ToString();
                cbxCekSenet_Firma.Text = (cekSenet_DataGrid.SelectedItem as dynamic).FirmaAdı.ToString();
                cekSenet_Aciklama.Text = (cekSenet_DataGrid.SelectedItem as dynamic).Açıklama.ToString();
                cekSenet_YazmaTarih.SelectedDate = DateTime.Parse((cekSenet_DataGrid.SelectedItem as dynamic).YazılmaTarihi.ToString());
                cekSenet_OdemeTarih.SelectedDate = DateTime.Parse((cekSenet_DataGrid.SelectedItem as dynamic).OdemeTarihi.ToString());
            }
            var secildata = cekSenet_DataGrid.SelectedItem;
            if (secildata != null)
            {
                int ceksenetId = (secildata as dynamic).ID;
                var odemeler = db.TblCEKSENET.Where(x => x.CeksenetId == ceksenetId).ToList()
                    .Select(x => new
                    {

                        FirmaAdı = x.FirmaId == null ? "Firma Yok" : x.TblFIRMA.FirmaAdi,
                        ÖdemeTarihi = x.OTarih.HasValue ? x.OTarih.Value.ToString("dd.MM.yyyy") : "",
                        Tutar = x.Tutar,
                        Durum = (bool)x.Durum ? "Ödendi" : "Ödenmedi"
                    });
                cekSenetOdeme_DataGrid.ItemsSource = odemeler;
            }

        }
        private async void cekSenet_Ekle_Click(object sender, RoutedEventArgs e)
        {
            TblCEKSENET yeniCekSenet = new TblCEKSENET();
            yeniCekSenet.Tutar = decimal.Parse(cekSenet_Tutar.Text);
            yeniCekSenet.OdemeTuru = cbxCekSenet.Text;
            var firma = db.TblFIRMA.FirstOrDefault(x => x.FirmaAdi == cbxCekSenet_Firma.Text);
            if (firma != null)
            {
                yeniCekSenet.FirmaId = firma.FirmaId;
            }
            else
            {
                yeniCekSenet.FirmaId = null;
            }
            yeniCekSenet.Aciklama = cekSenet_Aciklama.Text;
            yeniCekSenet.YTarih = cekSenet_YazmaTarih.SelectedDate.HasValue ? cekSenet_YazmaTarih.SelectedDate.Value : DateTime.Now;
            yeniCekSenet.OTarih = cekSenet_OdemeTarih.SelectedDate.HasValue ? cekSenet_OdemeTarih.SelectedDate.Value : (DateTime?)null;
            yeniCekSenet.Durum = false;
            db.TblCEKSENET.Add(yeniCekSenet);
            db.SaveChanges();
            MessageBox.Show("Çek/Senet Eklendi.");
            await cekSenetListeleAsync();
        }

        private void ceksetnet_Temizle_Click(object sender, RoutedEventArgs e)
        {
            CeksenetDataGridAralik.Text = "";
            CeksenetDataGridAralik2.Text = "";
            cekSenet_Tutar.Text = "";
            cbxCekSenet.Text = "";
            cbxCekSenet_Firma.Text = "";
            cekSenet_Aciklama.Text = "";
            cekSenet_YazmaTarih.SelectedDate = null;
            cekSenet_OdemeTarih.SelectedDate = null;
        }
        private async void cekSenet_Ara_TextChanged(object sender, TextChangedEventArgs e)
        {
            var aranacak = cekSenet_Ara.Text;
            var liste = db.TblCEKSENET.OrderByDescending(x => x.CeksenetId).Where(x => x.SatisNo.ToString().Contains(aranacak) ||
            x.Tutar.ToString().Contains(aranacak) ||
            x.OdemeTuru.Contains(aranacak) ||
            (x.TblFIRMA != null && x.TblFIRMA.FirmaAdi.Contains(aranacak)) ||
            x.Aciklama.Contains(aranacak)
            ).ToList().Select
                (x => new
                {
                    ID = x.CeksenetId,
                    SatisNo = x.SatisNo,
                    Tutar = x.Tutar,
                    OdemeTuru = x.OdemeTuru,
                    FirmaAdı = x.FirmaId == null ? "Firma Yok" : x.TblFIRMA.FirmaAdi,
                    Açıklama = x.Aciklama,
                    YazılmaTarihi = x.YTarih.ToString("dd.MM.yyyy"),
                    OdemeTarihi = x.OTarih
                })
                ;
            cekSenet_DataGrid.ItemsSource = liste;

        }

        private void cbxCekSenet_TurSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilen = cbxCekSenet_Tur.SelectedItem as ComboBoxItem;
            if (secilen != null)
            {
                string secilenTur = secilen.Content.ToString();
                var filtreliListe = db.TblCEKSENET.OrderByDescending(x => x.CeksenetId).Where(x => x.OdemeTuru == secilenTur).ToList().Select
                (x => new
                {
                    ID = x.CeksenetId,
                    SatisNo = x.SatisNo,
                    Tutar = x.Tutar,
                    OdemeTuru = x.OdemeTuru,
                    FirmaAdı = x.FirmaId == null ? "Firma Yok" : x.TblFIRMA.FirmaAdi,
                    Açıklama = x.Aciklama,
                    YazılmaTarihi = x.YTarih.ToString("dd.MM.yyyy"),
                    OdemeTarihi = x.OTarih

                })
                ;

                cekSenet_DataGrid.ItemsSource = filtreliListe;
            }

        }

        private void ceksetnet_OdemeYap_Click(object sender, RoutedEventArgs e)
        {
            var secildata = cekSenet_DataGrid.SelectedItem;

            if (secildata != null)
            {
                int ceksenetId = (secildata as dynamic).ID;
                var cekSenet = db.TblCEKSENET.Include("TblFIRMA").FirstOrDefault(x => x.CeksenetId == ceksenetId);

                if (cekSenet != null)
                {
                    if (cekSenet.Durum == true)
                    {
                        MessageBox.Show("Bu ödeme zaten yapılmış.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var mudur = db.TblPERSONELLER.FirstOrDefault(x => x.Pozisyon == "Müdür");
                    int atananPersonelId = (mudur != null) ? mudur.PersonelID : 1;
                    cekSenet.Durum = true;
                    if (cekSenet.OTarih == null) cekSenet.OTarih = DateTime.Now;
                    TblGIDER yeniGider = new TblGIDER();
                    yeniGider.Tarih = DateTime.Now;
                    yeniGider.Tutar = cekSenet.Tutar;
                    yeniGider.FirmaId = cekSenet.FirmaId;
                    yeniGider.PersonelId = atananPersonelId;
                    string firmaAdi = cekSenet.TblFIRMA != null ? cekSenet.TblFIRMA.FirmaAdi : "Firma Yok";
                    yeniGider.Aciklama = $"{firmaAdi} firmasına ait {cekSenet.OdemeTuru} ödendi. (No: {ceksenetId})";
                    if (cekSenet.OdemeTuru == "Senet")
                        yeniGider.GiderTuru = "Senet Ödemesi";
                    else
                        yeniGider.GiderTuru = "Çek Ödemesi";
                    db.TblGIDER.Add(yeniGider);
                    db.SaveChanges();
                    MessageBox.Show("Ödeme yapıldı ve Gider olarak işlendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                    var odemeler = db.TblCEKSENET
                        .Where(x => x.CeksenetId == ceksenetId)
                        .ToList()
                        .Select(x => new
                        {
                            FirmaAdı = x.TblFIRMA != null ? x.TblFIRMA.FirmaAdi : "Firma Yok",
                            ÖdemeTarihi = x.OTarih.HasValue ? x.OTarih.Value.ToString("dd.MM.yyyy") : "",
                            Tutar = x.Tutar,
                            Durum = (bool)x.Durum ? "Ödendi" : "Ödenmedi"
                        });

                    cekSenetOdeme_DataGrid.ItemsSource = odemeler;
                }
            }
            else
            {
                MessageBox.Show("Lütfen ödemesi yapılacak satırı seçiniz.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void cekSenet_Filtrele_Click(object sender, RoutedEventArgs e)
        {
            DateTime ilktarih = CeksenetDataGridAralik.SelectedDate ?? DateTime.MinValue;
            DateTime sontarih = CeksenetDataGridAralik2.SelectedDate ?? DateTime.MaxValue;
            if (ilktarih > sontarih)
            {
                var trh = ilktarih;
                ilktarih = sontarih;
                sontarih = trh;
            }
            var bk = db.TblCEKSENET
                        .Where(x => x.YTarih >= ilktarih && (x.OTarih.HasValue ? x.OTarih.Value : DateTime.MaxValue) <= sontarih)
                        .OrderByDescending(x => x.YTarih)
                        .ToList();
            var filtreliListe = bk.Select(x => new
            {
                ID = x.CeksenetId,
                SatisNo = x.SatisNo,
                Tutar = x.Tutar,
                OdemeTuru = x.OdemeTuru,
                FirmaAdı = x.FirmaId == null ? "Firma Yok" : x.TblFIRMA.FirmaAdi,
                Açıklama = x.Aciklama,
                YazılmaTarihi = x.YTarih.ToString("dd.MM.yyyy"),
                OdemeTarihi = x.OTarih.HasValue ? x.OTarih.Value.ToString("dd.MM.yyyy") : ""
            }).ToList();
            cekSenet_DataGrid.ItemsSource = filtreliListe;

            //DateTime ilktarih = CeksenetDataGridAralik.SelectedDate.HasValue ? CeksenetDataGridAralik.SelectedDate.Value : DateTime.MinValue;
            //DateTime sontarih = CeksenetDataGridAralik2.SelectedDate.HasValue ? CeksenetDataGridAralik2.SelectedDate.Value : DateTime.MinValue;
            //var filtreliListe = from x in db.TblCEKSENET.OrderByDescending(x => x.CeksenetId)
            //    .Where(x => x.YTarih >= ilktarih && x.OTarih <= sontarih)
            //                    select new
            //                    {
            //                        ID = x.CeksenetId,
            //                        SatisNo = x.SatisNo,
            //                        Tutar = x.Tutar,
            //                        OdemeTuru = x.OdemeTuru,
            //                        FirmaAdı = x.FirmaId == null ? "Firma Yok" : x.TblFIRMA.FirmaAdi,
            //                        Açıklama = x.Aciklama,
            //                        YazılmaTarihi = x.YTarih.ToString("dd.MM.yyyy"),
            //                        OdemeTarihi = x.OTarih

            //                    };
            //cekSenet_DataGrid.ItemsSource = filtreliListe.ToList();
        }

        private async void ceksetnet_tahsilet_Click(object sender, RoutedEventArgs e)
        {
            var secildata = cekSenet_DataGrid.SelectedItem;

            if (secildata != null)
            {
                int ceksenetId = (secildata as dynamic).ID;
                var cekSenet = db.TblCEKSENET.Include("TblFIRMA").FirstOrDefault(x => x.CeksenetId == ceksenetId);

                if (cekSenet != null)
                {
                    if (cekSenet.Durum == true)
                    {
                        MessageBox.Show("Bu zaten tahsil edilmiş.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    var mudur = db.TblPERSONELLER.FirstOrDefault(x => x.Pozisyon == "Müdür");
                    int atananPersonelId = (mudur != null) ? mudur.PersonelID : 1;
                    cekSenet.Durum = true;
                    if (cekSenet.OTarih == null) cekSenet.OTarih = DateTime.Now;
                    TblGELIR yeniGelir = new TblGELIR();
                    yeniGelir.Tarih = DateTime.Now;
                    yeniGelir.Tutar = cekSenet.Tutar;
                    yeniGelir.PersonelId = atananPersonelId;
                    string firmaAdi = cekSenet.TblFIRMA != null ? cekSenet.TblFIRMA.FirmaAdi : "Firma Yok";
                    yeniGelir.Aciklama = $"{firmaAdi} - Çek/Senet No: {ceksenetId} Tahsilatı (Oto. Kayıt)";
                    yeniGelir.GelirTuru = (cekSenet.OdemeTuru == "Senet") ? "Senet Tahsilatı" : "Çek Tahsilatı";
                    db.TblGELIR.Add(yeniGelir);
                    db.SaveChanges();
                    MessageBox.Show("Tahsilat işlemi başarıyla kaydedildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                    var odemeler = db.TblCEKSENET
                        .Where(x => x.CeksenetId == ceksenetId)
                        .ToList()
                        .Select(x => new
                        {
                            FirmaAdı = x.TblFIRMA != null ? x.TblFIRMA.FirmaAdi : "Firma Yok",
                            ÖdemeTarihi = x.OTarih.HasValue ? x.OTarih.Value.ToString("dd.MM.yyyy") : "",
                            Tutar = x.Tutar,
                            Durum = (bool)x.Durum ? "Tahsil Edildi" : "Bekliyor"
                        });

                    cekSenetOdeme_DataGrid.ItemsSource = odemeler;
                }
            }
            else MessageBox.Show("Seçim yapmadınız.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
