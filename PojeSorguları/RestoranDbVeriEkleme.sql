USE RESTORANDB
GO

--############################## -KULLANICININ VERDÝÐÝ DATALAR (DÜZELTÝLMÝÞ HALÝ)- #############################################--

INSERT INTO TblFIRMA(FirmaAdi,Telefon,Email,Adres)
VALUES 
('Anadolu Et ve Süt Ürünleri A.Þ.','0212 555 1010','anadoluetsut@hotmail.com','Ýstanbul, Büyükçekmece'),
('Yeþilbahçe Sebze Meyve Hali','0532 123 4567','yesilbahcehal@hotmail.com','Ýstanbul, Büyükçekmece'),
('Toptan Ýçecek Daðýtým Merkezi','0850 444 2000','tptnicecekdagitim@hotmail.com','Ankara, Sincan'),
('Temizlik ve Hijyen Çözümleri Ltd.','0216 777 3030','temizlikhijyencozum@hotmail.com','Kocaeli, Gebze'),
('Mutfak Ekipmanlarý A.Þ.', '0212 999 8877', 'info@mutfakekipman.com', 'Ýstanbul, Pendik');
GO

INSERT INTO TblPERSONELLER(Ad,Soyad,TCKimlikNo,Telefon,Adres,Email,Pozisyon,Tarih,Durum,Resim)
VALUES
('Mustafa','Altýnkaynak','50222435426','05346548474','Ýstanbul','altinkaynak@hotmail.com','Garson','2023-10-10',1,'2.jpg'),
('Buðrahan','Yýlmaz','50226445440','05356142460','Ýstanbul','yilmazbugrahan@gmail.com','Aþçý','2022-10-10',1,'3.jpg'),
('Hüseyin','Yüce','50126445426','05356152160','Ýstanbul','yucehuseyin@hotmail.com','Muhasebe','2021-08-10',1,'4.jpg'),
('Adahan','Akdeniz','50128645446','05356161260','Ýstanbul','akdenizadahan@hotmail.com','Garson','2020-06-10',0,'5.jpg'),
('Elmas','Mutlu','50162498740','05357406074','Ýstanbul','elmasmutlu@gmail.com','Temizlikçi','2024-10-10',1,'6.jpg'),
('Kadir','Kara','50678945612','05347406174','Ýstanbul','karakadir@gmail.com','Temizlikçi','2024-10-10',0,'7.jpg');
GO

INSERT INTO TblMUSTERILER(Ad,Soyad,MasaId,Tarih,Saat,Aciklama,Durum)
VALUES 
('Merve', 'Kaya', 3, '2025-10-22', '18:00:00', 'Ýþ toplantýsý', 1),
('Burak', 'Demir', 7, '2025-10-23', '20:15:00', 'Yemek daveti', 1),
('Elif', 'Turan', 2, '2025-10-24', '17:45:00', 'Çocuklarla akþam yemeði', 0),
('Can', 'Öztürk', 6, '2025-10-25', '19:00:00', 'Yýldönümü kutlamasý', 1),
('Zeynep', 'Aslan', 4, '2025-10-26', '21:00:00', 'Arkadaþ buluþmasý', 1),
('Emre', 'Yýldýz', 1, '2025-10-27', '18:30:00', 'Ýþ yemeði', 0),
('Gamze', 'Çelik', 8, '2025-10-28', '20:00:00', 'Romantik akþam', 1),
('Tolga', 'Aksoy', 5, '2025-10-29', '19:30:00', 'Kutlama yemeði', 1),
('Selin', 'Erdoðan', 9, '2025-10-30', '17:00:00', 'Erken akþam yemeði', 0),
('Kerem', 'Güneþ', 10, '2025-10-31', '22:00:00', 'Gece kahvesi', 1);
GO

INSERT INTO TblKATEGORI(KategoriAdi)
VALUES
('Çorbalar'), ('Ana Yemekler'), ('Izgaralar'), ('Makarnalar'), ('Salatalar'),
('Tatlýlar'), ('Ýçecekler'), ('Kahvaltýlýklar'), ('Fast Food'), ('Yan Ürünler'),
('Çocuk Menüleri'), ('Vejetaryen'), ('Deniz Ürünleri'), ('Soslar'), ('Ekstralar');
GO

-- KULLANICININ INSERT'ÜNDEKÝ EKSÝK 'FirmaId' SÜTUNU EKLENDÝ (NOT NULL OLDUÐU ÝÇÝN)
INSERT INTO TblURUN(UrunAdi, Fiyat, KategoriId, FirmaId, Aciklama, StokMiktari, Birim, Durum)
VALUES
('Mercimek Çorbasý', 35.00, 1, 2, 'Kýrmýzý mercimekten hazýrlanmýþ klasik çorba', 50, 'Adet', 1),
('Adana Kebap', 95.00, 2, 1, 'Acýlý kebap, közlenmiþ biber ve pilav ile servis edilir', 30, 'Adet', 1),
('Tavuk Izgara', 85.00, 3, 1, 'Izgara tavuk göðsü, salata ve pilav ile', 40, 'Adet', 1),
('Spagetti Napoliten', 70.00, 4, 2, 'Domates soslu klasik Ýtalyan makarna', 25, 'Adet', 1),
('Çoban Salata', 40.00, 5, 2, 'Domates, salatalýk, biber, soðan, zeytinyaðý', 60, 'Adet', 1),
('Sütlaç', 30.00, 6, 1, 'Fýrýnlanmýþ sütlü tatlý', 20, 'Adet', 1),
('Kola 330ml', 25.00, 7, 3, 'Soðuk servis edilen gazlý içecek', 100, 'Adet', 1),
('Serpme Kahvaltý', 120.00, 8, 1, 'Peynir, zeytin, yumurta, reçel, çay dahil', 15, 'Adet', 1),
('Hamburger Menü', 90.00, 9, 1, 'Hamburger, patates kýzartmasý ve içecek', 35, 'Adet', 1),
('Patates Kýzartmasý', 30.00, 10, 2, 'Kýzarmýþ patates, ketçap ve mayonez ile', 50, 'Adet', 1),
('Et Menü', 150.00, 2, 1, 'Et yemeði, salata, pilav ve içecek', 20, 'Adet', 1),
('Sebzeli Güveç', 80.00, 12, 2, 'Vejetaryen güveç, fýrýnda piþmiþ sebzeler', 20, 'Adet', 1),
('Mini Tavuk Menü', 65.00, 11, 1, 'Çocuklar için tavuk, pilav ve meyve suyu', 15, 'Adet', 1),
('Karides Tava', 130.00, 13, 1, 'Tereyaðýnda sotelenmiþ karides', 10, 'Adet', 1),
('Acý Sos', 10.00, 14, 2, 'Ev yapýmý acý sos', 100, 'Adet', 1),
('Kaþar Ekstra', 15.00, 15, 1, 'Yemek üzerine ekstra kaþar peyniri', 80, 'Adet', 1);
GO

INSERT INTO TblMASA(MasaNo,Aciklama,Tutar,Durum)
VALUES
(1, 'Pencere Kenarý, 2 kiþilik', 0.00, 0),
(2, 'Salon Ortasý, 4 kiþilik', 0.00, 0),
(3, 'Bahçe Tarafý, 6 kiþilik', 0.00, 0),
(4, 'Teras, 8 kiþilik', 0.00, 0),
(5, 'Teras, 2 kiþilik', 0.00, 0),
(6, 'Salon Köþesi, 2 kiþilik', 0.00, 0),
(7, 'Bahçe Kenarý, 4 kiþilik', 0.00, 0),
(8, 'Salon Giriþi, 2 kiþilik', 0.00, 0),
(9, 'Cam kenarý, 6 kiþilik', 0.00, 0),
(10, 'Teras, 4 kiþilik', 0.00, 0);
GO

INSERT INTO TblREZARVASYON(MusteriId, MasaNoId, KisiSayisi, Tarih, Saat, Aciklama, Durum)
VALUES
(2, 2, '4','2025-10-22','20:00','Ýþ toplantýsý', 1),
(3, 3, '3','2025-10-23','18:30','Aile yemeði', 1),
(4, 4, '5','2025-10-23','19:00','Doðum günü kutlamasý', 1),
(5, 5, '2','2025-10-24','20:00','Romantik akþam', 0),
(6, 6, '6','2025-10-24','19:30','Ýþ arkadaþlarýyla buluþma', 1),
(7, 7, '3', '2025-10-25', '18:00', 'Ön rezervasyon', 1);
GO

--############################## -YENÝ EKLENEN DATALAR- #############################################--

INSERT INTO TblGUNLUKHARCAMA (HarcananYer, Tarih, Saat, Aciklama, Tutar, PersonelID, FirmaId) VALUES
('Pazar Alýþveriþi', '2025-10-25', '09:30:00', 'Salý pazarý taze sebze', 750.00, 2, NULL),
('Kýrtasiye', '2025-10-26', '14:15:00', 'Ofis için adisyon fiþleri', 400.00, 3, NULL),
('Taksi', '2025-10-27', '11:00:00', 'Banka iþlemi için gidiþ-dönüþ', 250.00, 3, NULL),
('Temizlik Malzemesi', '2025-10-28', '10:00:00', 'Eksik deterjanlar', 600.00, 5, 4),
('Personel Avansý', '2025-10-28', '23:00:00', 'Garson avansý', 1000.00, 1, NULL);
GO

INSERT INTO TblCEKSENET (SatisNo, MusteriId, Tutar, OdemeTuru, PersonelId, Tarih, Durum) VALUES
(1, 1, 5000.00, 'Çek', 3, '2025-11-15', 0),
(2, 2, 2500.00, 'Senet', 3, '2025-11-20', 0),
(3, 4, 10000.00, 'Çek', 3, '2025-12-01', 0),
(4, 7, 3000.00, 'Senet', 3, '2025-11-10', 1), -- Ödendi
(5, 8, 7500.00, 'Çek', 3, '2025-11-25', 0);
GO

INSERT INTO TblSATIS (UrunId) VALUES 
(1), -- Mercimek Çorbasý
(2), -- Adana Kebap
(7), -- Kola
(7), -- Kola
(5); -- Çoban Salata
GO

INSERT INTO TblSTOKHAREKET (UrunId, FirmaId, HareketTipi, Miktar, BirimFiyat, Tarih, Aciklama, PersonelId) VALUES
(2, 1, 'Stok Giriþi', 50, 70.00, '2025-10-20', 'Anadolu Et''ten mal alýmý', 2),
(7, 3, 'Stok Giriþi', 200, 15.00, '2025-10-21', 'Toptan Ýçecek''ten alým', 3),
(1, 2, 'Stok Giriþi', 100, 20.00, '2025-10-21', 'Yeþilbahçe Hal''den alým', 2),
(2, NULL, 'Satýþ', 1, 95.00, '2025-10-22', 'Masa 3 satýþý', 1),
(7, NULL, 'Satýþ', 2, 25.00, '2025-10-22', 'Masa 3 satýþý', 1),
(1, NULL, 'Zayi', 5, 20.00, '2025-10-23', 'Çorba döküldü', 2);
GO

-- Sipariþ 1 (Masa 3, Garson 1 - Mustafa)
INSERT INTO TblSIPARIS (MasaId, PersonelId, Tarih, ToplamTutar, OdemeDurumu) VALUES (3, 1, '2025-10-22 18:05:00', 0, 1); -- SiparisId = 1
INSERT INTO TblSIPARISDETAY (SiparisId, UrunId, Miktar, BirimFiyat) VALUES
(1, 1, 2, 35.00),  -- 2x Mercimek (UrunId 1)
(1, 2, 1, 95.00),  -- 1x Adana (UrunId 2)
(1, 5, 1, 40.00),  -- 1x Çoban Salata (UrunId 5)
(1, 7, 3, 25.00);  -- 3x Kola (UrunId 7)

-- Sipariþ 2 (Masa 7, Garson 1 - Mustafa, PersonelID 4 (Adahan) Durum=0)
INSERT INTO TblSIPARIS (MasaId, PersonelId, Tarih, ToplamTutar, OdemeDurumu) VALUES (7, 1, '2025-10-23 20:20:00', 0, 1); -- SiparisId = 2
INSERT INTO TblSIPARISDETAY (SiparisId, UrunId, Miktar, BirimFiyat) VALUES
(2, 3, 2, 85.00),  -- 2x Tavuk Izgara (UrunId 3)
(2, 6, 2, 30.00),  -- 2x Sütlaç (UrunId 6)
(2, 7, 2, 25.00);  -- 2x Kola (UrunId 7)

-- Sipariþ 3 (Masa 2, Garson 1 - Mustafa)
INSERT INTO TblSIPARIS (MasaId, PersonelId, Tarih, ToplamTutar, OdemeDurumu) VALUES (2, 1, '2025-10-24 17:50:00', 0, 1); -- SiparisId = 3
INSERT INTO TblSIPARISDETAY (SiparisId, UrunId, Miktar, BirimFiyat) VALUES
(3, 9, 1, 90.00),  -- 1x Hamburger Menü (UrunId 9)
(3, 13, 1, 65.00); -- 1x Mini Tavuk Menü (UrunId 13)

-- Sipariþ 4 (Masa 6, Garson 1 - Mustafa)
INSERT INTO TblSIPARIS (MasaId, PersonelId, Tarih, ToplamTutar, OdemeDurumu) VALUES (6, 1, '2025-10-25 19:05:00', 0, 0); -- SiparisId = 4
INSERT INTO TblSIPARISDETAY (SiparisId, UrunId, Miktar, BirimFiyat) VALUES
(4, 14, 1, 130.00), -- 1x Karides Tava (UrunId 14)
(4, 4, 1, 70.00);   -- 1x Spagetti (UrunId 4)

-- Sipariþ 5 (Masa 4, Garson 1 - Mustafa)
INSERT INTO TblSIPARIS (MasaId, PersonelId, Tarih, ToplamTutar, OdemeDurumu) VALUES (4, 1, '2025-10-26 21:02:00', 0, 1); -- SiparisId = 5
INSERT INTO TblSIPARISDETAY (SiparisId, UrunId, Miktar, BirimFiyat) VALUES
(5, 11, 2, 150.00), -- 2x Et Menü (UrunId 11)
(5, 12, 1, 80.00),  -- 1x Sebzeli Güveç (UrunId 12)
(5, 7, 4, 25.00);   -- 4x Kola (UrunId 7)
GO

-- TblMAAS (Sözleþme/Net Maaþ Tanýmlarý)
INSERT INTO TblMAAS (PersonelID, NetTutar, BaslangicTarihi) VALUES
(1, 18000.00, '2025-01-01'), -- Mustafa (Garson)
(2, 28000.00, '2025-01-01'), -- Buðrahan (Aþçý)
(3, 22000.00, '2025-01-01'), -- Hüseyin (Muhasebe)
(5, 17000.00, '2025-01-01'), -- Elmas (Temizlikçi)
(1, 20000.00, '2025-07-01'); -- Mustafa'ya zam
GO

-- TblBORDROLAR (Aylýk Ödeme Fiþleri)
INSERT INTO TblBORDROLAR (PersonelID, Ay, Yil, AnaMaas, Prim, Kesinti, Avans, OdemeTarihi) VALUES
(1, 10, 2025, 20000.00, 500.00, 0.00, 1000.00, '2025-10-31'), -- Mustafa (Zamlý maaþ)
(2, 10, 2025, 28000.00, 1000.00, 250.00, 2000.00, '2025-10-31'), -- Buðrahan
(3, 10, 2025, 22000.00, 0.00, 0.00, 1500.00, '2025-10-31'), -- Hüseyin
(5, 10, 2025, 17000.00, 0.00, 0.00, 500.00, '2025-10-31'), -- Elmas
(1, 9, 2025, 20000.00, 0.00, 0.00, 0.00, '2025-09-30'); -- Mustafa (Önceki ay)
GO


-- MUHASEBE TABLOLARI (GELÝR VE GÝDER)
-- GÝDERLER (Stok alýmlarý, günlük harcamalar ve maaþ ödemeleri)
INSERT INTO TblGIDER (GiderTuru, Tutar, Tarih, Aciklama, PersonelId, FirmaId, ReferansTablo, ReferansId) VALUES
('Stok Alýmý', 3500.00, '2025-10-20', 'Anadolu Et''ten mal alýmý (50x70)', 2, 1, 'TblSTOKHAREKET', 1),
('Stok Alýmý', 3000.00, '2025-10-21', 'Toptan Ýçecek''ten alým (200x15)', 3, 3, 'TblSTOKHAREKET', 2),
('Stok Alýmý', 2000.00, '2025-10-21', 'Yeþilbahçe Hal''den alým (100x20)', 2, 2, 'TblSTOKHAREKET', 3),
('Günlük Harcama', 750.00, '2025-10-25', 'Pazar Alýþveriþi', 2, NULL, 'TblGUNLUKHARCAMA', 1),
('Günlük Harcama', 400.00, '2025-10-26', 'Ofis için adisyon fiþleri', 3, NULL, 'TblGUNLUKHARCAMA', 2),
-- Bordrolardan Toplam Ödemeyi Gider Olarak Ekleme (AnaMaas + Prim - Kesinti - Avans)
('Maaþ Ödemesi', 19500.00, '2025-10-31', 'Ekim 2025 Maaþ - Mustafa A.', 3, NULL, 'TblBORDROLAR', 1),
('Maaþ Ödemesi', 26750.00, '2025-10-31', 'Ekim 2025 Maaþ - Buðrahan Y.', 3, NULL, 'TblBORDROLAR', 2),
('Maaþ Ödemesi', 20500.00, '2025-10-31', 'Ekim 2025 Maaþ - Hüseyin Y.', 3, NULL, 'TblBORDROLAR', 3);
GO

-- GELÝRLER (Ödenen sipariþler ve tahsil edilen çek/senetler)
INSERT INTO TblGELIR (GelirTuru, Tutar, Tarih, Aciklama, PersonelId, MusteriId, ReferansTablo, ReferansId) VALUES
('Satýþ (Nakit)', 280.00, '2025-10-22', 'Sipariþ 1 Ödemesi (Masa 3)', 1, 1, 'TblSIPARIS', 1), -- Tutar: (2*35)+(1*95)+(1*40)+(3*25) = 280
('Satýþ (Kredi Kartý)', 280.00, '2025-10-23', 'Sipariþ 2 Ödemesi (Masa 7)', 1, 2, 'TblSIPARIS', 2), -- Tutar: (2*85)+(2*30)+(2*25) = 280
('Satýþ (Nakit)', 155.00, '2025-10-24', 'Sipariþ 3 Ödemesi (Masa 2)', 1, 3, 'TblSIPARIS', 3), -- Tutar: (1*90)+(1*65) = 155
('Satýþ (Kredi Kartý)', 480.00, '2025-10-26', 'Sipariþ 5 Ödemesi (Masa 4)', 1, 5, 'TblSIPARIS', 5), -- Tutar: (2*150)+(1*80)+(4*25) = 480
('Senet Tahsilatý', 3000.00, '2025-11-10', 'Gamze Çelik senet ödemesi', 3, 7, 'TblCEKSENET', 4);
GO