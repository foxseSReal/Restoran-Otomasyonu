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
        RESTORANDBEntities1 db = new RESTORANDBEntities1();
        public cekSenet()
        {
            InitializeComponent();
            cekSenetListele();
            var ceksenet = db.TblCEKSENET.OrderByDescending(x => x.CeksenetId).FirstOrDefault();
            if (ceksenet != null)
            {
                cbxCekSenet_Firma.Text = ceksenet.TblFIRMA.FirmaAdi;
                cekSenet_Tutar.Text = ceksenet.Tutar.ToString();
                cbxCekSenet.Text = ceksenet.OdemeTuru.ToString();
                cekSenet_Aciklama.Text = ceksenet.Aciklama;
                cekSenet_YazmaTarih.Text = ceksenet.YTarih.ToString("dd.MM.yyyy");
                cekSenet_OdemeTarih.Text = ceksenet.OTarih.ToString();
            }


        }
        public void cekSenetListele()
        {
            var liste = db.TblCEKSENET.OrderByDescending(x => x.CeksenetId).ToList().Select
                (x => new
                {
                    ID = x.CeksenetId,
                    FirmaAdı = x.FirmaId == null ? "Firma Yok" : x.TblFIRMA.FirmaAdi,
                    Tutar = x.Tutar,
                    OdemeTuru = x.OdemeTuru,
                    Açıklama = x.Aciklama,
                    YazılmaTarihi = x.YTarih.ToString("dd.MM.yyyy"),
                    OdemeTarihi = x.OTarih

                })
                ;
            cekSenet_DataGrid.ItemsSource = liste;

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

            //secili alamı alttaki  cekSenetOdeme_DataGrid e doldur
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

        private void cekSenet_Ekle_Click(object sender, RoutedEventArgs e)
        {
            //çek senet ekleme işlemi
            TblCEKSENET yeniCekSenet = new TblCEKSENET();
            yeniCekSenet.Tutar = decimal.Parse(cekSenet_Tutar.Text);
            yeniCekSenet.OdemeTuru = cbxCekSenet.Text;
            //firma id sini bul
            var firma = db.TblFIRMA.FirstOrDefault(x => x.FirmaAdi == cbxCekSenet_Firma.Text);
            if (firma != null)
            {
                yeniCekSenet.FirmaId = firma.FirmaId;
            }
            else
            {
                yeniCekSenet.FirmaId = null; // Firma bulunamazsa null atayın
            }
            yeniCekSenet.Aciklama = cekSenet_Aciklama.Text;
            yeniCekSenet.YTarih = cekSenet_YazmaTarih.SelectedDate.HasValue ? cekSenet_YazmaTarih.SelectedDate.Value : DateTime.Now;
            yeniCekSenet.OTarih = cekSenet_OdemeTarih.SelectedDate.HasValue ? cekSenet_OdemeTarih.SelectedDate.Value : (DateTime?)null;
            yeniCekSenet.Durum = false; // Başlangıçta ödenmemiş olarak ayarla
            db.TblCEKSENET.Add(yeniCekSenet);
            db.SaveChanges();
            MessageBox.Show("Çek/Senet Eklendi.");
            cekSenetListele();



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

        private void cekSenet_Ara_TextChanged(object sender, TextChangedEventArgs e)
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




        private void cekSenetOdeme_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cekSenetListele();
        }

        private void cbxCekSenet_TurSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // çek ,senet tıklanınca ilk datagridine sadec seçileni filtrele
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
                // 1. Seçilen satırın ID'sini al
                int ceksenetId = (secildata as dynamic).ID;

                // 2. Veritabanından çeki ve kime ait olduğunu (Firma) bul
                var cekSenet = db.TblCEKSENET.Include("TblFIRMA").FirstOrDefault(x => x.CeksenetId == ceksenetId);

                if (cekSenet != null)
                {
                    // Kontrol: Zaten ödenmiş mi?
                    if (cekSenet.Durum == true)
                    {
                        MessageBox.Show("Bu ödeme zaten yapılmış.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // --- OTOMATİK MÜDÜR ID BULMA ---
                    // Gideri yapan personel olarak "Müdür"ü atıyoruz.
                    var mudur = db.TblPERSONELLER.FirstOrDefault(x => x.Pozisyon == "Müdür");

                    // Müdür yoksa hata vermesin diye varsayılan 1 yapıyoruz (Varsa kendi ID'si)
                    int atananPersonelId = (mudur != null) ? mudur.PersonelID : 1;


                    // --- ÇEK DURUMUNU GÜNCELLE ---
                    cekSenet.Durum = true; // Ödendi yap
                    if (cekSenet.OTarih == null) cekSenet.OTarih = DateTime.Now;


                    // --- GİDER TABLOSUNA KAYIT (TblGIDER) ---
                    TblGIDER yeniGider = new TblGIDER();

                    // Önemli: Textbox yerine veritabanındaki gerçek veriyi kullanıyoruz (Daha güvenli)
                    yeniGider.Tarih = DateTime.Now;
                    yeniGider.Tutar = cekSenet.Tutar;

                    // Gideri kime ödedik? (Çekin ait olduğu firma)
                    yeniGider.FirmaId = cekSenet.FirmaId;

                    // İşlemi Yapan Personel (Otomatik Müdür)
                    yeniGider.PersonelId = atananPersonelId;

                    // Açıklama
                    string firmaAdi = cekSenet.TblFIRMA != null ? cekSenet.TblFIRMA.FirmaAdi : "Firma Yok";
                    yeniGider.Aciklama = $"{firmaAdi} firmasına ait {cekSenet.OdemeTuru} ödendi. (No: {ceksenetId})";

                    // Gider Türü (Senet mi Çek mi?)
                    if (cekSenet.OdemeTuru == "Senet")
                        yeniGider.GiderTuru = "Senet Ödemesi";
                    else
                        yeniGider.GiderTuru = "Çek Ödemesi";

                    // Tabloya Ekle ve Kaydet
                    db.TblGIDER.Add(yeniGider);
                    db.SaveChanges();

                    MessageBox.Show("Ödeme yapıldı ve Gider olarak işlendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);

                    // --- LİSTEYİ YENİLE ---
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

        private void cekSenet_Filtrele_Click(object sender, RoutedEventArgs e)
        {
            DateTime ilktarih = CeksenetDataGridAralik.SelectedDate ?? DateTime.MinValue;
            DateTime sontarih = CeksenetDataGridAralik2.SelectedDate ?? DateTime.MaxValue;

            // Eğer kullanıcı yanlışlıkla tarihleri ters seçerse düzelt
            if (ilktarih > sontarih)
            {
                var trh = ilktarih;
                ilktarih = sontarih;
                sontarih = trh;
            }

            // Veritabanında filtreleme
            var bk = db.TblCEKSENET
                        .Where(x => x.YTarih >= ilktarih && (x.OTarih.HasValue ? x.OTarih.Value : DateTime.MaxValue) <= sontarih)
                        .OrderByDescending(x => x.YTarih)
                        .ToList();

            // Bellekte formatlayıp DataGrid'e ver
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

            // 🔹 Sonuçları DataGrid'e ata
            cekSenet_DataGrid.ItemsSource = filtreliListe;



            //
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

        private void ceksetnet_tahsilet_Click(object sender, RoutedEventArgs e)
        {
            var secildata = cekSenet_DataGrid.SelectedItem;

            if (secildata != null)
            {
                int ceksenetId = (secildata as dynamic).ID;

                // 1. Çek/Senet bilgisini getir
                var cekSenet = db.TblCEKSENET.Include("TblFIRMA").FirstOrDefault(x => x.CeksenetId == ceksenetId);

                if (cekSenet != null)
                {
                    // Kontrol: Daha önce tahsil edilmiş mi?
                    if (cekSenet.Durum == true)
                    {
                        MessageBox.Show("Bu zaten tahsil edilmiş.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // --- OTOMATİK MÜDÜR ID BULMA ---
                    // Ekrana sadece Müdür girebildiği için, veritabanından Pozisyonu 'Müdür' olan ilk kaydı alıyoruz.
                    var mudur = db.TblPERSONELLER.FirstOrDefault(x => x.Pozisyon == "Müdür");

                    // Eğer veritabanında hiç 'Müdür' yoksa hata vermemesi için önlem:
                    int atananPersonelId = (mudur != null) ? mudur.PersonelID : 1;
                    // Not: Eğer müdür bulunamazsa geçici olarak 1 nolu ID'yi atar.


                    // --- ÇEK DURUMUNU GÜNCELLE ---
                    cekSenet.Durum = true;
                    if (cekSenet.OTarih == null) cekSenet.OTarih = DateTime.Now;


                    // --- GELİR TABLOSUNA İŞLEME ---
                    TblGELIR yeniGelir = new TblGELIR();
                    yeniGelir.Tarih = DateTime.Now;
                    yeniGelir.Tutar = cekSenet.Tutar;

                    // !!! BURADA OTOMATİK ATAMA YAPIYORUZ !!!
                    yeniGelir.PersonelId = atananPersonelId;

                    // Açıklama Hazırlığı
                    string firmaAdi = cekSenet.TblFIRMA != null ? cekSenet.TblFIRMA.FirmaAdi : "Firma Yok";
                    yeniGelir.Aciklama = $"{firmaAdi} - Çek/Senet No: {ceksenetId} Tahsilatı (Oto. Kayıt)";

                    // Tür Belirleme
                    yeniGelir.GelirTuru = (cekSenet.OdemeTuru == "Senet") ? "Senet Tahsilatı" : "Çek Tahsilatı";

                    // Ekle ve Kaydet
                    db.TblGELIR.Add(yeniGelir);
                    db.SaveChanges();

                    MessageBox.Show("Tahsilat işlemi başarıyla kaydedildi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);

                    // --- LİSTEYİ YENİLE ---
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
            else
            {
                MessageBox.Show("Seçim yapmadınız.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
