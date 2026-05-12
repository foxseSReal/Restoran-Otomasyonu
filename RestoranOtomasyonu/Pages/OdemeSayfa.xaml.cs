using RestoranOtomasyonu.Entity;
using RestoranOtomasyonu.OtherWindows;
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
                    decimal kalanBorc = _toplamBorc - _tahsilEdilen;
                    if (miktar > kalanBorc)
                    {
                        miktar = kalanBorc;
                    }

                    var masa = db.TblMASA.FirstOrDefault(m => m.MasaId == _masaId);
                    string masaAdi = masa != null ? "Masa " + masa.MasaNo.ToString() : "Bilinmeyen Masa";

                    var yeniOdeme = new TblADISYON_ODEME
                    {
                        AdisyonId = _adisyonId,
                        OdemeTuru = txt_OdemeSekli.Text,
                        OdenenTutar = miktar,
                        Tarih = DateTime.Now
                    };
                    db.TblADISYON_ODEME.Add(yeniOdeme);
                    db.SaveChanges();

                    db.TblGELIR.Add(new TblGELIR
                    {
                        GelirTuru = "Restoran Satışı",
                        Tutar = miktar,
                        Tarih = DateTime.Now,
                        Aciklama = $"{masaAdi} ödemesi ({yeniOdeme.OdemeTuru})",
                        PersonelId = 1,
                        ReferansTablo = "TblADISYON_ODEME",
                        ReferansId = yeniOdeme.OdemeId
                    });

                    _tahsilEdilen += miktar;

                    if (_tahsilEdilen >= _toplamBorc)
                    {
                        var ad = db.TblADISYON.Find(_adisyonId);
                        if (ad != null) { ad.Durum = false; ad.KapanisZamani = DateTime.Now; }
                        if (masa != null) { masa.Statu = "B"; masa.Tutar = 0; }
                    }

                    db.SaveChanges();

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
                if (txt_OdemeSekli.Text == "İNDİRİM") txt_OdemeSekli.Text = "NAKIT";

            }
        }

        private void KalanTutariGuncelle()
        {
            var tumOdemeler = db.TblADISYON_ODEME
                                .AsNoTracking()
                                .Where(x => x.AdisyonId == _adisyonId)
                                .ToList();

            decimal gercekOdenen = tumOdemeler
                                    .Where(x => x.OdemeTuru != "İNDİRİM")
                                    .Sum(x => x.OdenenTutar);

            decimal indirimToplami = tumOdemeler
                                     .Where(x => x.OdemeTuru == "İNDİRİM")
                                     .Sum(x => x.OdenenTutar);

            _tahsilEdilen = gercekOdenen + indirimToplami;

            decimal kalan = _toplamBorc - _tahsilEdilen;
            txt_KalanTutar.Text = string.Format("₺{0:N2}", kalan > 0 ? kalan : 0);
            txt_OdenenTutar.Text = string.Format("₺{0:N2}", gercekOdenen);
            txt_IndirimTutar.Text = string.Format("₺{0:N2}", indirimToplami);
            if (kalan <= 0) txt_KalanTutar.Foreground = Brushes.Green;
            else txt_KalanTutar.Foreground = (Brush)new BrushConverter().ConvertFromString("#E91E63");
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
            decimal kalan = _toplamBorc - _tahsilEdilen;
            if (kalan <= 0) _numpadMetni = "0";
            else _numpadMetni = kalan.ToString("0.00").Replace(",", ".");
            TutarGuncelle();
        }
    }
}