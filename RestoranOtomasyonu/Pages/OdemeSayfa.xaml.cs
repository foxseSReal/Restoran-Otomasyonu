using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.OtherWindows; // Adisyon penceresine ulaşmak için
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RestoranOtomasyonu.Pages
{
    public partial class OdemeSayfa : Page
    {
        RESTORANDBEntities db = new RESTORANDBEntities();
        private Adisyon _anaPencere;
        private int _masaId;
        private int _adisyonId;
        private decimal _toplamBorc;
        private decimal _tahsilEdilen;
        private string _numpadMetni = "0";

        public OdemeSayfa(string gelenTutar, int masaId, Adisyon anaPencere)
        {
            InitializeComponent();
            this._masaId = masaId;
            this._anaPencere = anaPencere;

            // "₺ 150,00" formatını temizleyip sayıya çeviriyoruz
            decimal.TryParse(gelenTutar.Replace("₺", "").Trim(), out _toplamBorc);

            VerileriYukle();
        }

        private void VerileriYukle()
        {
            var aktif = db.TblADISYON.FirstOrDefault(x => x.MasaId == _masaId && x.Durum == true);
            if (aktif != null)
            {
                _adisyonId = aktif.AdisyonId;
                // SQL'den bu adisyona ait önceki ödemelerin toplamını al
                _tahsilEdilen = db.TblADISYON_ODEME
                                  .Where(x => x.AdisyonId == _adisyonId)
                                  .Sum(x => (decimal?)x.OdenenTutar) ?? 0;
            }
            KalanTutariGuncelle();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(_numpadMetni.Replace(".", ","), out decimal miktar) && miktar > 0)
            {
                try
                {
                    // Önce masa adını çekelim ki açıklamada kullanalım
                    var masa = db.TblMASA.FirstOrDefault(m => m.MasaId == _masaId);
                    string masaAdi = masa != null ? "Masa " + masa.MasaNo.ToString() : "Bilinmeyen Masa";

                    // 1. SQL'e Yeni Ödeme Kaydı
                    var yeniOdeme = new TblADISYON_ODEME
                    {
                        AdisyonId = _adisyonId,
                        OdemeTuru = txt_OdemeSekli.Text,
                        OdenenTutar = miktar,
                        Tarih = DateTime.Now
                    };
                    db.TblADISYON_ODEME.Add(yeniOdeme);
                    db.SaveChanges();

                    // 2. KASA (GELİR) TABLOSUNA KAYIT - Masa Adı ile
                    db.TblGELIR.Add(new TblGELIR
                    {
                        GelirTuru = "Restoran Satışı",
                        Tutar = miktar,
                        Tarih = DateTime.Now,
                        // Açıklamayı burada güncelledik:
                        Aciklama = $"{masaAdi} ödemesi ({yeniOdeme.OdemeTuru})",
                        PersonelId = 1,
                        ReferansTablo = "TblADISYON_ODEME",
                        ReferansId = yeniOdeme.OdemeId
                    });

                    _tahsilEdilen += miktar;

                    // 3. Borç Tamamlandıysa Adisyonu ve Masayı Kapat
                    if (_tahsilEdilen >= _toplamBorc)
                    {
                        var ad = db.TblADISYON.Find(_adisyonId);
                        if (ad != null) { ad.Durum = false; ad.KapanisZamani = DateTime.Now; }

                        // Masayı zaten yukarıda çekmiştik, tekrar çekmeye gerek yok
                        if (masa != null) { masa.Statu = "B"; masa.Tutar = 0; }
                    }

                    db.SaveChanges();

                    // UI ve Bildirim işlemleri...
                    if (_anaPencere != null) _anaPencere.GenelToplamiHesapla();
                    _numpadMetni = "0";
                    TutarGuncelle();
                    KalanTutariGuncelle();
                    MessageBox.Show("Ödeme Başarıyla Alındı.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void KalanTutariGuncelle()
        {
            // 1. Kalan borcu hesapla ve yaz
            decimal kalan = _toplamBorc - _tahsilEdilen;
            txt_KalanTutar.Text = string.Format("₺{0:N2}", kalan > 0 ? kalan : 0);

            // 2. ÖDENEN TUTAR kısmını güncelle (İstediğin kısım burası)
            txt_OdenenTutar.Text = string.Format("₺{0:N2}", _tahsilEdilen);

            // 3. Görsel geri bildirim (Borç bittiyse yeşil yap)
            if (kalan <= 0)
            {
                txt_KalanTutar.Foreground = Brushes.Green;
            }
        }

        // --- BUTON İŞLEVLERİ ---
        private void Num_Click(object sender, RoutedEventArgs e)
        {
            string d = (sender as Button).Content.ToString();
            if (d == "." && _numpadMetni.Contains(".")) return;
            if (_numpadMetni == "0" && d != ".") _numpadMetni = "";
            _numpadMetni += d; TutarGuncelle();
        }
        private void QuickAmount_Click(object sender, RoutedEventArgs e)
        {
            _numpadMetni = (sender as Button).Content.ToString().Replace("₺", "").Trim();
            TutarGuncelle();
        }
        private void TutarGuncelle() => txt_Tutar.Text = "₺" + _numpadMetni;
        private void Clear_Click(object sender, RoutedEventArgs e) { _numpadMetni = "0"; TutarGuncelle(); }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_numpadMetni.Length > 0) _numpadMetni = _numpadMetni.Substring(0, _numpadMetni.Length - 1);
            if (_numpadMetni == "") _numpadMetni = "0";
            TutarGuncelle();
        }
        private void PaymentMethod_Click(object sender, RoutedEventArgs e) => txt_OdemeSekli.Text = (sender as Button).Content.ToString();

        private void AllAmount_Click(object sender, RoutedEventArgs e)
        {
            // 1. Kalan borcu hesapla (Toplam - Tahsil Edilen)
            decimal kalan = _toplamBorc - _tahsilEdilen;

            // 2. Borç 0'dan küçükse işlem yapma (Hatalı durumları önlemek için)
            if (kalan <= 0)
            {
                _numpadMetni = "0";
            }
            else
            {
                // 3. Kalan tutarı numpad formatına uygun hale getiriyoruz.
                // Numpad metninde nokta kullandığın için "." formatına çeviriyoruz.
                _numpadMetni = kalan.ToString("0.00").Replace(",", ".");
            }

            // 4. UI'daki tutar kutusunu güncelle
            TutarGuncelle();
        }

    }
}