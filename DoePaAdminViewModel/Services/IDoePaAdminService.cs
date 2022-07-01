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
    public interface IDoePaAdminService
    {
        #region Kostenstelle
        public Task<IEnumerable<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default);
        public Task<Kostenstelle> CreateKostenstelleAsync(CancellationToken cancellationToken = default);
        public void RemoveKostenstelle(Kostenstelle kostenstelleToRemove);
        public Task<Kostenstellenart> CreateKostenstellenartAsync(CancellationToken cancellationToken = default);
        #endregion 

        #region Mitarbeiter
        public Task<IEnumerable<Mitarbeiter>> GetMitarbeiterAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Taetigkeit>> GetTaetigkeitenAsync(CancellationToken cancellationToken = default);
        public Task<Mitarbeiter> CreateMitarbeiterAsync(CancellationToken cancellationToken = default);
        Task<Taetigkeit> CreateTaetigkeitAsync(CancellationToken cancellationToken = default);
        Task<Anstellungsdetail> CreateAnstellungsdetailAsync(CancellationToken cancellationToken = default);
        public void RemoveMitarbeiter(Mitarbeiter mitarbeiterToRemove);
        #endregion

        #region Kunde
        public Task<Kunde> CreateKundeAsync(CancellationToken cancellationToken = default);
        #endregion

        #region Projekt
        public Task<Projekt> CreateProjektAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Projekt>> GetProjekteAsync(CancellationToken cancellationToken = default);
        #endregion

        #region Auftrag

        public Task<IEnumerable<Kunde>> GetKundeAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Auftrag>> GetAuftraegeAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Auftragsposition>> GetAuftragspositionAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Abrechnungseinheit>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default);

        public Task<Auftrag> CreateAuftragAsync(CancellationToken cancellationToken = default);

        public Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Ausgangsrechnungen

        public Task<IEnumerable<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Ausgangsrechnung>> GetAusgangsrechnungenAsync(CancellationToken cancellationToken = default);

        public Task<Ausgangsrechnung> CreateAusgangsrechnungAsync(CancellationToken cancellationToken = default);

        public void RemoveAusgangsrechnung(Ausgangsrechnung ausgangsrechnungToRemove);

        public Task<Waehrung> CreateWaehrungAsync(CancellationToken cancellationToken = default);

        public Task<Geschaeftsjahr> CreateGeschaeftsjahrAsync(CancellationToken cancellationToken = default);

        #endregion

        public Task<bool> CheckForChangesAsync(CancellationToken cancellationToken = default);

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
        
    }
}
