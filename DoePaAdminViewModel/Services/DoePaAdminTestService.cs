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
    public class DoePaAdminTestService : IDoePaAdminService
    {

        private List<Kostenstelle> KostenstellenSet { get; set; }

        private List<Kostenstellenart> KostenstellenartenSet { get; set; }

        private List<Mitarbeiter> MitarbeiterSet { get; set; } = new();

        private List<Anstellungsdetail> AnstellungsdetailSet { get; set; } = new();

        private List<Taetigkeit> TaetigkeitenSet { get; set; } = new();

        public DoePaAdminTestService()
        {

            _ = CreateKostenstellenSet().ToList();
            _ = CreateMitarbeiterSet().ToList();

        }

        private IEnumerable<Kostenstelle> CreateKostenstellenSet()
        {
            Kostenstellenart kstArtMitarbeiter = Task.Run(async () => await CreateKostenstellenartAsync()).Result;
            kstArtMitarbeiter.Kostenstellenartbezeichnung = "Mitarbeiterkostenstelle";

            Kostenstellenart kstArtOffice = Task.Run(async () => await CreateKostenstellenartAsync()).Result;
            kstArtOffice.Kostenstellenartbezeichnung = "Office";

            Kostenstelle kstOfficeRichardson = Task.Run(async () => await CreateKostenstelleAsync()).Result;
            kstOfficeRichardson.Kostenstellenbezeichnung = "Office Richardson";
            kstOfficeRichardson.GueltigAb = new(1991, 2, 1);
            kstOfficeRichardson.ZugehoerigeKostenstellenart = kstArtOffice;

            Kostenstelle currentKostenstelle;

            currentKostenstelle = Task.Run(async () => await CreateKostenstelleAsync()).Result;
            currentKostenstelle.Kostenstellenbezeichnung = "John Carmack";
            currentKostenstelle.ZugehoerigeKostenstellenartId = kstArtMitarbeiter.KostenstellenartID;
            currentKostenstelle.ZugehoerigeKostenstellenart = kstArtMitarbeiter;
            currentKostenstelle.GueltigAb = new(1991, 2, 1);
            currentKostenstelle.UebergeordneteKostenstellen.Add(kstOfficeRichardson);
            kstOfficeRichardson.UntergeordneteKostenstellen.Add(currentKostenstelle);

            yield return currentKostenstelle;

            currentKostenstelle = Task.Run(async () => await CreateKostenstelleAsync()).Result;
            currentKostenstelle.Kostenstellenbezeichnung = "John Romero";
            currentKostenstelle.ZugehoerigeKostenstellenartId = kstArtMitarbeiter.KostenstellenartID;
            currentKostenstelle.ZugehoerigeKostenstellenart = kstArtMitarbeiter;
            currentKostenstelle.GueltigAb = new(1991, 2, 1);
            currentKostenstelle.UebergeordneteKostenstellen.Add(kstOfficeRichardson);
            kstOfficeRichardson.UntergeordneteKostenstellen.Add(currentKostenstelle);

            yield return currentKostenstelle;

            currentKostenstelle = Task.Run(async () => await CreateKostenstelleAsync()).Result;
            currentKostenstelle.Kostenstellenbezeichnung = "Adrian Carmack";
            currentKostenstelle.ZugehoerigeKostenstellenartId = kstArtMitarbeiter.KostenstellenartID;
            currentKostenstelle.ZugehoerigeKostenstellenart = kstArtMitarbeiter;
            currentKostenstelle.GueltigAb = new(1991, 2, 1);
            currentKostenstelle.UebergeordneteKostenstellen.Add(kstOfficeRichardson);
            kstOfficeRichardson.UntergeordneteKostenstellen.Add(currentKostenstelle);

            yield return currentKostenstelle;

            currentKostenstelle = Task.Run(async () => await CreateKostenstelleAsync()).Result;
            currentKostenstelle.Kostenstellenbezeichnung = "Tom Hall";
            currentKostenstelle.ZugehoerigeKostenstellenartId = kstArtMitarbeiter.KostenstellenartID;
            currentKostenstelle.ZugehoerigeKostenstellenart = kstArtMitarbeiter;
            currentKostenstelle.GueltigAb = new(1991, 2, 1);
            currentKostenstelle.UebergeordneteKostenstellen.Add(kstOfficeRichardson);
            kstOfficeRichardson.UntergeordneteKostenstellen.Add(currentKostenstelle);

            yield return currentKostenstelle;

        }

        private IEnumerable<Mitarbeiter> CreateMitarbeiterSet()
        {
            Taetigkeit taetGamedesigner = Task.Run(async () => await CreateTaetigkeitAsync()).Result;
            taetGamedesigner.Taetigkeitsbeschreibung = "Game Designer";

            Taetigkeit taetTechnicalDirector = Task.Run(async () => await CreateTaetigkeitAsync()).Result;
            taetTechnicalDirector.Taetigkeitsbeschreibung = "Technical Director";

            Taetigkeit taetArtist = Task.Run(async () => await CreateTaetigkeitAsync()).Result;
            taetArtist.Taetigkeitsbeschreibung = "Artist";

            List<Anstellungsdetail> currentAnstellungshistorie;
            Anstellungsdetail currentAnstellungsdetail;
            Mitarbeiter currentMitarbeiter;

            /*
             * John Carmack
             */
            currentAnstellungshistorie = new();

            currentMitarbeiter = Task.Run(async () => await CreateMitarbeiterAsync()).Result;
            currentMitarbeiter.Anrede = "Herr";
            currentMitarbeiter.Vorname = "John";
            currentMitarbeiter.Nachname = "Carmack";
            currentMitarbeiter.Geburtsdatum = new(1970, 8, 20);
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = "JOCA";
            currentMitarbeiter.PersonalnummerDatev = 1;
            currentMitarbeiter.ZugehoerigeKostenstelle = KostenstellenSet.Where(kst => kst.Kostenstellenbezeichnung.Equals("John Carmack")).First();

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 2, 1);
            currentAnstellungsdetail.Monatsgehalt = 250;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetTechnicalDirector;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 11, 30);
            currentAnstellungsdetail.Monatsgehalt = 2090;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetTechnicalDirector;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 0;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 0;
            currentAnstellungsdetail.GueltigAb = new(2013, 11, 22);
            currentAnstellungsdetail.Monatsgehalt = 0;
            currentAnstellungsdetail.IstGekuendigt = true;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = null;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            yield return currentMitarbeiter;

            /*
             * John Romero
             */
            currentAnstellungshistorie = new();

            currentMitarbeiter = Task.Run(async () => await CreateMitarbeiterAsync()).Result;
            currentMitarbeiter.Anrede = "Herr";
            currentMitarbeiter.Vorname = "John";
            currentMitarbeiter.Nachname = "Romero";
            currentMitarbeiter.Geburtsdatum = new(1967, 10, 28);
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = "JORO";
            currentMitarbeiter.PersonalnummerDatev = 2;
            currentMitarbeiter.ZugehoerigeKostenstelle = KostenstellenSet.Where(kst => kst.Kostenstellenbezeichnung.Equals("John Romero")).First();
            
            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 2, 1);
            currentAnstellungsdetail.Monatsgehalt = 250;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetGamedesigner;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 11, 30);
            currentAnstellungsdetail.Monatsgehalt = 2090;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetGamedesigner;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 0;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 0;
            currentAnstellungsdetail.GueltigAb = new(1996, 8, 6);
            currentAnstellungsdetail.Monatsgehalt = 0;
            currentAnstellungsdetail.IstGekuendigt = true;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = null;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            yield return currentMitarbeiter;

            /*
             * Tom Hall
             */
            currentAnstellungshistorie = new();

            currentMitarbeiter = Task.Run(async () => await CreateMitarbeiterAsync()).Result;
            currentMitarbeiter.Anrede = "Herr";
            currentMitarbeiter.Vorname = "Tom";
            currentMitarbeiter.Nachname = "Hall";
            currentMitarbeiter.Geburtsdatum = DateTime.MinValue;
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = "TOHA";
            currentMitarbeiter.PersonalnummerDatev = 3;
            currentMitarbeiter.ZugehoerigeKostenstelle = KostenstellenSet.Where(kst => kst.Kostenstellenbezeichnung.Equals("Tom Hall")).First();

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 2, 1);
            currentAnstellungsdetail.Monatsgehalt = 250;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetGamedesigner;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 11, 30);
            currentAnstellungsdetail.Monatsgehalt = 2090;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetGamedesigner;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 0;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 0;
            currentAnstellungsdetail.GueltigAb = new(1993, 8, 1);
            currentAnstellungsdetail.Monatsgehalt = 0;
            currentAnstellungsdetail.IstGekuendigt = true;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = null;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            yield return currentMitarbeiter;

            /*
             * Adrian Carmack
             */
            currentAnstellungshistorie = new();

            currentMitarbeiter = Task.Run(async () => await CreateMitarbeiterAsync()).Result;
            currentMitarbeiter.Anrede = "Herr";
            currentMitarbeiter.Vorname = "Adrian";
            currentMitarbeiter.Nachname = "Carmack";
            currentMitarbeiter.Geburtsdatum = new(1969, 5, 5);
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = "ADCA";
            currentMitarbeiter.PersonalnummerDatev = 4;
            currentMitarbeiter.ZugehoerigeKostenstelle = KostenstellenSet.Where(kst => kst.Kostenstellenbezeichnung.Equals("Adrian Carmack")).First();

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 2, 1);
            currentAnstellungsdetail.Monatsgehalt = 250;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetArtist;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 12;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 40;
            currentAnstellungsdetail.GueltigAb = new(1991, 11, 30);
            currentAnstellungsdetail.Monatsgehalt = 2090;
            currentAnstellungsdetail.IstGekuendigt = false;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = taetArtist;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            currentAnstellungsdetail = Task.Run(async () => await CreateAnstellungsdetailAsync()).Result;
            currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
            currentAnstellungsdetail.AnzahlMonatsgehaelter = 0;
            currentAnstellungsdetail.AnzahlArbeitsstunden = 0;
            currentAnstellungsdetail.GueltigAb = new(2005, 1, 1);
            currentAnstellungsdetail.Monatsgehalt = 0;
            currentAnstellungsdetail.IstGekuendigt = true;
            currentAnstellungsdetail.ZugehoerigeTaetigkeit = null;

            currentAnstellungshistorie.Add(currentAnstellungsdetail);

            yield return currentMitarbeiter;

        }

        public Task<bool> CheckForChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Anstellungsdetail> CreateAnstellungsdetailAsync(CancellationToken cancellationToken = default)
        {
            Anstellungsdetail newDetail = new()
            {
                AnstellungsdetailID = AnstellungsdetailSet.Max(ad => ad.AnstellungsdetailID) + 1
            };

            AnstellungsdetailSet.Add(newDetail);
            return Task.FromResult(newDetail);
        }

        public Task<Taetigkeit> CreateTaetigkeitAsync(CancellationToken cancellationToken = default)
        {
            Taetigkeit newTaetigkeit = new()
            {
                TaetigkeitID = TaetigkeitenSet.Max(t => t.TaetigkeitID) + 1
            };

            TaetigkeitenSet.Add(newTaetigkeit);
            return Task.FromResult(newTaetigkeit);
        }

        public Task<Auftrag> CreateAuftragAsync(CancellationToken cancellationToke = default)
        {
            throw new NotImplementedException();
        }

        public Task<Ausgangsrechnung> CreateAusgangsrechnungAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Kostenstelle> CreateKostenstelleAsync(CancellationToken cancellationToken = default)
        {
            Kostenstelle newKostenstelle = new()
            {
                KostenstelleID = KostenstellenSet.Max(k => k.KostenstelleID) + 1
            };

            KostenstellenSet.Add(newKostenstelle);
            return Task.FromResult(newKostenstelle);
        }

        public Task<Kostenstellenart> CreateKostenstellenartAsync(CancellationToken cancellationToken = default)
        {
            Kostenstellenart newKostenstellenart = new()
            {
                KostenstellenartID = KostenstellenartenSet.Max(k => k.KostenstellenartID) + 1
            };

            KostenstellenartenSet.Add(newKostenstellenart);
            return Task.FromResult(newKostenstellenart);
        }

        public Task<Kunde> CreateKundeAsync(CancellationToken cancellationToke = default)
        {
            throw new NotImplementedException();
        }

        public Task<Mitarbeiter> CreateMitarbeiterAsync(CancellationToken cancellationToken = default)
        {
            Mitarbeiter newMitarbeiter = new()
            {
                MitarbeiterID = MitarbeiterSet.Max(ma => ma.MitarbeiterID) + 1
            };

            MitarbeiterSet.Add(newMitarbeiter);
            return Task.FromResult(newMitarbeiter);
        }

        public Task<Projekt> CreateProjektAsync(CancellationToken cancellationToke = default)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Abrechnungseinheit>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Auftrag>> GetAlleAuftraegeAsync(CancellationToken cancellationToke = default)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Auftrag>> GetAuftragAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Auftragsposition>> GetAuftragspositionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Ausgangsrechnung>> GetAusgangsrechnungenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ObservableCollection<Kostenstellenart>(KostenstellenartenSet));
        }

        public Task<ObservableCollection<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ObservableCollection<Kostenstelle>(KostenstellenSet));
        }

        public Task<ObservableCollection<Kunde>> GetKundeAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Mitarbeiter>> GetMitarbeiterAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ObservableCollection<Mitarbeiter>(MitarbeiterSet));
        }

        public Task<ObservableCollection<Projekt>> GetProjekteAsync(CancellationToken cancellationToke = default)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Taetigkeit>> GetTaetigkeitenAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new ObservableCollection<Taetigkeit>(TaetigkeitenSet));
        }

        public void RemoveAusgangsrechnung(Ausgangsrechnung ausgangsrechnungToRemove)
        {
            throw new NotImplementedException();
        }

        public void RemoveKostenstelle(Kostenstelle kostenstelleToRemove)
        {
            KostenstellenSet.Remove(kostenstelleToRemove);
        }

        public void RemoveMitarbeiter(Mitarbeiter mitarbeiterToRemove)
        {
            MitarbeiterSet.Remove(mitarbeiterToRemove);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

    }
}
