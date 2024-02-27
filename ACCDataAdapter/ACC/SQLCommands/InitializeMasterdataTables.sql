INSERT INTO [dbo].[Kostenstellenarten] ([Kostenstellenartbezeichnung])
VALUES
	('Angestellte Mitarbeiter/innen'),
	('Erlöse'),
	('Freie Mitarbeiter/innen'),
	('Sonstige Kostenstellen');

INSERT INTO [dbo].[Taetigkeiten] ([Taetigkeitsbeschreibung])
VALUES
	('Geschäftsführerin / -Führer'),
	('Werkstudent'),
	('Beraterin / Berater'),
	('Bürokauffrau / -Mann');

INSERT INTO [dbo].[Abrechnungseinheiten] ([AbrechnungseinheitName])
VALUES
	('Stunden'),
	('Personentage'),
	('Stück');

INSERT INTO [dbo].[Geschaeftsjahre] ([Name], [DatumVon], [DatumBis], [Rechnungsprefix])
VALUES
	('2019', '2019-01-01' , '2019-12-31', '2019'),
	('2020', '2020-01-01' , '2020-12-31', '2020'),
	('2021', '2021-01-01' , '2021-06-30', '2021'),
	('2021/2022', '2021-07-01' , '2022-06-30', '2021'),
	('2022/2023', '2022-07-01' , '2023-06-30', '2022');

INSERT INTO [dbo].[Waehrungen] ([WaehrungISO], [WaehrungName], [WaehrungZeichen])
VALUES
	('EUR', 'Euro', '€'),
	('USD', 'US Dollar', '$'),
	('CHF', 'Schweizer Franken', 'Fr'),
	('GBP', 'Britisches Pfund', '£')