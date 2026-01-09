using Microsoft.Win32;
using RestoranOtomasyonu.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RestoranOtomasyonu.userControls
{
    public partial class urunler : UserControl
    {
        private string seciliResimYolu = null;

        public urunler()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await UrunListeleAsync();
            await SonUrunuGetirAsync();
        }

        public async Task UrunListeleAsync()
        {
            try
            {
                var listele = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities1())
                    {
                        return db.TblURUN
                                 .OrderByDescending(x => x.UrunId)
                                 .Where(x => x.Durum == true)
                                 .Select(x => new
                                 {
                                     ID = x.UrunId,
                                     ÜrünAdı = x.UrunAdi,
                                     Tutar = x.Fiyat,
                                     Kategori = x.TblKATEGORI != null ? x.TblKATEGORI.KategoriAdi : "-",
                                 })
                                 .ToList();
                    }
                });

                urunDataGrid.ItemsSource = listele;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }

        private async Task SonUrunuGetirAsync()
        {
            try
            {
                var sonUrunVerisi = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities1())
                    {
                        var sonurun = db.TblURUN.OrderByDescending(x => x.UrunId).FirstOrDefault();

                        if (sonurun != null)
                        {
                            return new
                            {
                                sonurun.UrunAdi,
                                sonurun.Fiyat,
                                sonurun.Durum,
                                KategoriAdi = sonurun.TblKATEGORI != null ? sonurun.TblKATEGORI.KategoriAdi : "",
                                FirmaAdi = sonurun.TblFIRMA != null ? sonurun.TblFIRMA.FirmaAdi : "",
                                sonurun.ResimYolu
                            };
                        }
                        return null;
                    }
                });

                if (sonUrunVerisi != null)
                {
                    urun_isim.Text = sonUrunVerisi.UrunAdi;
                    urunFiyat.Text = sonUrunVerisi.Fiyat.ToString();
                    urun_ToggleButton.IsChecked = sonUrunVerisi.Durum;
                    cbxUrun_Kategori.Text = sonUrunVerisi.KategoriAdi;
                    cbxUrun_Firma.Text = sonUrunVerisi.FirmaAdi;

                    if (!string.IsNullOrEmpty(sonUrunVerisi.ResimYolu) && File.Exists(sonUrunVerisi.ResimYolu))
                    {
                        await ResimYukleAsync(sonUrunVerisi.ResimYolu);
                    }
                }
            }
            catch { /* Hata olursa sessiz kal */ }
        }
        private async void urunDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var seciliOge = urunDataGrid.SelectedItem as dynamic;
            if (seciliOge == null) return;

            try
            {
                int id = seciliOge.ID;
                var urunDetay = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities1())
                    {
                        var urun = db.TblURUN.Find(id);
                        if (urun != null)
                        {
                            return new
                            {
                                urun.UrunAdi,
                                urun.Fiyat,
                                urun.Durum,
                                KategoriAdi = urun.TblKATEGORI != null ? urun.TblKATEGORI.KategoriAdi : "",
                                FirmaAdi = urun.TblFIRMA != null ? urun.TblFIRMA.FirmaAdi : "",
                                urun.ResimYolu
                            };
                        }
                        return null;
                    }
                });

                if (urunDetay != null)
                {
                    urun_isim.Text = urunDetay.UrunAdi;
                    cbxUrun_Kategori.Text = urunDetay.KategoriAdi;
                    urunFiyat.Text = urunDetay.Fiyat.ToString();
                    urun_ToggleButton.IsChecked = urunDetay.Durum;
                    cbxUrun_Firma.Text = urunDetay.FirmaAdi;
                    seciliResimYolu = urunDetay.ResimYolu;

                    if (!string.IsNullOrEmpty(urunDetay.ResimYolu) && File.Exists(urunDetay.ResimYolu))
                    {
                        await ResimYukleAsync(urunDetay.ResimYolu);
                    }
                    else
                    {
                        urun_resimKutusu.ImageSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seçim hatası: " + ex.Message);
            }
        }

        private async Task ResimYukleAsync(string dosyaYolu)
        {
            try
            {
                var resim = await Task.Run(() =>
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(dosyaYolu, UriKind.Absolute);
                    bitmap.EndInit();
                    bitmap.Freeze();
                    return bitmap;
                });
                urun_resimKutusu.ImageSource = resim;
            }
            catch
            {
                urun_resimKutusu.ImageSource = null;
            }
        }
        private async void Resim(object sender, RoutedEventArgs e)
        {
            OpenFileDialog resimSec = new OpenFileDialog();
            resimSec.Filter = "Resim Dosyaları (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (resimSec.ShowDialog() == true)
            {
                seciliResimYolu = resimSec.FileName;
                await ResimYukleAsync(seciliResimYolu);
            }
        }
        private async void urun_ekleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new RESTORANDBEntities1())
                {
                    int? kategoriId = db.TblKATEGORI.FirstOrDefault(x => x.KategoriAdi == cbxUrun_Kategori.Text)?.KategoriId;
                    int? firmaId = db.TblFIRMA.FirstOrDefault(x => x.FirmaAdi == cbxUrun_Firma.Text)?.FirmaId;

                    if (kategoriId == null || kategoriId == 0)
                    {
                        MessageBox.Show("Geçersiz kategori."); return;
                    }
                    if (firmaId == null || firmaId == 0)
                    {
                        MessageBox.Show("Geçersiz firma."); return;
                    }

                    var yeniUrun = new TblURUN
                    {
                        UrunAdi = urun_isim.Text,
                        Fiyat = decimal.Parse(urunFiyat.Text),
                        Durum = urun_ToggleButton.IsChecked ?? true,
                        KategoriId = kategoriId.Value,
                        FirmaId = firmaId.Value,
                        ResimYolu = seciliResimYolu
                    };

                    db.TblURUN.Add(yeniUrun);
                    await db.SaveChangesAsync();
                }

                MessageBox.Show("Ürün başarıyla eklendi.");
                await UrunListeleAsync();
                urun_temizleButton_Click(null, null);
            }
            catch (FormatException)
            {
                MessageBox.Show("Fiyat alanına geçerli sayı giriniz.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private async void urun_guncelleButton_Click(object sender, RoutedEventArgs e)
        {
            var seciliOge = urunDataGrid.SelectedItem as dynamic;
            if (seciliOge == null) { MessageBox.Show("Seçim yapınız."); return; }

            if (MessageBox.Show("Güncellemek istiyor musunuz?", "Onay", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    int id = seciliOge.ID;
                    using (var db = new RESTORANDBEntities1())
                    {
                        var urun = db.TblURUN.Find(id);
                        if (urun != null)
                        {
                            urun.UrunAdi = urun_isim.Text;
                            urun.Fiyat = decimal.Parse(urunFiyat.Text);
                            urun.Durum = urun_ToggleButton.IsChecked ?? true;
                            urun.ResimYolu = seciliResimYolu;

                            var kat = db.TblKATEGORI.FirstOrDefault(x => x.KategoriAdi == cbxUrun_Kategori.Text);
                            if (kat != null) urun.KategoriId = kat.KategoriId;

                            var firma = db.TblFIRMA.FirstOrDefault(x => x.FirmaAdi == cbxUrun_Firma.Text);
                            if (firma != null) urun.FirmaId = firma.FirmaId;

                            await db.SaveChangesAsync();
                            MessageBox.Show("Güncellendi.");
                        }
                    }
                    await UrunListeleAsync();
                    urun_temizleButton_Click(null, null);
                }
                catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            }
        }

        private async void urun_silButton_Click(object sender, RoutedEventArgs e)
        {
            var seciliOge = urunDataGrid.SelectedItem as dynamic;
            if (seciliOge == null) { MessageBox.Show("Seçim yapınız."); return; }

            try
            {
                int id = seciliOge.ID;
                using (var db = new RESTORANDBEntities1())
                {
                    var urun = db.TblURUN.Find(id);
                    if (urun != null)
                    {
                        urun.Durum = false;
                        await db.SaveChangesAsync();
                        MessageBox.Show("Ürün silindi (Pasife alındı).");
                    }
                }
                await UrunListeleAsync();
                urun_temizleButton_Click(null, null);
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void urun_temizleButton_Click(object sender, RoutedEventArgs e)
        {
            urun_isim.Clear();
            urunFiyat.Clear();
            cbxUrun_Kategori.SelectedIndex = -1;
            cbxUrun_Firma.SelectedIndex = -1;
            urun_ToggleButton.IsChecked = false;
            urun_resimKutusu.ImageSource = null;
            seciliResimYolu = null;
            urunDataGrid.SelectedItem = null;
        }

        private async void urunAra_TextChanged(object sender, TextChangedEventArgs e)
        {
            string aranan = urunAra.Text.ToLower();

            try
            {
                var filtreli = await Task.Run(() =>
                {
                    using (var db = new RESTORANDBEntities1())
                    {
                        return db.TblURUN.OrderByDescending(x => x.UrunId)
                                 .Where(x => x.UrunAdi.ToLower().Contains(aranan) ||
                                             (x.TblKATEGORI != null && x.TblKATEGORI.KategoriAdi.ToLower().Contains(aranan)))
                                 .Select(x => new
                                 {
                                     ID = x.UrunId,
                                     ÜrünAdı = x.UrunAdi,
                                     Tutar = x.Fiyat,
                                     Kategori = x.TblKATEGORI != null ? x.TblKATEGORI.KategoriAdi : "-"
                                 })
                                 .ToList();
                    }
                });
                urunDataGrid.ItemsSource = filtreli;
            }
            catch { }
        }
    }
}