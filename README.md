<!--Sayfa olarak görüntülemek için Visual Studio Code ile açılmalı ardından CTRL+SHIFT+V kombinasyonunu kullanın. -->
# 🍽️ Restoran Otomasyonu (WPF+XAML (Material Design XAML Tool Kit) )
- Projenin hedef Framework = .NET Framework(4.8)
---

## ℹ️ Bilgilendirme!

>[!WARNING]
> Proje tamamen Türkçe hazırlanmıştır ve ortalama bir Restoranın takibini yapmayı hedefler.
> Bu Proje bir okul projesidir. 
> Tamamen kullanıma hazır olmayabilir...

> [!NOTE]
> Çalıştırma kısmı aşağıdaki başlıklarda değinişmiştir.

## 📖 Proje Hakkında
---

Proje ortalama bir restoranın işleyişini takip etmek için hazırlanmıştır.
Aşağıda belirtilen özellikleri kontrol eder; 

- Gelir-Gider Takibi
- Personel Yönetimi
- Muhasebe
- Satış Durumu
- Ürün Yönetimi
- Rezervasyon
- Stok
- Günlük Harcama
---
## Projeyi Geliştirirken Kullanılan IDE

- Visual Studio Community 2022

## ⚙️ Kurulum ve Çalıştırma

Projeyi kendi bilgisayarınızda çalıştırmak için lütfen aşağıdaki adımları uygulayın:

### 1. Veritabanı Kurulumu
Proje dosyaları içerisindeki `Veri Tabanı` klasörüne gidin.
* SSMS (SQL Server Management Studio) üzerinden `DBRestoranProje.bak` dosyasını **Restore** ederek veritabanını oluşturun.
* *Alternatif:* `.mdf` ve `_log.mdf` dosyalarını "Attach" yöntemiyle de ekleyebilirsiniz.

### 2. Bağlantı Ayarları (App.config)
Veritabanını kurduktan sonra projenin veritabanına erişebilmesi için bağlantı dizesini (connection string) kendi bilgisayarınıza göre düzenlemeniz gerekebilir.

1.  Visual Studio'da **`App.config`** dosyasını açın.
2.  `<connectionStrings>` etiketi altındaki satırı bulun.
3.  `connectionString` parametresini kendi yerel sunucu isminize (Server Name) göre güncelleyin.

Örnek `App.config` yapısı:
```xml
<connectionStrings>
  <add name="DBRestoranProjeEntities" 
       connectionString="[...] provider connection string=&quot;data source=BILGISAYAR-ADI\SQLEXPRESS;initial [...]" 
       providerName="System.Data.EntityClient" />
</connectionStrings>
```
---

## 📚 Kullanılan Nuget Paketleri & Diğer
* 📦 MaterialDesignTheme
* 📦Microsoft.SqlServer.SqlManagementObjects
* 🧩 Entity Framework 5 ya da Entity Framework 6

---
## 👥 Katkıda Bulunanlar

- <a href=https://github.com/foxseSReal> **Yusuf Erdoğan** </a>— Proje Yönetimi, Arayüz Tasarımı, Veritabanı, Test, C#
- <a href=https://github.com/GencayCeliker> **Gencay Çeliker** </a>— Proje Yönetimi, Veritabanı, Hata Ayıklama, C#
