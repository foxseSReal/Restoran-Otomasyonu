using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.OtherWindows;
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
    /// Firmalar.xaml etkileşim mantığı
    /// </summary>
    public partial class Firmalar : UserControl
    {
        private int _seciliFirmaId = 0;
        public Firmalar()
        {
            InitializeComponent();
        }

        private void FirmaDetaylar(object sender, RoutedEventArgs e)
        {
            FirmaDetaylar FirmaDetaylar = new FirmaDetaylar();
            FirmaDetaylar.ShowDialog();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await FirmaListeleAsync();
            await SonFirmayiGetirAsync();
        }

        private async void musteriFirma_Ara_TextChanged(object sender, TextChangedEventArgs e)
        {
            string urunara = musteriFirma_Ara.Text.ToLower();
            try
            {
                var filtreliListe = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblFIRMA.OrderByDescending(x => x.FirmaId)
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
                                }).ToList();
                    }
                });
                firma_DataGrid.ItemsSource = filtreliListe;
            }
            catch { }
        }

        private async void firma_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilmis = firma_DataGrid.SelectedItem as dynamic;
            if (secilmis != null)
            {
                int id = secilmis.ID;
                _seciliFirmaId = id;
                await FirmaGetirAsync(id);
                await FMusteriGetirAsync(id);
                await BorclariGetirByFirmaAsync(id);
            }
        }

        private async Task BorclariGetirByFirmaAsync(int firmaId)
        {
            try
            {
                var borclar = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblMODEME.Where(b => b.FmusteriID == firmaId)
                                 .Select(b => new
                                 {
                                     ID = b.OdemeId,
                                     MüşteriFirma = b.TblFIRMA.FirmaAdi,
                                     BorcTutar = b.BorcTutar,
                                     OdenecekTutar = b.OdenenTutar,
                                     Tarih = b.Tarih,
                                     Aciklama = b.Aciklama
                                 }).ToList();
                    }
                });
                Fodeme_DataGrid.ItemsSource = borclar;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public async Task FMusteriGetirAsync(int mId)
        {
            try
            {
                var firmaData = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        var bukim = db.TblFIRMA.FirstOrDefault(x => x.FirmaId == mId);
                        return bukim != null ? new { bukim.FirmaId, bukim.FirmaAdi } : null;
                    }
                });

                if (firmaData != null)
                {
                    gizliText.Text = firmaData.FirmaId.ToString();
                    txtMusteri.Text = firmaData.FirmaAdi;
                    _seciliFirmaId = firmaData.FirmaId;
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        public async Task MusteriGetirAsync(int mId)
        {
            try
            {
                var odemeDetay = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        var bukim = db.TblMODEME.FirstOrDefault(x => x.OdemeId == mId);
                        if (bukim != null)
                        {
                            return new
                            {
                                bukim.OdemeId,
                                FirmaAdi = bukim.TblFIRMA != null ? bukim.TblFIRMA.FirmaAdi : "",
                                BorcTutar = bukim.BorcTutar ?? 0,
                                OdenenTutar = bukim.OdenenTutar ?? 0,

                                bukim.Aciklama,
                                bukim.Tarih
                            };
                        }
                        return null;
                    }
                });

                if (odemeDetay != null)
                {
                    gizliText.Text = odemeDetay.OdemeId.ToString();
                    txtMusteri.Text = odemeDetay.FirmaAdi;
                    txtBorcTutar.Text = odemeDetay.BorcTutar.ToString();
                    txtOdenecekTutar.Text = odemeDetay.OdenenTutar.ToString();
                    txtAciklama.Text = odemeDetay.Aciklama;
                    BorcTarih.Text = odemeDetay.Tarih.ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private async void Fodeme_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilmis = Fodeme_DataGrid.SelectedItem as dynamic;
            if (secilmis != null)
            {
                int mId = secilmis.ID;
                await MusteriGetirAsync(mId);
            }
        }

        private async void musteri_ekleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new RESTORANDBEntities())
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
                    await db.SaveChangesAsync();
                }
                MessageBox.Show("Yeni Müşteri/Firma Başarıyla Eklendi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                await FirmaListeleAsync();
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private async void btnBorcListele_Click(object sender, RoutedEventArgs e)
        {
            await BorcListeleAsync();
        }
        public async Task FirmaListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblFIRMA.OrderByDescending(x => x.FirmaId).Where(x => x.Unvan == "Firma")
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
                                 }).ToList();
                    }
                });

                firma_DataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }

        private async void musteri_silButton_Click(object sender, RoutedEventArgs e)
        {
            var secilmis = firma_DataGrid.SelectedItem as dynamic;
            if (secilmis == null) return;

            if (MessageBox.Show("Silmek İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    int id = secilmis.ID;
                    using (var db = new RESTORANDBEntities())
                    {
                        var bukim = db.TblFIRMA.Find(id);
                        if (bukim != null)
                        {
                            db.TblFIRMA.Remove(bukim);
                            await db.SaveChangesAsync();
                        }
                    }
                    MessageBox.Show("Silindi.");
                    await FirmaListeleAsync();
                }
                catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            }
        }

        private async void musteri_guncelleButton_Click(object sender, RoutedEventArgs e)
        {
            var secilmis = firma_DataGrid.SelectedItem as dynamic;
            if (secilmis == null) return;

            if (MessageBox.Show("Güncellemek İstediğinize Emin Misiniz?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    int id = secilmis.ID;
                    using (var db = new RESTORANDBEntities())
                    {
                        var bukim = db.TblFIRMA.Find(id);
                        if (bukim != null)
                        {
                            bukim.FirmaAdi = musteriFirma_isim.Text;
                            bukim.Unvan = cbxMusteri_Firma.Text;
                            bukim.Telefon = musteriTelefon.Text;
                            bukim.Telefonİki = musteriTelefon2.Text;
                            bukim.WebSitesi = musteriWeb.Text;
                            bukim.Email = musteriEmail.Text;
                            bukim.Adres = musteriAdres.Text;
                            bukim.VergiDairesi = musteri_vergiDairesi.Text;
                            bukim.HesapNo = musteri_vergiDairesi_HesapNo.Text;
                            await db.SaveChangesAsync();
                        }
                    }
                    MessageBox.Show("Güncellendi.");
                    await FirmaListeleAsync();
                }
                catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
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
            _seciliFirmaId = 0;
        }


        private async void btnBorcEkle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new RESTORANDBEntities())
                {
                    var yeniBorc = new TblMODEME();
                    int hedefFirmaId = _seciliFirmaId;
                    if (hedefFirmaId == 0)
                    {
                        var firma = db.TblFIRMA.FirstOrDefault(x => x.FirmaAdi == txtMusteri.Text);
                        if (firma != null) hedefFirmaId = firma.FirmaId;
                        else { MessageBox.Show("Geçerli bir firma bulunamadı."); return; }
                    }
                    yeniBorc.FmusteriID = hedefFirmaId;
                    yeniBorc.BorcTutar = decimal.TryParse(txtBorcTutar.Text, out decimal b) ? b : 0;
                    yeniBorc.OdenenTutar = decimal.TryParse(txtOdenecekTutar.Text, out decimal o) ? o : 0;
                    yeniBorc.Tarih = DateTime.TryParse(BorcTarih.Text, out DateTime t) ? t : DateTime.Now;
                    yeniBorc.Aciklama = txtAciklama.Text;
                    db.TblMODEME.Add(yeniBorc);
                    await db.SaveChangesAsync();
                }
                MessageBox.Show("Borç Eklendi.");
                await BorcListeleAsync();
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private async void btnBorcGuncelle_Click(object sender, RoutedEventArgs e)
        {
            var secilmis = Fodeme_DataGrid.SelectedItem as dynamic;
            if (secilmis == null) { MessageBox.Show("Seçim yapınız."); return; }
            try
            {
                int mId = secilmis.ID;
                using (var db = new RESTORANDBEntities())
                {
                    var bukim = db.TblMODEME.Include("TblFIRMA").FirstOrDefault(x => x.OdemeId == mId);

                    if (bukim != null)
                    {
                        bukim.BorcTutar = decimal.TryParse(txtBorcTutar.Text, out decimal b) ? b : 0;
                        bukim.OdenenTutar = decimal.TryParse(txtOdenecekTutar.Text, out decimal o) ? o : 0;
                        bukim.Tarih = DateTime.TryParse(BorcTarih.Text, out DateTime t) ? t : DateTime.Now;
                        bukim.Aciklama = txtAciklama.Text;
                        string firmaAdi = bukim.TblFIRMA != null ? bukim.TblFIRMA.FirmaAdi : "Bilinmiyor";
                        var mudur = db.TblPERSONELLER.FirstOrDefault(x => x.Pozisyon == "Müdür");
                        int personelId = (mudur != null) ? mudur.PersonelID : 1;
                        TblGELIR yeniGelir = new TblGELIR
                        {
                            Tarih = DateTime.Now,
                            Tutar = bukim.OdenenTutar ?? 0,
                            PersonelId = personelId,
                            Aciklama = $"{firmaAdi} - Tahsilat - {txtAciklama.Text}",
                            GelirTuru = "Müşteri Tahsilatı"
                        };

                        db.TblGELIR.Add(yeniGelir);
                        await db.SaveChangesAsync();
                        MessageBox.Show("Ödeme alındı ve işlendi.");
                    }
                }
                await BorcListeleAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        public async Task BorcListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        return db.TblMODEME.OrderByDescending(x => x.OdemeId)
                                 .Where(x => x.TblFIRMA.Unvan != "Müşteri")
                                 .Select(x => new
                                 {
                                     ID = x.OdemeId,
                                     MüşteriFirma = x.TblFIRMA.FirmaAdi,
                                     BorcTutar = x.BorcTutar ?? 0,
                                     OdenecekTutar = x.OdenenTutar ?? 0,
                                     Tarih = x.Tarih,
                                     Aciklama = x.Aciklama
                                 }).ToList();
                    }
                });
                Fodeme_DataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Borç listeleme hatası: " + ex.Message);
            }
        }

        public async Task FirmaGetirAsync(int firmaId)
        {
            try
            {
                var firmaDetay = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities())
                    {
                        var bukim = db.TblFIRMA.FirstOrDefault(x => x.FirmaId == firmaId);
                        if (bukim != null)
                        {
                            return new
                            {
                                bukim.FirmaAdi,
                                bukim.Telefon,
                                bukim.Unvan,
                                bukim.WebSitesi,
                                bukim.Email,
                                bukim.Telefonİki,
                                bukim.Adres,
                                bukim.VergiDairesi,
                                bukim.HesapNo
                            };
                        }
                        return null;
                    }
                });

                if (firmaDetay != null)
                {
                    musteriFirma_isim.Text = firmaDetay.FirmaAdi;
                    musteriTelefon.Text = firmaDetay.Telefon;
                    cbxMusteri_Firma.Text = firmaDetay.Unvan;
                    musteriWeb.Text = firmaDetay.WebSitesi;
                    musteriEmail.Text = firmaDetay.Email;
                    musteriTelefon2.Text = firmaDetay.Telefonİki;
                    musteriAdres.Text = firmaDetay.Adres;
                    musteri_vergiDairesi.Text = firmaDetay.VergiDairesi;
                    musteri_vergiDairesi_HesapNo.Text = firmaDetay.HesapNo;
                }
            }
            catch (Exception ex) { MessageBox.Show("Firma Getir Hata: " + ex.Message); }
        }

        private async void btnBorcSil_Click(object sender, RoutedEventArgs e)
        {
            var secilmis = Fodeme_DataGrid.SelectedItem as dynamic;
            if (secilmis == null) return;

            try
            {
                int mId = secilmis.ID;
                using (var db = new RESTORANDBEntities())
                {
                    var bukim = db.TblMODEME.Find(mId);
                    if (bukim != null)
                    {
                        db.TblMODEME.Remove(bukim);
                        await db.SaveChangesAsync();
                    }
                }
                MessageBox.Show("Silindi.");
                await BorcListeleAsync();
                txtMusteri.Clear();
                txtBorcTutar.Clear();
                txtOdenecekTutar.Clear();
                txtAciklama.Clear();
                BorcTarih.Text = "";
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }
        private async Task SonFirmayiGetirAsync()
        {
            int? sonId = await Task.Run(() =>
            {
                using (var db = new RESTORANDBEntities())
                {
                    return db.TblFIRMA.OrderByDescending(x => x.FirmaId).Where(x => x.Unvan == "Müşteri")
                                    .Select(x => (int?)x.FirmaId)
                                    .FirstOrDefault();
                }
            });

            if (sonId.HasValue)
                await FirmaGetirAsync(sonId.Value);
        }
    }
}
