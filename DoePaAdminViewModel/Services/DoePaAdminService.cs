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
            return await GetDataFromDbSetAsync(DBContext.Kostenstellen.Include(k => k.UebergeordneteKostenstellen), cancellationToken);
        }

        public async Task<Kostenstelle> CreateKostenstelleAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Kostenstellen, cancellationToken);
        }

        public void RemoveKostenstelle(Kostenstelle kostenstelleToRemove)
        {
            _ = DBContext.Kostenstellen.Remove(kostenstelleToRemove);
        }

        public async Task<IEnumerable<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Kostenstellenarten, cancellationToken);
        }

        public async Task<Kostenstellenart> CreateKostenstellenartAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Kostenstellenarten, cancellationToken);
        }
        
        #endregion

        #region Mitarbeiter

        public async Task<IEnumerable<Mitarbeiter>> GetMitarbeiterAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(
                DBContext.Mitarbeiter
                    .Include(d => d.Anstellungshistorie)
                        .ThenInclude(t => t.ZugehoerigeTaetigkeit)
                    .Include(m => m.ZugehoerigeAdresse)
                        .ThenInclude(a => a.ZugehoerigePostleitzahl)
                , cancellationToken);
        }

        public async Task<Mitarbeiter> CreateMitarbeiterAsync(CancellationToken cancellationToken = default)
        {
            Mitarbeiter newMitarbeiter = await AddDataToDbSetAsync(DBContext.Mitarbeiter, cancellationToken);
            newMitarbeiter.ZugehoerigeAdresse = await CreateAdresseAsync(cancellationToken);

            return newMitarbeiter;
        }

        public void RemoveMitarbeiter(Mitarbeiter mitarbeiterToRemove)
        {
            _ = DBContext.Mitarbeiter.Remove(mitarbeiterToRemove);
        }

        public async Task<IEnumerable<Taetigkeit>> GetTaetigkeitenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Taetigkeiten, cancellationToken);
        }
        
        public async Task<Taetigkeit> CreateTaetigkeitAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Taetigkeiten, cancellationToken);
        }

        public async Task<Anstellungsdetail> CreateAnstellungsdetailAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Anstellungsdetails, cancellationToken);
        }

        #endregion

        #region Kunde

        public async Task<IEnumerable<Kunde>> GetKundeAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Kunden.Include(a => a.Auftraege).ThenInclude(ap => ap.Auftragspositionen), cancellationToken);
        }

        public async Task<Kunde> CreateKundeAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Kunden, cancellationToken);
        }
        
        #endregion

        #region Auftrag

        public async Task<IEnumerable<Auftrag>> GetAuftraegeAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Auftraege, cancellationToken);
        }

        public async Task<Auftrag> CreateAuftragAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Auftraege, cancellationToken);
        }

        public async Task<IEnumerable<Auftragsposition>> GetAuftragspositionAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Auftragspositionen, cancellationToken);
        }

        public async Task<Auftragsposition> CreateAuftragspositionAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Auftragspositionen, cancellationToken);
        }
        
        #endregion

        #region Projekt
        public async Task<IEnumerable<Projekt>> GetProjekteAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Projekte.Include(p => p.Skills).Include(a => a.ZugehoerigeAuftraege), cancellationToken);
        }
        
        public async Task<Projekt> CreateProjektAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Projekte, cancellationToken);
        }
        #endregion

        #region Ausgangsrechnungen

        public async Task<IEnumerable<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Geschaeftsjahre, cancellationToken);
        }

        public async Task<IEnumerable<Ausgangsrechnung>> GetAusgangsrechnungenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Ausgangsrechnungen.Include(ar => ar.Rechnungspositionen), cancellationToken);
        }

        public async Task<Ausgangsrechnung> CreateAusgangsrechnungAsync(CancellationToken cancellationToken = default)
        {
            
            Ausgangsrechnung newAusgangsrechnung = await AddDataToDbSetAsync(DBContext.Ausgangsrechnungen, cancellationToken);
            newAusgangsrechnung.RechnungsDatum = DateTime.Now;

            return newAusgangsrechnung;

        }

        public void RemoveAusgangsrechnung(Ausgangsrechnung ausgangsrechnungToRemove)
        {
            _ = DBContext.Ausgangsrechnungen.Remove(ausgangsrechnungToRemove);
        }



        #endregion

        #region Geschäftspartner

        public async Task<IEnumerable<T>> GetGeschaeftspartnerAsync<T>(CancellationToken cancellationToken = default) where T : Geschaeftspartner
        {

            IQueryable result;

            if (typeof(T) == typeof(Debitor))
            {
                result = DBContext.Debitoren;
            }
            else if (typeof(T) == typeof(Kreditor))
            {
                result = DBContext.Kreditoren;
            }
            else
            {
                return null;
            }

            Task<List<T>> taskToListAsync = result.Cast<T>().ToListAsync(cancellationToken);
            List<T> listGeschaeftspartner = await taskToListAsync;

            return listGeschaeftspartner;

        }

        public async Task<T> CreateGeschaeftspartnerAsync<T>(CancellationToken cancellationToken = default) where T : Geschaeftspartner, new()
        {
            T newGeschaeftspartner = new();
            _ = await DBContext.AddAsync<T>(newGeschaeftspartner, cancellationToken);

            newGeschaeftspartner.ZugehoerigeAdresse = await CreateAdresseAsync(cancellationToken);

            return newGeschaeftspartner;
        }

        public void RemoveGeschaeftspartner<T>(T geschaeftspartnerToRemove) where T : Geschaeftspartner
        {
            _ = DBContext.Remove<T>(geschaeftspartnerToRemove);
        }

        #endregion

        #region Masterdata

        public async Task<IEnumerable<Waehrung>> GetWaehrungenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Waehrungen, cancellationToken);
        }

        public async Task<Waehrung> CreateWaehrungAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Waehrungen, cancellationToken);
        }

        public async Task<IEnumerable<Abrechnungseinheit>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Abrechnungseinheiten, cancellationToken);
        }

        public async Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Abrechnungseinheiten, cancellationToken);
        }

        public async Task<Geschaeftsjahr> CreateGeschaeftsjahrAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Geschaeftsjahre, cancellationToken);
        }
        
        public async Task<IEnumerable<Postleitzahl>> GetPostleitzahlenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Postleitzahlen, cancellationToken);
        }

        public async Task<Postleitzahl> CreatePostleitzahlAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Postleitzahlen, cancellationToken);
        }

        public async Task<Adresse> CreateAdresseAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Adressen, cancellationToken);
        }

        #endregion

        #region Utility functions

        public async Task<bool> CheckForChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => DBContext.ChangeTracker.HasChanges(), cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _ = await DBContext.SaveChangesAsync(cancellationToken);
        }

        private async Task<IEnumerable<T>> GetDataFromDbSetAsync<T>(IQueryable<T> dbQuery, CancellationToken cancellationToken) where T : class
        {
            Task<List<T>> taskToListAsync = dbQuery.ToListAsync(cancellationToken);
            List<T> listData = await taskToListAsync;

            return listData;
        }

        private async Task<T> AddDataToDbSetAsync<T>(DbSet<T> dbSet, CancellationToken cancellationToken) where T : class, new()
        {
            T newItem = new();
            _ = await dbSet.AddAsync(newItem, cancellationToken);
            return newItem;
        }

        #endregion

    }
}
