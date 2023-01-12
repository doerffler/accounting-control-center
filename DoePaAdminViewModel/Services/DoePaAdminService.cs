using DoePaAdmin.ViewModel.Model;
using DoePaAdminDataAdapter.DoePaAdmin;
using DoePaAdminDataModel.DTO;
using DoePaAdminDataModel.Kostenrechnung;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Kostenstelle> CreateKostenstelleAsync(string kostenstellenBezeichnung, int kostenstellenNummer, Kostenstellenart kostenstellenArt, CancellationToken cancellationToken = default)
        {
            Kostenstelle newKostenstelle = await CreateKostenstelleAsync(cancellationToken);
            newKostenstelle.Kostenstellenbezeichnung = kostenstellenBezeichnung;
            newKostenstelle.KostenstellenNummer = kostenstellenNummer;
            newKostenstelle.ZugehoerigeKostenstellenart = kostenstellenArt;

            return newKostenstelle;
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

        public async Task<Kostenstellenart> CreateKostenstellenartAsync(string bezeichnung, CancellationToken cancellationToken = default)
        {
            Kostenstellenart newKostenstellenart = await CreateKostenstellenartAsync(cancellationToken);
            newKostenstellenart.Kostenstellenartbezeichnung = bezeichnung;

            return newKostenstellenart;
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

        public async Task<Taetigkeit> CreateTaetigkeitAsync(string beschreibung, CancellationToken cancellationToken = default)
        {
            Taetigkeit newTaetigkeit = await CreateTaetigkeitAsync(cancellationToken);
            newTaetigkeit.Taetigkeitsbeschreibung = beschreibung;

            return newTaetigkeit;
        }

        public async Task<Anstellungsdetail> CreateAnstellungsdetailAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Anstellungsdetails, cancellationToken);
        }

        public async Task<IEnumerable<EmployeeAccountingDTO>> GetEmployeeAccountingAsync(string email, DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {

            Kostenstelle maKostenstelle = DBContext.Mitarbeiter.Where(ma => ma.Email == email).Select(ma => ma.ZugehoerigeKostenstelle).First();

            IQueryable<Ausgangsrechnungsposition> query = DBContext.Ausgangsrechnungspositionen
                            .Include(arp => arp.ZugehoerigeAuftragsposition)
                                .ThenInclude(zap => zap.Auftrag)
                                    .ThenInclude(a => a.ZugehoerigesProjekt)
                                        .ThenInclude(p => p.Rechnungsempfaenger)
                                            .ThenInclude(r => r.ZugehoerigerKunde)
                            .Include(arp => arp.ZugehoerigeAbrechnungseinheit)
                            .Where(arp => arp.ZugehoerigeKostenstelle == maKostenstelle && arp.LeistungszeitraumBis >= from && arp.LeistungszeitraumBis <= to);

            IEnumerable<Ausgangsrechnungsposition> queryData = await GetDataFromDbSetAsync(query, cancellationToken);

            IEnumerable<EmployeeAccountingDTO> dtoObject = queryData
                            .GroupBy(arp => new { arp.ZugehoerigeAuftragsposition.Auftrag.ZugehoerigesProjekt, arp.ZugehoerigeAbrechnungseinheit, arp.LeistungszeitraumBis.Year, arp.LeistungszeitraumBis.Month })
                            .Select(project => new EmployeeAccountingDTO {
                                Month = $"{project.Key.Year}-{project.Key.Month}",
                                Project = project.Key.ZugehoerigesProjekt.Projektname,
                                Customer = project.Key.ZugehoerigesProjekt.Rechnungsempfaenger.ZugehoerigerKunde.Kundenname,
                                AccountingCount = project.Sum(p => p.Stueckzahl),
                                AccountingUnitName = project.Key.ZugehoerigeAbrechnungseinheit.Name
                            });

            return dtoObject;

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

        public async Task<Kunde> CreateKundeAsync(string kundenname, string kundennameLang = null, CancellationToken cancellationToken = default)
        {
            Kunde newKunde = await CreateKundeAsync(cancellationToken);
            newKunde.Kundenname = kundenname;
            newKunde.Langname = !string.IsNullOrEmpty(kundennameLang) ? kundennameLang : kundenname;

            return newKunde;
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

        public async Task<IEnumerable<Auftragsposition>> GetAuftragspositionenAsync(CancellationToken cancellationToken = default)
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

        public async Task<Skill> CreateSkillAsync(string skillName, CancellationToken cancellationToken = default)
        {
            Skill newSkill = await CreateSkillAsync(cancellationToken);
            newSkill.SkillName = skillName;

            return newSkill;
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Skills, cancellationToken);
        }

        public async Task<IEnumerable<Skill>> GetSkillTreeAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Skill> result = await GetDataFromDbSetAsync(DBContext.Skills, cancellationToken);
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
            return await GetDataFromDbSetAsync(
                DBContext.Ausgangsrechnungen
                .Include(ar => ar.ZugehoerigeWaehrung)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeAbrechnungseinheit)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeKostenstelle)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeAuftragsposition)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeFremdleistungen)
                , cancellationToken);
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

        public async Task<IEnumerable<RemainingBudgetOnOrdersDTO>> GetRemainingBudgetOnOrdersAsync(int AuftragspositionID, CancellationToken cancellationToken = default)
        {

            //TODO: I tend to move this to our DTOFactory introduced today.
            IQueryable<Ausgangsrechnungsposition> query = DBContext.Ausgangsrechnungen
                .Include(ar => ar.Rechnungspositionen)
                .SelectMany(rechnung => rechnung.Rechnungspositionen)
                .Where(arp => arp.ZugehoerigeAuftragsposition.AuftragspositionID == AuftragspositionID);

            IEnumerable<Ausgangsrechnungsposition> queryData = await GetDataFromDbSetAsync(query, cancellationToken);

            IEnumerable<RemainingBudgetOnOrdersDTO> dtoObject = queryData
                                .GroupBy(arp => arp.LeistungszeitraumBis)
                                .Select(arp => new RemainingBudgetOnOrdersDTO
                                {
                                    Date = arp.Key.Date,
                                    Remaining = arp.Sum(rb => rb.Stueckzahl)
                                }).ToList();

            return dtoObject;
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

        public async Task<Waehrung> CreateWaehrungAsync(String waehrungName, string waehrungZeichen, string waehrungISO, CancellationToken cancellationToken = default)
        { 
            Waehrung newWaehrung = await CreateWaehrungAsync(cancellationToken);
            newWaehrung.WaehrungName = waehrungName;
            newWaehrung.WaehrungZeichen = waehrungZeichen;
            newWaehrung.WaehrungISO = waehrungISO;

            return newWaehrung;
        }

        public async Task<IEnumerable<Abrechnungseinheit>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Abrechnungseinheiten, cancellationToken);
        }

        public async Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Abrechnungseinheiten, cancellationToken);
        }

        public async Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(string name, string abkuerzung, CancellationToken cancellationToken = default)
        {
            Abrechnungseinheit newAbrechnungseinheit = await CreateAbrechnungseinheitAsync(cancellationToken);
            newAbrechnungseinheit.Name = name;
            newAbrechnungseinheit.Abkuerzung = abkuerzung;

            return newAbrechnungseinheit;
        }

        public void RemoveAbrechnungseinheit(Abrechnungseinheit selectedAbrechnungseinheit)
        {
            _ = DBContext.Abrechnungseinheiten.Remove(selectedAbrechnungseinheit);
        }

        public async Task<IEnumerable<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Geschaeftsjahre.Include(g => g.Auftraege).ThenInclude(a => a.Auftragspositionen), cancellationToken);
        }

        public async Task<Geschaeftsjahr> CreateGeschaeftsjahrAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Geschaeftsjahre, cancellationToken);
        }

        public async Task<Geschaeftsjahr> CreateGeschaeftsjahrAsync(DateTime datumBis, DateTime datumVon, string geschaeftsjahrName, string rechnungsprefix, CancellationToken cancellationToken = default)
        {
            Geschaeftsjahr newGeschaeftsjahr = await CreateGeschaeftsjahrAsync(cancellationToken);
            newGeschaeftsjahr.DatumBis = datumBis;
            newGeschaeftsjahr.DatumVon = datumVon;
            newGeschaeftsjahr.Name = geschaeftsjahrName;
            newGeschaeftsjahr.Rechnungsprefix = rechnungsprefix;

            return newGeschaeftsjahr;
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

        public async Task<Postleitzahl> CreatePostleitzahlAsync(string bundesland, string land, string ortsname, string plz, CancellationToken cancellationToken = default)
        {
            Postleitzahl newPostleitzahl = await CreatePostleitzahlAsync(cancellationToken);
            newPostleitzahl.Bundesland = bundesland;
            newPostleitzahl.Land = land;
            newPostleitzahl.Ortsname = ortsname;
            newPostleitzahl.PLZ = plz;

            return newPostleitzahl;
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
