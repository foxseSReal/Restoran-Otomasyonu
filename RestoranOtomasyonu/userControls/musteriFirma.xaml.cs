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

namespace ProjePersonelYonetım.userControls
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
        //Müşteri Listele
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
        //=================Veri Getir=======================/
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
        private void musteri_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilmis = musteri_DataGrid.SelectedItem;
            if (secilmis != null)
            {
                dynamic item = secilmis;
                int musterifirma = item.ID;
                FirmaGetir(musterifirma);
            }
            var musteri = musteri_DataGrid.SelectedItem;
            var sec = musteri_DataGrid.SelectedItem;
            if (sec != null)
            {
                dynamic item = secilmis;
                int id = item.ID;
                FMusteriGetir(id);
            }
            var musteri1 = musteri_DataGrid.SelectedItem;
            



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
        {//müşteri borç getir

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
        {//borc listeleme ekranı açılacak

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
            //borc ekleme
            var yeniBorc = new TblMODEME();
            yeniBorc.FmusteriID = db.TblFIRMA.Where(x => x.FirmaAdi == txtMusteri.Text).Select(x => x.FirmaId).FirstOrDefault();
            yeniBorc.OdemeId = db.TblFIRMA.Where(x => x.FirmaAdi == txtMusteri.Text).Select(x => x.FirmaId).FirstOrDefault();
            yeniBorc.BorcTutar = decimal.Parse(txtBorcTutar.Text);
            yeniBorc.OdenenTutar = decimal.Parse(txtOdenecekTutar.Text);
            yeniBorc.Tarih = DateTime.Parse(BorcTarih.Text);
            yeniBorc.Aciklama = txtAciklama.Text;
            db.TblMODEME.Add(yeniBorc);
            db.SaveChanges();
            MessageBox.Show("Yeni Borç Kaydı Başarıyla Eklendi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            BorcListele();

        }

        private void btnBorcGuncelle_Click(object sender, RoutedEventArgs e)
        {
            // Borç Ödeme
            var secilmis = Modeme_DataGrid.SelectedItem;
            dynamic item = secilmis;
            int mId = item.ID;
            var bukim = db.TblMODEME.Find(mId);
            bukim.BorcTutar = decimal.Parse(txtBorcTutar.Text);
            bukim.OdenenTutar = decimal.Parse(txtOdenecekTutar.Text);
            bukim.Tarih = DateTime.Parse(BorcTarih.Text);
            bukim.Aciklama = txtAciklama.Text;
            db.SaveChanges();
            MessageBox.Show("Ödeme İşlemi Başarılı", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
           
            BorcListele();
            



        }

        private void btnBorcSil_Click(object sender, RoutedEventArgs e)
        {
            //borç silme drumu true ise sil
            var secilmis = Modeme_DataGrid.SelectedItem;
            dynamic item = secilmis;
            int mId = item.ID;
            var bukim = db.TblMODEME.Find(mId);
            db.TblMODEME.Remove(bukim);
            db.SaveChanges();
            MessageBox.Show("Seçili Borç Kaydı Başarıyla Silindi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            BorcListele();
            //temizle
            txtMusteri.Clear();
            txtBorcTutar.Clear();
            txtOdenecekTutar.Clear();
            txtAciklama.Clear();
            BorcTarih.Text = "";

        }

        
    }
}
