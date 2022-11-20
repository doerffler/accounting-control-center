using DoePaAdmin.ViewModel.Model;
using DoePaAdminDataAdapter.DoePaAdmin;
using DoePaAdminDataModel.API;
using DoePaAdminDataModel.Kostenrechnung;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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

        public async Task<IEnumerable<EmployeeInvoicedHours>> GetEmployeeInvoicedHoursAsync(string email, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            var result = DBContext
                .Ausgangsrechnungspositionen
                        .Include(arp => arp.ZugehoerigeRechnung)
                        .Include(arp => arp.ZugehoerigeKostenstelle)
                        .Include(arp => arp.ZugehoerigeAuftragsposition)
                            .ThenInclude(zap => zap.Auftrag)
                                .ThenInclude(a => a.ZugehoerigesProjekt)
                                    .ThenInclude(p => p.Rechnungsempfaenger)
                                        .ThenInclude(r => r.ZugehoerigerKunde)
                        .Include(arp => arp.ZugehoerigeAuftragsposition.Auftrag.VerantwortlicherMitarbeiter)
                        .Include(arp => arp.ZugehoerigeAbrechnungseinheit)
                    .Where(arp => arp.ZugehoerigeAuftragsposition.Auftrag.VerantwortlicherMitarbeiter.Email == email)
                    .Where(arp => arp.ZugehoerigeAbrechnungseinheit.Name.Equals("Stunden"))
                    .Where(arp => arp.LeistungszeitraumVon >= from)
                    .Where(arp => arp.LeistungszeitraumBis <= to)
                    .ToList()
                    .GroupBy(arp => arp.ZugehoerigeAuftragsposition.Auftrag.ZugehoerigesProjekt)
                    .Select(project => new EmployeeInvoicedHours {
                        Month = string.Format("{0}-{1}", to.Year, to.Month),
                        Project = project.Key.Projektname,
                        Customer = project.Key.Rechnungsempfaenger.ZugehoerigerKunde.Kundenname,
                        HoursCount = (double)project.Sum(p => p.Stueckzahl),
                    });

            return result;
        }

        #endregion

        #region Kunde

        public async Task<IEnumerable<Kunde>> GetKundenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Kunden, cancellationToken);
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
            return await GetDataFromDbSetAsync(DBContext.Projekte
                .Include(p => p.Skills)
                .Include(a => a.ZugehoerigeAuftraege)
                .ThenInclude(ap => ap.Auftragspositionen)
                .Include(p => p.Rechnungsempfaenger)
                .ThenInclude(re => re.ZugehoerigerKunde), cancellationToken);
        }
        
        public async Task<Projekt> CreateProjektAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Projekte, cancellationToken);
        }

        public async Task<Skill> CreateSkillAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Skills, cancellationToken);
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync(CancellationToken cancellationToken = default)
        {
            var result = await GetDataFromDbSetAsync(DBContext.Skills, cancellationToken);
            return result.Where(s => s.ParentSkill == null);
        }

        public void RemoveSkill(Skill skillToRemove)
        {
            _ = DBContext.Skills.Remove(skillToRemove);
        }


        #endregion

        #region Ausgangsrechnungen

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

        public async Task<Ausgangsrechnungsposition> CreateAusgangsrechnungspositionAsync(CancellationToken cancellationToken = default)
        {

            Ausgangsrechnungsposition newAusgangsrechnungsposition = await AddDataToDbSetAsync(DBContext.Ausgangsrechnungspositionen, cancellationToken);

            return newAusgangsrechnungsposition;

        }

        #endregion

        #region Geschäftspartner

        public async Task<IEnumerable<T>> GetGeschaeftspartnerAsync<T>(CancellationToken cancellationToken = default) where T : Geschaeftspartner
        {

            IQueryable result;

            if (typeof(T) == typeof(Debitor))
            {
                result = DBContext.Debitoren.Include(d => d.ZugehoerigerKunde);
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

        public void RemoveAbrechnungseinheit(Abrechnungseinheit selectedAbrechnungseinheit)
        {
            _ = DBContext.Abrechnungseinheiten.Remove(selectedAbrechnungseinheit);
        }

        public async Task<IEnumerable<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Geschaeftsjahre, cancellationToken);
        }

        public async Task<Geschaeftsjahr> CreateGeschaeftsjahrAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Geschaeftsjahre, cancellationToken);
        }

        public void RemoveGeschaeftsjahr(Geschaeftsjahr geschaeftsjahrToRemove)
        {
            _ = DBContext.Geschaeftsjahre.Remove(geschaeftsjahrToRemove);
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

        public async Task<Feiertag> CreateFeiertagAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Feiertage, cancellationToken);
        }

        public async Task<IEnumerable<Feiertag>> GetFeiertageAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Feiertage, cancellationToken);
        }

        public void RemoveFeiertag(Feiertag feiertagToRemove)
        {
            _ = DBContext.Feiertage.Remove(feiertagToRemove);
        }

        public void RemoveKunde(Kunde selectedKunde)
        {
            _ = DBContext.Kunden.Remove(selectedKunde);
        }

        public void RemoveWaehrung(Waehrung selectedWaehrung)
        {
            _ = DBContext.Waehrungen.Remove(selectedWaehrung);
        }

        public void RemovePostleitzahl(Postleitzahl selectedPostleitzahl)
        {
            _ = DBContext.Postleitzahlen.Remove(selectedPostleitzahl);
        }

        public async Task<IEnumerable<Adresse>> GetAdressenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Adressen, cancellationToken);
        }

        public void RemoveAdresse(Adresse selectedAdresse)
        {
            _ = DBContext.Adressen.Remove(selectedAdresse);
        }

        public void RemoveKostenstellenart(Kostenstellenart selectedKostenstellenart)
        {
            _ = DBContext.Kostenstellenarten.Remove(selectedKostenstellenart);
        }

        public void RemoveTaetigkeit(Taetigkeit selectedTaetigkeit)
        {
            _ = DBContext.Taetigkeiten.Remove(selectedTaetigkeit);
        }


        #endregion

        #region Utility functions

        public async Task<bool> CheckForChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => DBContext.ChangeTracker.HasChanges(), cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await DBContext.SaveChangesAsync(cancellationToken);
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
