using DoePaAdminDataModel.Kostenrechnung;
using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel.Services
{
    internal static class DoePaAdminTestDataCreator
    {

        public static async Task CreateCompleteTestDataAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            await CreateMasterdataAsync(doePaAdminService, cancellationToken);
            
            await CreateKostenstellenAsync(doePaAdminService, cancellationToken);
                                                
            await CreateMitarbeiterAsync(doePaAdminService, cancellationToken);

            await CreateAuftraegeAsync(doePaAdminService, cancellationToken);

            await CreateAusgangsrechnungenAsync(doePaAdminService, cancellationToken);
                        
        }


        public static async Task CreateMasterdataAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            _ = await doePaAdminService.CreateTaetigkeitAsync("Game Designer", cancellationToken);
            _ = await doePaAdminService.CreateTaetigkeitAsync("Technical Director", cancellationToken);
            _ = await doePaAdminService.CreateTaetigkeitAsync("Artist", cancellationToken);
                        
            _ = await doePaAdminService.CreateKostenstellenartAsync("Angestellte Mitarbeiter/innen", cancellationToken);
            _ = await doePaAdminService.CreateKostenstellenartAsync("Geschäftsräume", cancellationToken);
            _ = await doePaAdminService.CreateKostenstellenartAsync("Freie Mitarbeiter/innen", cancellationToken);
            _ = await doePaAdminService.CreateKostenstellenartAsync("Sonstige Kostenstellen", cancellationToken);
            
            _ = await doePaAdminService.CreateAbrechnungseinheitAsync("Stunden", "h", cancellationToken);
            _ = await doePaAdminService.CreateAbrechnungseinheitAsync("Personentage", "PT", cancellationToken);
            _ = await doePaAdminService.CreateAbrechnungseinheitAsync("Stück", "Stk", cancellationToken);

            _ = await doePaAdminService.CreateWaehrungAsync("Euro", "€", "EUR", cancellationToken);
            _ = await doePaAdminService.CreateWaehrungAsync("US Dollar", "$", "USD", cancellationToken);
            _ = await doePaAdminService.CreateWaehrungAsync("Schweizer Franken", "Fr", "CHF", cancellationToken);
            _ = await doePaAdminService.CreateWaehrungAsync("Britisches Pfund", "£", "GBP", cancellationToken);
                        
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1991, 12, 31), new(1991, 1, 1), "1991", "1991", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1992, 12, 31), new(1992, 1, 1), "1992", "1992", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1993, 6, 30), new(1993, 1, 1), "1993", "1993", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1994, 6, 30), new(1993, 7, 1), "1993/1994", "1993", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1995, 6, 30), new(1994, 7, 1), "1994/1995", "1994", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2020, 12, 31), new(2020, 1, 1), "2020", "2020", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2021, 6, 30), new(2021, 1, 1),  "2021", "2021", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2022, 6, 30), new(2021, 7, 1), "2021/2022", "2021", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2023, 6, 30), new(2022, 7, 1), "2022/2023", "2022", cancellationToken);
                        
            _ = await doePaAdminService.CreatePostleitzahlAsync("Niedersachsen", "Deutschland", "Hannover", "30173", cancellationToken);
            _ = await doePaAdminService.CreatePostleitzahlAsync("Baden-Württemberg", "Deutschland", "Mühlhausen (Kraichgau)", "69242", cancellationToken);
            _ = await doePaAdminService.CreatePostleitzahlAsync("Bayern", "Deutschland", "München", "80807", cancellationToken);

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

        private static async Task CreateMitarbeiterAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken)
        {

            IEnumerable<Kostenstelle> listKostenstellen = await doePaAdminService.GetKostenstellenAsync(cancellationToken);
            IEnumerable<Taetigkeit> listTaetigkeiten = await doePaAdminService.GetTaetigkeitenAsync(cancellationToken);
            
            List<Anstellungsdetail> currentAnstellungshistorie;
            Anstellungsdetail currentAnstellungsdetail;
            Mitarbeiter currentMitarbeiter;

            var listMitarbeiter = new[]
            {
                /*
                * John Carmack
                */
                new {
                    Anrede = "Herr",
                    Vorname = "John",
                    Nachname = "Carmack",
                    Geburtsdatum = new DateTime(1970, 8, 20),
                    Kuerzel = "JOCA",
                    PersonalnummerDatev = 1,
                    Postleitzahl = "30173",
                    Hausnummer = "23",
                    Strasse = "Freundallee",
                    Kostenstellennummer = 1003,
                    Anstellungsdetails = new[]
                    {
                        new { AnzahlMonatsgehaelter = 12, AnzahlArbeitsstunden = 40, GueltigAb = new DateTime(1991, 2, 1), Monatsgehalt = 250, IstGekuendigt = false, Taetigkeitsbeschreibung = "Technical Director"},
                        new { AnzahlMonatsgehaelter = 12, AnzahlArbeitsstunden = 40, GueltigAb = new DateTime(1991, 11, 30), Monatsgehalt = 2090, IstGekuendigt = false, Taetigkeitsbeschreibung = "Technical Director"},
                        new { AnzahlMonatsgehaelter = 0, AnzahlArbeitsstunden = 0, GueltigAb = new DateTime(2013, 11, 22), Monatsgehalt = 0, IstGekuendigt = true, Taetigkeitsbeschreibung = string.Empty}
                    }
                },
                /*
                * John Romero
                */
                new {
                    Anrede = "Herr",
                    Vorname = "John",
                    Nachname = "Romero",
                    Geburtsdatum = new DateTime(1967, 10, 28),
                    Kuerzel = "JORO",
                    PersonalnummerDatev = 2,
                    Postleitzahl = "30173",
                    Hausnummer = "59",
                    Strasse = "Georgstr.",
                    Kostenstellennummer = 1006,
                    Anstellungsdetails = new[]
                    {
                        new { AnzahlMonatsgehaelter = 12, AnzahlArbeitsstunden = 40, GueltigAb = new DateTime(1991, 2, 1), Monatsgehalt = 250, IstGekuendigt = false, Taetigkeitsbeschreibung = "Game Designer"},
                        new { AnzahlMonatsgehaelter = 12, AnzahlArbeitsstunden = 40, GueltigAb = new DateTime(1991, 11, 30), Monatsgehalt = 2090, IstGekuendigt = false, Taetigkeitsbeschreibung = "Game Designer"},
                        new { AnzahlMonatsgehaelter = 0, AnzahlArbeitsstunden = 0, GueltigAb = new DateTime(1996, 8, 6), Monatsgehalt = 0, IstGekuendigt = true, Taetigkeitsbeschreibung = string.Empty}
                    }
                },
                /*
                * Tom Hall
                */
                new {
                    Anrede = "Herr",
                    Vorname = "Tom",
                    Nachname = "Hall",
                    Geburtsdatum = DateTime.MinValue,
                    Kuerzel = "TOHA",
                    PersonalnummerDatev = 3,
                    Postleitzahl = "30173",
                    Hausnummer = "1",
                    Strasse = "Heinrich-Heine-Str. 1",
                    Kostenstellennummer = 1012,
                    Anstellungsdetails = new[]
                    {
                        new { AnzahlMonatsgehaelter = 12, AnzahlArbeitsstunden = 40, GueltigAb = new DateTime(1991, 2, 1), Monatsgehalt = 250, IstGekuendigt = false, Taetigkeitsbeschreibung = "Game Designer"},
                        new { AnzahlMonatsgehaelter = 12, AnzahlArbeitsstunden = 40, GueltigAb = new DateTime(1991, 11, 30), Monatsgehalt = 2090, IstGekuendigt = false, Taetigkeitsbeschreibung = "Game Designer"},
                        new { AnzahlMonatsgehaelter = 0, AnzahlArbeitsstunden = 0, GueltigAb = new DateTime(1993, 8, 1), Monatsgehalt = 0, IstGekuendigt = true, Taetigkeitsbeschreibung = string.Empty}
                    }
                },
                /*
                * Adrian Carmack
                */
                new {
                    Anrede = "Herr",
                    Vorname = "Adrian",
                    Nachname = "Carmack",
                    Geburtsdatum = new DateTime(1969, 5, 5),
                    Kuerzel = "ADCA",
                    PersonalnummerDatev = 4,
                    Postleitzahl = "30173",
                    Hausnummer = "43",
                    Strasse = "Marienstr.",
                    Kostenstellennummer = 1009,
                    Anstellungsdetails = new[]
                    {
                        new { AnzahlMonatsgehaelter = 12, AnzahlArbeitsstunden = 40, GueltigAb = new DateTime(1991, 2, 1), Monatsgehalt = 250, IstGekuendigt = false, Taetigkeitsbeschreibung = "Artist"},
                        new { AnzahlMonatsgehaelter = 12, AnzahlArbeitsstunden = 40, GueltigAb = new DateTime(1991, 11, 30), Monatsgehalt = 2090, IstGekuendigt = false, Taetigkeitsbeschreibung = "Artist"},
                        new { AnzahlMonatsgehaelter = 0, AnzahlArbeitsstunden = 0, GueltigAb = new DateTime(2005, 1, 1), Monatsgehalt = 0, IstGekuendigt = true, Taetigkeitsbeschreibung = string.Empty}
                    }
                }
            };
            
            foreach (var mitarbeiter in listMitarbeiter)
            {
                currentAnstellungshistorie = new();

                currentMitarbeiter = await doePaAdminService.CreateMitarbeiterAsync(cancellationToken);
                currentMitarbeiter.Anrede = mitarbeiter.Anrede;
                currentMitarbeiter.Vorname = mitarbeiter.Vorname;
                currentMitarbeiter.Nachname = mitarbeiter.Vorname;
                currentMitarbeiter.Geburtsdatum = mitarbeiter.Geburtsdatum;
                currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
                currentMitarbeiter.Kuerzel = mitarbeiter.Kuerzel;
                currentMitarbeiter.PersonalnummerDatev = mitarbeiter.PersonalnummerDatev;

                currentMitarbeiter.ZugehoerigeAdresse.ZugehoerigePostleitzahl = (await doePaAdminService.GetPostleitzahlenAsync(cancellationToken)).Where(plz => plz.PLZ.Equals(mitarbeiter.Postleitzahl)).First();
                currentMitarbeiter.ZugehoerigeAdresse.Hausnummer = mitarbeiter.Hausnummer;
                currentMitarbeiter.ZugehoerigeAdresse.Strasse = mitarbeiter.Strasse;

                currentMitarbeiter.ZugehoerigeKostenstelle = listKostenstellen.Where(kst => kst.KostenstellenNummer.Equals(mitarbeiter.Kostenstellennummer)).First();

                foreach (var anstellungsdetail in mitarbeiter.Anstellungsdetails)
                {
                    currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
                    currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
                    currentAnstellungsdetail.AnzahlMonatsgehaelter = anstellungsdetail.AnzahlMonatsgehaelter;
                    currentAnstellungsdetail.AnzahlArbeitsstunden = anstellungsdetail.AnzahlArbeitsstunden;
                    currentAnstellungsdetail.GueltigAb = anstellungsdetail.GueltigAb;
                    currentAnstellungsdetail.Monatsgehalt = anstellungsdetail.Monatsgehalt;
                    currentAnstellungsdetail.IstGekuendigt = anstellungsdetail.IstGekuendigt;
                    currentAnstellungsdetail.ZugehoerigeTaetigkeit = listTaetigkeiten.Where(t => t.Taetigkeitsbeschreibung.Equals(anstellungsdetail.Taetigkeitsbeschreibung)).FirstOrDefault();

                    currentAnstellungshistorie.Add(currentAnstellungsdetail);
                }

            }

            await doePaAdminService.SaveChangesAsync(cancellationToken);

        }

        private static async Task CreateKostenstellenAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            IEnumerable<Kostenstellenart> listKostenstellenarten = await doePaAdminService.GetKostenstellenartenAsync(cancellationToken);
            
            Kostenstelle kstOfficeRichardson = await doePaAdminService.CreateKostenstelleAsync(cancellationToken);
            kstOfficeRichardson.Kostenstellenbezeichnung = "Office Richardson";
            kstOfficeRichardson.KostenstellenNummer = 5020;
            kstOfficeRichardson.ZugehoerigeKostenstellenart = listKostenstellenarten.Where(ka => ka.Kostenstellenartbezeichnung.Equals("Geschäftsräume")).First();

            Kostenstelle currentKostenstelle;

            currentKostenstelle = await doePaAdminService.CreateKostenstelleAsync(cancellationToken);
            currentKostenstelle.Kostenstellenbezeichnung = "Bobby Prince";
            currentKostenstelle.KostenstellenNummer = 2002;
            currentKostenstelle.ZugehoerigeKostenstellenart = listKostenstellenarten.Where(ka => ka.Kostenstellenartbezeichnung.Equals("Freie Mitarbeiter/innen")).First();

            
            foreach (var currentEmployee in new[] { new { Name = "John Carmack", KstNummer = 1003 }, new { Name = "John Romero", KstNummer = 1006 }, new { Name = "Adrian Carmack", KstNummer = 1009 }, new { Name = "Tom Hall", KstNummer = 1012 } })
            { 
                currentKostenstelle = await doePaAdminService.CreateKostenstelleAsync(cancellationToken);
                currentKostenstelle.Kostenstellenbezeichnung = currentEmployee.Name;
                currentKostenstelle.KostenstellenNummer = currentEmployee.KstNummer;
                currentKostenstelle.ZugehoerigeKostenstellenart = listKostenstellenarten.Where(ka => ka.Kostenstellenartbezeichnung.Equals("Angestellte Mitarbeiter/innen")).First(); ;
                currentKostenstelle.UebergeordneteKostenstellen.Add(kstOfficeRichardson);
                kstOfficeRichardson.UntergeordneteKostenstellen.Add(currentKostenstelle);
            }

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

        private static async Task CreateAuftraegeAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            Kunde currentKunde;
            Debitor currentRechnungsempfaenger;
            Projekt currentProjekt;
            Auftrag currentAuftrag;
            Adresse currentAdresse;
            Auftragsposition currentAuftragsposition;

            Abrechnungseinheit aeStunden = (await doePaAdminService.GetAbrechnungseinheitenAsync(cancellationToken)).Where(ae => ae.Name.Equals("Stunden")).First();
            Waehrung wEuro = (await doePaAdminService.GetWaehrungenAsync(cancellationToken)).Where(w => w.WaehrungName.Equals("Euro")).First();
            Geschaeftsjahr gJahr1991 = (await doePaAdminService.GetGeschaeftsjahreAsync(cancellationToken)).Where(gj => gj.Name.Equals("1991")).First();

            IEnumerable<Mitarbeiter> listMitarbeiter = await doePaAdminService.GetMitarbeiterAsync(cancellationToken);

            currentKunde = await doePaAdminService.CreateKundeAsync(cancellationToken);
            currentKunde.Kundenname = "Softdisk";
            currentKunde.Langname = "Softdisk Magazette";

            currentRechnungsempfaenger = await doePaAdminService.CreateGeschaeftspartnerAsync<Debitor>(cancellationToken);
            currentRechnungsempfaenger.Anschrift = "Gamer's Edge";
            currentRechnungsempfaenger.ZugehoerigerKunde = currentKunde;
            //currentRechnungsempfaenger.ZugehoerigeAdresse = currentAdresse;
            currentKunde.Rechnungsempfaenger.Add(currentRechnungsempfaenger);

            currentProjekt = await doePaAdminService.CreateProjektAsync(cancellationToken);
            currentProjekt.Projektstart = new(1991, 2, 1);
            currentProjekt.Projektende = new(1992, 12, 31);
            currentProjekt.Projektname = "Vertragliche Verbindlichkeiten gegenüber Softdisk";
            currentProjekt.Rechnungsempfaenger = currentRechnungsempfaenger;
            currentRechnungsempfaenger.Projekte.Add(currentProjekt);

            Skill techSkill = await doePaAdminService.CreateSkillAsync(cancellationToken);
            techSkill.SkillName = "Technische Skills";

            Skill progSkill = await doePaAdminService.CreateSkillAsync(cancellationToken);
            progSkill.SkillName = "Programmiersprachen ";

            Skill netSkill = await doePaAdminService.CreateSkillAsync(cancellationToken);
            netSkill.SkillName = ".NET";

            Skill csSkill = await doePaAdminService.CreateSkillAsync(cancellationToken);
            csSkill.SkillName = "C#";

            techSkill.ChildSkills.Add(progSkill);
            progSkill.ChildSkills.Add(netSkill);
            netSkill.ChildSkills.Add(csSkill);

            currentProjekt.Skills.Add(techSkill);

            currentAuftrag = await doePaAdminService.CreateAuftragAsync(cancellationToken);
            currentAuftrag.Auftragsbeginn = new(1991, 2, 1);
            currentAuftrag.Auftragsdatum = new(1991, 2, 1);
            currentAuftrag.Auftragsende = new(1991, 03, 31);
            currentAuftrag.Auftragsname = "Gamer's Edge Q1 1991";
            currentAuftrag.ZugehoerigesGeschaeftsjahr = gJahr1991;
            currentAuftrag.ZugehoerigesProjekt = currentProjekt;
            currentProjekt.ZugehoerigeAuftraege.Add(currentAuftrag);
            currentAuftrag.VerantwortlicherMitarbeiter = listMitarbeiter.Where(m => m.Nachname.Equals("Hall")).First();
            currentAuftrag.Vertragsnummer = 1;

            currentAuftragsposition = await doePaAdminService.CreateAuftragspositionAsync(cancellationToken);
            currentAuftragsposition.Abrechnungseinheit = aeStunden;
            currentAuftragsposition.AuftragspositionNummer = 1;
            currentAuftragsposition.Auftragsvolumen = 480;
            currentAuftragsposition.Positionsbezeichnung = "Spieleentwicklung";
            currentAuftragsposition.Waehrung = wEuro;

            currentAuftragsposition.Auftrag = currentAuftrag;
            currentAuftrag.Auftragspositionen.Add(currentAuftragsposition);
            
            currentKunde = await doePaAdminService.CreateKundeAsync(cancellationToken);
            currentKunde.Kundenname = "Apogee";

            await doePaAdminService.SaveChangesAsync(cancellationToken);

        }

        private static async Task CreateAusgangsrechnungenAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken)
        {

            Ausgangsrechnung currentAusgangsrechnung;
            Ausgangsrechnungsposition currentAusgangsrechnungsposition;

            Geschaeftsjahr gJahr1991 = (await doePaAdminService.GetGeschaeftsjahreAsync(cancellationToken)).Where(gj => gj.Name.Equals("1991")).First();
            Debitor gamersEdge = (await doePaAdminService.GetGeschaeftspartnerAsync<Debitor>(cancellationToken)).Where(gp => gp.Anschrift.Equals("Gamer's Edge")).First();
            Waehrung wEuro = (await doePaAdminService.GetWaehrungenAsync(cancellationToken)).Where(w => w.WaehrungName.Equals("Euro")).First();
            Abrechnungseinheit aeStunden = (await doePaAdminService.GetAbrechnungseinheitenAsync(cancellationToken)).Where(ae => ae.Name.Equals("Stunden")).First();
            Auftragsposition apGESpieleentwicklung = (await doePaAdminService.GetAuftragspositionAsync(cancellationToken)).Where(ap => ap.Positionsbezeichnung.Equals("Spieleentwicklung")).First();
            Kostenstelle kstCarmack = (await doePaAdminService.GetKostenstellenAsync(cancellationToken)).Where(kst => kst.Kostenstellenbezeichnung.Equals("John Carmack")).First();
            Kostenstelle kstHall = (await doePaAdminService.GetKostenstellenAsync(cancellationToken)).Where(kst => kst.Kostenstellenbezeichnung.Equals("Tom Hall")).First();

            currentAusgangsrechnung = await doePaAdminService.CreateAusgangsrechnungAsync(cancellationToken);
            
            currentAusgangsrechnung.RechnungsDatum = new(1991, 2, 28);
            currentAusgangsrechnung.BezahltDatum = new(1991,3,15);
            currentAusgangsrechnung.RechnungsNummer = "19910001";
            currentAusgangsrechnung.ZugehoerigesGeschaeftsjahr = gJahr1991;
            currentAusgangsrechnung.Rechnungsempfaenger = gamersEdge;

            currentAusgangsrechnungsposition = await doePaAdminService.CreateAusgangsrechnungspositionAsync(cancellationToken);

            currentAusgangsrechnungsposition.PositionsNummer = 1;
            currentAusgangsrechnungsposition.LeistungszeitraumBis = new(1991, 2, 28);
            currentAusgangsrechnungsposition.LeistungszeitraumVon = new(1991, 2, 1);
            currentAusgangsrechnungsposition.NettobetragWaehrung = wEuro;
            currentAusgangsrechnungsposition.Positionsbeschreibung = "Entwicklung der Engine für Commander Keen";
            currentAusgangsrechnungsposition.Steuersatz = 0.16M;
            currentAusgangsrechnungsposition.StueckpreisNetto = 50M;
            currentAusgangsrechnungsposition.Stueckzahl = 100;
            currentAusgangsrechnungsposition.ZugehoerigeAbrechnungseinheit = aeStunden;
            currentAusgangsrechnungsposition.ZugehoerigeAuftragsposition = apGESpieleentwicklung;
            currentAusgangsrechnungsposition.ZugehoerigeKostenstelle = kstCarmack;
            currentAusgangsrechnungsposition.ZugehoerigeRechnung = currentAusgangsrechnung;
            
            currentAusgangsrechnung.Rechnungspositionen.Add(currentAusgangsrechnungsposition);

            currentAusgangsrechnungsposition = await doePaAdminService.CreateAusgangsrechnungspositionAsync(cancellationToken);

            currentAusgangsrechnungsposition.PositionsNummer = 2;
            currentAusgangsrechnungsposition.LeistungszeitraumBis = new(1991, 2, 28);
            currentAusgangsrechnungsposition.LeistungszeitraumVon = new(1991, 2, 1);
            currentAusgangsrechnungsposition.NettobetragWaehrung = wEuro;
            currentAusgangsrechnungsposition.Positionsbeschreibung = "Design der Sprites für Commander Keen";
            currentAusgangsrechnungsposition.Steuersatz = 0.16M;
            currentAusgangsrechnungsposition.StueckpreisNetto = 45M;
            currentAusgangsrechnungsposition.Stueckzahl = 50;
            currentAusgangsrechnungsposition.ZugehoerigeAbrechnungseinheit = aeStunden;
            currentAusgangsrechnungsposition.ZugehoerigeAuftragsposition = apGESpieleentwicklung;
            currentAusgangsrechnungsposition.ZugehoerigeKostenstelle = kstHall;
            currentAusgangsrechnungsposition.ZugehoerigeRechnung = currentAusgangsrechnung;

            currentAusgangsrechnung.Rechnungspositionen.Add(currentAusgangsrechnungsposition);

            currentAusgangsrechnung = await doePaAdminService.CreateAusgangsrechnungAsync(cancellationToken);

            currentAusgangsrechnung.RechnungsDatum = new(1991, 3, 5);
            currentAusgangsrechnung.BezahltDatum = new(1991, 3, 5);
            currentAusgangsrechnung.RechnungsNummer = "19910002";
            currentAusgangsrechnung.ZugehoerigesGeschaeftsjahr = gJahr1991;
            currentAusgangsrechnung.Rechnungsempfaenger = gamersEdge;

            currentAusgangsrechnungsposition = await doePaAdminService.CreateAusgangsrechnungspositionAsync(cancellationToken);

            currentAusgangsrechnungsposition.PositionsNummer = 1;
            currentAusgangsrechnungsposition.LeistungszeitraumBis = new(1991, 2, 28);
            currentAusgangsrechnungsposition.LeistungszeitraumVon = new(1991, 2, 1);
            currentAusgangsrechnungsposition.NettobetragWaehrung = wEuro;
            currentAusgangsrechnungsposition.Positionsbeschreibung = "Gutschrift für nicht akkzeptierte Entwicklungsstunden an der Engine für Commander Keen";
            currentAusgangsrechnungsposition.Steuersatz = 0.16M;
            currentAusgangsrechnungsposition.StueckpreisNetto = 50M;
            currentAusgangsrechnungsposition.Stueckzahl = -20;
            currentAusgangsrechnungsposition.ZugehoerigeAbrechnungseinheit = aeStunden;
            currentAusgangsrechnungsposition.ZugehoerigeAuftragsposition = apGESpieleentwicklung;
            currentAusgangsrechnungsposition.ZugehoerigeKostenstelle = kstCarmack;
            currentAusgangsrechnungsposition.ZugehoerigeRechnung = currentAusgangsrechnung;

            currentAusgangsrechnung.Rechnungspositionen.Add(currentAusgangsrechnungsposition);

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

    }
}
