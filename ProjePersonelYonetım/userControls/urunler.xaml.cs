using Microsoft.Win32;
using ProjePersonelYonetım.Entity;
using System;
using System.Collections.Generic;
using System.IO; // <--- DEĞİŞİKLİK: Dosya işlemleri için bu kütüphane eklendi
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
    /// urunler.xaml etkileşim mantığı
    /// </summary>
    public partial class urunler : UserControl
    {
        RESTORANDBEntities2 db = new RESTORANDBEntities2();
        private string seciliResimYolu = null; // <--- YENİ SATIR: Seçilen resmin dosya yolunu tutmak için

        public urunler()
        {
            InitializeComponent();
            UrunListele();
            var sonurun = db.TblURUN.OrderByDescending(x => x.UrunId).FirstOrDefault();
            if (sonurun != null)
            {
                urun_isim.Text = sonurun.UrunAdi;
                urunFiyat.Text = sonurun.Fiyat.ToString();
                urun_ToggleButton.IsChecked = sonurun.Durum;
                cbxUrun_Kategori.Text = sonurun.TblKATEGORI.KategoriAdi;
                cbxUrun_Firma.Text = sonurun.TblFIRMA.FirmaAdi;
               
            }
            if (!string.IsNullOrEmpty(sonurun.ResimYolu) && File.Exists(sonurun.ResimYolu))
            {
                // 2. Dosya varsa, yüklemeyi DENE (Dosya bozuk olabilir)
                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(sonurun.ResimYolu, UriKind.Absolute));
                    urun_resimKutusu.ImageSource = bitmap;
                }
                catch (Exception)
                {
                    // Resim dosyası bozuksa veya bir sebepten okunamadıysa, kutuyu boşalt
                    urun_resimKutusu.ImageSource = null;
                }
            }
            else
            {
                // 1. koşul sağlanmadıysa (yol boşsa veya dosya yoksa) kutuyu boşalt
                urun_resimKutusu.ImageSource = null;
            }

        }

        public void UrunListele()
        {
          
            var listele = db.TblURUN.OrderByDescending(x=>x.UrunId)
                            .Select(x => new
                            {
                                ID = x.UrunId,
                                ÜrünAdı = x.UrunAdi,
                                Tutar = x.Fiyat,
                                Kategori = x.TblKATEGORI.KategoriAdi,
                                
                            })
                            .ToList();
            urunDataGrid.ItemsSource = listele;
        }

        private void Resim(object sender, RoutedEventArgs e)
        {
            OpenFileDialog resim = new OpenFileDialog();
            resim.Filter = "Resim Dosyaları (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";

            if (resim.ShowDialog() == true)
            {
                // <--- DEĞİŞİKLİK BAŞLANGIÇ ---
                // Seçilen resmin dosya yolunu değişkene kaydet
                seciliResimYolu = resim.FileName;

                // Resmi önizleme kutusunda göster
                BitmapImage bitmap = new BitmapImage(new Uri(seciliResimYolu, UriKind.Absolute));
                urun_resimKutusu.ImageSource = bitmap;
                // <--- DEĞİŞİKLİK BİTİŞ ---
            }
        }

        private void urun_ekleButton_Click(object sender, RoutedEventArgs e)
        {
            int? kategoriId = db.TblKATEGORI.FirstOrDefault(x => x.KategoriAdi == cbxUrun_Kategori.Text)?.KategoriId;
            int? firmaId = db.TblFIRMA.FirstOrDefault(x => x.FirmaAdi == cbxUrun_Firma.Text)?.FirmaId;

            if (kategoriId == null || kategoriId == 0)
            {
                MessageBox.Show("Lütfen geçerli bir kategori seçin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (firmaId == null || firmaId == 0)
            {
                MessageBox.Show("Lütfen geçerli bir firma seçin. (Seçilen firma TblFIRMA'da bulunamadı)", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var yeniUrun = new TblURUN
                {
                    UrunAdi = urun_isim.Text,
                    Fiyat = decimal.Parse(urunFiyat.Text),
                    Durum = urun_ToggleButton.IsChecked ?? true,
                    KategoriId = kategoriId.Value,
                    FirmaId = firmaId.Value,
                    ResimYolu= seciliResimYolu // <--- YENİ SATIR: Resim yolunu veritabanına ekle
                };

                db.TblURUN.Add(yeniUrun);
                db.SaveChanges();
                MessageBox.Show("Ürün başarıyla eklendi.");
                UrunListele();
                urun_temizleButton_Click(null, null); // <--- YENİ SATIR: Ekleme sonrası formu temizle
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen fiyat alanına geçerli bir sayı girin (örn: 12,50).", "Geçersiz Fiyat", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün eklenirken bir hata oluştu:\n" + ex.Message, "Veritabanı Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // UrunListele(); // <--- Bu satır try bloğunun içinde zaten var, tekrar gerekmez
        }

        private void urunDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var urungetir = urunDataGrid.SelectedItem;
            if (urungetir == null) return;

            int urunId = ((dynamic)urungetir).ID;
            var urun = db.TblURUN.Find(urunId);

            if (urun == null) return; // <--- YENİ SATIR: Güvenlik kontrolü

            urun_isim.Text = urun.UrunAdi;
            cbxUrun_Kategori.Text = urun.TblKATEGORI.KategoriAdi;
            urunFiyat.Text = urun.Fiyat.ToString();
            urun_ToggleButton.IsChecked = urun.Durum;
            cbxUrun_Firma.Text = urun.TblFIRMA.FirmaAdi;

            // <--- DEĞİŞİKLİK BAŞLANGIÇ: Ürünün resmini yükleme ---
            // Veritabanından gelen Resim (string) kolonunu kontrol et
            if (!string.IsNullOrEmpty(urun.ResimYolu) && File.Exists(urun.ResimYolu))
            {
                try
                {
                    // Veritabanından gelen dosya yolundan resmi yükle
                    BitmapImage bitmap = new BitmapImage(new Uri(urun.ResimYolu, UriKind.Absolute));
                    urun_resimKutusu.ImageSource = bitmap;
                    // Bu yolu sakla, olur da kullanıcı resmi değiştirmeden güncelle'ye basar
                    seciliResimYolu = urun.ResimYolu;
                }
                catch (Exception)
                {
                    // Resim dosyası bozuksa veya yol hatalıysa
                    urun_resimKutusu.ImageSource = null;
                    seciliResimYolu = null;
                }
            }
            else
            {
                // Ürünün resmi yoksa veya dosya yolu artık geçersizse
                urun_resimKutusu.ImageSource = null;
                seciliResimYolu = null;
            }
            // <--- DEĞİŞİKLİK BİTİŞ ---
        }

        private void urun_silButton_Click(object sender, RoutedEventArgs e)
        {
            var seciliOge = urunDataGrid.SelectedItem;
            if (seciliOge == null)
            {
                MessageBox.Show("Lütfen pasif hale getirmek için bir ürün seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                int urunId = ((dynamic)seciliOge).ID;
                var urun = db.TblURUN.Find(urunId);
                if (urun != null)
                {
                    urun.Durum = false;

                    db.SaveChanges();

                    MessageBox.Show("Ürün başarıyla pasif hale getirildi.", "İşlem Başarılı");

                    UrunListele();
                    urun_temizleButton_Click(null, null); // <--- YENİ SATIR: Silme sonrası formu temizle
                }
                else
                {
                    MessageBox.Show("Pasifleştirme işlemi için ürün bulunamadı.", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                MessageBox.Show("Seçili öğeden ID alınamadı. DataGrid'in kolon yapısını kontrol edin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("İşlem sırasında beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // UrunListele(); // <--- Bu satır try bloğunun içinde zaten var, tekrar gerekmez
        }

        private void urun_guncelleButton_Click(object sender, RoutedEventArgs e)
        {
            var güncelle = MessageBox.Show("Seçili ürünü güncellemek istediğinize emin misiniz?", "Uyarı", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (güncelle == MessageBoxResult.Yes)
            {
                var seciliOge = urunDataGrid.SelectedItem;
                if (seciliOge == null)
                {
                    MessageBox.Show("Lütfen güncellemek için bir ürün seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                try
                {
                    int urunId = ((dynamic)seciliOge).ID;
                    var urun = db.TblURUN.Find(urunId);
                    if (urun != null)
                    {
                        urun.UrunAdi = urun_isim.Text;
                        urun.Fiyat = decimal.Parse(urunFiyat.Text);
                        urun.Durum = urun_ToggleButton.IsChecked ?? true;

                        // <--- YENİ SATIR: Güncel resim yolunu kaydet ---
                        // seciliResimYolu, ya grid'den seçildiğinde ya da "Resim"
                        // butonuna basıldığında güncellenmişti.
                        urun.ResimYolu = seciliResimYolu;

                        var kategori = db.TblKATEGORI.FirstOrDefault(x => x.KategoriAdi == cbxUrun_Kategori.Text);
                        if (kategori != null)
                        {
                            urun.KategoriId = kategori.KategoriId;
                        }
                        else
                        {
                            MessageBox.Show("Geçersiz kategori adı girdiniz.", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        var firma = db.TblFIRMA.FirstOrDefault(x => x.FirmaAdi == cbxUrun_Firma.Text);
                        if (firma != null)
                        {
                            urun.FirmaId = firma.FirmaId;
                        }
                        else
                        {
                            MessageBox.Show("Geçersiz firma adı girdiniz.", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        db.SaveChanges();
                        MessageBox.Show("Ürün başarıyla güncellendi.", "İşlem Başarılı");
                        UrunListele();
                        urun_temizleButton_Click(null, null); // <--- YENİ SATIR: Güncelleme sonrası formu temizle
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme işlemi için ürün bulunamadı.", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Lütfen fiyat alanına geçerli bir sayı girin (örn: 12,50).", "Geçersiz Fiyat", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    MessageBox.Show("Seçili öğeden ID alınamadı. DataGrid'in kolon yapısını kontrol edin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İşlem sırasında beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            // UrunListele(); // <--- Bu satır try bloğunun içinde zaten var, tekrar gerekmez
        }

        private void urun_temizleButton_Click(object sender, RoutedEventArgs e)
        {
            urun_isim.Clear();
            urunFiyat.Clear();
            cbxUrun_Kategori.SelectedIndex = -1;
            cbxUrun_Firma.SelectedIndex = -1;
            urun_ToggleButton.IsChecked = false; // <--- false olarak ayarlamak daha doğru
            urun_resimKutusu.ImageSource = null;
            seciliResimYolu = null; // <--- YENİ SATIR: Hafızadaki resim yolunu da temizle
            urunDataGrid.SelectedItem = null; // <--- YENİ SATIR: DataGrid seçimini kaldır
        }

        private void urunAra_TextChanged(object sender, TextChangedEventArgs e)
        {
            var urunara = urunAra.Text.ToLower();
            var filtreliListe = db.TblURUN.OrderByDescending(x=>x.UrunId)
                .Where(x => x.UrunAdi.ToLower().Contains(urunara) ||
                            x.TblKATEGORI.KategoriAdi.ToLower().Contains(urunara))
                .Select(x => new
                {
                    ÜrünAdı = x.UrunAdi,
                    Tutar = x.Fiyat,
                    Kategori = x.TblKATEGORI.KategoriAdi,
                })
                .ToList();
            urunDataGrid.ItemsSource = filtreliListe;
        }
    }
}