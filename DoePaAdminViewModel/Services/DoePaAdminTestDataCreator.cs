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

        public static async Task CreateTestDataAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            await CreateKostenstellenAsync(doePaAdminService, cancellationToken);
            await CreateMitarbeiterAsync(doePaAdminService, cancellationToken);

            await doePaAdminService.SaveChangesAsync(cancellationToken);
            
        }

        private static async Task CreateMitarbeiterAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken)
        {

            IEnumerable<Kostenstelle> listKostenstellen = await doePaAdminService.GetKostenstellenAsync(cancellationToken);
            
            Taetigkeit taetGamedesigner = await doePaAdminService.CreateTaetigkeitAsync(cancellationToken);
            taetGamedesigner.Taetigkeitsbeschreibung = "Game Designer";

            Taetigkeit taetTechnicalDirector = await doePaAdminService.CreateTaetigkeitAsync(cancellationToken);
            taetTechnicalDirector.Taetigkeitsbeschreibung = "Technical Director";

            Taetigkeit taetArtist = await doePaAdminService.CreateTaetigkeitAsync(cancellationToken);
            taetArtist.Taetigkeitsbeschreibung = "Artist";

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
            //TODO: There seems to be a problem somewhere over here
            currentMitarbeiter.PersonalnummerDatev = 1;
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

        }

        private static async Task CreateKostenstellenAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            Kostenstellenart kstArtMitarbeiter = await doePaAdminService.CreateKostenstellenartAsync(cancellationToken);
            kstArtMitarbeiter.Kostenstellenartbezeichnung = "Mitarbeiterkostenstelle";

            Kostenstellenart kstArtOffice = await doePaAdminService.CreateKostenstellenartAsync(cancellationToken);
            kstArtOffice.Kostenstellenartbezeichnung = "Office";

            Kostenstelle kstOfficeRichardson = await doePaAdminService.CreateKostenstelleAsync(cancellationToken);
            kstOfficeRichardson.Kostenstellenbezeichnung = "Office Richardson";
            kstOfficeRichardson.GueltigAb = new(1991, 2, 1);
            kstOfficeRichardson.ZugehoerigeKostenstellenart = kstArtOffice;

            Kostenstelle currentKostenstelle;

            foreach (string currentEmployeeName in new String[] { "John Carmack", "John Romero", "Adrian Carmack", "Tom Hall" })
            { 
            currentKostenstelle = await doePaAdminService.CreateKostenstelleAsync(cancellationToken);
            currentKostenstelle.Kostenstellenbezeichnung = currentEmployeeName;
            currentKostenstelle.ZugehoerigeKostenstellenart = kstArtMitarbeiter;
            currentKostenstelle.GueltigAb = new(1991, 2, 1);
            currentKostenstelle.UebergeordneteKostenstellen.Add(kstOfficeRichardson);
            kstOfficeRichardson.UntergeordneteKostenstellen.Add(currentKostenstelle);
            }

        }

    }
}
