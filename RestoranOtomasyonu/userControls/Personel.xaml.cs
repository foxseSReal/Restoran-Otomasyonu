using Microsoft.Win32;
using RestoranOtomasyonu.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
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
using static MaterialDesignThemes.Wpf.Theme;

namespace RestoranOtomasyonu.userControls
{
    /// <summary>
    /// Personel.xaml etkileşim mantığı
    /// </summary>
    public partial class Personel : UserControl
    {

        //==================================================//
        RESTORANDBEntities1 ResDB = new RESTORANDBEntities1();
        //==================================================//

        public Personel()
        {
            InitializeComponent();

            var sonPersonel = ResDB.TblPERSONELLER.OrderByDescending(x => x.PersonelID).FirstOrDefault();

            if (sonPersonel != null)
            {
                var maasKaydi = ResDB.TblMAAS
                                     .Where(x => x.PersonelID == sonPersonel.PersonelID)
                                     .OrderByDescending(x => x.NetTutar)
                                     .FirstOrDefault();
                adSoyad.Text = sonPersonel.Ad + " " + sonPersonel.Soyad;
                maas.Text = maasKaydi != null ? maasKaydi.NetTutar.ToString() : "Maaş Yok";
                adres.Text = sonPersonel.Adres;
                telefon.Text = sonPersonel.Telefon;
                email.Text = sonPersonel.Email;
                if (sonPersonel.Tarih != null)
                {
                    Tarih.SelectedDate = sonPersonel.Tarih;
                }
                cbxPosizyon.Text = sonPersonel.Pozisyon;
                tckimlik.Text = sonPersonel.TCKimlikNo;
                IsTakip_ToggleButton.IsChecked = sonPersonel.Durum;
                ResimYukle(sonPersonel.Resim);
            }
            else
            {
                MessageBox.Show("Sistemde kayıtlı personel bulunamadı.");
            }
        }
        private string secilenResimYolu; 
        public void personelListele()
        {
            var personel = from x in ResDB.TblPERSONELLER.OrderByDescending(x => x.PersonelID)
                           select new
                           {
                               x.PersonelID,
                               x.Ad,
                               x.Soyad,
                               x.Telefon,
                               x.Adres,
                               x.Pozisyon,
                               x.Email,
                               x.Tarih,
                               DURUMU = x.Durum == true ? "Aktif" : "Pasif"
                           };
            personel_DataGrid.ItemsSource = personel.ToList();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            personelListele();
        }
        private void ResimYukle(string dosyaAdi)
        {
            if (string.IsNullOrEmpty(dosyaAdi))
            {
                dosyaAdi = "resimyok.png"; 
            }
            try
            {
                string klasorYolu = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resimler");
                string resimTamYolu = System.IO.Path.Combine(klasorYolu, dosyaAdi);

                if (File.Exists(resimTamYolu))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(resimTamYolu, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    resim.ImageSource = bitmap;
                }
                else
                {
                    resim.ImageSource = null;
                }
            }
            catch (Exception)
            {
                resim.ImageSource = null; 
            }
        }


        private void personel_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            secilenResimYolu = null;
            if (personel_DataGrid.SelectedItem == null)
                return;
            var selectedRow = personel_DataGrid.SelectedItem;
            var idProp = selectedRow.GetType().GetProperty("PersonelID");
            if (idProp == null)
                return;
            int calisanId = (int)idProp.GetValue(selectedRow);
            var bukim = ResDB.TblPERSONELLER.Find(calisanId);
            if (bukim != null)
            {
                var maasKaydi = ResDB.TblMAAS.Where(x => x.PersonelID == bukim.PersonelID).OrderByDescending(x => x.NetTutar).FirstOrDefault();
                adSoyad.Text = bukim.Ad + " " + bukim.Soyad;
                maas.Text = maasKaydi != null ? maasKaydi.NetTutar.ToString() : "Yok";
                adres.Text = bukim.Adres;
                telefon.Text = bukim.Telefon;
                email.Text = bukim.Email;
                Tarih.SelectedDate = bukim.Tarih;

                cbxPosizyon.Text = bukim.Pozisyon;
                tckimlik.Text = bukim.TCKimlikNo;

                if (bukim.Durum == true)
                {
                    IsTakip_ToggleButton.IsChecked = true;
                }
                else
                {
                    IsTakip_ToggleButton.IsChecked = false;
                }
                ResimYukle(bukim.Resim);
            }

            //// ... (Bordro listeleme kodunuz aynı kalıyor) ...
            //if (personel_DataGrid.SelectedItem == null) return;

            //int personelId = (int)((dynamic)personel_DataGrid.SelectedItem).PersonelID;

            //var bordroListesi = ResDB.TblBORDROLAR
            //    .Where(b => b.PersonelID == personelId)
            //    .Select(b => new
            //    {
            //        ADSOYAD = b.TblPERSONELLER.Ad + " " + b.TblPERSONELLER.Soyad,
            //        b.Ay,
            //        b.Yil,
            //        b.AnaMaas,
            //        b.Prim,
            //        b.Kesinti,
            //        b.Avans,
            //        b.ToplamOdeme,
            //        b.OdemeTarihi
            //    })
            //    .ToList();

            //personelOdeme_DataGrid.ItemsSource = bordroListesi;
            if (personel_DataGrid.SelectedItem == null) return;

            seciliPersonelId = (int)((dynamic)personel_DataGrid.SelectedItem).PersonelID;

            var bordroListesi = ResDB.TblPERSONELODEME
                .Where(b => b.PERSONEL == seciliPersonelId)
                .Select(b => new
                {
                    ID = b.ID,
                    Personel = b.TblPERSONELLER.Ad + " " + b.TblPERSONELLER.Soyad,
                    ÖdemeTürü = b.TUR,
                    Tutar = b.ODEMEMIKTARI,
                    Tarih = b.TARIH,
                    Açıklama = b.ACIKLAMA
                }).ToList();

            personelOdeme_DataGrid.ItemsSource = bordroListesi;
        }
        private int seciliPersonelId = -1;

        private void personel_DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void Resimekleme(object sender, RoutedEventArgs e)
        {
            OpenFileDialog resimSec = new OpenFileDialog();
            resimSec.Filter = "Resim Dosyaları (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (resimSec.ShowDialog() == true)
            {
                secilenResimYolu = resimSec.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(secilenResimYolu));
                resim.ImageSource = bitmap;
            }
        }

        private string ResmiKaydetVeAdiniGetir(string mevcutDosyaAdi)
        {
            if (string.IsNullOrEmpty(secilenResimYolu))
            {
                return string.IsNullOrEmpty(mevcutDosyaAdi) ? "resimyok.png" : mevcutDosyaAdi;
            }
            try
            {
                string klasorYolu = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resimler");
                if (!Directory.Exists(klasorYolu))
                    Directory.CreateDirectory(klasorYolu);
                string dosyaAdi = System.IO.Path.GetFileName(secilenResimYolu);
                string hedefYol = System.IO.Path.Combine(klasorYolu, dosyaAdi);

                File.Copy(secilenResimYolu, hedefYol, true);

                return dosyaAdi; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Resim kopyalanırken hata: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.IsNullOrEmpty(mevcutDosyaAdi) ? "resimyok.png" : mevcutDosyaAdi;
            }
        }

        private void btnekle(object sender, RoutedEventArgs e)
        {
            var adSoyadDizi = adSoyad.Text.Split(' ');
            string ad = adSoyadDizi.Length > 0 ? adSoyadDizi[0] : "";
            string soyad = adSoyadDizi.Length > 1 ? string.Join(" ", adSoyadDizi.Skip(1)) : "";
            var selectedItem = cbxPosizyon.SelectedItem as ComboBoxItem;
            if (!Tarih.SelectedDate.HasValue)
            {
                MessageBox.Show("Lütfen bir giriş tarihi seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; 
            }
            DateTime girisTarihi = Tarih.SelectedDate.Value;
            decimal odeme = 0;
            decimal.TryParse(maas.Text, out odeme);
            string resimDosyaAdi = ResmiKaydetVeAdiniGetir(null);
            TblPERSONELLER yeniCalisan = new TblPERSONELLER
            {
                Ad = ad,
                Soyad = soyad,
                Telefon = telefon.Text,
                Adres = adres.Text,
                Email = email.Text,
                Pozisyon = cbxPosizyon.Text,
                Tarih = girisTarihi,
                Durum = IsTakip_ToggleButton.IsChecked == true,
                TCKimlikNo = tckimlik.Text,
                Resim = resimDosyaAdi
            };
            ResDB.TblPERSONELLER.Add(yeniCalisan);
            try
            {
                ResDB.SaveChanges();
                TblMAAS yeniMaas = new TblMAAS
                {
                    PersonelID = yeniCalisan.PersonelID,
                    NetTutar = odeme,
                };
                ResDB.TblMAAS.Add(yeniMaas);
                ResDB.SaveChanges();

                personelListele();
                MessageBox.Show("Yeni Personel Eklendi", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                temizle();
            }
            catch (DbEntityValidationException ex)
            {
                string hata = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    hata += $"Entity: {eve.Entry.Entity.GetType().Name}\n";
                    foreach (var ve in eve.ValidationErrors)
                    {
                        hata += $"Property: {ve.PropertyName}, Error: {ve.ErrorMessage}\n";
                    }
                }
                MessageBox.Show(hata, "Doğrulama Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Genel Hata: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void guncelleButton_Click(object sender, RoutedEventArgs e)
        {
            if (personel_DataGrid.SelectedItem is null)
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz personeli seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int id = (int)((dynamic)personel_DataGrid.SelectedItem).PersonelID;
            var p = ResDB.TblPERSONELLER.Find(id);
            if (p == null) return;
            var adSoyadParca = adSoyad.Text.Split(' ');
            p.Ad = adSoyadParca[0];
            p.Soyad = adSoyadParca.Length > 1 ? string.Join(" ", adSoyadParca.Skip(1)) : "";
            p.Pozisyon = cbxPosizyon.Text;
            p.Telefon = telefon.Text;
            p.Adres = adres.Text;
            p.Email = email.Text;
            p.TCKimlikNo = tckimlik.Text;
            p.Tarih = Tarih.SelectedDate ?? DateTime.Now;
            p.Durum = IsTakip_ToggleButton.IsChecked == true;
            p.Resim = ResmiKaydetVeAdiniGetir(p.Resim);
            if (decimal.TryParse(maas.Text, out decimal odeme))
            {
                var maasKaydi = ResDB.TblMAAS.FirstOrDefault(x => x.PersonelID == id);
                if (maasKaydi != null)
                    maasKaydi.NetTutar = odeme;
                else
                    ResDB.TblMAAS.Add(new TblMAAS { PersonelID = id, NetTutar = odeme });
            }
            try
            {
                ResDB.SaveChanges();
                MessageBox.Show("Personel Güncelleme İşlemi Başarılı", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                personelListele();
                temizle();
            }
            catch (DbEntityValidationException ex)
            {
                string hata = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    hata += $"Entity: {eve.Entry.Entity.GetType().Name}\n";
                    foreach (var ve in eve.ValidationErrors)
                    {
                        hata += $"Property: {ve.PropertyName}, Error: {ve.ErrorMessage}\n";
                    }
                }
                MessageBox.Show(hata, "Doğrulama Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Genel bir hata oluştu: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnsil(object sender, RoutedEventArgs e)
        {
            if (personel_DataGrid.SelectedItem == null)
            {
                MessageBox.Show("Lütfen silmek istediğiniz personeli seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var seciliSatir = personel_DataGrid.SelectedItem;
            var idOzelligi = seciliSatir.GetType().GetProperty("PersonelID");

            if (idOzelligi == null)
            {
                MessageBox.Show("Seçili nesnede 'PersonelID' özelliği bulunamadı.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            int calisanKimlikNo = (int)idOzelligi.GetValue(seciliSatir);

            var guncellenecekPersonel = ResDB.TblPERSONELLER.Find(calisanKimlikNo);

            if (guncellenecekPersonel != null)
            {
                guncellenecekPersonel.Durum = false;
                ResDB.SaveChanges();
                personelListele();
                MessageBox.Show("Personel başarıyla silindi.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Personel bulunamadı.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            temizle();
        }

        public void temizle()
        {
            tckimlik.Text = "";
            adSoyad.Text = "";
            maas.Text = "";
            adres.Text = "";
            telefon.Text = "";
            email.Text = "";
            Tarih.SelectedDate = null;
            cbxPosizyon.Text = "";
            IsTakip_ToggleButton.IsChecked = false;
            resim.ImageSource = null;
            secilenResimYolu = null;
        }

        private void temizleButton_Click(object sender, RoutedEventArgs e)
        {
            temizle();
        }
        private void personelOdeme_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var secilen = personelOdeme_DataGrid.SelectedItem;
            if (secilen != null)
            {
                dynamic secim = secilen;
                int odemeId = secim.ID;
                var odeme = ResDB.TblPERSONELODEME.Find(odemeId);
                if (odeme != null)
                {
                    odemeTarih.Text = odeme.TARIH.ToString();
                    cbxOdemeTuru.Text = odeme.TUR;
                    odemeTutar.Text = odeme.ODEMEMIKTARI.ToString();
                    aciklama.Text = odeme.ACIKLAMA;
                }
            }
        }
        private void odemegetir()
        {
            var odemeler = from x in ResDB.TblPERSONELODEME
                           where x.PERSONEL == seciliPersonelId
                           select new
                           {
                               ID = x.ID,
                               Personel = x.TblPERSONELLER.Ad + " " + x.TblPERSONELLER.Soyad,
                               ÖdemeTürü = x.TUR,
                               Tutar = x.ODEMEMIKTARI,
                               Tarih = x.TARIH,
                               Açıklama = x.ACIKLAMA
                           };
            personelOdeme_DataGrid.ItemsSource = odemeler.OrderByDescending(x => x.Tarih).ToList();
        }
        private void odemeButton_Click(object sender, RoutedEventArgs e)
        {
            if (seciliPersonelId == -1)
            {
                MessageBox.Show("Lütfen önce bir personel seçin!");

            }
            else
            {
                TblPERSONELODEME yeniOdeme = new TblPERSONELODEME();

                yeniOdeme.TARIH = DateTime.Parse(odemeTarih.Text);
                yeniOdeme.TUR = cbxOdemeTuru.Text;
                yeniOdeme.ACIKLAMA = aciklama.Text;
                yeniOdeme.ODEMEMIKTARI = Convert.ToDecimal(odemeTutar.Text);
                yeniOdeme.PERSONEL = seciliPersonelId;

                ResDB.TblPERSONELODEME.Add(yeniOdeme);

                TblGIDER gider = new TblGIDER();
                gider.PersonelId = seciliPersonelId;
                gider.Aciklama = aciklama.Text;
                gider.Tarih = DateTime.Parse(odemeTarih.Text);
                gider.Tutar = Convert.ToDecimal(odemeTutar.Text);
                gider.GiderTuru = cbxOdemeTuru.Text;
                ResDB.TblGIDER.Add(gider);
               ResDB.SaveChanges();

                MessageBox.Show("Personele ödeme başarıyla yapıldı.");
            }
            odemegetir();


        }
        private void odemegrtdoldur()
        {
            var secilenOdeme = personelOdeme_DataGrid.SelectedItem;
        }

        private void txtPersonelAra_TextChanged(object sender, TextChangedEventArgs e)
        {
            var personelara = txtPersonelAra.Text;
            var personel = from x in ResDB.TblPERSONELLER.OrderByDescending(x => x.PersonelID)
                .Where(x => x.Ad.ToLower().Contains(personelara) ||
                            x.Soyad.ToLower().Contains(personelara))
                           select new
                           {
                               x.PersonelID,
                               x.Ad,
                               x.Soyad,
                               x.Telefon,
                               x.Adres,
                               x.Pozisyon,
                               x.Email,
                               x.Tarih,
                               DURUMU = x.Durum == true ? "Aktif" : "Pasif"
                           };
            personel_DataGrid.ItemsSource = personel.ToList();
        }

        private void odemeGuncelle_Click(object sender, RoutedEventArgs e)
        {
            var güncellenecek = personelOdeme_DataGrid.SelectedItem;
            if (güncellenecek != null)
            {
                dynamic secim = güncellenecek;
                int odemeId = secim.ID;
                var odeme = ResDB.TblPERSONELODEME.Find(odemeId);
                if (odeme != null)
                {
                    odeme.TARIH = DateTime.Parse(odemeTarih.Text);
                    odeme.TUR = cbxOdemeTuru.Text;
                    odeme.ODEMEMIKTARI = Convert.ToDecimal(odemeTutar.Text);
                    odeme.ACIKLAMA = aciklama.Text;
                    ResDB.SaveChanges();
                    MessageBox.Show("Ödeme Güncellendi.");
                    odemegetir();
                }
            }
        }
    }
}