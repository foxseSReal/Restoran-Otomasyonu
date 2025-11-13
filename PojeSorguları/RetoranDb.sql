CREATE DATABASE RESTORANDB
GO
USE RESTORANDB 
GO
CREATE TABLE TblPERSONELLER
(
PersonelID INT IDENTITY(1,1) PRIMARY KEY,
Ad NVARCHAR(500) NOT NULL,
Soyad NVARCHAR(500) NOT NULL,
TCKimlikNo NCHAR(11) UNIQUE CHECK(TCKimlikNo LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0,2,4,6,8]' ) NOT NULL,
Telefon NCHAR(12) NOT NULL,
Adres NVARCHAR(200) NOT NULL,
Email NVARCHAR(250) NOT NULL,
Pozisyon NVARCHAR(50) NOT NULL,
Tarih DATE NOT NULL,
Durum BIT DEFAULT(0) NOT NULL,
Resim NVARCHAR(500)
)
GO
--GO
--CREATE TABLE TblMAAS
--(
--MaasId INT IDENTITY(1,1) PRIMARY KEY,
--PerId INT NOT NULL,
--Ay SMALLINT NOT NULL,                            -- Ay bilgisi (1-12)
--Yil SMALLINT NOT NULL,                           -- Yýl bilgisi (örn: 2025)
--ToplamCalismaSaati DECIMAL(5,2) NOT NULL,        -- Aylýk toplam çalýþma süresi (örn: 180.00 saat)
--SaatlikUcret DECIMAL(6,2) NOT NULL,              -- Saat baþý ücret (örn: 120.00 TL)
--Prim DECIMAL(8,2) DEFAULT 0,                     -- Ek ödeme (performans, fazla mesai vb.)
--Kesinti DECIMAL(8,2) DEFAULT 0,                  -- Ceza, izin vb. maaþ kesintisi
--Avans DECIMAL(8,2) DEFAULT 0,                    -- Ay içinde verilen avans
--Maas AS CAST(ToplamCalismaSaati * SaatlikUcret AS DECIMAL(10,2)) PERSISTED,--Brüt Maaþ 
--NetMaas AS CAST((ToplamCalismaSaati * SaatlikUcret + Prim - Kesinti - Avans) AS DECIMAL(10,2)) PERSISTED
---- CAST bir veri türündeki deðeri açýkça baþka bir veri türüne dönüþtüren bir iþlevdir
----)
--GO
CREATE TABLE TblGUNLUKHARCAMA
(
GunlukId INT IDENTITY(1,1) PRIMARY KEY,
HarcananYer NVARCHAR(50) NOT NULL,
Tarih DATE NOT NULL,
Saat TIME NOT NULL,
Aciklama NVARCHAR(50) NOT NULL,
Tutar DECIMAL(10,2)NOT NULL,
PersonelID INT,
FirmaId INT
)
GO
--CREATE TABLE TblMuhasebe
--(
--MuhasebeId INT IDENTITY(1,1) PRIMARY KEY,
--Islem NVARCHAR(80) NOT NULL,
--
--)
GO
CREATE TABLE TblMUSTERILER
(
MusteriId INT IDENTITY (1,1) PRIMARY KEY,
Ad NVARCHAR(250) NOT NULL,
Soyad NVARCHAR(250) NOT NULL,
MasaId INT NOT NULL,
Tarih DATE NOT NULL,
Saat TIME NOT NULL,
Aciklama NVARCHAR(250) NOT NULL,
Telefon NVARCHAR(20) NOT NULL,
Durum Bit DEFAULT (0)NOT NULL
)
GO
CREATE TABLE TblMASA
(
MasaId INT IDENTITY(1,1) PRIMARY KEY,
MasaNo INT NOT NULL,
Aciklama NVARCHAR(250) NOT NULL,
Tutar DECIMAL(10,2) DEFAULT (0),
Durum Bit DEFAULT (0)NOT NULL
)
GO
CREATE TABLE TblFIRMA
(
FirmaId INT IDENTITY(1,1)  PRIMARY KEY,
FirmaAdi NVARCHAR(100) NOT NULL,
Telefon NVARCHAR(30) NOT NULL, 
TelefonÝki NVARCHAR(30),
Email NVARCHAR(100) NOT NULL,
WebSitesi NVARCHAR(250),
VergiDairesi NVARCHAR(MAX) NOT NULL,
HesapNo NVARCHAR(MAX) NOT NULL,
Adres NVARCHAR(200)NOT NULL,
Durumu BIT DEFAULT (1)
)
GO
CREATE TABLE TblKATEGORI
(
KategoriId INT IDENTITY(1,1) PRIMARY KEY,
KategoriAdi NVARCHAR(50) NOT NULL
)

GO
CREATE TABLE TblURUN
(
UrunId INT IDENTITY(1,1) PRIMARY KEY,
UrunAdi NVARCHAR(100) NOT NULL,
Fiyat DECIMAL(10,2) NOT NULL,
KategoriId INT NOT NULL,
FirmaId INT,
Aciklama NVARCHAR(250),
StokMiktari INT DEFAULT 0,
Birim NVARCHAR(20) DEFAULT 'Adet', -- Adet, Gram, Litre vs.
Durum BIT DEFAULT 1, -- 1: Aktif, 0: Pasif
ResimYolu NVARCHAR(250),
EklenmeTarihi DATETIME DEFAULT GETDATE()
)
GO
CREATE TABLE TblREZARVASYON
(
RezarvasyonId INT IDENTITY(1,1) PRIMARY KEY,
MusteriId INT NOT NULL,
MasaNoId INT NOT NULL,
KisiSayisi INT CHECK(KisiSayisi BETWEEN 1 AND 20)NOT NULL,
Tarih DATE NOT NULL, 
Saat TIME NOT NULL,
Aciklama NVARCHAR(100),
Durum BIT DEFAULT(1)
)
GO
CREATE TABLE TblCEKSENET
(
CeksenetId INT IDENTITY(1,1) PRIMARY KEY,
SatisNo INT,
MusteriId INT,
FirmaId INT,
Tutar DECIMAL(10,2) NOT NULL,
OdemeTuru NVARCHAR(50),
PersonelId INT,
Tarih DATE NOT NULL,
Durum Bit 
)
GO
CREATE TABLE TblSATIS --FK HAZIR DEÐÝL
(
SatisId INT IDENTITY PRIMARY KEY,
UrunId INT NOT NULL,
)
--CREATE TABLE TblMUHASEBE
--(
--MuhasebeId INT IDENTITY(1,1) PRIMARY KEY,
--IslemTuru NVARCHAR(100) NOT NULL, -- 'Maaþ Ödemesi', 'Stok Alýmý', 'Günlük Harcama', 'Satýþ Geliri', 'Avans'
--GelirGider NVARCHAR(10) NOT NULL CHECK(GelirGider IN ('Gelir', 'Gider')), -- Bu iþlem Gelir mi Gider mi?
--Tutar DECIMAL(12, 2) NOT NULL, -- Ýþlem tutarý
--Tarih DATETIME DEFAULT GETDATE(),
--Aciklama NVARCHAR(500),
    
--    -- Bu iþlemin kaynaðýný belirtmek için opsiyonel alanlar
--ReferansTablo NVARCHAR(50), -- Örn: 'TblMAAS', 'TblSATIS', 'TblGUNLUKHARCAMA'
--ReferansId INT -- Örn: Maaþ ID'si, Satýþ ID'si, Harcama ID'si
--)
GO
CREATE TABLE TblSTOKHAREKET
(
    StokHareketId INT IDENTITY(1,1) PRIMARY KEY,
    UrunId INT NOT NULL, -- TblURUN'dan ürün ID'si
    FirmaId INT, -- TblFIRMA'dan tedarikçi ID'si (eðer stok giriþi ise)
    HareketTipi NVARCHAR(20) NOT NULL CHECK(HareketTipi IN ('Stok Giriþi', 'Satýþ', 'Ýade', 'Zayi')), -- Giriþ, Satýþ, Ýade, Zayi
    Miktar DECIMAL(10, 2) NOT NULL, -- Miktar (Adet, Kg, Lt vb.)
    BirimFiyat DECIMAL(10,2) NOT NULL, -- Stoða giriþ/çýkýþ maliyeti veya satýþ fiyatý
    Tarih DATETIME DEFAULT GETDATE(),
    Aciklama NVARCHAR(250),
    PersonelId INT -- Ýþlemi yapan personel (TblPERSONELLER'den)
)
CREATE TABLE TblSIPARIS
(
    SiparisId INT IDENTITY(1,1) PRIMARY KEY,
    MasaId INT NOT NULL, -- TblMASA'dan masa ID'si
    PersonelId INT NOT NULL, -- TblPERSONELLER'den sipariþi alan personel
    Tarih DATETIME DEFAULT GETDATE(),
    ToplamTutar DECIMAL(10, 2) DEFAULT 0, -- Sipariþin toplam tutarý (detaylardan hesaplanacak)
    OdemeDurumu BIT DEFAULT 0 -- 0: Ödenmedi, 1: Ödendi
)
GO

-- Bu tablo, sipariþin içindeki her bir kalemi (örn: 2 Çorba, 1 Tatlý) temsil eder.
CREATE TABLE TblSIPARISDETAY
(
    SiparisDetayId INT IDENTITY(1,1) PRIMARY KEY,
    SiparisId INT NOT NULL, 
    UrunId INT NOT NULL, 
    Miktar INT NOT NULL,
    BirimFiyat DECIMAL(10, 2) NOT NULL, -- Ürünün o anki satýþ fiyatý (TblURUN'den çekilir)
    ToplamTutar AS CAST(Miktar * BirimFiyat AS DECIMAL(10, 2)) -- Hesaplanan alan
)
GO

--Muhasebe Gelir Gider
CREATE TABLE TblGIDER
(
    GiderId INT IDENTITY(1,1) PRIMARY KEY,
    GiderTuru NVARCHAR(100) NOT NULL, -- Örn: 'Maaþ Ödemesi', 'Stok Alýmý', 'Elektrik Faturasý', 'Personel Avans'
    Tutar DECIMAL(12, 2) NOT NULL,    -- Harcanan net tutar
    Tarih DATETIME DEFAULT GETDATE(),
    Aciklama NVARCHAR(500),
    PersonelId INT NOT NULL,          -- Bu harcamayý onaylayan/yapan personel
    FirmaId INT,                      -- Opsiyonel: Ödeme yapýlan tedarikçi firma
    
    -- Harcamanýn kaynaðýný izlemek için
    ReferansTablo NVARCHAR(50), -- 'TblMAAS', 'TblGUNLUKHARCAMA', 'TblSTOKHAREKET'
    ReferansId INT              -- Ýlgili tablodaki kaydýn ID'si
)
GO
CREATE TABLE TblGELIR
(
    GelirId INT IDENTITY(1,1) PRIMARY KEY,
    GelirTuru NVARCHAR(100) NOT NULL, -- Örn: 'Satýþ (Nakit)', 'Satýþ (Kredi Kartý)', 'Çek Tahsilatý'
    Tutar DECIMAL(12, 2) NOT NULL,   -- Kazanýlan net tutar
    Tarih DATETIME DEFAULT GETDATE(),
    Aciklama NVARCHAR(500),
    PersonelId INT NOT NULL,         -- Bu geliri kasaya iþleyen personel
    MusteriId INT,                   -- Opsiyonel: Ödemeyi yapan müþteri
    -- Gelirin kaynaðýný izlemek için
    ReferansTablo NVARCHAR(50), -- 'TblSIPARIS', 'TblCEKSENET'
    ReferansId INT              -- Ýlgili tablodaki kaydýn ID'si
)
GO
CREATE TABLE TblMAAS
(
    MaasTanimID INT IDENTITY(1,1) PRIMARY KEY,
    PersonelID INT NOT NULL,
    NetTutar DECIMAL(10, 2) NOT NULL,
    BaslangicTarihi DATE NOT NULL -- Bu maaþýn ne zamandan itibaren geçerli olduðu
)
GO
CREATE TABLE TblBORDROLAR
(
    BordroID INT IDENTITY(1,1) PRIMARY KEY,
    PersonelID INT NOT NULL FOREIGN KEY REFERENCES TblPERSONELLER(PersonelID),
    Ay SMALLINT NOT NULL,
    Yil SMALLINT NOT NULL,
    AnaMaas DECIMAL(10, 2) NOT NULL, -- O ayki net maaþý (TblMAAS_TANIMLARI'ndan geldi)
    Prim DECIMAL(8, 2) DEFAULT 0,
    Kesinti DECIMAL(8, 2) DEFAULT 0,
    Avans DECIMAL(8, 2) DEFAULT 0,
    -- Ödenecek Toplam Tutar (Net Maaþ + Ekler - Kesintiler)
    ToplamOdeme AS CAST(AnaMaas + Prim - Kesinti - Avans AS DECIMAL(10,2)) PERSISTED,
    OdemeTarihi DATE
)
GO