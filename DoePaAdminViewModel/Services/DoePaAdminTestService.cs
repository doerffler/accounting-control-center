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

        private List<Waehrung> WaehrungenSet { get; set; } = new();

        private List<Geschaeftsjahr> GeschaeftsjahreSet { get; set; } = new();

        private List<Abrechnungseinheit> AbrechnungseinheitenSet { get; set; } = new();

        public DoePaAdminTestService()
        {

            Task.Run(async () => await DoePaAdminTestDataCreator.CreateCompleteTestDataAsync(this));

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

        public Task<IEnumerable<Waehrung>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Auftrag>> GetAlleAuftraegeAsync(CancellationToken cancellationToke = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Auftrag>> GetAuftraegeAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Auftragsposition>> GetAuftragspositionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ausgangsrechnung>> GetAusgangsrechnungenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(KostenstellenartenSet.AsEnumerable());
        }

        public Task<IEnumerable<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(KostenstellenSet.AsEnumerable());
        }

        public Task<IEnumerable<Kunde>> GetKundenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Mitarbeiter>> GetMitarbeiterAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(MitarbeiterSet.AsEnumerable());
        }

        public Task<IEnumerable<Projekt>> GetProjekteAsync(CancellationToken cancellationToke = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Taetigkeit>> GetTaetigkeitenAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(TaetigkeitenSet.AsEnumerable());
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

        public Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(CancellationToken cancellationToken = default)
        {
            Abrechnungseinheit newAbrechnungseinheit = new()
            {
                AbrechnungseinheitID = AbrechnungseinheitenSet.Max(ae => ae.AbrechnungseinheitID) + 1
            };

            AbrechnungseinheitenSet.Add(newAbrechnungseinheit);
            return Task.FromResult(newAbrechnungseinheit);
        }

        public Task<Waehrung> CreateWaehrungAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Geschaeftsjahr> CreateGeschaeftsjahrAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Auftragsposition> CreateAuftragspositionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Waehrung>> GetWaehrungenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Kreditor>> GetDebitorenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Kreditor> CreateKreditorAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void RemoveKreditor(Kreditor kreditorToRemove)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetGeschaeftspartnerAsync<T>(CancellationToken cancellationToken = default) where T : Geschaeftspartner
        {
            throw new NotImplementedException();
        }

        public Task<T> CreateGeschaeftspartnerAsync<T>(CancellationToken cancellationToken = default) where T : Geschaeftspartner, new()
        {
            throw new NotImplementedException();
        }

        public void RemoveGeschaeftspartner<T>(T debitorToRemove) where T : Geschaeftspartner
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Postleitzahl>> GetPostleitzahlenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Postleitzahl> CreatePostleitzahlAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Adresse> CreateAdresseAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Feiertag> CreateFeiertagAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Feiertag>> GetFeiertageAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void RemoveGeschaeftsjahr(Geschaeftsjahr selectedGeschaeftsjahr)
        {
            throw new NotImplementedException();
        }

        public void RemoveFeiertag(Feiertag selectedFeiertag)
        {
            throw new NotImplementedException();
        }

        public void RemoveKunde(Kunde selectedKunde)
        {
            throw new NotImplementedException();
        }

        public void RemoveWaehrung(Waehrung selectedWaehrung)
        {
            throw new NotImplementedException();
        }

        public void RemoveAbrechnungseinheit(Waehrung selectedAbrechnungseinheit)
        {
            throw new NotImplementedException();
        }

        public void RemovePostleitzahl(Postleitzahl selectedPostleitzahl)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Adresse>> GetAdressenAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void RemoveAdresse(Adresse selectedAdresse)
        {
            throw new NotImplementedException();
        }

        public void RemoveKostenstellenart(Kostenstellenart selectedKostenstellenart)
        {
            throw new NotImplementedException();
        }

        public void RemoveTaetigkeit(Taetigkeit selectedTaetigkeit)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Abrechnungseinheit>> IDoePaAdminService.GetAbrechnungseinheitenAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void RemoveAbrechnungseinheit(Abrechnungseinheit selectedAbrechnungseinheit)
        {
            throw new NotImplementedException();
        }
    }
}
