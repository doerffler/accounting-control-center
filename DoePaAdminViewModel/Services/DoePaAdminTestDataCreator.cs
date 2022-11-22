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
            Taetigkeit currentTaetigkeit;

            currentTaetigkeit = await doePaAdminService.CreateTaetigkeitAsync(cancellationToken);
            currentTaetigkeit.Taetigkeitsbeschreibung = "Game Designer";

            currentTaetigkeit = await doePaAdminService.CreateTaetigkeitAsync(cancellationToken);
            currentTaetigkeit.Taetigkeitsbeschreibung = "Technical Director";

            currentTaetigkeit = await doePaAdminService.CreateTaetigkeitAsync(cancellationToken);
            currentTaetigkeit.Taetigkeitsbeschreibung = "Artist";

            Kostenstellenart currentKostenstellenart;

            currentKostenstellenart = await doePaAdminService.CreateKostenstellenartAsync(cancellationToken);
            currentKostenstellenart.Kostenstellenartbezeichnung = "Angestellte Mitarbeiter/innen";

            currentKostenstellenart = await doePaAdminService.CreateKostenstellenartAsync(cancellationToken);
            currentKostenstellenart.Kostenstellenartbezeichnung = "Geschäftsräume";

            currentKostenstellenart = await doePaAdminService.CreateKostenstellenartAsync(cancellationToken);
            currentKostenstellenart.Kostenstellenartbezeichnung = "Freie Mitarbeiter/innen";

            currentKostenstellenart = await doePaAdminService.CreateKostenstellenartAsync(cancellationToken);
            currentKostenstellenart.Kostenstellenartbezeichnung = "Sonstige Kostenstellen";

            Abrechnungseinheit currentAbrechnungseinheit;

            currentAbrechnungseinheit = await doePaAdminService.CreateAbrechnungseinheitAsync(cancellationToken);
            currentAbrechnungseinheit.Name = "Stunden";
            currentAbrechnungseinheit.Abkuerzung = "h";

            currentAbrechnungseinheit = await doePaAdminService.CreateAbrechnungseinheitAsync(cancellationToken);
            currentAbrechnungseinheit.Name = "Personentage";
            currentAbrechnungseinheit.Abkuerzung = "PT";

            currentAbrechnungseinheit = await doePaAdminService.CreateAbrechnungseinheitAsync(cancellationToken);
            currentAbrechnungseinheit.Name = "Stück";
            currentAbrechnungseinheit.Abkuerzung = "Stk";

            Waehrung currentWaehrung;

            currentWaehrung = await doePaAdminService.CreateWaehrungAsync(cancellationToken);
            currentWaehrung.WaehrungName = "Euro";
            currentWaehrung.WaehrungZeichen = "€";
            currentWaehrung.WaehrungISO = "EUR";

            currentWaehrung = await doePaAdminService.CreateWaehrungAsync(cancellationToken);
            currentWaehrung.WaehrungName = "US Dollar";
            currentWaehrung.WaehrungZeichen = "$";
            currentWaehrung.WaehrungISO = "USD";

            currentWaehrung = await doePaAdminService.CreateWaehrungAsync(cancellationToken);
            currentWaehrung.WaehrungName = "Schweizer Franken";
            currentWaehrung.WaehrungZeichen = "Fr";
            currentWaehrung.WaehrungISO = "CHF";

            currentWaehrung = await doePaAdminService.CreateWaehrungAsync(cancellationToken);
            currentWaehrung.WaehrungName = "Britisches Pfund";
            currentWaehrung.WaehrungZeichen = "£";
            currentWaehrung.WaehrungISO = "GBP";

            Geschaeftsjahr currentGeschaeftsjahr;

            currentGeschaeftsjahr = await doePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            currentGeschaeftsjahr.DatumBis = new(1991,12,31);
            currentGeschaeftsjahr.DatumVon = new(1991,1,1);
            currentGeschaeftsjahr.Name = "1991";
            currentGeschaeftsjahr.Rechnungsprefix = "1991";

            currentGeschaeftsjahr = await doePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            currentGeschaeftsjahr.DatumBis = new(1992, 12, 31);
            currentGeschaeftsjahr.DatumVon = new(1992, 1, 1);
            currentGeschaeftsjahr.Name = "1992";
            currentGeschaeftsjahr.Rechnungsprefix = "1992";

            currentGeschaeftsjahr = await doePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            currentGeschaeftsjahr.DatumBis = new(1993, 6, 30);
            currentGeschaeftsjahr.DatumVon = new(1993, 1, 1);
            currentGeschaeftsjahr.Name = "1993";
            currentGeschaeftsjahr.Rechnungsprefix = "1993";

            currentGeschaeftsjahr = await doePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            currentGeschaeftsjahr.DatumBis = new(1994, 6, 30);
            currentGeschaeftsjahr.DatumVon = new(1993, 7, 1);
            currentGeschaeftsjahr.Name = "1993/1994";
            currentGeschaeftsjahr.Rechnungsprefix = "1993";

            currentGeschaeftsjahr = await doePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            currentGeschaeftsjahr.DatumBis = new(1995, 6, 30);
            currentGeschaeftsjahr.DatumVon = new(1994, 7, 1);
            currentGeschaeftsjahr.Name = "1994/1995";
            currentGeschaeftsjahr.Rechnungsprefix = "1994";

            currentGeschaeftsjahr = await doePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            currentGeschaeftsjahr.DatumBis = new(2020, 12, 31);
            currentGeschaeftsjahr.DatumVon = new(2020, 1, 1);
            currentGeschaeftsjahr.Name = "2020";
            currentGeschaeftsjahr.Rechnungsprefix = "2020";

            currentGeschaeftsjahr = await doePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            currentGeschaeftsjahr.DatumBis = new(2021, 6, 30);
            currentGeschaeftsjahr.DatumVon = new(2021, 1, 1);
            currentGeschaeftsjahr.Name = "2021";
            currentGeschaeftsjahr.Rechnungsprefix = "2021";

            currentGeschaeftsjahr = await doePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            currentGeschaeftsjahr.DatumBis = new(2022, 6, 30);
            currentGeschaeftsjahr.DatumVon = new(2021, 7, 1);
            currentGeschaeftsjahr.Name = "2021/2022";
            currentGeschaeftsjahr.Rechnungsprefix = "2021";

            currentGeschaeftsjahr = await doePaAdminService.CreateGeschaeftsjahrAsync(cancellationToken);
            currentGeschaeftsjahr.DatumBis = new(2023, 6, 30);
            currentGeschaeftsjahr.DatumVon = new(2022, 7, 1);
            currentGeschaeftsjahr.Name = "2022/2023";
            currentGeschaeftsjahr.Rechnungsprefix = "2022";

            Postleitzahl currentPostleitzahl;

            currentPostleitzahl = await doePaAdminService.CreatePostleitzahlAsync(cancellationToken);
            currentPostleitzahl.Bundesland = "Niedersachsen";
            currentPostleitzahl.Land = "Deutschland";
            currentPostleitzahl.Ortsname = "Hannover";
            currentPostleitzahl.PLZ = "30173";

            currentPostleitzahl = await doePaAdminService.CreatePostleitzahlAsync(cancellationToken);
            currentPostleitzahl.Bundesland = "Baden-Württemberg";
            currentPostleitzahl.Land = "Deutschland";
            currentPostleitzahl.Ortsname = "Mühlhausen (Kraichgau)";
            currentPostleitzahl.PLZ = "69242";

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

        private static async Task CreateMitarbeiterAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken)
        {

            IEnumerable<Kostenstelle> listKostenstellen = await doePaAdminService.GetKostenstellenAsync(cancellationToken);
            IEnumerable<Taetigkeit> listTaetigkeiten = await doePaAdminService.GetTaetigkeitenAsync(cancellationToken);
            
            Taetigkeit taetTechnicalDirector = listTaetigkeiten.Where(t => t.Taetigkeitsbeschreibung.Equals("Technical Director")).First();
            Taetigkeit taetGamedesigner = listTaetigkeiten.Where(t => t.Taetigkeitsbeschreibung.Equals("Game Designer")).First();
            Taetigkeit taetArtist = listTaetigkeiten.Where(t => t.Taetigkeitsbeschreibung.Equals("Artist")).First();

            Postleitzahl plzHannover = (await doePaAdminService.GetPostleitzahlenAsync(cancellationToken)).Where(plz => plz.PLZ.Equals("30173")).First();

            List<Anstellungsdetail> currentAnstellungshistorie;
            Anstellungsdetail currentAnstellungsdetail;
            Mitarbeiter currentMitarbeiter;

            /*
             * John Carmack
             */
            currentAnstellungshistorie = new();

            currentMitarbeiter = await doePaAdminService.CreateMitarbeiterAsync(cancellationToken);
            currentMitarbeiter.Anrede = "Herr";
            currentMitarbeiter.Vorname = "John";
            currentMitarbeiter.Nachname = "Carmack";
            currentMitarbeiter.Geburtsdatum = new(1970, 8, 20);
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = "JOCA";
            currentMitarbeiter.PersonalnummerDatev = 1;

            currentMitarbeiter.ZugehoerigeAdresse.ZugehoerigePostleitzahl = plzHannover;
            currentMitarbeiter.ZugehoerigeAdresse.Hausnummer = "23";
            currentMitarbeiter.ZugehoerigeAdresse.Strasse = "Freundallee";

            currentMitarbeiter.ZugehoerigeKostenstelle = listKostenstellen.Where(kst => kst.Kostenstellenbezeichnung.Equals("John Carmack")).First();

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 2, 1);
            currentAnstellungsdetail.Monatsgehalt = 250;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetTechnicalDirector;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 11, 30);
            currentAnstellungsdetail.Monatsgehalt = 2090;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetTechnicalDirector;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 0;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 0;
            currentAnstellungsdetail.GueltigAb = new(2013, 11, 22);
            currentAnstellungsdetail.Monatsgehalt = 0;
            currentAnstellungsdetail.IstGekuendigt = true;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = null;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            /*
             * John Romero
             */
            currentAnstellungshistorie = new();

            currentMitarbeiter = await doePaAdminService.CreateMitarbeiterAsync(cancellationToken);
            currentMitarbeiter.Anrede = "Herr";
            currentMitarbeiter.Vorname = "John";
            currentMitarbeiter.Nachname = "Romero";
            currentMitarbeiter.Geburtsdatum = new(1967, 10, 28);
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = "JORO";
            currentMitarbeiter.PersonalnummerDatev = 2;

            currentMitarbeiter.ZugehoerigeAdresse.ZugehoerigePostleitzahl = plzHannover;
            currentMitarbeiter.ZugehoerigeAdresse.Hausnummer = "59";
            currentMitarbeiter.ZugehoerigeAdresse.Strasse = "Georgstr.";

            currentMitarbeiter.ZugehoerigeKostenstelle = listKostenstellen.Where(kst => kst.Kostenstellenbezeichnung.Equals("John Romero")).First();
            
            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 2, 1);
            currentAnstellungsdetail.Monatsgehalt = 250;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetGamedesigner;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 11, 30);
            currentAnstellungsdetail.Monatsgehalt = 2090;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetGamedesigner;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 0;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 0;
            currentAnstellungsdetail.GueltigAb = new(1996, 8, 6);
            currentAnstellungsdetail.Monatsgehalt = 0;
            currentAnstellungsdetail.IstGekuendigt = true;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = null;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            /*
             * Tom Hall
             */
            currentAnstellungshistorie = new();

            currentMitarbeiter = await doePaAdminService.CreateMitarbeiterAsync(cancellationToken);
            currentMitarbeiter.Anrede = "Herr";
            currentMitarbeiter.Vorname = "Tom";
            currentMitarbeiter.Nachname = "Hall";
            currentMitarbeiter.Geburtsdatum = DateTime.MinValue;
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = "TOHA";
            currentMitarbeiter.PersonalnummerDatev = 3;
            currentMitarbeiter.ZugehoerigeKostenstelle = listKostenstellen.Where(kst => kst.Kostenstellenbezeichnung.Equals("Tom Hall")).First();

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 2, 1);
            currentAnstellungsdetail.Monatsgehalt = 250;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetGamedesigner;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 11, 30);
            currentAnstellungsdetail.Monatsgehalt = 2090;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetGamedesigner;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 0;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 0;
            currentAnstellungsdetail.GueltigAb = new(1993, 8, 1);
            currentAnstellungsdetail.Monatsgehalt = 0;
            currentAnstellungsdetail.IstGekuendigt = true;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = null;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            /*
             * Adrian Carmack
             */
            currentAnstellungshistorie = new();

            currentMitarbeiter = await doePaAdminService.CreateMitarbeiterAsync(cancellationToken);
            currentMitarbeiter.Anrede = "Herr";
            currentMitarbeiter.Vorname = "Adrian";
            currentMitarbeiter.Nachname = "Carmack";
            currentMitarbeiter.Geburtsdatum = new(1969, 5, 5);
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = "ADCA";
            currentMitarbeiter.PersonalnummerDatev = 4;
            currentMitarbeiter.ZugehoerigeKostenstelle = listKostenstellen.Where(kst => kst.Kostenstellenbezeichnung.Equals("Adrian Carmack")).First();

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 2, 1);
            currentAnstellungsdetail.Monatsgehalt = 250;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetArtist;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 11, 30);
            currentAnstellungsdetail.Monatsgehalt = 2090;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetArtist;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 0;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 0;
            currentAnstellungsdetail.GueltigAb = new(2005, 1, 1);
            currentAnstellungsdetail.Monatsgehalt = 0;
            currentAnstellungsdetail.IstGekuendigt = true;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = null;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            await doePaAdminService.SaveChangesAsync(cancellationToken);

        }

        private static async Task CreateKostenstellenAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            IEnumerable<Kostenstellenart> listKostenstellenarten = await doePaAdminService.GetKostenstellenartenAsync(cancellationToken);
            
            Kostenstelle kstOfficeRichardson = await doePaAdminService.CreateKostenstelleAsync(cancellationToken);
            kstOfficeRichardson.Kostenstellenbezeichnung = "Office Richardson";
            kstOfficeRichardson.ZugehoerigeKostenstellenart = listKostenstellenarten.Where(ka => ka.Kostenstellenartbezeichnung.Equals("Geschäftsräume")).First();

            Kostenstelle currentKostenstelle;

            currentKostenstelle = await doePaAdminService.CreateKostenstelleAsync(cancellationToken);
            currentKostenstelle.Kostenstellenbezeichnung = "Bobby Prince";
            currentKostenstelle.ZugehoerigeKostenstellenart = listKostenstellenarten.Where(ka => ka.Kostenstellenartbezeichnung.Equals("Freie Mitarbeiter/innen")).First();

            foreach (string currentEmployeeName in new String[] { "John Carmack", "John Romero", "Adrian Carmack", "Tom Hall" })
            { 
            currentKostenstelle = await doePaAdminService.CreateKostenstelleAsync(cancellationToken);
            currentKostenstelle.Kostenstellenbezeichnung = currentEmployeeName;
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
            currentRechnungsempfaenger.ZugehoerigeAdresse = new();
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

            await doePaAdminService.SaveChangesAsync(cancellationToken);

            //TODO: We need a canceled invoice here.

        }

    }
}
