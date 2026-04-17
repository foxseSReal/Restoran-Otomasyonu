using RestoranOtomasyonu.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RestoranOtomasyonu.userControls
{
    public partial class satisDurumu : UserControl
    {
        // Context nesnesi
        RESTORANDBEntities db = new RESTORANDBEntities();

        public satisDurumu()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // İlk açılışta bugünün verilerini ve genel toplamları getir
            await KasaVerileriniYukleAsync();
        }

        public async Task KasaVerileriniYukleAsync(DateTime? baslangic = null, DateTime? bitis = null, string aramaMetni = "")
        {
            try
            {
                // UI'ı kitlememek için veritabanı işlemlerini Task.Run ile yapıyoruz
                var veriler = await Task.Run(() =>
                {
                    using (var context = new RESTORANDBEntities())
                    {
                        // 1. GELİR SORGUSU
                        var gelirQuery = context.TblGELIR.AsQueryable();
                        if (baslangic.HasValue) gelirQuery = gelirQuery.Where(x => x.Tarih >= baslangic.Value);
                        if (bitis.HasValue)
                        {
                            var bitisDuzenli = bitis.Value.AddDays(1).AddSeconds(-1); // Gün sonuna kadar al
                            gelirQuery = gelirQuery.Where(x => x.Tarih <= bitisDuzenli);
                        }
                        if (!string.IsNullOrEmpty(aramaMetni))
                            gelirQuery = gelirQuery.Where(x => x.Aciklama.Contains(aramaMetni) || x.GelirTuru.Contains(aramaMetni));

                        var gelirListesi = gelirQuery.OrderByDescending(x => x.Tarih).ToList();

                        // 2. GİDER SORGUSU
                        var giderQuery = context.TblGIDER.AsQueryable();
                        if (baslangic.HasValue) giderQuery = giderQuery.Where(x => x.Tarih >= baslangic.Value);
                        if (bitis.HasValue)
                        {
                            var bitisDuzenli = bitis.Value.AddDays(1).AddSeconds(-1);
                            giderQuery = giderQuery.Where(x => x.Tarih <= bitisDuzenli);
                        }
                        if (!string.IsNullOrEmpty(aramaMetni))
                            giderQuery = giderQuery.Where(x => x.Aciklama.Contains(aramaMetni) || x.GiderTuru.Contains(aramaMetni));

                        var giderListesi = giderQuery.OrderByDescending(x => x.Tarih).ToList();

                        return new { Gelirler = gelirListesi, Giderler = giderListesi };
                    }
                });

                // Verileri Tablolara Bağla
                dgGelirler.ItemsSource = veriler.Gelirler;
                dgGiderler.ItemsSource = veriler.Giderler;

                // Hesaplamaları Yap
                İstatistikleriHesapla(veriler.Gelirler, veriler.Giderler);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kasa verileri yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void İstatistikleriHesapla(List<TblGELIR> gelirler, List<TblGIDER> giderler)
        {
            DateTime bugun = DateTime.Today;

            // 1. Bugünkü Net Kasa (Bugünkü Gelir - Bugünkü Gider)
            decimal bugunGelir = gelirler.Where(x => x.Tarih >= bugun).Sum(x => (decimal?)x.Tutar) ?? 0;
            decimal bugunGider = giderler.Where(x => x.Tarih >= bugun).Sum(x => (decimal?)x.Tutar) ?? 0;
            lblKasaBugun.Text = (bugunGelir - bugunGider).ToString("C2");

            // 2. Toplam Gelir ve Gider (Filtreye göre gelen listeden)
            decimal toplamGelir = gelirler.Sum(x => (decimal?)x.Tutar) ?? 0;
            decimal toplamGider = giderler.Sum(x => (decimal?)x.Tutar) ?? 0;

            lblGelirAylik.Text = toplamGelir.ToString("C2");
            lblGiderAylik.Text = toplamGider.ToString("C2");
            lblToplamIslem.Text = (gelirler.Count + giderler.Count).ToString();

            // 3. Genel Net Durum
            decimal netDurum = toplamGelir - toplamGider;
            lblNetDurum.Text = netDurum.ToString("C2");
            lblNetDurum.Foreground = netDurum >= 0 ? Brushes.Green : Brushes.Red;
        }

        private async void btnSorgula_Click(object sender, RoutedEventArgs e)
        {
            DateTime? bas = dtBaslangic.SelectedDate;
            DateTime? bit = dtBitis.SelectedDate;
            string ara = txtKasaAra.Text;

            await KasaVerileriniYukleAsync(bas, bit, ara);
        }

        private async void btnTemizle_Click(object sender, RoutedEventArgs e)
        {
            dtBaslangic.SelectedDate = null;
            dtBitis.SelectedDate = null;
            txtKasaAra.Text = "";
            await KasaVerileriniYukleAsync();
        }

        private async void txtKasaAra_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Çok sık sorgu atmaması için basit bir karakter kontrolü (opsiyonel)
            if (txtKasaAra.Text.Length > 2 || txtKasaAra.Text.Length == 0)
            {
                await KasaVerileriniYukleAsync(dtBaslangic.SelectedDate, dtBitis.SelectedDate, txtKasaAra.Text);
            }
        }
    }
}