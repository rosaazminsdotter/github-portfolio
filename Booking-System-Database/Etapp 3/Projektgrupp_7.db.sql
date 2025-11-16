BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Hotell" (
	"HotellID"	INTEGER,
	"HotellNamn"	TEXT,
	"Omdöme"	NUMERIC,
	"Adress"	TEXT,
	"Stad"	TEXT,
	"Postnummer"	INTEGER,
	PRIMARY KEY("HotellID")
);
CREATE TABLE IF NOT EXISTS "Anställd" (
	"AnställdID"	INTEGER,
	"HotellID"	INTEGER,
	"RollID"	INTEGER,
	"Namn"	TEXT,
	"Email"	TEXT,
	"Telefonnummer"	TEXT,
	"Personnummer"	TEXT,
	"AntalTimmar"	INTEGER,
	"Lön"	INTEGER,
	FOREIGN KEY("HotellID") REFERENCES "Hotell",
	FOREIGN KEY("RollID") REFERENCES "Roll",
	PRIMARY KEY("AnställdID")
);
CREATE TABLE IF NOT EXISTS "Roll" (
	"RollID"	INTEGER,
	"RollTyp"	TEXT,
	PRIMARY KEY("RollID")
);
CREATE TABLE IF NOT EXISTS "RumsTyp" (
	"RumsTypID"	INTEGER,
	"RumsTyp"	TEXT,
	"Beskrivning"	TEXT,
	PRIMARY KEY("RumsTypID")
);
CREATE TABLE IF NOT EXISTS "Rum" (
	"RumID"	INTEGER,
	"HotellID"	INTEGER,
	"RumsTypID"	INTEGER,
	"Rumsnummer"	INTEGER,
	"Tillgänglighet"	TEXT,
	"Pris"	INTEGER,
	FOREIGN KEY("HotellID") REFERENCES "Hotell",
	FOREIGN KEY("RumsTypID") REFERENCES "RumsTyp",
	PRIMARY KEY("RumID")
);
CREATE TABLE IF NOT EXISTS "Gäst" (
	"GästID"	INTEGER,
	"Namn"	TEXT,
	"Email"	TEXT,
	"Telefonnummer"	TEXT,
	PRIMARY KEY("GästID")
);
CREATE TABLE IF NOT EXISTS "GästBokning" (
	"BokningsID"	INTEGER,
	"GästID"	INTEGER,
	"Rabattkod"	INTEGER,
	"Bokningsmetod"	TEXT,
	"Incheckningsdatum"	TEXT,
	"Utcheckningsdatum"	TEXT,
	"AntalGäster"	INTEGER,
	"AntalBarn"	INTEGER,
	FOREIGN KEY("GästID") REFERENCES "Gäst",
	PRIMARY KEY("BokningsID")
);
CREATE TABLE IF NOT EXISTS "FöretagsBokning" (
	"FöretagsbokningID"	INTEGER,
	"AnställdID"	INTEGER,
	"FöretagsNamn"	TEXT,
	"ÖnskatDatum"	TEXT,
	"EventBeskrivning"	TEXT,
	FOREIGN KEY("AnställdID") REFERENCES "Anställd",
	PRIMARY KEY("FöretagsbokningID")
);
CREATE TABLE IF NOT EXISTS "Faktura" (
	"OCR-nummer"	INTEGER,
	"BokningsID"	INTEGER,
	"FaktureringsDatum"	TEXT,
	"BetalningsDatum"	TEXT,
	"TotalBelopp"	INTEGER,
	"Betalningsmetod"	TEXT,
	FOREIGN KEY("BokningsID") REFERENCES "GästBokning",
	PRIMARY KEY("OCR-nummer")
);
CREATE TABLE IF NOT EXISTS "GästBokning_Rum" (
	"BokningsID"	INTEGER,
	"RumID"	INTEGER,
	"TotalRumKostnad"	INTEGER,
	FOREIGN KEY("BokningsID") REFERENCES "GästBokning",
	FOREIGN KEY("RumID") REFERENCES "Rum",
	PRIMARY KEY("BokningsID","RumID")
);
CREATE TABLE IF NOT EXISTS "Service" (
	"ServiceID"	INTEGER,
	"ServiceTyp"	TEXT,
	PRIMARY KEY("ServiceID")
);
CREATE TABLE IF NOT EXISTS "GästBokning_Service" (
	"BokningsID"	INTEGER,
	"ServiceID"	INTEGER,
	"PrisSpenderat"	INTEGER,
	FOREIGN KEY("BokningsID") REFERENCES "GästBokning",
	FOREIGN KEY("ServiceID") REFERENCES "Service",
	PRIMARY KEY("BokningsID","ServiceID")
);
INSERT INTO "Hotell" ("HotellID","HotellNamn","Omdöme","Adress","Stad","Postnummer") VALUES (1001,'Grand Hotel',7,'Skolgatan 45','Uppsala',75332),
 (1002,'Stockholm Hotell',8,'Vasaplan 4','Stockholm',11120),
 (1003,'Hotell Centralen',2,'Stationsgatan 4','Uppsala',75340),
 (1004,'Luxe Grand Hotel',9,'Fyrislundsgatan 81','Uppsala',75340),
 (1005,'Radison Blue',5,'Ferkens Gränd 2','Stockholm',11130);
INSERT INTO "Anställd" ("AnställdID","HotellID","RollID","Namn","Email","Telefonnummer","Personnummer","AntalTimmar","Lön") VALUES (3001,1001,2001,'Siiri Hansa','Siiri@Hotel.com','0731298643','930202-9039',158,39000),
 (3002,1002,2002,'Nikola Enis','Nikola@Hotel.com','0726417207','601007-7151',160,42000),
 (3003,1003,2003,'Wina Bardulf','Wina@Hotel.com','0738761809','690417-7059',164,49000),
 (3004,1004,2004,'Ingmar Tatiana','Ingmar@Hotel.com','0716702357','850824-1554',142,32000),
 (3005,1005,2005,'Enok Johannes','Enok@Hotel.com','0739836721','620930-4283',151,38000),
 (3006,1002,2005,'Rosa Hans','Rosa01@Hotel.com','0725967229','760725-9632',161,40000),
 (3007,1002,2001,'Rosa Hans','Rosa02@Hotel.com','0736892345','031210-5447',157,38000);
INSERT INTO "Roll" ("RollID","RollTyp") VALUES (2001,'Receptionist'),
 (2002,'Mötesrådgivare'),
 (2003,'Städare'),
 (2004,'Kock'),
 (2005,'Bartender');
INSERT INTO "RumsTyp" ("RumsTypID","RumsTyp","Beskrivning") VALUES (8001,'Enkelrum','En enkelsäng, litet badrum'),
 (8002,'Dubbelrum','En dubbelsäng, litet badrum.'),
 (8003,'Suit','Tvåvåningsrum, 3 sovrum med 6 sovplatser, ett större badrum'),
 (8004,'Trebäddsrum','En dubbelsäng och en extrasäng, litet badrum'),
 (8005,'Familjerum','En dubbelsäng och två enkelsängar');
INSERT INTO "Rum" ("RumID","HotellID","RumsTypID","Rumsnummer","Tillgänglighet","Pris") VALUES (9001,1001,8001,101,'Inte Tillgängligt',5000),
 (9002,1001,8002,201,'Tillgängligt',7000),
 (9003,1002,8005,102,'Tillgängligt',8500),
 (9004,1002,8004,101,'Tillgängligt',8000),
 (9005,1003,8005,205,'Tillgängligt',8500),
 (9006,1004,8003,302,'Inte Tillgängligt',14500),
 (9007,1005,8002,201,'Inte Tillgängligt',7000),
 (9008,1003,8002,303,'Inte Tillgängligt',7000);
INSERT INTO "Gäst" ("GästID","Namn","Email","Telefonnummer") VALUES (4001,'Adam Abrahamsson','Adam.A@gmail.com','0716464897'),
 (4002,'Göran Lagerfelt','Göran.Fält@hotmail.com','0769826382'),
 (4003,'Gorm Hägg','Gorm.Hägg@outlook.com','0726302749'),
 (4004,'Jacob Bergkvist','Jacobbbb@gmail.com','0719272937'),
 (4005,'Alina Nilsson','Alina@live.com','0735566072'),
 (4006,'Melker Rosenkvist','Rosen@gmail.com','0712233996'),
 (4007,'Melker Rosenkvist','Melker@live.com','0748263828');
INSERT INTO "GästBokning" ("BokningsID","GästID","Rabattkod","Bokningsmetod","Incheckningsdatum","Utcheckningsdatum","AntalGäster","AntalBarn") VALUES (5001,4001,'HOTELL20','Receptionist','2023-12-15','2023-12-20',1,NULL),
 (5002,4002,NULL,'Webbplats','2023-12-31','2024-01-05',2,1),
 (5003,4003,NULL,'Receptionist','2024-01-01','2024-01-05',3,2),
 (5004,4004,NULL,'Webbplats','2024-01-05','2024-01-10',5,3),
 (5005,4006,'Grand25','Receptionist','2024-01-05','2024-01-11',2,NULL),
 (5006,4005,NULL,'Webbplats','2024-01-05','2024-01-12',2,NULL),
 (5007,4007,NULL,'Webbplats','2024-01-05','2024-01-11',6,NULL);
INSERT INTO "FöretagsBokning" ("FöretagsbokningID","AnställdID","FöretagsNamn","ÖnskatDatum","EventBeskrivning") VALUES (6001,3002,'Systembolaget','2023-12-31','Nyårs firande'),
 (6002,NULL,'H&M','2024-01-07','Födelsedag'),
 (6003,3002,'Zara','2024-01-07','Möte'),
 (6004,NULL,'Clas Ohlsson','2024-01-20','Möte'),
 (6005,3002,'Normal','2024-01-22','Konferans för året');
INSERT INTO "Faktura" ("OCR-nummer","BokningsID","FaktureringsDatum","BetalningsDatum","TotalBelopp","Betalningsmetod") VALUES (71538452,5002,'2024-01-05','2024-01-19',42180,'Reception'),
 (79273625,5001,'2023-12-20','2024-01-03',20080,'Reception'),
 (79287362,5003,'2024-01-05','2024-01-19',32750,'Faktura'),
 (706278393,5004,'2024-01-10','2024-01-24',65000,'Faktura'),
 (781029462,5005,'2024-01-11','2024-01-25',31500,'Faktura'),
 (782638263,5006,'2024-01-12','2024-01-26',49700,'Reception'),
 (799825378,5007,'2024-01-11','2024-01-27',87300,'Faktura');
INSERT INTO "GästBokning_Rum" ("BokningsID","RumID","TotalRumKostnad") VALUES (5001,9001,25000),
 (5002,9002,42000),
 (5003,9004,32000),
 (5004,9001,25000),
 (5005,9007,42000),
 (5006,9008,49000),
 (5007,9006,87000),
 (5004,9004,40000);
INSERT INTO "Service" ("ServiceID","ServiceTyp") VALUES (2101,'Roomservice'),
 (2102,'Frukost'),
 (2103,'Mini-bar'),
 (2104,'Massage'),
 (2105,'Minigolf');
INSERT INTO "GästBokning_Service" ("BokningsID","ServiceID","PrisSpenderat") VALUES (5003,2101,450),
 (5006,2102,700),
 (5001,2103,80),
 (5007,2104,300),
 (5002,2105,180),
 (5003,2104,300);
COMMIT;
