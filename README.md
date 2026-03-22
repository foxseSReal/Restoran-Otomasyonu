<!--Sayfa olarak görüntülemek için Visual Studio Code ile açılmalı ardından CTRL+SHIFT+V kombinasyonunu kullanın. -->
# 🍽️ Restoran Otomasyonu (WPF & Material Design XAML Tool Kit)
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

- Masa Kontrolleri
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

### 🛠 1. Veritabanı Kurulumu

Projenin veritabanı yapısını oluşturmak için artık hantal `.mdf` dosyaları yerine daha hafif, taşınabilir ve güvenli olan **SQL Script** yöntemi kullanılmaktadır. Kurulum için aşağıdaki adımları izleyin:

1. **SQL Server Management Studio (SSMS)** uygulamasını açın.
2. Proje dizinindeki `Veri Tabanı/DB.sql` dosyasını bir metin editörüyle açıp içeriğini kopyalayın veya doğrudan SSMS içine sürükleyin.
3. Sorgu ekranında **Execute (F5)** tuşuna basarak scripti çalıştırın.

> [!NOTE]
> Script, gerekli tüm tabloları ve veritabanı şemasını otomatik olarak oluşturacaktır.
---

### ⚙️ 2. Bağlantı Ayarları (App.config)

Veritabanını kurduktan sonra projenin yerel SQL Server'ınıza erişebilmesi için bağlantı dizesini (connection string) kendi bilgisayarınıza göre düzenlemeniz gerekmektedir.

1. Visual Studio'da **`App.config`** dosyasını açın.
2. `<connectionStrings>` etiketi altındaki ilgili satırı bulun.
3. `connectionString` içindeki `data source` parametresini kendi yerel sunucu isminize (Server Name) göre güncelleyin.

**Örnek `App.config` Yapısı:**
```xml
<connectionStrings>
  <add name="DBRestoranProjeEntities" 
       connectionString="[...] string=&quot;data source=BILGISAYAR-ADI\SQLEXPRESS;initial catalog=DBRestoranProje;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" 
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
