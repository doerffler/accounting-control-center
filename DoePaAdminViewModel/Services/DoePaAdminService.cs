using DoePaAdmin.ViewModel.Model;
using DoePaAdminDataAdapter.DoePaAdmin;
using DoePaAdminDataModel.Kostenrechnung;
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
                //_ = Task.Run(async () => await dbContext.InitializeMasterdataTablesAsync());
            }

            DBContext = dbContext;
        }

        #region Kostenstellen

        public async Task<IEnumerable<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Kostenstelle> result = DBContext.Kostenstellen.Include(k => k.UebergeordneteKostenstellen);
            Task<List<Kostenstelle>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Kostenstelle> listKostenstellen = await taskToListAsync;

            return listKostenstellen;
        }

        public async Task<IEnumerable<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Kostenstellenart> result = DBContext.Kostenstellenarten;
            Task<List<Kostenstellenart>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Kostenstellenart> listKostenstellenarten = await taskToListAsync;
            
            return listKostenstellenarten;
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

        public async Task<IEnumerable<Mitarbeiter>> GetMitarbeiterAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Mitarbeiter> result = DBContext.Mitarbeiter.Include(d => d.Anstellungshistorie).ThenInclude(t => t.ZugehoerigeTaetigkeit);
            Task<List<Mitarbeiter>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Mitarbeiter> listMitarbeiter = await taskToListAsync;
            
            return listMitarbeiter;
        }

        public async Task<IEnumerable<Taetigkeit>> GetTaetigkeitenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Taetigkeit> result = DBContext.Taetigkeiten;
            Task<List<Taetigkeit>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Taetigkeit> listTaetigkeiten = await taskToListAsync;
            
            return listTaetigkeiten;
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

        public async Task<Taetigkeit> CreateTaetigkeitAsync(CancellationToken cancellationToken = default)
        {
            Taetigkeit newTaetigkeit = new();
            _ = await DBContext.Taetigkeiten.AddAsync(newTaetigkeit, cancellationToken);
            return newTaetigkeit;
        }


        public async Task<Anstellungsdetail> CreateAnstellungsdetailAsync(CancellationToken cancellationToken = default)
        {
            Anstellungsdetail newAnstellungsdetail = new();
            _ = await DBContext.Anstellungsdetails.AddAsync(newAnstellungsdetail, cancellationToken);
            return newAnstellungsdetail;
        }

        #endregion

        #region Kunde
        public async Task<Kunde> CreateKundeAsync(CancellationToken cancellationToke = default)
        {
            Kunde newKunde = new();
            _ = await DBContext.Kunden.AddAsync(newKunde, cancellationToke);
            return newKunde;
        }
        public async Task<IEnumerable<Kunde>> GetKundeAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Kunde> result = DBContext.Kunden.Include(a => a.Auftraege).ThenInclude(ap => ap.Auftragspositionen);
            Task<List<Kunde>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Kunde> listKunden = await taskToListAsync;
            
            return listKunden;
        }

        #endregion

        #region Auftrag

        public async Task<IEnumerable<Auftrag>> GetAuftraegeAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Auftrag> result = DBContext.Auftraege;
            Task<List<Auftrag>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Auftrag> listAuftraege = await taskToListAsync;

            return listAuftraege;
        }

        public async Task<IEnumerable<Auftragsposition>> GetAuftragspositionAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Auftragsposition> result = DBContext.Auftragspositionen;
            Task<List<Auftragsposition>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Auftragsposition> listAuftragspositionen = await taskToListAsync;

            return listAuftragspositionen;
        }

        public async Task<Auftrag> CreateAuftragAsync(CancellationToken cancellationToke = default)
        {
            Auftrag newAuftrag = new();
            _ = await DBContext.Auftraege.AddAsync(newAuftrag, cancellationToke);
            return newAuftrag;
        }
        public async Task<IEnumerable<Abrechnungseinheit>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Abrechnungseinheit> result = DBContext.Abrechnungseinheiten;
            Task<List<Abrechnungseinheit>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Abrechnungseinheit> listAbrechnungseinheiten = await taskToListAsync;
            
            return listAbrechnungseinheiten;
        }
        #endregion

        #region Projekt
        public async Task<IEnumerable<Projekt>> GetProjekteAsync(CancellationToken cancellationToken = default)
        {
            // hp: Lazy Loading shit
            IQueryable<Projekt> result = DBContext.Projekte.Include(p => p.Skills).Include(a => a.ZugehoerigeAuftraege);
            Task<List<Projekt>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Projekt> listProjekt = await taskToListAsync;
            
            return listProjekt;
        }
        
        public async Task<Projekt> CreateProjektAsync(CancellationToken cancellationToke = default)
        {
            Projekt newProjekt = new();
            _ = await DBContext.Projekte.AddAsync(newProjekt, cancellationToke);
            return newProjekt;
        }
        #endregion

        #region Ausgangsrechnungen

        public async Task<IEnumerable<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Geschaeftsjahr> result = DBContext.Geschaeftsjahre;
            Task<List<Geschaeftsjahr>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Geschaeftsjahr> listGeschaeftsjahre = await taskToListAsync;
            
            return listGeschaeftsjahre;
        }

        public async Task<IEnumerable<Ausgangsrechnung>> GetAusgangsrechnungenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Ausgangsrechnung> result = DBContext.Ausgangsrechnungen.Include(ar => ar.Rechnungspositionen);
            Task<List<Ausgangsrechnung>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Ausgangsrechnung> listAusgangsrechnungen = await taskToListAsync;
            
            return listAusgangsrechnungen;
        }

        public async Task<Ausgangsrechnung> CreateAusgangsrechnungAsync(CancellationToken cancellationToken = default)
        {
            Ausgangsrechnung newAusgangsrechnung = new()
            {
                RechnungsDatum = DateTime.Now
            };

            _ = await DBContext.Ausgangsrechnungen.AddAsync(newAusgangsrechnung, cancellationToken);
            return newAusgangsrechnung;
        }

        public void RemoveAusgangsrechnung(Ausgangsrechnung ausgangsrechnungToRemove)
        {
            _ = DBContext.Ausgangsrechnungen.Remove(ausgangsrechnungToRemove);
        }

        public async Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(CancellationToken cancellationToken = default)
        {
            Abrechnungseinheit newAbrechnungseinheit = new();

            _ = await DBContext.Abrechnungseinheiten.AddAsync(newAbrechnungseinheit, cancellationToken);
            return newAbrechnungseinheit;
        }

        public async Task<Waehrung> CreateWaehrungAsync(CancellationToken cancellationToken = default)
        {
            Waehrung newWaehrung = new();

            _ = await DBContext.Waehrungen.AddAsync(newWaehrung, cancellationToken);
            return newWaehrung;
        }

        public async Task<Geschaeftsjahr> CreateGeschaeftsjahrAsync(CancellationToken cancellationToken = default)
        {
            Geschaeftsjahr newGeschaeftsjahr = new();

            _ = await DBContext.Geschaeftsjahre.AddAsync(newGeschaeftsjahr, cancellationToken);
            return newGeschaeftsjahr;
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
