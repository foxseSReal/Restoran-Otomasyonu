using RestoranOtomasyonu.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// musteriFirma.xaml etkileşim mantığı
    /// </summary>

    public partial class musteriFirma : UserControl
    {
        RESTORANDBEntities1 db = new RESTORANDBEntities1();
        public musteriFirma()
        {
            InitializeComponent();
            MusteriListele();
            var rezarvasyon = db.TblFIRMA.OrderByDescending(x => x.FirmaId).FirstOrDefault();
        }
        public void MusteriListele()
        {

            var listele = db.TblFIRMA.OrderByDescending(x => x.FirmaId).ToList()
                        .Select(x => new
                        {
                            ID = x.FirmaId,
                            MüşteriFirma = x.FirmaAdi,
                            Adres = x.Adres,
                            Telefon = x.Telefon,
                            Telefonİki = x.Telefonİki,
                            WebSitesi = x.WebSitesi,
                            VergiDairesi = x.VergiDairesi,
                            HesapNo = x.HesapNo
                        });
            musteri_DataGrid.ItemsSource = listele;
        }
        public void FirmaGetir(int firmaId)
        {
            var bukim = db.TblFIRMA.Find(firmaId);
            musteriFirma_isim.Text = bukim.FirmaAdi;
            musteriTelefon.Text = bukim.Telefon.ToString();
            cbxMusteri_Firma.Text = bukim.Unvan;
            musteriWeb.Text = bukim.WebSitesi;
            musteriEmail.Text = bukim.Email;
            musteriTelefon.Text = bukim.Telefon;
            musteriTelefon2.Text = bukim.Telefonİki;
            musteriAdres.Text = bukim.Adres;
            musteri_vergiDairesi.Text = bukim.VergiDairesi;
            musteri_vergiDairesi_HesapNo.Text = bukim.HesapNo;
        }
        int musterifirma;
        private void musteri_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilmis = musteri_DataGrid.SelectedItem;
            if (secilmis != null)
            {
                dynamic item = secilmis;
                musterifirma = item.ID; 
                FirmaGetir(musterifirma);
                FMusteriGetir(musterifirma);
                var odeme = db.TblMODEME
                    .Where(b => b.FmusteriID == musterifirma)
                    .Select(b => new
                    {
                        ID = b.OdemeId,
                        MüşteriFirma = b.TblFIRMA.FirmaAdi,
                        BorcTutar = b.BorcTutar,
                        OdenecekTutar = b.OdenenTutar,
                        Tarih = b.Tarih,
                        Aciklama = b.Aciklama
                    }).ToList();

                Modeme_DataGrid.ItemsSource = odeme;
            }
        }
        private void musteri_ekleButton_Click(object sender, RoutedEventArgs e)
        {
            var yeniFirma = new TblFIRMA();
            yeniFirma.FirmaAdi = musteriFirma_isim.Text;
            yeniFirma.Unvan = cbxMusteri_Firma.Text;
            yeniFirma.Telefon = musteriTelefon.Text;
            yeniFirma.Telefonİki = musteriTelefon2.Text;
            yeniFirma.WebSitesi = musteriWeb.Text;
            yeniFirma.Email = musteriEmail.Text;
            yeniFirma.Adres = musteriAdres.Text;
            yeniFirma.VergiDairesi = musteri_vergiDairesi.Text;
            yeniFirma.HesapNo = musteri_vergiDairesi_HesapNo.Text;
            db.TblFIRMA.Add(yeniFirma);
            db.SaveChanges();
            MessageBox.Show("Yeni Müşteri/Firma Başarıyla Eklendi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            MusteriListele();

        }

        private void musteri_silButton_Click(object sender, RoutedEventArgs e)
        {
            var sildurum = MessageBox.Show("Seçili Müşteri/Firmayı Silmek İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (sildurum == MessageBoxResult.Yes)
            {
                var secilmis = musteri_DataGrid.SelectedItem;
                if (secilmis != null)
                {
                    dynamic item = secilmis;
                    int musterifirma = item.ID;
                    var bukim = db.TblFIRMA.Find(musterifirma);
                    db.TblFIRMA.Remove(bukim);
                    db.SaveChanges();
                    MessageBox.Show("Seçili Müşteri/Firma Başarıyla Silindi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                    MusteriListele();
                }
            }
        }

        private void musteri_guncelleButton_Click(object sender, RoutedEventArgs e)
        {
            var güncelle = MessageBox.Show("Seçili Müşteri/Firmayı Güncellemek İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (güncelle == MessageBoxResult.Yes)
            {
                var secilmis = musteri_DataGrid.SelectedItem;
                if (secilmis != null)
                {
                    dynamic item = secilmis;
                    int musterifirma = item.ID;
                    var bukim = db.TblFIRMA.Find(musterifirma);
                    bukim.FirmaAdi = musteriFirma_isim.Text;
                    bukim.Unvan = cbxMusteri_Firma.Text;
                    bukim.Telefon = musteriTelefon.Text;
                    bukim.Telefonİki = musteriTelefon2.Text;
                    bukim.WebSitesi = musteriWeb.Text;
                    bukim.Email = musteriEmail.Text;
                    bukim.Adres = musteriAdres.Text;
                    bukim.VergiDairesi = musteri_vergiDairesi.Text;
                    bukim.HesapNo = musteri_vergiDairesi_HesapNo.Text;
                    db.SaveChanges();
                    MessageBox.Show("Seçili Müşteri/Firma Başarıyla Güncellendi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                    MusteriListele();
                }
            }
        }

        private void musteri_temizleButton_Click(object sender, RoutedEventArgs e)
        {
            musteriFirma_isim.Clear();
            cbxMusteri_Firma.SelectedIndex = -1;
            musteriTelefon.Clear();
            musteriTelefon2.Clear();
            musteriWeb.Clear();
            musteriEmail.Clear();
            musteriAdres.Clear();
            musteri_vergiDairesi.Clear();
            musteri_vergiDairesi_HesapNo.Clear();
        }

        private void musteriFirma_Ara_TextChanged(object sender, TextChangedEventArgs e)
        {
            var urunara = musteriFirma_Ara.Text.ToLower();
            var filtreliListe = db.TblFIRMA.OrderByDescending(x => x.FirmaId)
                .Where(x => x.FirmaAdi.ToLower().Contains(urunara))
                .Select(x => new
                {
                    ID = x.FirmaId,
                    MüşteriFirma = x.FirmaAdi,
                    Adres = x.Adres,
                    Telefon = x.Telefon,
                    Telefonİki = x.Telefonİki,
                    WebSitesi = x.WebSitesi,
                    VergiDairesi = x.VergiDairesi,
                    HesapNo = x.HesapNo
                })
                .ToList();
            musteri_DataGrid.ItemsSource = filtreliListe;
        }
        public void BorcListele()
        {
            var listele = db.TblMODEME.OrderByDescending(x => x.OdemeId)
                         .Where(x=>x.FmusteriID==x.TblFIRMA.FirmaId)
                         .Select(x => new
                         {
                             ID = x.OdemeId,
                             MüşteriFirma = x.TblFIRMA.FirmaAdi,
                             BorcTutar = x.BorcTutar,
                             OdenecekTutar = x.OdenenTutar,
                             Tarih = x.Tarih,
                             Aciklama = x.Aciklama
                         }).ToList();
            Modeme_DataGrid.ItemsSource = listele;
        }

        private void btnBorcListele_Click(object sender, RoutedEventArgs e)
        {
            BorcListele();
        }
        public void MusteriGetir(int mId)
        {
            var bukim = db.TblMODEME.Find(mId);
            gizliText.Text = bukim.OdemeId.ToString();
            txtMusteri.Text = bukim.TblFIRMA.FirmaAdi;
            txtBorcTutar.Text = bukim.BorcTutar.ToString();
            txtOdenecekTutar.Text = bukim.OdenenTutar.ToString();
            txtAciklama.Text = bukim.Aciklama;
            BorcTarih.Text = bukim.Tarih.ToString();
        }
        public void FMusteriGetir(int mId)
        {
            var bukim = db.TblFIRMA.Find(mId);
            gizliText.Text = bukim.FirmaId.ToString();
            txtMusteri.Text = bukim.FirmaAdi;
        }

        private void Modeme_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilmis = Modeme_DataGrid.SelectedItem;
            if (secilmis != null)
            {
                dynamic item = secilmis;
                int mId = item.ID;
                MusteriGetir(mId);
            }

        }

        private void btnBorcEkle_Click(object sender, RoutedEventArgs e)
        {
            var yeniBorc = new TblMODEME();
            yeniBorc.FmusteriID = db.TblFIRMA.Where(x => x.FirmaAdi == txtMusteri.Text).Select(x => x.FirmaId).FirstOrDefault();
            yeniBorc.OdemeId = db.TblFIRMA.Where(x => x.FirmaAdi == txtMusteri.Text).Select(x => x.FirmaId).FirstOrDefault();
            yeniBorc.BorcTutar = decimal.Parse(txtBorcTutar.Text);
            yeniBorc.OdenenTutar = decimal.TryParse(txtOdenecekTutar.Text, out decimal odenen) ? odenen : 0; ;
            yeniBorc.Tarih = DateTime.Parse(BorcTarih.Text);
            yeniBorc.Aciklama = txtAciklama.Text;
            db.TblMODEME.Add(yeniBorc);
            db.SaveChanges();
            MessageBox.Show("Yeni Borç Kaydı Başarıyla Eklendi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            BorcListele();
        }

        private void btnBorcGuncelle_Click(object sender, RoutedEventArgs e)
        {
            var secilmis = Modeme_DataGrid.SelectedItem;

            if (secilmis != null)
            {
                dynamic item = secilmis;
                int mId = item.ID; 
                var bukim = db.TblMODEME.Include("TblFIRMA").FirstOrDefault(x => x.OdemeId == mId);

                if (bukim != null)
                {
                    decimal borc, odenen;
                    DateTime tarih;
                    if (!decimal.TryParse(txtBorcTutar.Text, out borc)) borc = 0;
                    if (!decimal.TryParse(txtOdenecekTutar.Text, out odenen)) odenen = 0;
                    if (!DateTime.TryParse(BorcTarih.Text, out tarih)) tarih = DateTime.Now;
                    bukim.BorcTutar = borc;
                    bukim.OdenenTutar = odenen;
                    bukim.Tarih = tarih;
                    bukim.Aciklama = txtAciklama.Text;
                    string isim = "Bilinmiyor";
                    if (bukim.TblFIRMA != null)
                    {
                        isim = bukim.TblFIRMA.FirmaAdi;
                    }
                    var mudur = db.TblPERSONELLER.FirstOrDefault(x => x.Pozisyon == "Müdür");
                    int atananPersonelId = (mudur != null) ? mudur.PersonelID : 1;
                    TblGELIR yeniGelir = new TblGELIR();
                    yeniGelir.Tarih = DateTime.Now;
                    yeniGelir.Tutar = odenen;
                    yeniGelir.PersonelId = atananPersonelId;
                    yeniGelir.Aciklama = $"{isim} - Tahsilat - {txtAciklama.Text}";
                    yeniGelir.GelirTuru = "Müşteri Tahsilatı";
                    db.TblGELIR.Add(yeniGelir);
                    db.SaveChanges();
                    MessageBox.Show($"Sayın {isim} için ödeme alındı ve işlendi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                    BorcListele();
                }
            }
            else
            {
                MessageBox.Show("Lütfen listeden bir kayıt seçiniz.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnBorcSil_Click(object sender, RoutedEventArgs e)
        {
            var secilmis = Modeme_DataGrid.SelectedItem;
            dynamic item = secilmis;
            int mId = item.ID;
            var bukim = db.TblMODEME.Find(mId);
            db.TblMODEME.Remove(bukim);
            db.SaveChanges();
            MessageBox.Show("Seçili Borç Kaydı Başarıyla Silindi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            BorcListele();
            txtMusteri.Clear();
            txtBorcTutar.Clear();
            txtOdenecekTutar.Clear();
            txtAciklama.Clear();
            BorcTarih.Text = "";
        }
    }
}
