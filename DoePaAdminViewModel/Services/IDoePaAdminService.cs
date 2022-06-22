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
        public Task<ObservableCollection<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default);
        public Task<ObservableCollection<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default);
        public Task<Kostenstelle> CreateKostenstelleAsync(CancellationToken cancellationToken = default);
        public void RemoveKostenstelle(Kostenstelle kostenstelleToRemove);
        public Task<Kostenstellenart> CreateKostenstellenartAsync(CancellationToken cancellationToken = default);
        #endregion 

        #region Mitarbeiter
        public Task<ObservableCollection<Mitarbeiter>> GetMitarbeiterAsync(CancellationToken cancellationToken = default);
        public Task<ObservableCollection<Taetigkeit>> GetTaetigkeitenAsync(CancellationToken cancellationToken = default);
        public Task<Mitarbeiter> CreateMitarbeiterAsync(CancellationToken cancellationToken = default);
        public void RemoveMitarbeiter(Mitarbeiter mitarbeiterToRemove);
        #endregion

        #region Kunde
        public Task<Kunde> CreateKundeAsync(CancellationToken cancellationToke = default);
        public Task<Auftrag> CreateAuftragAsync(CancellationToken cancellationToke = default);
        #endregion

        #region Projekt
        public Task<Projekt> CreateProjektAsync(CancellationToken cancellationToke = default);
        public Task<ObservableCollection<Projekt>> GetProjekteAsync(CancellationToken cancellationToke = default);
        public Task<ObservableCollection<Auftrag>> GetAlleAuftraegeAsync(CancellationToken cancellationToke = default);
        #endregion

        #region Auftrag
        public Task<ObservableCollection<Kunde>> GetKundeAsync(CancellationToken cancellationToken = default);
        public Task<ObservableCollection<Auftrag>> GetAuftragAsync(CancellationToken cancellationToken = default);
        public Task<ObservableCollection<Auftragsposition>> GetAuftragspositionAsync(CancellationToken cancellationToken = default);
        public Task<ObservableCollection<Abrechnungseinheit>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default);
        #endregion

        #region Ausgangsrechnungen

        public Task<ObservableCollection<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default);

        #endregion

        public Task<bool> CheckForChangesAsync(CancellationToken cancellationToken = default);

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
