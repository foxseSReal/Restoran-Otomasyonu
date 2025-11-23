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

## ⚙️ Çalıştırma
> Projeyi indirdikten sonra içerisinde bulunan `Veri Tabanı` dosyasından `DBRestoranProje.bak` ya da `DBRestoranProje.mdf` ve `DBRestoranProje_log.mdf` dosyalarını SSMS'e veri tabanı olarak ekliyoruz.
> Projemizde halihazırda olan `Entity` dosyasının içerisindeki `Model1.edmx` adlı dosyayı silin.
>`Entity` dosyasını sağ tıklayarak;
`Ekle` --> `Yeni Öğe` --> `Veri` --> `ADO.NET Entity Data Model` ekliyoruz.

* `App.config` içerisinde aşağidakine benzer bir yapı vardır. name=[`...`] kısmında Veri Tabanının ismi alıyoruz.
> [!NOTE]
>Burada birden fazla isim olabilir; her yeni eklenen model ismi alt alta sıralanır ya da halihazırda olan `<add name [...]` kodunun yanına eklenir.
```xml
 <connectionStrings>
   <add name="DBRestoranProje" connectionString="[...]"/>
   <add name="DBRestoranProje1" connectionString="[...]"/>
 </connectionStrings>
```

> Sonrasında Projemizdeki UserControls klasöründeki bütün UserControllerin C# dosyalarına girerek yeni Modelimizi UserControle tanıtıyoruz.

* `Örnek= Personel.xaml.cs` 
```c#
public partial class Personel : UserControl
{
    DBRestoranProjeEntities db = new DBRestoranProjeEntities();
    [...]
}
```
* Bütün UserContollere bu işlemi uyguladıktan sonra projemizi çalıştırabiliriz.

---

## 📚 Kullanılan Nuget Paketleri & Diğer
* 📦 MaterialDesignTheme
* 🧩 Entity Framework 5 ya da Entity Framework 6

---
## 👥 Katkıda Bulunanlar

- <a href=https://github.com/foxseSReal> **Yusuf Erdoğan** </a>— Proje Yönetimi, Arayüz Tasarımı, Veritabanı, Test, C#
- <a href=https://github.com/GencayCeliker> **Gencay Çeliker** </a>— Proje Yönetimi, Veritabanı, Hata Ayıklama, C#
- **Emrah Çapkan** — Test , C#
