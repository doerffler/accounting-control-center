using DoePaAdmin.ViewModel.Model;
using DoePaAdminDataAdapter.DoePaAdmin;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel.Services
{
    public class DoePaAdminService : IDoePaAdminService
    {

        private string DoePaAdminConnectionString { get; set; }

        private DPAdminDataContext DBContext { get; set; }

        public DoePaAdminService(IOptions<DoePaAdminConnectionSettings> doePaAdminConnectionSettings)
        {
            DoePaAdminConnectionString = doePaAdminConnectionSettings.Value.ConnectionString;

            DPAdminDataContext dbContext = new()
            {
                ConnectionString = DoePaAdminConnectionString
            };

            if (dbContext.Database.EnsureCreated())
            {
                _ = Task.Run(async () => await dbContext.InitializeMasterdataTablesAsync());
            }

            DBContext = dbContext;
        }

        #region Kostenstellen

        public async Task<ObservableCollection<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Kostenstelle> result = DBContext.Kostenstellen.Include(k => k.UebergeordneteKostenstellen);
            Task<List<Kostenstelle>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Kostenstelle> listKostenstellen = await taskToListAsync;
            ObservableCollection<Kostenstelle> kostenstellen = new(listKostenstellen);

            return kostenstellen;
        }

        public async Task<ObservableCollection<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Kostenstellenart> result = DBContext.Kostenstellenarten;
            Task<List<Kostenstellenart>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Kostenstellenart> listKostenstellenarten = await taskToListAsync;
            ObservableCollection<Kostenstellenart> kostenstellenarten = new(listKostenstellenarten);

            return kostenstellenarten;
        }

        public async Task<Kostenstelle> CreateKostenstelleAsync(CancellationToken cancellationToken = default)
        {
            Kostenstelle newKostenstelle = new();
            newKostenstelle.GueltigAb = DateTime.MinValue;
            _ = await DBContext.Kostenstellen.AddAsync(newKostenstelle, cancellationToken);
            return newKostenstelle;
        }

        public async Task<Kostenstellenart> CreateKostenstellenartAsync(CancellationToken cancellationToken = default)
        {
            Kostenstellenart newKostenstellenart = new();
            _ = await DBContext.Kostenstellenarten.AddAsync(newKostenstellenart, cancellationToken);
            return newKostenstellenart;
        }

        public void RemoveKostenstelle(Kostenstelle kostenstelleToRemove)
        {
            _ = DBContext.Kostenstellen.Remove(kostenstelleToRemove);
        }

        #endregion

        #region Mitarbeiter

        public async Task<ObservableCollection<Mitarbeiter>> GetMitarbeiterAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Mitarbeiter> result = DBContext.Mitarbeiter.Include(d => d.Anstellungshistorie).ThenInclude(t => t.ZugehoerigeTaetigkeit);
            Task<List<Mitarbeiter>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Mitarbeiter> listMitarbeiter = await taskToListAsync;
            ObservableCollection<Mitarbeiter> mitarbeiter = new(listMitarbeiter);

            return mitarbeiter;
        }

        public async Task<ObservableCollection<Taetigkeit>> GetTaetigkeitenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Taetigkeit> result = DBContext.Taetigkeiten;
            Task<List<Taetigkeit>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Taetigkeit> listTaetigkeiten = await taskToListAsync;
            ObservableCollection<Taetigkeit> taetigkeiten = new(listTaetigkeiten);

            return taetigkeiten;
        }

        public async Task<Mitarbeiter> CreateMitarbeiterAsync(CancellationToken cancellationToken = default)
        {
            Mitarbeiter newMitarbeiter = new();
            _ = await DBContext.Mitarbeiter.AddAsync(newMitarbeiter, cancellationToken);
            return newMitarbeiter;
        }

        public void RemoveMitarbeiter(Mitarbeiter mitarbeiterToRemove)
        {
            _ = DBContext.Mitarbeiter.Remove(mitarbeiterToRemove);
        }

        #endregion

        #region Kunde
        public async Task<Kunde> CreateKundeAsync(CancellationToken cancellationToke = default)
        {
            Kunde newKunde = new();
            _ = await DBContext.Kunden.AddAsync(newKunde, cancellationToke);
            return newKunde;
        }
        public async Task<ObservableCollection<Kunde>> GetKundeAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Kunde> result = DBContext.Kunden.Include(a => a.Auftraege).ThenInclude(ap => ap.Auftragspositionen);
            Task<List<Kunde>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Kunde> listKunde = await taskToListAsync;
            ObservableCollection<Kunde> kunden = new(listKunde);

            return kunden;
        }

        public async Task<ObservableCollection<Auftrag>> GetAuftragAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Auftrag> result = DBContext.Auftraege;
            Task<List<Auftrag>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Auftrag> listAuftrag = await taskToListAsync;
            ObservableCollection<Auftrag> auftraege = new(listAuftrag);

            return auftraege;
        }

        public async Task<ObservableCollection<Auftragsposition>> GetAuftragspositionAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Auftragsposition> result = DBContext.Auftragspositionen;
            Task<List<Auftragsposition>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Auftragsposition> listAuftragsposition = await taskToListAsync;
            ObservableCollection<Auftragsposition> auftragspositionen = new(listAuftragsposition);

            return auftragspositionen;
        }

        #endregion

        #region Auftrag

        public async Task<Auftrag> CreateAuftragAsync(CancellationToken cancellationToke = default)
        {
            Auftrag newAuftrag = new();
            _ = await DBContext.Auftraege.AddAsync(newAuftrag, cancellationToke);
            return newAuftrag;
        }
        public async Task<ObservableCollection<Abrechnungseinheit>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Abrechnungseinheit> result = DBContext.Abrechnungseinheiten;
            Task<List<Abrechnungseinheit>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Abrechnungseinheit> listAbrechnungseinheiten = await taskToListAsync;
            ObservableCollection<Abrechnungseinheit> abrechnungseinheiten = new(listAbrechnungseinheiten);

            return abrechnungseinheiten;
        }
        #endregion

        #region Projekt
        public async Task<ObservableCollection<Projekt>> GetProjekteAsync(CancellationToken cancellationToken = default)
        {
            // hp: Lazy Loading shit
            IQueryable<Projekt> result = DBContext.Projekte.Include(p => p.Skills).Include(a => a.ZugehoerigeAuftraege);
            Task<List<Projekt>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Projekt> listProjekt = await taskToListAsync;
            ObservableCollection<Projekt> projekte = new(listProjekt);

            return projekte;
        }
        public async Task<ObservableCollection<Auftrag>> GetAlleAuftraegeAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Auftrag> result = DBContext.Auftraege;
            Task<List<Auftrag>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Auftrag> listAuftrag = await taskToListAsync;
            ObservableCollection<Auftrag> auftraege = new(listAuftrag);

            return auftraege;
        }

        public async Task<Projekt> CreateProjektAsync(CancellationToken cancellationToke = default)
        {
            Projekt newProjekt = new();
            _ = await DBContext.Projekte.AddAsync(newProjekt, cancellationToke);
            return newProjekt;
        }
        #endregion

        #region Ausgangsrechnungen

        public async Task<ObservableCollection<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Geschaeftsjahr> result = DBContext.Geschaeftsjahre;
            Task<List<Geschaeftsjahr>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Geschaeftsjahr> listGeschaeftsjahre = await taskToListAsync;
            ObservableCollection<Geschaeftsjahr> geschaeftsjahre = new(listGeschaeftsjahre);

            return geschaeftsjahre;
        }

        #endregion

        public async Task<bool> CheckForChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => DBContext.ChangeTracker.HasChanges(), cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _ = await DBContext.SaveChangesAsync(cancellationToken);
        }
        
    }
}
