USE [RESTORANDB]
GO
/****** Object:  Table [dbo].[TblADISYON]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblADISYON](
	[AdisyonId] [int] IDENTITY(1,1) NOT NULL,
	[MasaId] [int] NULL,
	[AcilisZamani] [datetime] NULL,
	[KapanisZamani] [datetime] NULL,
	[Durum] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdisyonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblADISYON_DETAY]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblADISYON_DETAY](
	[DetayId] [int] IDENTITY(1,1) NOT NULL,
	[AdisyonId] [int] NULL,
	[UrunId] [int] NULL,
	[Adet] [int] NULL,
	[Fiyat] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[DetayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblBORDROLAR]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblBORDROLAR](
	[BordroID] [int] IDENTITY(1,1) NOT NULL,
	[PersonelID] [int] NOT NULL,
	[Ay] [smallint] NOT NULL,
	[Yil] [smallint] NOT NULL,
	[AnaMaas] [decimal](10, 2) NOT NULL,
	[Prim] [decimal](8, 2) NULL,
	[Kesinti] [decimal](8, 2) NULL,
	[Avans] [decimal](8, 2) NULL,
	[ToplamOdeme]  AS (CONVERT([decimal](10,2),(([AnaMaas]+[Prim])-[Kesinti])-[Avans])) PERSISTED,
	[OdemeTarihi] [date] NULL,
	[Aciklama] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[BordroID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblCEKSENET]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblCEKSENET](
	[CeksenetId] [int] IDENTITY(1,1) NOT NULL,
	[SatisNo] [int] NULL,
	[MusteriId] [int] NULL,
	[FirmaId] [int] NULL,
	[Tutar] [decimal](10, 2) NOT NULL,
	[OdemeTuru] [nvarchar](50) NULL,
	[PersonelId] [int] NULL,
	[YTarih] [date] NOT NULL,
	[OTarih] [date] NULL,
	[Aciklama] [nvarchar](200) NULL,
	[Durum] [bit] NULL,
 CONSTRAINT [PK__TblCEKSE__0A4B240B7AEDC236] PRIMARY KEY CLUSTERED 
(
	[CeksenetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFIRMA]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFIRMA](
	[FirmaId] [int] IDENTITY(1,1) NOT NULL,
	[FirmaAdi] [nvarchar](100) NOT NULL,
	[Unvan] [nvarchar](50) NULL,
	[Telefon] [nvarchar](30) NOT NULL,
	[Telefonİki] [nvarchar](30) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Adres] [nvarchar](200) NOT NULL,
	[WebSitesi] [nvarchar](250) NULL,
	[VergiDairesi] [nvarchar](max) NULL,
	[HesapNo] [nvarchar](max) NULL,
	[Durumu] [bit] NULL,
 CONSTRAINT [PK__TblFIRMA__CD9C5E2FE0AAFC90] PRIMARY KEY CLUSTERED 
(
	[FirmaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFIRMAHAREKET]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFIRMAHAREKET](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UrunId] [int] NULL,
	[Aciklama] [nvarchar](100) NULL,
	[FirmaId] [int] NULL,
	[Tutar] [decimal](18, 2) NULL,
	[Adet] [int] NULL,
	[Tarih] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblFIRMAODEME]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblFIRMAODEME](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirmaId] [int] NULL,
	[Aciklama] [nvarchar](100) NULL,
	[Tutar] [decimal](18, 2) NULL,
	[Tarih] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblGELIR]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblGELIR](
	[GelirId] [int] IDENTITY(1,1) NOT NULL,
	[GelirTuru] [nvarchar](100) NOT NULL,
	[Tutar] [decimal](12, 2) NOT NULL,
	[Tarih] [datetime] NULL,
	[Aciklama] [nvarchar](500) NULL,
	[PersonelId] [int] NOT NULL,
	[MusteriId] [int] NULL,
	[ReferansTablo] [nvarchar](50) NULL,
	[ReferansId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GelirId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblGIDER]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblGIDER](
	[GiderId] [int] IDENTITY(1,1) NOT NULL,
	[GiderTuru] [nvarchar](100) NOT NULL,
	[Tutar] [decimal](12, 2) NOT NULL,
	[Tarih] [datetime] NULL,
	[Aciklama] [nvarchar](500) NULL,
	[PersonelId] [int] NULL,
	[FirmaId] [int] NULL,
	[ReferansTablo] [nvarchar](50) NULL,
	[ReferansId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GiderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblGUNLUKHARCAMA]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblGUNLUKHARCAMA](
	[GunlukId] [int] IDENTITY(1,1) NOT NULL,
	[HarcananYer] [nvarchar](50) NOT NULL,
	[Tarih] [date] NOT NULL,
	[Saat] [time](7) NOT NULL,
	[Aciklama] [nvarchar](50) NOT NULL,
	[Tutar] [decimal](10, 2) NOT NULL,
	[PersonelID] [int] NULL,
	[FirmaId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GunlukId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblKATEGORI]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblKATEGORI](
	[KategoriId] [int] IDENTITY(1,1) NOT NULL,
	[KategoriAdi] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[KategoriId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLKULLANICI]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLKULLANICI](
	[KullaniciId] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciAdSoyad] [nvarchar](100) NULL,
	[KullaniciEmail] [nvarchar](150) NULL,
	[KullaniciAdi] [nvarchar](18) NULL,
	[Sifre] [nvarchar](18) NULL,
	[KullaniciResim] [nvarchar](max) NULL,
	[Yetki] [nvarchar](1) NULL,
	[GUNLUKHARCAMA] [bit] NULL,
	[MUHASEBE] [bit] NULL,
	[CEKSENET] [bit] NULL,
	[SATISDURUMU] [bit] NULL,
	[PERSONEL] [bit] NULL,
	[MUSTERIFIRMA] [bit] NULL,
	[STOK] [bit] NULL,
	[URUNLER] [bit] NULL,
	[REZERVASYON] [bit] NULL,
	[VERITABANI] [bit] NULL,
	[YETKILENDIRMEYAP] [bit] NULL,
 CONSTRAINT [PK_TBLKULLANICI] PRIMARY KEY CLUSTERED 
(
	[KullaniciId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblMAAS]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblMAAS](
	[MaasTanimID] [int] IDENTITY(1,1) NOT NULL,
	[PersonelID] [int] NOT NULL,
	[NetTutar] [decimal](10, 2) NOT NULL,
	[BaslangicTarihi] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaasTanimID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblMASA]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblMASA](
	[MasaId] [int] IDENTITY(1,1) NOT NULL,
	[MasaNo] [int] NOT NULL,
	[Aciklama] [nvarchar](250) NOT NULL,
	[Tutar] [decimal](10, 2) NULL,
	[Statu] [char](1) NULL,
	[Durum] [bit] NOT NULL,
	[RezervasyonSaati] [nvarchar](50) NULL,
 CONSTRAINT [PK__TblMASA__9F94EBF3158AB893] PRIMARY KEY CLUSTERED 
(
	[MasaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblMODEME]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblMODEME](
	[OdemeId] [int] IDENTITY(1,1) NOT NULL,
	[FmusteriID] [int] NULL,
	[BorcTutar] [decimal](18, 0) NULL,
	[OdenenTutar] [decimal](18, 0) NULL,
	[Tarih] [date] NULL,
	[Aciklama] [nvarchar](50) NULL,
	[durum] [bit] NULL,
 CONSTRAINT [PK_TblMODEME] PRIMARY KEY CLUSTERED 
(
	[OdemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblMUSTERILER]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblMUSTERILER](
	[MusteriId] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [nvarchar](250) NOT NULL,
	[Soyad] [nvarchar](250) NOT NULL,
	[MasaId] [int] NOT NULL,
	[Tarih] [date] NOT NULL,
	[Saat] [time](7) NOT NULL,
	[Aciklama] [nvarchar](250) NOT NULL,
	[Telefon] [nvarchar](20) NULL,
	[Durum] [bit] NOT NULL,
 CONSTRAINT [PK__TblMUSTE__72624791AA3C0E1A] PRIMARY KEY CLUSTERED 
(
	[MusteriId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblPERSONELLER]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblPERSONELLER](
	[PersonelID] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [nvarchar](500) NOT NULL,
	[Soyad] [nvarchar](500) NOT NULL,
	[TCKimlikNo] [nchar](11) NOT NULL,
	[Telefon] [nchar](12) NOT NULL,
	[Adres] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Pozisyon] [nvarchar](50) NOT NULL,
	[Tarih] [date] NOT NULL,
	[Durum] [bit] NOT NULL,
	[Resim] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[PersonelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblPERSONELODEME]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblPERSONELODEME](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PERSONEL] [int] NULL,
	[TUR] [nvarchar](50) NULL,
	[ODEMEMIKTARI] [decimal](10, 2) NULL,
	[TARIH] [date] NULL,
	[ACIKLAMA] [nvarchar](50) NULL,
 CONSTRAINT [PK_TblPERSONELODEME] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblREZARVASYON]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblREZARVASYON](
	[RezarvasyonId] [int] IDENTITY(1,1) NOT NULL,
	[MusteriId] [int] NOT NULL,
	[MasaNoId] [int] NOT NULL,
	[KisiSayisi] [int] NOT NULL,
	[Tarih] [date] NOT NULL,
	[Saat] [time](7) NOT NULL,
	[Aciklama] [nvarchar](100) NULL,
	[Durum] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[RezarvasyonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblSATIS]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblSATIS](
	[SatisId] [int] IDENTITY(1,1) NOT NULL,
	[UrunId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SatisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblSIPARIS]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblSIPARIS](
	[SiparisId] [int] IDENTITY(1,1) NOT NULL,
	[MasaId] [int] NOT NULL,
	[PersonelId] [int] NOT NULL,
	[Tarih] [datetime] NULL,
	[ToplamTutar] [decimal](10, 2) NULL,
	[OdemeDurumu] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[SiparisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblSIPARISDETAY]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblSIPARISDETAY](
	[SiparisDetayId] [int] IDENTITY(1,1) NOT NULL,
	[SiparisId] [int] NOT NULL,
	[UrunId] [int] NOT NULL,
	[Miktar] [int] NOT NULL,
	[BirimFiyat] [decimal](10, 2) NOT NULL,
	[ToplamTutar]  AS (CONVERT([decimal](10,2),[Miktar]*[BirimFiyat])),
PRIMARY KEY CLUSTERED 
(
	[SiparisDetayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblSTOKHAREKET]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblSTOKHAREKET](
	[StokHareketId] [int] IDENTITY(1,1) NOT NULL,
	[UrunId] [int] NOT NULL,
	[FirmaId] [int] NULL,
	[HareketTipi] [nvarchar](20) NULL,
	[Miktar] [decimal](10, 2) NULL,
	[BirimTuru] [nvarchar](50) NULL,
	[BirimFiyat] [decimal](10, 2) NULL,
	[Tarih] [date] NULL,
	[Saat] [time](3) NULL,
	[Aciklama] [nvarchar](250) NULL,
	[PersonelId] [int] NULL,
 CONSTRAINT [PK__TblSTOKH__8F9B28E0E51B040A] PRIMARY KEY CLUSTERED 
(
	[StokHareketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TblURUN]    Script Date: 6.03.2026 00:53:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblURUN](
	[UrunId] [int] IDENTITY(1,1) NOT NULL,
	[UrunAdi] [nvarchar](100) NOT NULL,
	[Fiyat] [decimal](10, 2) NOT NULL,
	[KategoriId] [int] NOT NULL,
	[FirmaId] [int] NOT NULL,
	[Aciklama] [nvarchar](250) NULL,
	[StokMiktari] [int] NULL,
	[Birim] [nvarchar](20) NULL,
	[Durum] [bit] NULL,
	[ResimYolu] [nvarchar](250) NULL,
	[EklenmeTarihi] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UrunId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TblADISYON] ON 
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (18, 1, CAST(N'2026-03-02T15:43:08.403' AS DateTime), NULL, 0)
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (19, 1, CAST(N'2026-03-04T17:03:10.397' AS DateTime), NULL, 0)
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (20, 4, CAST(N'2026-03-04T17:03:22.180' AS DateTime), NULL, 0)
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (21, 1, CAST(N'2026-03-04T17:10:19.223' AS DateTime), NULL, 0)
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (22, 1, CAST(N'2026-03-04T17:22:11.627' AS DateTime), NULL, 0)
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (23, 1, CAST(N'2026-03-04T17:22:21.960' AS DateTime), NULL, 0)
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (24, 4, CAST(N'2026-03-04T17:22:38.270' AS DateTime), NULL, 0)
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (25, 13, CAST(N'2026-03-05T23:05:49.107' AS DateTime), NULL, 0)
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (26, 33, CAST(N'2026-03-06T00:00:24.553' AS DateTime), NULL, 0)
GO
INSERT [dbo].[TblADISYON] ([AdisyonId], [MasaId], [AcilisZamani], [KapanisZamani], [Durum]) VALUES (27, 13, CAST(N'2026-03-06T00:49:07.987' AS DateTime), NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[TblADISYON] OFF
GO
SET IDENTITY_INSERT [dbo].[TblBORDROLAR] ON 
GO
INSERT [dbo].[TblBORDROLAR] ([BordroID], [PersonelID], [Ay], [Yil], [AnaMaas], [Prim], [Kesinti], [Avans], [OdemeTarihi], [Aciklama]) VALUES (1, 1, 10, 2025, CAST(20000.00 AS Decimal(10, 2)), CAST(500.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(1000.00 AS Decimal(8, 2)), CAST(N'2025-10-31' AS Date), NULL)
GO
INSERT [dbo].[TblBORDROLAR] ([BordroID], [PersonelID], [Ay], [Yil], [AnaMaas], [Prim], [Kesinti], [Avans], [OdemeTarihi], [Aciklama]) VALUES (2, 2, 10, 2025, CAST(28000.00 AS Decimal(10, 2)), CAST(1000.00 AS Decimal(8, 2)), CAST(250.00 AS Decimal(8, 2)), CAST(2000.00 AS Decimal(8, 2)), CAST(N'2025-10-31' AS Date), NULL)
GO
INSERT [dbo].[TblBORDROLAR] ([BordroID], [PersonelID], [Ay], [Yil], [AnaMaas], [Prim], [Kesinti], [Avans], [OdemeTarihi], [Aciklama]) VALUES (3, 3, 10, 2025, CAST(22000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(1500.00 AS Decimal(8, 2)), CAST(N'2025-10-31' AS Date), NULL)
GO
INSERT [dbo].[TblBORDROLAR] ([BordroID], [PersonelID], [Ay], [Yil], [AnaMaas], [Prim], [Kesinti], [Avans], [OdemeTarihi], [Aciklama]) VALUES (4, 5, 10, 2025, CAST(17000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(500.00 AS Decimal(8, 2)), CAST(N'2025-10-31' AS Date), NULL)
GO
INSERT [dbo].[TblBORDROLAR] ([BordroID], [PersonelID], [Ay], [Yil], [AnaMaas], [Prim], [Kesinti], [Avans], [OdemeTarihi], [Aciklama]) VALUES (5, 1, 9, 2025, CAST(20000.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(N'2025-09-30' AS Date), NULL)
GO
INSERT [dbo].[TblBORDROLAR] ([BordroID], [PersonelID], [Ay], [Yil], [AnaMaas], [Prim], [Kesinti], [Avans], [OdemeTarihi], [Aciklama]) VALUES (6, 5, 11, 2025, CAST(5.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(N'2025-11-07' AS Date), NULL)
GO
SET IDENTITY_INSERT [dbo].[TblBORDROLAR] OFF
GO
SET IDENTITY_INSERT [dbo].[TblCEKSENET] ON 
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (1, 1, 1, 1, CAST(5000.00 AS Decimal(10, 2)), N'Çek', 3, CAST(N'2025-11-15' AS Date), CAST(N'2025-11-25' AS Date), N'eqwewq', 0)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (2, 2, 2, 2, CAST(2500.00 AS Decimal(10, 2)), N'Senet', 3, CAST(N'2025-11-20' AS Date), CAST(N'2025-11-30' AS Date), N'qwewq', 1)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (3, 3, 4, 3, CAST(10000.00 AS Decimal(10, 2)), N'Çek', 3, CAST(N'2025-12-01' AS Date), CAST(N'2025-12-15' AS Date), N'qwewq', 0)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (4, 4, 7, 4, CAST(3000.00 AS Decimal(10, 2)), N'Senet', 3, CAST(N'2025-12-10' AS Date), CAST(N'2025-12-25' AS Date), N'qewq', 1)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (5, 5, 8, 5, CAST(7500.00 AS Decimal(10, 2)), N'Çek', 3, CAST(N'2025-11-25' AS Date), CAST(N'2025-11-15' AS Date), N'qewqe', 1)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (7, 1, NULL, 2, CAST(6500.00 AS Decimal(10, 2)), N'Çek', 2, CAST(N'2025-11-10' AS Date), CAST(N'2025-11-30' AS Date), N'Ödeme', 1)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (8, NULL, NULL, 1, CAST(75000.00 AS Decimal(10, 2)), N'Çek', NULL, CAST(N'2025-11-01' AS Date), CAST(N'2025-11-19' AS Date), N'Yüklü Et ve süt alımı', 1)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (9, NULL, NULL, 5, CAST(1.00 AS Decimal(10, 2)), N'Çek', NULL, CAST(N'2025-11-14' AS Date), CAST(N'2025-11-16' AS Date), N'alındı', 0)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (10, NULL, NULL, 5, CAST(1.00 AS Decimal(10, 2)), N'Çek', NULL, CAST(N'2025-11-14' AS Date), CAST(N'2025-11-16' AS Date), N'alındı', 0)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (11, NULL, NULL, 5, CAST(4856.00 AS Decimal(10, 2)), N'Çek', NULL, CAST(N'2025-11-14' AS Date), CAST(N'2025-11-16' AS Date), N'qewqe', 0)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (12, NULL, NULL, 5, CAST(1223.00 AS Decimal(10, 2)), N'Çek', NULL, CAST(N'2025-11-14' AS Date), CAST(N'2025-11-16' AS Date), N'alındı', 1)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (13, NULL, NULL, 2, CAST(6500.00 AS Decimal(10, 2)), N'Çek', NULL, CAST(N'2025-11-10' AS Date), CAST(N'2025-11-30' AS Date), N'Ödeme', 1)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (14, NULL, NULL, 5, CAST(1.00 AS Decimal(10, 2)), N'Çek', NULL, CAST(N'2025-11-14' AS Date), CAST(N'2025-11-16' AS Date), N'alındı', 1)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (15, NULL, NULL, 5, CAST(150.00 AS Decimal(10, 2)), N'Senet', NULL, CAST(N'2025-11-14' AS Date), CAST(N'2025-11-16' AS Date), N'mustafa altinkaynak', 1)
GO
INSERT [dbo].[TblCEKSENET] ([CeksenetId], [SatisNo], [MusteriId], [FirmaId], [Tutar], [OdemeTuru], [PersonelId], [YTarih], [OTarih], [Aciklama], [Durum]) VALUES (16, NULL, NULL, 5, CAST(1500.00 AS Decimal(10, 2)), N'Senet', NULL, CAST(N'2025-11-14' AS Date), CAST(N'2026-01-07' AS Date), N'mustafa altinkaynak', 1)
GO
SET IDENTITY_INSERT [dbo].[TblCEKSENET] OFF
GO
SET IDENTITY_INSERT [dbo].[TblFIRMA] ON 
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (1, N'Anadolu Et ve Süt Ürünleri A.Ş.', N'Firma', N'0212 555 1010', NULL, N'anadoluetsut@hotmail.com', N'İstanbul, Büyükçekmece', N'anadoluet.com.tr', N'Ankara Vergi Dairesi', N'123456789', 1)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (2, N'Yeşilbahçe Sebze Meyve Hali', N'Firma', N'0532 123 4567', NULL, N'yesilbahcehal@hotmail.com', N'İstanbul, Büyükçekmece', NULL, N'Ankara Vergi Dairesi', N'223456782', 1)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (3, N'Toptan İçecek Dağıtım Merkezi', N'Firma', N'0850 444 2000', NULL, N'tptnicecekdagitim@hotmail.com', N'Ankara, Sincan', NULL, N'Ankara Vergi Dairesi', N'323456784', 1)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (4, N'Temizlik ve Hijyen Çözümleri Ltd.', N'Firma', N'0216 777 3030', NULL, N'temizlikhijyencozum@hotmail.com', N'Kocaeli, Gebze', NULL, NULL, N'423456786', 1)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (5, N'Mutfak Ekipmanları A.Ş.', N'Firma', N'0212 999 8877', NULL, N'info@mutfakekipman.com', N'İstanbul, Pendik', NULL, NULL, N'523456784', 1)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (6, N'Yusuf ERDOĞAN', N'Müşteri', N'05510426262', N'055162783246', N'ysuf@hoıtma.com', N'Çorum', N'ysuuferd.com.tr', N'Çorum Vergi Dairesi', N'567898776', 1)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (8, N'Kerem Çetin', N'Müşteri', N'05358645575', N'', N'kerem12@hotmail.com', N'Ankara', N'', N'', N'', 1)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (9, N'Emrah Çapkın', N'Müşteri', N'05467891524', N'', N'emrah@hotmail.com', N'Ankara', N'', N'Ankara Vergi Dairesi', N'456123456', 0)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (10, N'Karaca', N'Firma', N'0216 777 3030', N'', N'karaca@hotmail.com', N'Kocaeli, Gebze', N'', N'Ankara Vergi Dairesi', N'423456786', NULL)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (11, N'Kerem Çetin', N'Müşteri', N'05358645575', N'', N'kerem12@hotmail.com', N'Ankara', N'', N'', N'', NULL)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (12, N'Mustafa AltınKaynak', N'Müşteri', N'05564567891', N'', N'mustafa60@gmail.com', N'Ankara', N'', N'', N'', NULL)
GO
INSERT [dbo].[TblFIRMA] ([FirmaId], [FirmaAdi], [Unvan], [Telefon], [Telefonİki], [Email], [Adres], [WebSitesi], [VergiDairesi], [HesapNo], [Durumu]) VALUES (13, N'Deneme', N'Firma', N'0216 777 312', N'', N'temizlikhijyencozum@hotmail.com', N'Kocaeli, Gebze', N'', N'', N'423456786', NULL)
GO
SET IDENTITY_INSERT [dbo].[TblFIRMA] OFF
GO
SET IDENTITY_INSERT [dbo].[TblFIRMAHAREKET] ON 
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (1, 1, N'-', 1, CAST(12200.00 AS Decimal(18, 2)), 1, CAST(N'2025-10-03' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (2, 2, N'-', 2, CAST(20000.00 AS Decimal(18, 2)), 3, CAST(N'2025-05-10' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (6, 9, NULL, 3, CAST(870.00 AS Decimal(18, 2)), 3, CAST(N'2025-12-04' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (7, 3, NULL, 1, CAST(255.00 AS Decimal(18, 2)), 3, CAST(N'2025-12-05' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (8, 4, N'wqeqw', 1, CAST(140.00 AS Decimal(18, 2)), 2, CAST(N'2025-12-05' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (9, 21, N'qweqeqw', 1, CAST(45.00 AS Decimal(18, 2)), 3, CAST(N'2025-12-05' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (10, 16, NULL, 1, CAST(30.00 AS Decimal(18, 2)), 2, CAST(N'2025-12-05' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (11, 14, NULL, 2, CAST(260.00 AS Decimal(18, 2)), 2, CAST(N'2025-12-05' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (12, 22, NULL, 2, CAST(25.00 AS Decimal(18, 2)), 5, CAST(N'2025-12-20' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (13, 8, NULL, 1, CAST(480.00 AS Decimal(18, 2)), 4, CAST(N'2025-12-20' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (14, 7, NULL, 4, CAST(250.00 AS Decimal(18, 2)), 10, CAST(N'2025-12-20' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (15, 22, NULL, 5, CAST(5.00 AS Decimal(18, 2)), 1, CAST(N'2025-12-20' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (16, 22, NULL, 2, CAST(15.00 AS Decimal(18, 2)), 3, CAST(N'2025-12-24' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (17, 8, NULL, 5, CAST(480.00 AS Decimal(18, 2)), 4, CAST(N'2025-12-24' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (18, 16, NULL, 4, CAST(30.00 AS Decimal(18, 2)), 2, CAST(N'2025-12-24' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (21, 22, NULL, 5, CAST(10.00 AS Decimal(18, 2)), 2, CAST(N'2025-12-24' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (23, 23, NULL, 5, CAST(50.00 AS Decimal(18, 2)), 2, CAST(N'2025-12-24' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (24, 23, N'', 10, CAST(6250.00 AS Decimal(18, 2)), 250, CAST(N'2025-12-25' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (25, 23, NULL, 10, CAST(100.00 AS Decimal(18, 2)), 4, CAST(N'2026-01-07' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (26, 25, NULL, 1, CAST(240.00 AS Decimal(18, 2)), 8, CAST(N'2026-01-07' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (27, 25, NULL, 10, CAST(120.00 AS Decimal(18, 2)), 4, CAST(N'2026-01-07' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (28, 25, NULL, 5, CAST(0.00 AS Decimal(18, 2)), 0, CAST(N'2026-01-09' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (29, 24, NULL, 5, CAST(50.00 AS Decimal(18, 2)), 5, CAST(N'2026-02-20' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (30, 27, NULL, 1, CAST(500.00 AS Decimal(18, 2)), 5, CAST(N'2026-02-20' AS Date))
GO
INSERT [dbo].[TblFIRMAHAREKET] ([ID], [UrunId], [Aciklama], [FirmaId], [Tutar], [Adet], [Tarih]) VALUES (31, 26, NULL, 13, CAST(700.00 AS Decimal(18, 2)), 7, CAST(N'2026-02-20' AS Date))
GO
SET IDENTITY_INSERT [dbo].[TblFIRMAHAREKET] OFF
GO
SET IDENTITY_INSERT [dbo].[TblFIRMAODEME] ON 
GO
INSERT [dbo].[TblFIRMAODEME] ([Id], [FirmaId], [Aciklama], [Tutar], [Tarih]) VALUES (1, 1, N'-', CAST(14000.00 AS Decimal(18, 2)), CAST(N'2025-10-03' AS Date))
GO
INSERT [dbo].[TblFIRMAODEME] ([Id], [FirmaId], [Aciklama], [Tutar], [Tarih]) VALUES (2, 2, N'-', CAST(20000.00 AS Decimal(18, 2)), CAST(N'2025-05-03' AS Date))
GO
SET IDENTITY_INSERT [dbo].[TblFIRMAODEME] OFF
GO
SET IDENTITY_INSERT [dbo].[TblGELIR] ON 
GO
INSERT [dbo].[TblGELIR] ([GelirId], [GelirTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [MusteriId], [ReferansTablo], [ReferansId]) VALUES (1, N'Satış (Nakit)', CAST(280.00 AS Decimal(12, 2)), CAST(N'2025-10-22T00:00:00.000' AS DateTime), N'Sipariş 1 Ödemesi (Masa 3)', 1, 1, N'TblSIPARIS', 1)
GO
INSERT [dbo].[TblGELIR] ([GelirId], [GelirTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [MusteriId], [ReferansTablo], [ReferansId]) VALUES (2, N'Satış (Kredi Kartı)', CAST(280.00 AS Decimal(12, 2)), CAST(N'2025-10-23T00:00:00.000' AS DateTime), N'Sipariş 2 Ödemesi (Masa 7)', 1, 2, N'TblSIPARIS', 2)
GO
INSERT [dbo].[TblGELIR] ([GelirId], [GelirTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [MusteriId], [ReferansTablo], [ReferansId]) VALUES (3, N'Satış (Nakit)', CAST(155.00 AS Decimal(12, 2)), CAST(N'2025-10-24T00:00:00.000' AS DateTime), N'Sipariş 3 Ödemesi (Masa 2)', 1, 3, N'TblSIPARIS', 3)
GO
INSERT [dbo].[TblGELIR] ([GelirId], [GelirTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [MusteriId], [ReferansTablo], [ReferansId]) VALUES (4, N'Satış (Kredi Kartı)', CAST(480.00 AS Decimal(12, 2)), CAST(N'2025-10-26T00:00:00.000' AS DateTime), N'Sipariş 5 Ödemesi (Masa 4)', 1, 5, N'TblSIPARIS', 5)
GO
INSERT [dbo].[TblGELIR] ([GelirId], [GelirTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [MusteriId], [ReferansTablo], [ReferansId]) VALUES (5, N'Senet Tahsilatı', CAST(3000.00 AS Decimal(12, 2)), CAST(N'2025-11-10T00:00:00.000' AS DateTime), N'Gamze Çelik senet ödemesi', 3, 7, N'TblCEKSENET', 4)
GO
INSERT [dbo].[TblGELIR] ([GelirId], [GelirTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [MusteriId], [ReferansTablo], [ReferansId]) VALUES (9, N'Senet Tahsilatı', CAST(120000.00 AS Decimal(12, 2)), CAST(N'2025-11-09T00:00:00.000' AS DateTime), N'Tolga Aksoy senet ödemesi', 3, 8, N'TblCEKSENET', NULL)
GO
INSERT [dbo].[TblGELIR] ([GelirId], [GelirTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [MusteriId], [ReferansTablo], [ReferansId]) VALUES (10, N'Çek Tahsilatı', CAST(1223.00 AS Decimal(12, 2)), CAST(N'2025-12-25T18:00:19.613' AS DateTime), N'Mutfak Ekipmanları A.Ş. - Çek/Senet No: 12 Tahsilatı (Oto. Kayıt)', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGELIR] ([GelirId], [GelirTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [MusteriId], [ReferansTablo], [ReferansId]) VALUES (11, N'Müşteri Tahsilatı', CAST(100.00 AS Decimal(12, 2)), CAST(N'2026-02-19T21:22:49.207' AS DateTime), N'Deneme - Tahsilat - Dneme', 1, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TblGELIR] OFF
GO
SET IDENTITY_INSERT [dbo].[TblGIDER] ON 
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (1, N'Stok Alımı', CAST(3500.00 AS Decimal(12, 2)), CAST(N'2025-10-20T00:00:00.000' AS DateTime), N'Anadolu Et''ten mal alımı (50x70)', 2, 1, N'TblSTOKHAREKET', 1)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (2, N'Stok Alımı', CAST(3000.00 AS Decimal(12, 2)), CAST(N'2025-10-21T00:00:00.000' AS DateTime), N'Toptan İçecek''ten alım (200x15)', 3, 3, N'TblSTOKHAREKET', 2)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (3, N'Stok Alımı', CAST(2000.00 AS Decimal(12, 2)), CAST(N'2025-10-21T00:00:00.000' AS DateTime), N'Yeşilbahçe Hal''den alım (100x20)', 2, 2, N'TblSTOKHAREKET', 3)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (4, N'Günlük Harcama', CAST(750.00 AS Decimal(12, 2)), CAST(N'2025-10-25T00:00:00.000' AS DateTime), N'Pazar Alışverişi', 2, NULL, N'TblGUNLUKHARCAMA', 1)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (5, N'Günlük Harcama', CAST(400.00 AS Decimal(12, 2)), CAST(N'2025-10-26T00:00:00.000' AS DateTime), N'Ofis için adisyon fişleri', 3, NULL, N'TblGUNLUKHARCAMA', 2)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (6, N'Maaş Ödemesi', CAST(19500.00 AS Decimal(12, 2)), CAST(N'2025-10-31T00:00:00.000' AS DateTime), N'Ekim 2025 Maaş - Mustafa A.', 3, NULL, N'TblBORDROLAR', 1)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (7, N'Maaş Ödemesi', CAST(26750.00 AS Decimal(12, 2)), CAST(N'2025-10-31T00:00:00.000' AS DateTime), N'Ekim 2025 Maaş - Buğrahan Y.', 3, NULL, N'TblBORDROLAR', 2)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (8, N'Stok Gideri', CAST(140.00 AS Decimal(12, 2)), CAST(N'2025-12-05T00:00:00.000' AS DateTime), N'wqeqw', 3, 1, N'TblBORDROLAR', 3)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (12, N'Stok Alım', CAST(870.00 AS Decimal(12, 2)), CAST(N'2025-12-04T23:42:55.910' AS DateTime), N'Toptan İçecek Dağıtım Merkezi firmasından Hamburger Menü ürünü için ödeme', 2, 3, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (13, N'Stok Alım', CAST(255.00 AS Decimal(12, 2)), CAST(N'2025-12-05T00:30:35.413' AS DateTime), N'Anadolu Et ve Süt Ürünleri A.Ş. FirmasındanTavuk Izgara Ürünü İçin Ödeme', 3, 1, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (14, N'Stok Alım', CAST(140.00 AS Decimal(12, 2)), CAST(N'2025-12-05T00:40:29.127' AS DateTime), N'Anadolu Et ve Süt Ürünleri A.Ş. FirmasındanSpagetti Napoliten Ürünü İçin Ödeme', 1, 1, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (15, N'Stok Alım', CAST(45.00 AS Decimal(12, 2)), CAST(N'2025-12-05T00:58:45.407' AS DateTime), N'Yeşilbahçe Sebze Meyve Hali Firmasındanqeqeqwe Ürünü İçin Ödeme', 2, 2, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (16, N'Stok Alım', CAST(30.00 AS Decimal(12, 2)), CAST(N'2025-12-05T00:59:55.473' AS DateTime), N'Yeşilbahçe Sebze Meyve Hali FirmasındanKaşar Ekstra Ürünü İçin Ödeme', 1, 2, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (17, N'Stok Alım', CAST(260.00 AS Decimal(12, 2)), CAST(N'2025-12-05T01:10:17.880' AS DateTime), N'Toptan İçecek Dağıtım Merkezi FirmasındanKarides Tava Ürünü İçin Ödeme', 1, 2, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (18, N'Avans', CAST(5000.00 AS Decimal(12, 2)), CAST(N'2025-12-05T00:00:00.000' AS DateTime), N'-', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (19, N'Stok Alım', CAST(25.00 AS Decimal(12, 2)), CAST(N'2025-12-20T18:49:00.263' AS DateTime), N'Toptan İçecek Dağıtım Merkezi FirmasındanRanc Sos Ürünü İçin Ödeme', NULL, 2, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (20, N'Stok Alım', CAST(480.00 AS Decimal(12, 2)), CAST(N'2025-12-20T18:49:37.440' AS DateTime), N'Yeşilbahçe Sebze Meyve Hali FirmasındanSerpme Kahvaltı Ürünü İçin Ödeme', NULL, 1, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (21, N'Stok Alım', CAST(250.00 AS Decimal(12, 2)), CAST(N'2025-12-20T18:50:21.953' AS DateTime), N'Mutfak Ekipmanları A.Ş. FirmasındanKola 330ml Ürünü İçin Ödeme', NULL, 4, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (22, N'Stok Alım', CAST(5.00 AS Decimal(12, 2)), CAST(N'2025-12-20T18:50:43.197' AS DateTime), N'Karaca FirmasındanRanc Sos Ürünü İçin Ödeme', NULL, 5, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (23, N'Stok Alım', CAST(15.00 AS Decimal(12, 2)), CAST(N'2025-12-24T22:10:48.013' AS DateTime), N'Toptan İçecek Dağıtım Merkezi FirmasındanRanc Sos Ürünü İçin Ödeme', NULL, 2, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (24, N'Stok Gideri', CAST(6250.00 AS Decimal(12, 2)), CAST(N'2025-12-25T00:00:00.000' AS DateTime), N'', NULL, 10, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (25, N'Stok Alım', CAST(30.00 AS Decimal(12, 2)), CAST(N'2025-12-24T22:19:25.973' AS DateTime), N'Mutfak Ekipmanları A.Ş. FirmasındanKaşar Ekstra Ürünü İçin Ödeme', NULL, 4, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (26, N'Stok Alım', CAST(10.00 AS Decimal(12, 2)), CAST(N'2025-12-24T22:22:14.720' AS DateTime), N'Karaca FirmasındanRanc Sos Ürünü İçin Ödeme', NULL, 5, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (27, N'Günlük Harcama', CAST(120.00 AS Decimal(12, 2)), CAST(N'2025-12-24T00:00:00.000' AS DateTime), N'Manav - Domates alindi', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (28, N'Günlük Harcama', CAST(12312.00 AS Decimal(12, 2)), CAST(N'2025-12-24T00:00:00.000' AS DateTime), N'asdaw  - asdaw', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (29, N'Stok Alım', CAST(50.00 AS Decimal(12, 2)), CAST(N'2025-12-24T22:38:53.517' AS DateTime), N'Karaca FirmasındanDeneme Ürünü İçin Ödeme', NULL, 5, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (30, N'Stok Alım', CAST(75.00 AS Decimal(12, 2)), CAST(N'2025-12-25T16:41:39.547' AS DateTime), N'Karaca firmasından Deneme alımı.', 1, 10, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (31, N'Çek Ödemesi', CAST(6500.00 AS Decimal(12, 2)), CAST(N'2025-12-25T18:00:09.490' AS DateTime), N'Yeşilbahçe Sebze Meyve Hali firmasına ait Çek ödendi. (No: 13)', 1, 2, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (32, N'Günlük Harcama', CAST(329.00 AS Decimal(12, 2)), CAST(N'2025-12-27T00:00:00.000' AS DateTime), N'Aaasdw - Bardak', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (33, N'Çek Ödemesi', CAST(1.00 AS Decimal(12, 2)), CAST(N'2026-01-04T14:32:52.643' AS DateTime), N'Mutfak Ekipmanları A.Ş. firmasına ait Çek ödendi. (No: 14)', 1, 5, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (34, N'Senet Ödemesi', CAST(150.00 AS Decimal(12, 2)), CAST(N'2026-01-04T14:34:12.357' AS DateTime), N'Mutfak Ekipmanları A.Ş. firmasına ait Senet ödendi. (No: 15)', 1, 5, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (35, N'Stok Alım', CAST(100.00 AS Decimal(12, 2)), CAST(N'2026-01-07T00:57:35.877' AS DateTime), N'Karaca firmasından Deneme alımı.', 1, 10, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (36, N'Stok Alım', CAST(240.00 AS Decimal(12, 2)), CAST(N'2026-01-07T00:59:30.097' AS DateTime), N'Anadolu Et ve Süt Ürünleri A.Ş. firmasından Deneme Urun alımı.', 1, 1, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (37, N'Günlük Harcama', CAST(500.00 AS Decimal(12, 2)), CAST(N'2026-01-07T00:00:00.000' AS DateTime), N'hal - Taze Marul', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (38, N'Maaş', CAST(20000.00 AS Decimal(12, 2)), CAST(N'2026-01-07T00:00:00.000' AS DateTime), N'oCAK ayı ödenme', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (39, N'Günlük Harcama', CAST(500.00 AS Decimal(12, 2)), CAST(N'2026-01-07T00:00:00.000' AS DateTime), N'hal - Taze Marul', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (40, N'Günlük Harcama', CAST(200.00 AS Decimal(12, 2)), CAST(N'2026-01-07T00:00:00.000' AS DateTime), N'Manav - Armut', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (41, N'Stok Alım', CAST(120.00 AS Decimal(12, 2)), CAST(N'2026-01-07T14:37:56.650' AS DateTime), N'Karaca firmasından Deneme Urun alımı.', 1, 10, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (42, N'Senet Ödemesi', CAST(1500.00 AS Decimal(12, 2)), CAST(N'2026-01-07T14:42:59.713' AS DateTime), N'Mutfak Ekipmanları A.Ş. firmasına ait Senet ödendi. (No: 16)', 1, 5, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (43, N'Maaş', CAST(20000.00 AS Decimal(12, 2)), CAST(N'2026-01-07T00:00:00.000' AS DateTime), N'Ocak AYı maas', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (44, N'Günlük Harcama', CAST(150.00 AS Decimal(12, 2)), CAST(N'2026-01-07T00:00:00.000' AS DateTime), N'Manav - Domates', 1, NULL, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (45, N'Stok Alım', CAST(0.00 AS Decimal(12, 2)), CAST(N'2026-01-09T19:00:59.737' AS DateTime), N'Mutfak Ekipmanları A.Ş. firmasından Deneme Urun alımı.', 1, 5, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (46, N'Stok Alım', CAST(50.00 AS Decimal(12, 2)), CAST(N'2026-02-20T13:32:17.613' AS DateTime), N'Mutfak Ekipmanları A.Ş. firmasından Acı Sos alımı.', 1, 5, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (47, N'Stok Alım', CAST(500.00 AS Decimal(12, 2)), CAST(N'2026-02-20T13:33:49.573' AS DateTime), N'Anadolu Et ve Süt Ürünleri A.Ş. firmasından jkl alımı.', 1, 1, NULL, NULL)
GO
INSERT [dbo].[TblGIDER] ([GiderId], [GiderTuru], [Tutar], [Tarih], [Aciklama], [PersonelId], [FirmaId], [ReferansTablo], [ReferansId]) VALUES (48, N'Stok Alım', CAST(700.00 AS Decimal(12, 2)), CAST(N'2026-02-20T13:40:28.040' AS DateTime), N'Deneme firmasından jkl alımı.', 1, 13, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TblGIDER] OFF
GO
SET IDENTITY_INSERT [dbo].[TblGUNLUKHARCAMA] ON 
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (1, N'Pazar Alışverişi', CAST(N'2025-10-25' AS Date), CAST(N'09:30:00' AS Time), N'Salı pazarı taze sebze', CAST(750.00 AS Decimal(10, 2)), 2, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (2, N'Kırtasiye', CAST(N'2025-10-26' AS Date), CAST(N'14:15:00' AS Time), N'Ofis için adisyon fişleri', CAST(400.00 AS Decimal(10, 2)), 3, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (3, N'Taksi', CAST(N'2025-10-27' AS Date), CAST(N'11:00:00' AS Time), N'Banka işlemi için gidiş-dönüş', CAST(250.00 AS Decimal(10, 2)), 3, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (4, N'Temizlik Malzemesi', CAST(N'2025-10-28' AS Date), CAST(N'10:00:00' AS Time), N'Eksik deterjanlar', CAST(600.00 AS Decimal(10, 2)), 5, 4)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (5, N'Personel Avansı', CAST(N'2025-10-28' AS Date), CAST(N'23:00:00' AS Time), N'Garson avansı', CAST(1000.00 AS Decimal(10, 2)), 1, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (6, N'Hal', CAST(N'2025-10-16' AS Date), CAST(N'12:30:00' AS Time), N'Sebze Alış Verişi', CAST(500.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (7, N'Hal', CAST(N'2025-10-31' AS Date), CAST(N'12:30:00' AS Time), N'Marul', CAST(200.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (8, N'Hal', CAST(N'2025-11-03' AS Date), CAST(N'12:29:00' AS Time), N'Marul Alındı', CAST(400.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (9, N'Zincir Marketler', CAST(N'2025-11-03' AS Date), CAST(N'12:05:00' AS Time), N'Temizlik malzemesi alındı', CAST(5000.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (10, N'Hal', CAST(N'2025-11-09' AS Date), CAST(N'12:55:00' AS Time), N'Marul Domates', CAST(120.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (11, N'Market', CAST(N'2025-11-13' AS Date), CAST(N'04:19:00' AS Time), N'domates ve patates alındı', CAST(500.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (12, N'Hal', CAST(N'2025-11-14' AS Date), CAST(N'12:30:00' AS Time), N'sebze', CAST(200.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (13, N'pazar', CAST(N'2025-11-16' AS Date), CAST(N'12:31:00' AS Time), N'toki', CAST(5000.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (14, N'Manav', CAST(N'2025-12-24' AS Date), CAST(N'01:15:00' AS Time), N'Domates alindi', CAST(120.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (15, N'asdaw ', CAST(N'2025-12-24' AS Date), CAST(N'12:09:00' AS Time), N'asdaw', CAST(12312.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (16, N'Aaasdw', CAST(N'2025-12-27' AS Date), CAST(N'12:37:00' AS Time), N'Bardak', CAST(329.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (17, N'hal', CAST(N'2026-01-07' AS Date), CAST(N'08:31:00' AS Time), N'Taze Marul', CAST(500.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (18, N'hal', CAST(N'2026-01-07' AS Date), CAST(N'08:31:00' AS Time), N'Taze Marul', CAST(500.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (19, N'Manav', CAST(N'2026-01-07' AS Date), CAST(N'04:06:00' AS Time), N'Armut', CAST(200.00 AS Decimal(10, 2)), NULL, NULL)
GO
INSERT [dbo].[TblGUNLUKHARCAMA] ([GunlukId], [HarcananYer], [Tarih], [Saat], [Aciklama], [Tutar], [PersonelID], [FirmaId]) VALUES (20, N'Manav', CAST(N'2026-01-07' AS Date), CAST(N'11:55:00' AS Time), N'Domates', CAST(150.00 AS Decimal(10, 2)), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[TblGUNLUKHARCAMA] OFF
GO
SET IDENTITY_INSERT [dbo].[TblKATEGORI] ON 
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (1, N'Çorbalar')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (2, N'Ana Yemekler')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (3, N'Izgaralar')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (4, N'Makarnalar')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (5, N'Salatalar')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (6, N'Tatlılar')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (7, N'İçecekler')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (8, N'Kahvaltılıklar')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (9, N'Fast Food')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (10, N'Yan Ürünler')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (11, N'Çocuk Menüleri')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (12, N'Vejetaryen')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (13, N'Deniz Ürünleri')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (14, N'Soslar')
GO
INSERT [dbo].[TblKATEGORI] ([KategoriId], [KategoriAdi]) VALUES (15, N'Ekstralar')
GO
SET IDENTITY_INSERT [dbo].[TblKATEGORI] OFF
GO
SET IDENTITY_INSERT [dbo].[TBLKULLANICI] ON 
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (1, N'Yusuf Erdoğan', N'erdoy@hotmaile.come', N'yusuferdgn', N'123', NULL, N'A', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (2, N'admin', N'admin', N'admin', N'admin', NULL, N'A', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (3, N'garson1', N'garson1', N'garson1', N'123', NULL, N'K', 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (4, N'gencay', N'gencay', N'gencay', N'gencay', NULL, N'A', 1, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (5, N'deneme', N'deneme', N'deneme', N'deneme', NULL, N'K', 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (6, N'deneme1', N'deneme1', N'deneme1', N'deneme1', NULL, N'K', 1, 0, 1, 1, 0, 0, 0, 1, 1, 0, 1)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (7, N'a', N'a', N'a', N'a', NULL, N'K', 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (8, N'b', N'b', N'b', N'b', NULL, N'K', 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (9, N'q', N'q', N'q', N'q', NULL, N'K', 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (10, N'w', N'w', N'w', N'w', NULL, N'K', 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (11, N'y', N'y', N'y', N'y', NULL, N'K', 1, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (12, N'yusuf', N'yusuf', N'yusuf', N'yusuf', NULL, N'k', 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (13, N'ik', N'ik', N'ik', N'ik', NULL, N'k', 0, 0, 0, 1, 1, 0, 1, 1, 1, 0, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (14, N'yigit', N'bayram.sutcu60gmail.com', N'syigitw', N'hhhh', NULL, N'k', 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0)
GO
INSERT [dbo].[TBLKULLANICI] ([KullaniciId], [KullaniciAdSoyad], [KullaniciEmail], [KullaniciAdi], [Sifre], [KullaniciResim], [Yetki], [GUNLUKHARCAMA], [MUHASEBE], [CEKSENET], [SATISDURUMU], [PERSONEL], [MUSTERIFIRMA], [STOK], [URUNLER], [REZERVASYON], [VERITABANI], [YETKILENDIRMEYAP]) VALUES (15, N'a', N'a', N'a', N'a', NULL, N'k', 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[TBLKULLANICI] OFF
GO
SET IDENTITY_INSERT [dbo].[TblMAAS] ON 
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (1, 1, CAST(20000.00 AS Decimal(10, 2)), CAST(N'2025-01-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (2, 2, CAST(28000.00 AS Decimal(10, 2)), CAST(N'2025-01-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (3, 3, CAST(22000.00 AS Decimal(10, 2)), CAST(N'2025-01-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (4, 5, CAST(17000.00 AS Decimal(10, 2)), CAST(N'2025-01-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (5, 1, CAST(20000.00 AS Decimal(10, 2)), CAST(N'2025-07-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (6, 4, CAST(24000.00 AS Decimal(10, 2)), CAST(N'0001-01-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (7, 6, CAST(20000.00 AS Decimal(10, 2)), CAST(N'0001-01-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (8, 9, CAST(45000.00 AS Decimal(10, 2)), CAST(N'0001-01-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (9, 11, CAST(25000.00 AS Decimal(10, 2)), CAST(N'0001-01-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (10, 19, CAST(17000.00 AS Decimal(10, 2)), CAST(N'0001-01-01' AS Date))
GO
INSERT [dbo].[TblMAAS] ([MaasTanimID], [PersonelID], [NetTutar], [BaslangicTarihi]) VALUES (11, 20, CAST(22000.00 AS Decimal(10, 2)), CAST(N'0001-01-01' AS Date))
GO
SET IDENTITY_INSERT [dbo].[TblMAAS] OFF
GO
SET IDENTITY_INSERT [dbo].[TblMASA] ON 
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (1, 1, N'Pencere Kenarı, 2 kişilik', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (2, 2, N'Salon Ortası, 4 kişilik', CAST(0.00 AS Decimal(10, 2)), N'R', 1, N'12:00')
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (3, 3, N'Bahçe Tarafı, 6 kişilik', CAST(0.00 AS Decimal(10, 2)), N'R', 1, N'14:15')
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (4, 4, N'Teras, 8 kişilik', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (5, 5, N'Teras, 2 kişilik', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (6, 6, N'Salon Köşesi, 2 kişilik', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (7, 7, N'Bahçe Kenarı, 4 kişilik', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (8, 8, N'Salon Girişi, 2 kişilik', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (9, 9, N'Cam kenarı, 6 kişilik', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (10, 10, N'Teras, 4 kişilik', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (11, 11, N'Deneme', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (12, 12, N'DENEM2', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (13, 13, N'DENEM3', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (14, 14, N'DENEME4', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (15, 15, N'Deneme5', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (16, 16, N'D6', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (17, 17, N'D7', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (18, 18, N'D8', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (19, 19, N'D9', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (20, 20, N'D10', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (21, 21, N'd11', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (22, 22, N'D12', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (23, 23, N'D13', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (24, 24, N'D14', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (25, 25, N'D15', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (26, 26, N'D16', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (27, 27, N'D17', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (28, 28, N'D18', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (29, 29, N'D19', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (30, 30, N'D20', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (31, 31, N'', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (32, 32, N'32', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (33, 33, N'33', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
INSERT [dbo].[TblMASA] ([MasaId], [MasaNo], [Aciklama], [Tutar], [Statu], [Durum], [RezervasyonSaati]) VALUES (34, 34, N'34', CAST(0.00 AS Decimal(10, 2)), N'B', 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[TblMASA] OFF
GO
SET IDENTITY_INSERT [dbo].[TblMODEME] ON 
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (1, 11, CAST(200 AS Decimal(18, 0)), CAST(50 AS Decimal(18, 0)), CAST(N'2025-10-09' AS Date), N'..', 1)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (2, 6, CAST(1000 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(N'2025-10-10' AS Date), N'..', 1)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (3, 8, CAST(2000 AS Decimal(18, 0)), CAST(300 AS Decimal(18, 0)), CAST(N'2025-11-10' AS Date), N'..', 1)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (4, 9, CAST(500 AS Decimal(18, 0)), CAST(500 AS Decimal(18, 0)), CAST(N'2025-11-09' AS Date), N'..', 1)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (5, 6, CAST(1000 AS Decimal(18, 0)), CAST(250 AS Decimal(18, 0)), CAST(N'2025-10-10' AS Date), N'..', NULL)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (6, 8, CAST(5000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(N'2025-11-09' AS Date), N'qeqwe', NULL)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (8, 6, CAST(500 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(N'2025-11-09' AS Date), N'..', NULL)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (10, 12, CAST(7000 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(N'2025-11-09' AS Date), N'..', NULL)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (11, 4, CAST(500 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(N'2025-11-09' AS Date), N'..', NULL)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (12, 3, CAST(2000 AS Decimal(18, 0)), CAST(300 AS Decimal(18, 0)), CAST(N'2025-11-10' AS Date), N'..', NULL)
GO
INSERT [dbo].[TblMODEME] ([OdemeId], [FmusteriID], [BorcTutar], [OdenenTutar], [Tarih], [Aciklama], [durum]) VALUES (13, 1, CAST(500 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(N'2025-11-09' AS Date), N'..', NULL)
GO
SET IDENTITY_INSERT [dbo].[TblMODEME] OFF
GO
SET IDENTITY_INSERT [dbo].[TblMUSTERILER] ON 
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (1, N'Merve', N'Kaya', 3, CAST(N'2025-10-22' AS Date), CAST(N'18:00:00' AS Time), N'İş toplantısı', N'05343211522', 1)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (2, N'Burak', N'Demir', 7, CAST(N'2025-10-23' AS Date), CAST(N'20:15:00' AS Time), N'Yemek daveti', N'05353211524', 1)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (3, N'Elif', N'Turan', 2, CAST(N'2025-10-24' AS Date), CAST(N'17:45:00' AS Time), N'Çocuklarla akşam yemeği', N'05363211526', 0)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (4, N'Can', N'Öztürk', 6, CAST(N'2025-10-25' AS Date), CAST(N'19:00:00' AS Time), N'Yıldönümü kutlaması', N'05343211526', 1)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (5, N'Zeynep', N'Aslan', 4, CAST(N'2025-10-26' AS Date), CAST(N'21:00:00' AS Time), N'Arkadaş buluşması', N'05343211522', 1)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (6, N'Emre', N'Yıldız', 1, CAST(N'2025-10-27' AS Date), CAST(N'18:30:00' AS Time), N'İş yemeği', N'05343211522', 0)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (7, N'Gamze', N'Çelik', 8, CAST(N'2025-10-28' AS Date), CAST(N'20:00:00' AS Time), N'Romantik akşam', N'05343211522', 1)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (8, N'Tolga', N'Aksoy', 5, CAST(N'2025-10-29' AS Date), CAST(N'19:30:00' AS Time), N'Kutlama yemeği', N'05343211522', 1)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (9, N'Selin', N'Erdoğan', 9, CAST(N'2025-10-30' AS Date), CAST(N'17:00:00' AS Time), N'Erken akşam yemeği', N'05343211522', 0)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (10, N'Kerem', N'Güneş', 10, CAST(N'2025-10-31' AS Date), CAST(N'22:00:00' AS Time), N'Gece kahvesi', N'05343211522', 1)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (11, N'Yusuf', N'Erdoğan', 1, CAST(N'2026-01-01' AS Date), CAST(N'22:00:00' AS Time), N'asaw', N'05415215536', 1)
GO
INSERT [dbo].[TblMUSTERILER] ([MusteriId], [Ad], [Soyad], [MasaId], [Tarih], [Saat], [Aciklama], [Telefon], [Durum]) VALUES (12, N'İsmail', N'Küçükali', 2, CAST(N'2026-01-05' AS Date), CAST(N'20:00:00' AS Time), N'asdaw', N'05423652634', 1)
GO
SET IDENTITY_INSERT [dbo].[TblMUSTERILER] OFF
GO
SET IDENTITY_INSERT [dbo].[TblPERSONELLER] ON 
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (1, N'Mustafa', N'Altınkaynak', N'50222435424', N'05346548474 ', N'İstanbul', N'altinkaynak@hotmail.com', N'Garson', CAST(N'2023-10-10' AS Date), 1, N'1760373462952.png')
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (2, N'Buğrahan', N'Yılmaz', N'50226445440', N'05356142460 ', N'İstanbul', N'yilmazbugrahan@gmail.com', N'Aşçı', CAST(N'2022-10-10' AS Date), 1, N'1.jpg')
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (3, N'Hüseyin', N'Yüce', N'50126445426', N'05356152160 ', N'İstanbul', N'yucehuseyin@hotmail.com', N'Muhasebe', CAST(N'2021-08-10' AS Date), 1, N'5.jpg')
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (4, N'Adahan', N'Akdeniz', N'12345678966', N'05356161260 ', N'İstanbul', N'akdenizadahan@hotmail.com', N'Garson', CAST(N'2020-06-10' AS Date), 1, N'1.jpg')
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (5, N'Elmas', N'Mutlu', N'50162498740', N'05357406074 ', N'İstanbul', N'elmasmutlu@gmail.com', N'Temizlikçi', CAST(N'2024-10-10' AS Date), 1, N'5.jpg')
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (6, N'Kadir', N'Kara', N'50678945612', N'05347406174 ', N'İstanbul', N'karakadir@gmail.com', N'Temizlikçi', CAST(N'2024-10-10' AS Date), 0, N'7.jpg')
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (9, N'Kerem', N'Çetin', N'50475435612', N'05387456212 ', N'İstanbul', N'kerem@gmail.com', N'Garson', CAST(N'2025-10-30' AS Date), 0, N'logo.png')
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (11, N'Ahmet', N'Çakır', N'45061235646', N'5384525760  ', N'İstanbul', N'cakir@gmail.com', N'Aşçı', CAST(N'2024-02-14' AS Date), 0, N'6.jpg')
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (19, N'Elmas', N'Elmas', N'50162498742', N'05357406074 ', N'İstanbul', N'elmasmutlu@gmail.com', N'Temizlikçi', CAST(N'2024-10-10' AS Date), 0, N'resimyok.png')
GO
INSERT [dbo].[TblPERSONELLER] ([PersonelID], [Ad], [Soyad], [TCKimlikNo], [Telefon], [Adres], [Email], [Pozisyon], [Tarih], [Durum], [Resim]) VALUES (20, N'Hüseyin', N'aAli', N'50126445428', N'05356152160 ', N'İstanbul', N'yucehuseyina@hotmail.com', N'Muhasebe', CAST(N'2021-08-10' AS Date), 1, N'resimyok.png')
GO
SET IDENTITY_INSERT [dbo].[TblPERSONELLER] OFF
GO
SET IDENTITY_INSERT [dbo].[TblPERSONELODEME] ON 
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (1, 1, N'Borç', CAST(1500.00 AS Decimal(10, 2)), CAST(N'2025-10-31' AS Date), N'İhtiyaç')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (2, 2, N'Prim', CAST(10000.00 AS Decimal(10, 2)), CAST(N'2025-10-30' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (3, 2, N'Borç', CAST(400.00 AS Decimal(10, 2)), CAST(N'2025-10-29' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (4, 3, N'Diğer', CAST(3500.00 AS Decimal(10, 2)), CAST(N'2025-10-10' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (5, 11, N'Avans', CAST(4500.00 AS Decimal(10, 2)), CAST(N'2025-11-07' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (6, 9, N'Avans', CAST(5.00 AS Decimal(10, 2)), CAST(N'2025-11-07' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (7, 9, N'Avans', CAST(400.00 AS Decimal(10, 2)), CAST(N'2025-10-07' AS Date), N'...')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (8, 5, N'Avans', CAST(500.00 AS Decimal(10, 2)), CAST(N'2025-11-07' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (9, 5, N'Avans', CAST(500.00 AS Decimal(10, 2)), CAST(N'2025-11-07' AS Date), N'...')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (10, 6, N'Avans', CAST(400.00 AS Decimal(10, 2)), CAST(N'2025-11-07' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (11, 5, N'Avans', CAST(500.00 AS Decimal(10, 2)), CAST(N'2025-11-07' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (12, 5, N'Prim', CAST(500.00 AS Decimal(10, 2)), CAST(N'2025-10-07' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (13, 2, N'Borç', CAST(5.00 AS Decimal(10, 2)), CAST(N'2025-10-07' AS Date), N'...')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (14, 5, N'Diğer...', CAST(20.00 AS Decimal(10, 2)), CAST(N'2025-11-08' AS Date), N'..')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (15, 11, N'Avans', CAST(4500.00 AS Decimal(10, 2)), CAST(N'2025-11-07' AS Date), N'Fazla mesai')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (16, 4, N'Avans', CAST(2000.00 AS Decimal(10, 2)), CAST(N'2025-11-07' AS Date), N'Lazımış')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (17, 6, N'Prim', CAST(10000.00 AS Decimal(10, 2)), CAST(N'2025-12-05' AS Date), N'-')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (18, 1, N'Avans', CAST(5000.00 AS Decimal(10, 2)), CAST(N'2025-12-05' AS Date), N'-')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (19, 1, N'Maaş', CAST(20000.00 AS Decimal(10, 2)), CAST(N'2026-01-07' AS Date), N'oCAK ayı ödenme')
GO
INSERT [dbo].[TblPERSONELODEME] ([ID], [PERSONEL], [TUR], [ODEMEMIKTARI], [TARIH], [ACIKLAMA]) VALUES (20, 1, N'Maaş', CAST(20000.00 AS Decimal(10, 2)), CAST(N'2026-01-07' AS Date), N'Ocak AYı maas')
GO
SET IDENTITY_INSERT [dbo].[TblPERSONELODEME] OFF
GO
SET IDENTITY_INSERT [dbo].[TblREZARVASYON] ON 
GO
INSERT [dbo].[TblREZARVASYON] ([RezarvasyonId], [MusteriId], [MasaNoId], [KisiSayisi], [Tarih], [Saat], [Aciklama], [Durum]) VALUES (1, 2, 2, 4, CAST(N'2025-10-22' AS Date), CAST(N'20:00:00' AS Time), N'İş toplantısı', 1)
GO
INSERT [dbo].[TblREZARVASYON] ([RezarvasyonId], [MusteriId], [MasaNoId], [KisiSayisi], [Tarih], [Saat], [Aciklama], [Durum]) VALUES (2, 3, 3, 3, CAST(N'2025-10-23' AS Date), CAST(N'18:30:00' AS Time), N'Aile yemeği', 1)
GO
INSERT [dbo].[TblREZARVASYON] ([RezarvasyonId], [MusteriId], [MasaNoId], [KisiSayisi], [Tarih], [Saat], [Aciklama], [Durum]) VALUES (3, 4, 4, 5, CAST(N'2025-10-23' AS Date), CAST(N'19:00:00' AS Time), N'Doğum günü kutlaması', 1)
GO
INSERT [dbo].[TblREZARVASYON] ([RezarvasyonId], [MusteriId], [MasaNoId], [KisiSayisi], [Tarih], [Saat], [Aciklama], [Durum]) VALUES (4, 5, 5, 2, CAST(N'2025-10-24' AS Date), CAST(N'20:00:00' AS Time), N'Romantik akşam', 0)
GO
INSERT [dbo].[TblREZARVASYON] ([RezarvasyonId], [MusteriId], [MasaNoId], [KisiSayisi], [Tarih], [Saat], [Aciklama], [Durum]) VALUES (11, 1, 10, 2, CAST(N'2025-10-24' AS Date), CAST(N'20:00:00' AS Time), N'Romantik akşam', NULL)
GO
INSERT [dbo].[TblREZARVASYON] ([RezarvasyonId], [MusteriId], [MasaNoId], [KisiSayisi], [Tarih], [Saat], [Aciklama], [Durum]) VALUES (12, 11, 1, 2, CAST(N'2025-10-24' AS Date), CAST(N'20:00:00' AS Time), N'Romantik akşam', NULL)
GO
INSERT [dbo].[TblREZARVASYON] ([RezarvasyonId], [MusteriId], [MasaNoId], [KisiSayisi], [Tarih], [Saat], [Aciklama], [Durum]) VALUES (13, 11, 1, 2, CAST(N'2025-10-24' AS Date), CAST(N'20:00:00' AS Time), N'Romantik akşam', NULL)
GO
SET IDENTITY_INSERT [dbo].[TblREZARVASYON] OFF
GO
SET IDENTITY_INSERT [dbo].[TblSATIS] ON 
GO
INSERT [dbo].[TblSATIS] ([SatisId], [UrunId]) VALUES (1, 1)
GO
INSERT [dbo].[TblSATIS] ([SatisId], [UrunId]) VALUES (2, 2)
GO
INSERT [dbo].[TblSATIS] ([SatisId], [UrunId]) VALUES (3, 7)
GO
INSERT [dbo].[TblSATIS] ([SatisId], [UrunId]) VALUES (4, 7)
GO
INSERT [dbo].[TblSATIS] ([SatisId], [UrunId]) VALUES (5, 5)
GO
SET IDENTITY_INSERT [dbo].[TblSATIS] OFF
GO
SET IDENTITY_INSERT [dbo].[TblSIPARIS] ON 
GO
INSERT [dbo].[TblSIPARIS] ([SiparisId], [MasaId], [PersonelId], [Tarih], [ToplamTutar], [OdemeDurumu]) VALUES (1, 3, 1, CAST(N'2025-10-22T18:05:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[TblSIPARIS] ([SiparisId], [MasaId], [PersonelId], [Tarih], [ToplamTutar], [OdemeDurumu]) VALUES (2, 7, 1, CAST(N'2025-10-23T20:20:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[TblSIPARIS] ([SiparisId], [MasaId], [PersonelId], [Tarih], [ToplamTutar], [OdemeDurumu]) VALUES (3, 2, 1, CAST(N'2025-10-24T17:50:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), 1)
GO
INSERT [dbo].[TblSIPARIS] ([SiparisId], [MasaId], [PersonelId], [Tarih], [ToplamTutar], [OdemeDurumu]) VALUES (4, 6, 1, CAST(N'2025-10-25T19:05:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), 0)
GO
INSERT [dbo].[TblSIPARIS] ([SiparisId], [MasaId], [PersonelId], [Tarih], [ToplamTutar], [OdemeDurumu]) VALUES (5, 4, 1, CAST(N'2025-10-26T21:02:00.000' AS DateTime), CAST(0.00 AS Decimal(10, 2)), 1)
GO
SET IDENTITY_INSERT [dbo].[TblSIPARIS] OFF
GO
SET IDENTITY_INSERT [dbo].[TblSIPARISDETAY] ON 
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (1, 1, 1, 2, CAST(35.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (2, 1, 2, 1, CAST(95.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (3, 1, 5, 1, CAST(40.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (4, 1, 7, 3, CAST(25.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (5, 2, 3, 2, CAST(85.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (6, 2, 6, 2, CAST(30.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (7, 2, 7, 2, CAST(25.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (8, 3, 9, 1, CAST(90.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (9, 3, 13, 1, CAST(65.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (10, 4, 14, 1, CAST(130.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (11, 4, 4, 1, CAST(70.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (12, 5, 11, 2, CAST(150.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (13, 5, 12, 1, CAST(80.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[TblSIPARISDETAY] ([SiparisDetayId], [SiparisId], [UrunId], [Miktar], [BirimFiyat]) VALUES (14, 5, 7, 4, CAST(25.00 AS Decimal(10, 2)))
GO
SET IDENTITY_INSERT [dbo].[TblSIPARISDETAY] OFF
GO
SET IDENTITY_INSERT [dbo].[TblSTOKHAREKET] ON 
GO
INSERT [dbo].[TblSTOKHAREKET] ([StokHareketId], [UrunId], [FirmaId], [HareketTipi], [Miktar], [BirimTuru], [BirimFiyat], [Tarih], [Saat], [Aciklama], [PersonelId]) VALUES (1, 2, 1, N'Stok Girişi', CAST(50.00 AS Decimal(10, 2)), N'Adet', CAST(70.00 AS Decimal(10, 2)), CAST(N'2025-10-20' AS Date), CAST(N'12:30:00' AS Time), N'Anadolu Et''ten mal alımı', 2)
GO
INSERT [dbo].[TblSTOKHAREKET] ([StokHareketId], [UrunId], [FirmaId], [HareketTipi], [Miktar], [BirimTuru], [BirimFiyat], [Tarih], [Saat], [Aciklama], [PersonelId]) VALUES (2, 7, 3, N'Stok Girişi', CAST(200.00 AS Decimal(10, 2)), N'Koli', CAST(15.00 AS Decimal(10, 2)), CAST(N'2025-10-21' AS Date), CAST(N'11:00:00' AS Time), N'Toptan İçecek''ten alım', 3)
GO
INSERT [dbo].[TblSTOKHAREKET] ([StokHareketId], [UrunId], [FirmaId], [HareketTipi], [Miktar], [BirimTuru], [BirimFiyat], [Tarih], [Saat], [Aciklama], [PersonelId]) VALUES (3, 1, 2, N'Stok Girişi', CAST(100.00 AS Decimal(10, 2)), N'Litre', CAST(20.00 AS Decimal(10, 2)), CAST(N'2025-10-21' AS Date), CAST(N'11:50:00' AS Time), N'Yeşilbahçe Hal''den alım', 2)
GO
INSERT [dbo].[TblSTOKHAREKET] ([StokHareketId], [UrunId], [FirmaId], [HareketTipi], [Miktar], [BirimTuru], [BirimFiyat], [Tarih], [Saat], [Aciklama], [PersonelId]) VALUES (4, 2, NULL, N'Satış', CAST(1.00 AS Decimal(10, 2)), N'Kilo', CAST(95.00 AS Decimal(10, 2)), CAST(N'2025-10-22' AS Date), CAST(N'15:14:00' AS Time), N'Masa 3 satışı', 1)
GO
INSERT [dbo].[TblSTOKHAREKET] ([StokHareketId], [UrunId], [FirmaId], [HareketTipi], [Miktar], [BirimTuru], [BirimFiyat], [Tarih], [Saat], [Aciklama], [PersonelId]) VALUES (5, 7, NULL, N'Satış', CAST(2.00 AS Decimal(10, 2)), N'Paket', CAST(25.00 AS Decimal(10, 2)), CAST(N'2025-10-22' AS Date), CAST(N'15:10:00' AS Time), N'Masa 3 satışı', 1)
GO
INSERT [dbo].[TblSTOKHAREKET] ([StokHareketId], [UrunId], [FirmaId], [HareketTipi], [Miktar], [BirimTuru], [BirimFiyat], [Tarih], [Saat], [Aciklama], [PersonelId]) VALUES (6, 1, NULL, N'Zayi', CAST(30.00 AS Decimal(10, 2)), N'Diğer', CAST(20.00 AS Decimal(10, 2)), CAST(N'2025-10-23' AS Date), CAST(N'15:10:00' AS Time), N'Çorba döküldü', 2)
GO
INSERT [dbo].[TblSTOKHAREKET] ([StokHareketId], [UrunId], [FirmaId], [HareketTipi], [Miktar], [BirimTuru], [BirimFiyat], [Tarih], [Saat], [Aciklama], [PersonelId]) VALUES (7, 7, NULL, NULL, CAST(1.00 AS Decimal(10, 2)), N'Paket', CAST(25.00 AS Decimal(10, 2)), CAST(N'2025-10-22' AS Date), CAST(N'15:10:00' AS Time), N'Masa 3 satışı', NULL)
GO
INSERT [dbo].[TblSTOKHAREKET] ([StokHareketId], [UrunId], [FirmaId], [HareketTipi], [Miktar], [BirimTuru], [BirimFiyat], [Tarih], [Saat], [Aciklama], [PersonelId]) VALUES (9, 7, NULL, NULL, CAST(2.00 AS Decimal(10, 2)), N'Litre', CAST(25.00 AS Decimal(10, 2)), CAST(N'2025-10-22' AS Date), CAST(N'15:10:00' AS Time), N'Masa 3 satışı', NULL)
GO
SET IDENTITY_INSERT [dbo].[TblSTOKHAREKET] OFF
GO
SET IDENTITY_INSERT [dbo].[TblURUN] ON 
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (1, N'Mercimek Çorbası', CAST(35.00 AS Decimal(10, 2)), 1, 2, N'Kırmızı mercimekten hazırlanmış klasik çorba', 50, N'Adet', 1, N'C:\Users\Yusuf\Desktop\1760373462952.png', CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (2, N'Adana Kebap', CAST(95.00 AS Decimal(10, 2)), 2, 1, N'Acılı kebap, közlenmiş biber ve pilav ile servis edilir', 30, N'Adet', 1, N'C:\Users\genca\OneDrive\Desktop\ProjePersonelYonetım\ProjePersonelYonetım\bin\Debug\PersonelResimleri\Yemek\images.jpg', CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (3, N'Tavuk Izgara', CAST(85.00 AS Decimal(10, 2)), 3, 1, N'Izgara tavuk göğsü, salata ve pilav ile', 40, N'Adet', 1, NULL, CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (4, N'Spagetti Napoliten', CAST(70.00 AS Decimal(10, 2)), 4, 2, N'Domates soslu klasik İtalyan makarna', 25, N'Adet', 1, NULL, CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (5, N'Çoban Salata', CAST(40.00 AS Decimal(10, 2)), 5, 2, N'Domates, salatalık, biber, soğan, zeytinyağı', 60, N'Adet', 1, N'C:\Users\Yusuf\Desktop\logo.png', CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (6, N'Sütlaç', CAST(30.00 AS Decimal(10, 2)), 6, 1, N'Fırınlanmış sütlü tatlı', 20, N'Adet', 1, NULL, CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (7, N'Kola 330ml', CAST(25.00 AS Decimal(10, 2)), 7, 3, N'Soğuk servis edilen gazlı içecek', 100, N'Adet', 1, NULL, CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (8, N'Serpme Kahvaltı', CAST(120.00 AS Decimal(10, 2)), 8, 1, N'Peynir, zeytin, yumurta, reçel, çay dahil', 15, N'Adet', 1, NULL, CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (9, N'Hamburger Menü', CAST(290.00 AS Decimal(10, 2)), 9, 1, N'Hamburger, patates kızartması ve içecek', 35, N'Adet', 1, N'C:\Users\genca\OneDrive\Desktop\ProjePersonelYonetım\ProjePersonelYonetım\bin\Debug\PersonelResimleri\hamburger.jpg', CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (10, N'Patates Kızartması', CAST(30.00 AS Decimal(10, 2)), 10, 2, N'Kızarmış patates, ketçap ve mayonez ile', 50, N'Adet', 0, NULL, CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (11, N'Et Menü', CAST(150.00 AS Decimal(10, 2)), 2, 1, N'Et yemeği, salata, pilav ve içecek', 20, N'Adet', 1, N'C:\Users\genca\OneDrive\Desktop\ProjePersonelYonetım\ProjePersonelYonetım\bin\Debug\PersonelResimleri\Yemek\images.jpg', CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (12, N'Sebzeli Güveç', CAST(80.00 AS Decimal(10, 2)), 12, 2, N'Vejetaryen güveç, fırında pişmiş sebzeler', 20, N'Adet', 1, N'C:\Users\genca\OneDrive\Desktop\ProjePersonelYonetım\ProjePersonelYonetım\bin\Debug\PersonelResimleri\Yemek\lokanta-usulu-mercimek-corbasi.jpg', CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (13, N'Mini Tavuk Menü', CAST(65.00 AS Decimal(10, 2)), 11, 1, N'Çocuklar için tavuk, pilav ve meyve suyu', 15, N'Adet', 1, NULL, CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (14, N'Karides Tava', CAST(130.00 AS Decimal(10, 2)), 13, 1, N'Tereyağında sotelenmiş karides', 10, N'Adet', 1, N'C:\Users\genca\OneDrive\Desktop\ProjePersonelYonetım\ProjePersonelYonetım\bin\Debug\PersonelResimleri\Yemek\lokanta-usulu-mercimek-corbasi.jpg', CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (15, N'Acı Sos', CAST(10.00 AS Decimal(10, 2)), 12, 2, N'Ev yapımı acı sos', 100, N'Adet', 1, N'C:\Users\Yusuf\Desktop\1760373462952.png', CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (16, N'Kaşar Ekstra', CAST(15.00 AS Decimal(10, 2)), 15, 1, N'Yemek üzerine ekstra kaşar peyniri', 80, N'Adet', 0, NULL, CAST(N'2025-10-29T23:03:12.570' AS DateTime))
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (21, N'qeqeqwe', CAST(15.00 AS Decimal(10, 2)), 14, 2, NULL, NULL, NULL, 0, N'C:\Users\genca\OneDrive\Desktop\ProjePersonelYonetım\ProjePersonelYonetım\bin\Debug\PersonelResimleri\Yemek\lokanta-usulu-mercimek-corbasi.jpg', NULL)
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (22, N'Ranc Sos', CAST(5.00 AS Decimal(10, 2)), 14, 2, NULL, NULL, NULL, 0, N'C:\Users\genca\OneDrive\Desktop\ProjePersonelYonetım\ProjePersonelYonetım\bin\Debug\PersonelResimleri\Yemek\images.jpg', NULL)
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (23, N'Deneme', CAST(25.00 AS Decimal(10, 2)), 15, 2, NULL, NULL, NULL, 0, N'C:\Users\Yusuf\Desktop\logo.png', NULL)
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (24, N'Acı Sos', CAST(10.00 AS Decimal(10, 2)), 12, 2, NULL, NULL, NULL, 1, N'C:\Users\Yusuf\Desktop\logo.png', NULL)
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (25, N'Deneme Urun', CAST(30.00 AS Decimal(10, 2)), 10, 1, NULL, NULL, NULL, 1, N'C:\Users\Yusuf\Desktop\1760373462952.png', NULL)
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (26, N'jkl', CAST(100.00 AS Decimal(10, 2)), 1, 1, NULL, NULL, NULL, 1, N'C:\Users\Yusuf\Desktop\logo.png', NULL)
GO
INSERT [dbo].[TblURUN] ([UrunId], [UrunAdi], [Fiyat], [KategoriId], [FirmaId], [Aciklama], [StokMiktari], [Birim], [Durum], [ResimYolu], [EklenmeTarihi]) VALUES (27, N'jkl', CAST(100.00 AS Decimal(10, 2)), 1, 1, NULL, NULL, NULL, 1, N'C:\Users\Yusuf\Desktop\1760373462952.png', NULL)
GO
SET IDENTITY_INSERT [dbo].[TblURUN] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TblPERSO__7E1935ED49E56D03]    Script Date: 6.03.2026 00:53:33 ******/
ALTER TABLE [dbo].[TblPERSONELLER] ADD UNIQUE NONCLUSTERED 
(
	[TCKimlikNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TblBORDROLAR] ADD  DEFAULT ((0)) FOR [Prim]
GO
ALTER TABLE [dbo].[TblBORDROLAR] ADD  DEFAULT ((0)) FOR [Kesinti]
GO
ALTER TABLE [dbo].[TblBORDROLAR] ADD  DEFAULT ((0)) FOR [Avans]
GO
ALTER TABLE [dbo].[TblFIRMA] ADD  CONSTRAINT [DF__TblFIRMA__Durumu__44FF419A]  DEFAULT ((1)) FOR [Durumu]
GO
ALTER TABLE [dbo].[TblGELIR] ADD  DEFAULT (getdate()) FOR [Tarih]
GO
ALTER TABLE [dbo].[TblGIDER] ADD  DEFAULT (getdate()) FOR [Tarih]
GO
ALTER TABLE [dbo].[TblMASA] ADD  CONSTRAINT [DF__TblMASA__Tutar__412EB0B6]  DEFAULT ((0)) FOR [Tutar]
GO
ALTER TABLE [dbo].[TblMASA] ADD  CONSTRAINT [DF__TblMASA__Durum__4222D4EF]  DEFAULT ((0)) FOR [Durum]
GO
ALTER TABLE [dbo].[TblMUSTERILER] ADD  CONSTRAINT [DF__TblMUSTER__Durum__3E52440B]  DEFAULT ((0)) FOR [Durum]
GO
ALTER TABLE [dbo].[TblPERSONELLER] ADD  DEFAULT ((0)) FOR [Durum]
GO
ALTER TABLE [dbo].[TblREZARVASYON] ADD  DEFAULT ((1)) FOR [Durum]
GO
ALTER TABLE [dbo].[TblSIPARIS] ADD  DEFAULT (getdate()) FOR [Tarih]
GO
ALTER TABLE [dbo].[TblSIPARIS] ADD  DEFAULT ((0)) FOR [ToplamTutar]
GO
ALTER TABLE [dbo].[TblSIPARIS] ADD  DEFAULT ((0)) FOR [OdemeDurumu]
GO
ALTER TABLE [dbo].[TblSTOKHAREKET] ADD  CONSTRAINT [DF__TblSTOKHA__Tarih__5812160E]  DEFAULT (getdate()) FOR [Tarih]
GO
ALTER TABLE [dbo].[TblURUN] ADD  DEFAULT ((0)) FOR [StokMiktari]
GO
ALTER TABLE [dbo].[TblURUN] ADD  DEFAULT ('Adet') FOR [Birim]
GO
ALTER TABLE [dbo].[TblURUN] ADD  DEFAULT ((1)) FOR [Durum]
GO
ALTER TABLE [dbo].[TblURUN] ADD  DEFAULT (getdate()) FOR [EklenmeTarihi]
GO
ALTER TABLE [dbo].[TblADISYON]  WITH CHECK ADD  CONSTRAINT [FK_Adisyon_Masa] FOREIGN KEY([MasaId])
REFERENCES [dbo].[TblMASA] ([MasaId])
GO
ALTER TABLE [dbo].[TblADISYON] CHECK CONSTRAINT [FK_Adisyon_Masa]
GO
ALTER TABLE [dbo].[TblADISYON_DETAY]  WITH CHECK ADD  CONSTRAINT [FK_Detay_Adisyon] FOREIGN KEY([AdisyonId])
REFERENCES [dbo].[TblADISYON] ([AdisyonId])
GO
ALTER TABLE [dbo].[TblADISYON_DETAY] CHECK CONSTRAINT [FK_Detay_Adisyon]
GO
ALTER TABLE [dbo].[TblADISYON_DETAY]  WITH CHECK ADD  CONSTRAINT [FK_Detay_Urun] FOREIGN KEY([UrunId])
REFERENCES [dbo].[TblURUN] ([UrunId])
GO
ALTER TABLE [dbo].[TblADISYON_DETAY] CHECK CONSTRAINT [FK_Detay_Urun]
GO
ALTER TABLE [dbo].[TblBORDROLAR]  WITH CHECK ADD FOREIGN KEY([PersonelID])
REFERENCES [dbo].[TblPERSONELLER] ([PersonelID])
GO
ALTER TABLE [dbo].[TblCEKSENET]  WITH CHECK ADD  CONSTRAINT [FK_CekSenet_Firma] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[TblFIRMA] ([FirmaId])
GO
ALTER TABLE [dbo].[TblCEKSENET] CHECK CONSTRAINT [FK_CekSenet_Firma]
GO
ALTER TABLE [dbo].[TblCEKSENET]  WITH CHECK ADD  CONSTRAINT [FK_CekSenet_Musteri] FOREIGN KEY([MusteriId])
REFERENCES [dbo].[TblMUSTERILER] ([MusteriId])
GO
ALTER TABLE [dbo].[TblCEKSENET] CHECK CONSTRAINT [FK_CekSenet_Musteri]
GO
ALTER TABLE [dbo].[TblCEKSENET]  WITH CHECK ADD  CONSTRAINT [FK_CekSenet_Personel] FOREIGN KEY([PersonelId])
REFERENCES [dbo].[TblPERSONELLER] ([PersonelID])
GO
ALTER TABLE [dbo].[TblCEKSENET] CHECK CONSTRAINT [FK_CekSenet_Personel]
GO
ALTER TABLE [dbo].[TblCEKSENET]  WITH CHECK ADD  CONSTRAINT [FK_CekSenet_Siparis] FOREIGN KEY([SatisNo])
REFERENCES [dbo].[TblSIPARIS] ([SiparisId])
GO
ALTER TABLE [dbo].[TblCEKSENET] CHECK CONSTRAINT [FK_CekSenet_Siparis]
GO
ALTER TABLE [dbo].[TblFIRMAHAREKET]  WITH CHECK ADD  CONSTRAINT [FK_TblFIRMAHAREKET_TblFIRMA] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[TblFIRMA] ([FirmaId])
GO
ALTER TABLE [dbo].[TblFIRMAHAREKET] CHECK CONSTRAINT [FK_TblFIRMAHAREKET_TblFIRMA]
GO
ALTER TABLE [dbo].[TblFIRMAHAREKET]  WITH CHECK ADD  CONSTRAINT [FK_TblFIRMAHAREKET_TblURUN] FOREIGN KEY([UrunId])
REFERENCES [dbo].[TblURUN] ([UrunId])
GO
ALTER TABLE [dbo].[TblFIRMAHAREKET] CHECK CONSTRAINT [FK_TblFIRMAHAREKET_TblURUN]
GO
ALTER TABLE [dbo].[TblFIRMAODEME]  WITH CHECK ADD  CONSTRAINT [FK_TblFIRMAODEME_TblFIRMA] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[TblFIRMA] ([FirmaId])
GO
ALTER TABLE [dbo].[TblFIRMAODEME] CHECK CONSTRAINT [FK_TblFIRMAODEME_TblFIRMA]
GO
ALTER TABLE [dbo].[TblGELIR]  WITH CHECK ADD  CONSTRAINT [FK_Gelir_Musteri] FOREIGN KEY([MusteriId])
REFERENCES [dbo].[TblMUSTERILER] ([MusteriId])
GO
ALTER TABLE [dbo].[TblGELIR] CHECK CONSTRAINT [FK_Gelir_Musteri]
GO
ALTER TABLE [dbo].[TblGELIR]  WITH CHECK ADD  CONSTRAINT [FK_Gelir_Personel] FOREIGN KEY([PersonelId])
REFERENCES [dbo].[TblPERSONELLER] ([PersonelID])
GO
ALTER TABLE [dbo].[TblGELIR] CHECK CONSTRAINT [FK_Gelir_Personel]
GO
ALTER TABLE [dbo].[TblGIDER]  WITH CHECK ADD  CONSTRAINT [FK_Gider_Firma] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[TblFIRMA] ([FirmaId])
GO
ALTER TABLE [dbo].[TblGIDER] CHECK CONSTRAINT [FK_Gider_Firma]
GO
ALTER TABLE [dbo].[TblGIDER]  WITH CHECK ADD  CONSTRAINT [FK_Gider_Personel] FOREIGN KEY([PersonelId])
REFERENCES [dbo].[TblPERSONELLER] ([PersonelID])
GO
ALTER TABLE [dbo].[TblGIDER] CHECK CONSTRAINT [FK_Gider_Personel]
GO
ALTER TABLE [dbo].[TblGUNLUKHARCAMA]  WITH CHECK ADD  CONSTRAINT [FK_GunlukHarcama_Firma] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[TblFIRMA] ([FirmaId])
GO
ALTER TABLE [dbo].[TblGUNLUKHARCAMA] CHECK CONSTRAINT [FK_GunlukHarcama_Firma]
GO
ALTER TABLE [dbo].[TblGUNLUKHARCAMA]  WITH CHECK ADD  CONSTRAINT [FK_GunlukHarcama_Personel] FOREIGN KEY([PersonelID])
REFERENCES [dbo].[TblPERSONELLER] ([PersonelID])
GO
ALTER TABLE [dbo].[TblGUNLUKHARCAMA] CHECK CONSTRAINT [FK_GunlukHarcama_Personel]
GO
ALTER TABLE [dbo].[TblMAAS]  WITH CHECK ADD  CONSTRAINT [FK_TblMaas_Personel] FOREIGN KEY([PersonelID])
REFERENCES [dbo].[TblPERSONELLER] ([PersonelID])
GO
ALTER TABLE [dbo].[TblMAAS] CHECK CONSTRAINT [FK_TblMaas_Personel]
GO
ALTER TABLE [dbo].[TblMODEME]  WITH CHECK ADD  CONSTRAINT [FK_TblMusteriOdeme_TblFIRMA] FOREIGN KEY([FmusteriID])
REFERENCES [dbo].[TblFIRMA] ([FirmaId])
GO
ALTER TABLE [dbo].[TblMODEME] CHECK CONSTRAINT [FK_TblMusteriOdeme_TblFIRMA]
GO
ALTER TABLE [dbo].[TblPERSONELODEME]  WITH CHECK ADD  CONSTRAINT [FK_Personeller_Odeme] FOREIGN KEY([PERSONEL])
REFERENCES [dbo].[TblPERSONELLER] ([PersonelID])
GO
ALTER TABLE [dbo].[TblPERSONELODEME] CHECK CONSTRAINT [FK_Personeller_Odeme]
GO
ALTER TABLE [dbo].[TblREZARVASYON]  WITH CHECK ADD  CONSTRAINT [FK_Rezervasyon_Masa] FOREIGN KEY([MasaNoId])
REFERENCES [dbo].[TblMASA] ([MasaId])
GO
ALTER TABLE [dbo].[TblREZARVASYON] CHECK CONSTRAINT [FK_Rezervasyon_Masa]
GO
ALTER TABLE [dbo].[TblREZARVASYON]  WITH CHECK ADD  CONSTRAINT [FK_TblRezarvasyon_Musteri] FOREIGN KEY([MusteriId])
REFERENCES [dbo].[TblMUSTERILER] ([MusteriId])
GO
ALTER TABLE [dbo].[TblREZARVASYON] CHECK CONSTRAINT [FK_TblRezarvasyon_Musteri]
GO
ALTER TABLE [dbo].[TblSIPARIS]  WITH CHECK ADD  CONSTRAINT [FK_Siparis_Masa] FOREIGN KEY([MasaId])
REFERENCES [dbo].[TblMASA] ([MasaId])
GO
ALTER TABLE [dbo].[TblSIPARIS] CHECK CONSTRAINT [FK_Siparis_Masa]
GO
ALTER TABLE [dbo].[TblSIPARIS]  WITH CHECK ADD  CONSTRAINT [FK_Siparis_Personel] FOREIGN KEY([PersonelId])
REFERENCES [dbo].[TblPERSONELLER] ([PersonelID])
GO
ALTER TABLE [dbo].[TblSIPARIS] CHECK CONSTRAINT [FK_Siparis_Personel]
GO
ALTER TABLE [dbo].[TblSIPARISDETAY]  WITH CHECK ADD  CONSTRAINT [FK_SiparisDetay_Siparis] FOREIGN KEY([SiparisId])
REFERENCES [dbo].[TblSIPARIS] ([SiparisId])
GO
ALTER TABLE [dbo].[TblSIPARISDETAY] CHECK CONSTRAINT [FK_SiparisDetay_Siparis]
GO
ALTER TABLE [dbo].[TblSIPARISDETAY]  WITH CHECK ADD  CONSTRAINT [FK_SiparisDetay_Urun] FOREIGN KEY([UrunId])
REFERENCES [dbo].[TblURUN] ([UrunId])
GO
ALTER TABLE [dbo].[TblSIPARISDETAY] CHECK CONSTRAINT [FK_SiparisDetay_Urun]
GO
ALTER TABLE [dbo].[TblSTOKHAREKET]  WITH CHECK ADD  CONSTRAINT [FK_StokHareket_Firma] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[TblFIRMA] ([FirmaId])
GO
ALTER TABLE [dbo].[TblSTOKHAREKET] CHECK CONSTRAINT [FK_StokHareket_Firma]
GO
ALTER TABLE [dbo].[TblSTOKHAREKET]  WITH CHECK ADD  CONSTRAINT [FK_StokHareket_Personel] FOREIGN KEY([PersonelId])
REFERENCES [dbo].[TblPERSONELLER] ([PersonelID])
GO
ALTER TABLE [dbo].[TblSTOKHAREKET] CHECK CONSTRAINT [FK_StokHareket_Personel]
GO
ALTER TABLE [dbo].[TblSTOKHAREKET]  WITH CHECK ADD  CONSTRAINT [FK_StokHareket_Urun] FOREIGN KEY([UrunId])
REFERENCES [dbo].[TblURUN] ([UrunId])
GO
ALTER TABLE [dbo].[TblSTOKHAREKET] CHECK CONSTRAINT [FK_StokHareket_Urun]
GO
ALTER TABLE [dbo].[TblURUN]  WITH CHECK ADD  CONSTRAINT [FK_TblUrun_Firma] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[TblFIRMA] ([FirmaId])
GO
ALTER TABLE [dbo].[TblURUN] CHECK CONSTRAINT [FK_TblUrun_Firma]
GO
ALTER TABLE [dbo].[TblURUN]  WITH CHECK ADD  CONSTRAINT [FK_UrunKategori] FOREIGN KEY([KategoriId])
REFERENCES [dbo].[TblKATEGORI] ([KategoriId])
GO
ALTER TABLE [dbo].[TblURUN] CHECK CONSTRAINT [FK_UrunKategori]
GO
ALTER TABLE [dbo].[TblPERSONELLER]  WITH CHECK ADD CHECK  (([TCKimlikNo] like '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0,2,4,6,8]'))
GO
ALTER TABLE [dbo].[TblREZARVASYON]  WITH CHECK ADD CHECK  (([KisiSayisi]>=(1) AND [KisiSayisi]<=(20)))
GO
ALTER TABLE [dbo].[TblSTOKHAREKET]  WITH CHECK ADD  CONSTRAINT [CK__TblSTOKHA__Harek__571DF1D5] CHECK  (([HareketTipi]='Zayi' OR [HareketTipi]='İade' OR [HareketTipi]='Satış' OR [HareketTipi]='Stok Girişi'))
GO
ALTER TABLE [dbo].[TblSTOKHAREKET] CHECK CONSTRAINT [CK__TblSTOKHA__Harek__571DF1D5]
GO
