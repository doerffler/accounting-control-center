using ACC.ViewModel.Model;
using ACCDataAdapter.ACC;
using ACCDataModel.DTO;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel.Services
{
    public class ACCService : IACCService
    {
        public bool HasChanges
        {
            get { return DBContext.ChangeTracker.HasChanges(); }
        }

        public event EventHandler Changed;

        protected virtual void RaiseChangedEvent()
        {
            Changed?.Invoke(this, new EventArgs());
        }

        private string ACCConnectionString { get; set; }
        private string ACCFileShare { get; set; }

        private ACCDataContext DBContext { get; set; }

        public ACCService(IOptions<ACCConnectionSettings> accConnectionSettings)
        {
            ACCConnectionString = accConnectionSettings.Value.ConnectionString;
            ACCFileShare = accConnectionSettings.Value.FileShare;

            ACCDataContext dbContext = new()
            {
                ConnectionString = ACCConnectionString
            };

            if (dbContext.Database.EnsureCreated())
            {

            }

            DBContext = dbContext;
        }

        public string GetConnectionInformations()
        {
            return DBContext.Database.GetDbConnection().ConnectionString;
        }

        public string GetFileShare()
        {
            return ACCFileShare;
        }

        public ACCDataContext GetDbContext()
        {
            return DBContext;
        }

        #region Kostenstellen

        public async Task<IEnumerable<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0)
        {
            IQueryable<Kostenstelle> query = DBContext.Kostenstellen.Include(k => k.UebergeordneteKostenstellen);

            if ((currentPage.Value != 0) || (pageSize.Value != 0))
            {
                int skipCount = (currentPage.Value - 1) * pageSize.Value;
                query = query.Skip(skipCount).Take(pageSize.Value);
            }

            return await GetDataFromDbSetAsync(query, cancellationToken);
        }

        public async Task<int> GetKostenstellenCountAsync(CancellationToken cancellationToken = default)
        {
            var kostenstellen = await DBContext.Kostenstellen.CountAsync();
            return kostenstellen;
        }

        public async Task<IEnumerable<Kostenstelle>> GetKostenstelleAsync(int KostenstelleID, CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(
                DBContext.Kostenstellen
                    .Where(k => k.KostenstelleID == KostenstelleID)
                    .Include(k => k.UebergeordneteKostenstellen)
                , cancellationToken);
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
            RaiseChangedEvent();
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
            RaiseChangedEvent();
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
                            .Select(project => new EmployeeAccountingDTO
                            {
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

        public async Task<IEnumerable<Auftrag>> GetAuftraegeAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0)
        {
            IQueryable<Auftrag> query = DBContext.Auftraege
                .Include(a => a.VerantwortlicherMitarbeiter)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeAdresse)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeAdresse.ZugehoerigePostleitzahl)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeKostenstelle)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeKostenstelle.ZugehoerigeKostenstellenart)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeKostenstelle.UebergeordneteKostenstellen)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeKostenstelle.UntergeordneteKostenstellen)
                .Include(a => a.ZugehoerigesGeschaeftsjahr)
                .Include(a => a.ZugehoerigesProjekt)
                .Include(a => a.ZugehoerigesProjekt.Rechnungsempfaenger.ZugehoerigerKunde)
                .Include(a => a.ZugehoerigesProjekt.Rechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl)
                .Include(a => a.ZugehoerigeWaehrung)
                .Include(a => a.Auftragspositionen)
                .ThenInclude(ap => ap.Abrechnungseinheit);

            if ((currentPage.Value != 0) || (pageSize.Value != 0))
            {
                int skipCount = (currentPage.Value - 1) * pageSize.Value;
                query = query.Skip(skipCount).Take(pageSize.Value);
            }

            var auftraege = await GetDataFromDbSetAsync(query, cancellationToken);

            return auftraege;
        }

        public async Task<int> GetAuftraegeCountAsync(CancellationToken cancellationToken = default)
        {
            var auftraege = await DBContext.Auftraege.CountAsync();
            return auftraege;
        }

        public async Task<IEnumerable<Auftrag>> GetAuftragAsync(int AuftragID, CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Auftraege
                .Include(a => a.VerantwortlicherMitarbeiter)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeAdresse)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeAdresse.ZugehoerigePostleitzahl)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeKostenstelle)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeKostenstelle.ZugehoerigeKostenstellenart)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeKostenstelle.UebergeordneteKostenstellen)
                .Include(m => m.VerantwortlicherMitarbeiter.ZugehoerigeKostenstelle.UntergeordneteKostenstellen)
                .Include(a => a.ZugehoerigesGeschaeftsjahr)
                .Include(a => a.ZugehoerigesProjekt)
                .Include(a => a.ZugehoerigesProjekt.Rechnungsempfaenger.ZugehoerigerKunde)
                .Include(a => a.ZugehoerigesProjekt.Rechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl)
                .Include(a => a.ZugehoerigeWaehrung)
                .Include(a => a.Auftragspositionen)
                .ThenInclude(ap => ap.Abrechnungseinheit)
                .Where(a => a.AuftragID == AuftragID), cancellationToken);
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

        public void RemoveAuftrag(Auftrag auftragToRemove)
        {
            _ = DBContext.Auftraege.Remove(auftragToRemove);
            RaiseChangedEvent();
        }

        #endregion

        #region Leistungsnachweis
        public async Task<IEnumerable<Leistungsnachweis>> GetLeistungsnachweiseAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0)
        {
            IQueryable<Leistungsnachweis> query = DBContext.Leistungsnachweise
                .Include(lnw => lnw.Leistungsnachweispositionen)
                .Include(lnw => lnw.ZugehoerigeKostenstelle)
                .Include(lnw => lnw.ZugehoerigerAuftrag);

            if ((currentPage.Value != 0) || (pageSize.Value != 0))
            {
                int skipCount = (currentPage.Value - 1) * pageSize.Value;
                query = query.Skip(skipCount).Take(pageSize.Value);
            }

            var leistungsnachweise = await GetDataFromDbSetAsync(query, cancellationToken);

            return leistungsnachweise;
        }

        public async Task<int> GetLeistungsnachweiseCountAsync(CancellationToken cancellationToken = default)
        {
            var leistungsnachweise = await DBContext.Leistungsnachweise.CountAsync();
            return leistungsnachweise;
        }

        public async Task<IEnumerable<Leistungsnachweis>> GetLeistungsnachweisAsync(int LeistungsnachweisID, CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Leistungsnachweise
                .Include(lnw => lnw.Leistungsnachweispositionen)
                .Include(lnw => lnw.ZugehoerigeKostenstelle)
                .Include(lnw => lnw.ZugehoerigerAuftrag)
                .Where(a => a.LeistungsnachweisID == LeistungsnachweisID), cancellationToken);
        }

        public async Task<Leistungsnachweis> CreateLeistungsnachweisAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Leistungsnachweise, cancellationToken);
        }

        public async Task<IEnumerable<Leistungsnachweisposition>> GetLeistungsnachweispositionAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Leistungsnachweispositionen, cancellationToken);
        }

        public async Task<Leistungsnachweisposition> CreateLeistungsnachweispositionAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Leistungsnachweispositionen, cancellationToken);
        }

        public void RemoveLeistungsnachweis(Leistungsnachweis leistungsnachweisToRemove)
        {
            _ = DBContext.Leistungsnachweise.Remove(leistungsnachweisToRemove);
            RaiseChangedEvent();
        }
        #endregion

        #region Projekt
        public async Task<IEnumerable<Projekt>> GetProjekteAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0)
        {
            IQueryable<Projekt> query = DBContext.Projekte
                .Include(p => p.Skills)
                .Include(a => a.ZugehoerigeAuftraege)
                .ThenInclude(ap => ap.Auftragspositionen)
                .Include(p => p.Rechnungsempfaenger)
                .ThenInclude(re => re.ZugehoerigerKunde);

            if ((currentPage.Value != 0) || (pageSize.Value != 0))
            {
                int skipCount = (currentPage.Value - 1) * pageSize.Value;
                query = query.Skip(skipCount).Take(pageSize.Value);
            }

            var projekt = await GetDataFromDbSetAsync(query, cancellationToken);

            return projekt;
        }

        public async Task<int> GetProjekteCountAsync(CancellationToken cancellationToken = default)
        {
            var projekte = await DBContext.Projekte.CountAsync();
            return projekte;
        }

        public async Task<IEnumerable<Projekt>> GetProjektAsync(int ProjektID, CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Projekte
                .Include(p => p.Skills)
                .Include(a => a.ZugehoerigeAuftraege)
                .ThenInclude(ap => ap.Auftragspositionen)
                .Include(p => p.Rechnungsempfaenger)
                .ThenInclude(re => re.ZugehoerigerKunde)
                .Where(p => p.ProjektID == ProjektID), cancellationToken);
        }

        public async Task<Projekt> CreateProjektAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Projekte, cancellationToken);
        }

        public void RemoveProjekt(Projekt projektToRemove)
        {
            _ = DBContext.Projekte.Remove(projektToRemove);
            RaiseChangedEvent();
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

        #region Eingangsrechnungen

        public async Task<IEnumerable<Eingangsrechnung>> GetEingangsrechnungenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0)
        {
            IQueryable<Eingangsrechnung> query = DBContext.Eingangsrechnungen
                .Include(ar => ar.ZugehoerigeWaehrung)
                .Include(ar => ar.ZugehoerigesDokument)
                .Include(ar => ar.ZugehoerigesGeschaeftsjahr)
                .Include(ar => ar.KorrekturRechnung)
                .Include(ar => ar.ZugehoerigerKreditor)
                .Include(ar => ar.ZugehoerigerKreditor.ZugehoerigeAdresse)
                .Include(ar => ar.ZugehoerigerKreditor.ZugehoerigeAdresse.ZugehoerigePostleitzahl)
                .Include(ar => ar.ZugehoerigerVertrag)
                .Include(er => er.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeAbrechnungseinheit)
                .Include(er => er.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeKostenstelle);

            if ((currentPage.Value != 0) || (pageSize.Value != 0))
            {
                int skipCount = (currentPage.Value - 1) * pageSize.Value;
                query = query.Skip(skipCount).Take(pageSize.Value);
            }

            var eingangsrechnungen = await GetDataFromDbSetAsync(query, cancellationToken);

            return eingangsrechnungen;
        }

        public async Task<int> GetEingangsrechnungenCountAsync(CancellationToken cancellationToken = default)
        {
            var eingangsrechnungen = await DBContext.Eingangsrechnungen.CountAsync();
            return eingangsrechnungen;
        }

        public async Task<IEnumerable<Eingangsrechnung>> GetEingangsrechnungAsync(int EingangsrechnungID, CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(
                DBContext.Eingangsrechnungen
                .Where(ar => ar.EingangsrechnungID == EingangsrechnungID)
                .Include(ar => ar.ZugehoerigeWaehrung)
                .Include(ar => ar.ZugehoerigesDokument)
                .Include(ar => ar.ZugehoerigesGeschaeftsjahr)
                .Include(ar => ar.KorrekturRechnung)
                .Include(ar => ar.ZugehoerigerKreditor)
                .Include(ar => ar.ZugehoerigerKreditor.ZugehoerigeAdresse)
                .Include(ar => ar.ZugehoerigerKreditor.ZugehoerigeAdresse.ZugehoerigePostleitzahl)
                .Include(ar => ar.ZugehoerigerVertrag)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeAbrechnungseinheit)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeKostenstelle)
                , cancellationToken);
        }

        public async Task<Eingangsrechnung> CreateEingangsrechnungAsync(CancellationToken cancellationToken = default)
        {

            Eingangsrechnung newEingangsrechnung = await AddDataToDbSetAsync(DBContext.Eingangsrechnungen, cancellationToken);
            newEingangsrechnung.RechnungsDatum = DateTime.Now;

            return newEingangsrechnung;

        }

        public async Task AddEingangsrechnungAsync(Eingangsrechnung eingangsrechnungToAdd, CancellationToken cancellationToken = default)
        {
            _ = await DBContext.Eingangsrechnungen.AddAsync(eingangsrechnungToAdd, cancellationToken);
            RaiseChangedEvent();
        }

        public void RemoveEingangsrechnung(Eingangsrechnung eingangsrechnungToRemove)
        {
            _ = DBContext.Eingangsrechnungen.Remove(eingangsrechnungToRemove);
            RaiseChangedEvent();
        }

        public async Task<Eingangsrechnungsposition> CreateEingangsrechnungspositionAsync(CancellationToken cancellationToken = default)
        {

            Eingangsrechnungsposition newEingangsrechnungsposition = await AddDataToDbSetAsync(DBContext.Eingangsrechnungspositionen, cancellationToken);

            return newEingangsrechnungsposition;

        }
        #endregion

        #region Ausgangsrechnungen

        public async Task<IEnumerable<Ausgangsrechnung>> GetAusgangsrechnungenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0)
        {
            IQueryable<Ausgangsrechnung> query = DBContext.Ausgangsrechnungen
                .Include(ar => ar.ZugehoerigeWaehrung)
                .Include(ar => ar.ZugehoerigeAusgangsrechnungshistorie)
                .Include(ar => ar.Rechnungsempfaenger.Projekte)
                .Include(ar => ar.Rechnungsempfaenger.ZugehoerigerKunde)
                .Include(ar => ar.Rechnungsempfaenger.ZugehoerigerKunde.Rechnungsempfaenger)
                .Include(ar => ar.Rechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl)
                .Include(ar => ar.ZugehoerigesDokument)
                .Include(ar => ar.ZugehoerigesGeschaeftsjahr)
                .Include(ar => ar.KorrekturRechnung)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeAbrechnungseinheit)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeKostenstelle)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeAuftragsposition)
                .Include(ar => ar.Rechnungspositionen).ThenInclude(rp => rp.ZugehoerigeFremdleistungen);

            if ((currentPage.Value != 0) || (pageSize.Value != 0))
            {
                int skipCount = (currentPage.Value - 1) * pageSize.Value;
                query = query.Skip(skipCount).Take(pageSize.Value);
            }

            var ausgangsrechnungen = await GetDataFromDbSetAsync(query, cancellationToken);

            return ausgangsrechnungen;
        }

        public async Task<int> GetAusgangsrechnungenCountAsync(CancellationToken cancellationToken = default)
        {
            var ausgangsrechnungen = await DBContext.Ausgangsrechnungen.CountAsync();
            return ausgangsrechnungen;
        }

        public async Task<IEnumerable<Ausgangsrechnung>> GetAusgangsrechnungAsync(int AusgangsrechnungID, CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(
                DBContext.Ausgangsrechnungen
                .Where(ar => ar.AusgangsrechnungID == AusgangsrechnungID)
                .Include(ar => ar.ZugehoerigeWaehrung)
                .Include(ar => ar.ZugehoerigeAusgangsrechnungshistorie)
                .Include(ar => ar.Rechnungsempfaenger.Projekte)
                .Include(ar => ar.Rechnungsempfaenger.ZugehoerigerKunde)
                .Include(ar => ar.Rechnungsempfaenger.ZugehoerigerKunde.Rechnungsempfaenger)
                .Include(ar => ar.Rechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl)
                .Include(ar => ar.ZugehoerigesDokument)
                .Include(ar => ar.ZugehoerigesGeschaeftsjahr)
                .Include(ar => ar.KorrekturRechnung)
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

        public async Task AddAusgangsrechnungAsync(Ausgangsrechnung ausgangsrechnungToAdd, CancellationToken cancellationToken = default)
        {
            _ = await DBContext.Ausgangsrechnungen.AddAsync(ausgangsrechnungToAdd, cancellationToken);
            RaiseChangedEvent();
        }

        public void RemoveAusgangsrechnung(Ausgangsrechnung ausgangsrechnungToRemove)
        {
            _ = DBContext.Ausgangsrechnungen.Remove(ausgangsrechnungToRemove);
            RaiseChangedEvent();
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
                .ThenInclude(arp => arp.ZugehoerigeKostenstelle)
                .SelectMany(rechnung => rechnung.Rechnungspositionen)
                .Where(arp => arp.ZugehoerigeAuftragsposition.AuftragspositionID == AuftragspositionID);

            IEnumerable<Ausgangsrechnungsposition> queryData = await GetDataFromDbSetAsync(query, cancellationToken);

            IEnumerable<RemainingBudgetOnOrdersDTO> dtoObject = queryData
                                .GroupBy(arp => arp.LeistungszeitraumBis)
                                .Select(arp => new RemainingBudgetOnOrdersDTO
                                {
                                    OrderStart = arp.First().ZugehoerigeAuftragsposition.Auftrag.Auftragsbeginn,
                                    OrderEnd = arp.First().ZugehoerigeAuftragsposition.Auftrag.Auftragsende,
                                    Date = arp.Key.Date,
                                    KostenstellenNummer = arp.First().ZugehoerigeKostenstelle.KostenstellenNummer,
                                    OrderName = arp.First().ZugehoerigeAuftragsposition.Auftrag.Auftragsname,
                                    OrderPosition = arp.First().ZugehoerigeAuftragsposition.Positionsbezeichnung,
                                    ResidualBudgetActualBefore = arp.First().ZugehoerigeAuftragsposition.Auftragsvolumen,
                                    ActualConsumption = arp.Sum(rb => rb.Stueckzahl),
                                    ResidualBudgetTargetBefore = arp.First().ZugehoerigeAuftragsposition.Auftragsvolumen
                                }).ToList();

            var items = dtoObject.ToList();

            if (items.Count > 0)
            {
                var firstItem = dtoObject.First();
                items.Insert(0, new RemainingBudgetOnOrdersDTO
                {
                    OrderStart = firstItem.OrderStart,
                    OrderEnd = firstItem.OrderEnd,
                    Date = (DateTime)firstItem.OrderStart,
                    KostenstellenNummer = firstItem.KostenstellenNummer,
                    OrderName = firstItem.OrderName,
                    OrderPosition = firstItem.OrderPosition,
                    ResidualBudgetActualAfter = firstItem.ResidualBudgetActualBefore,
                    ResidualBudgetTargetAfter = firstItem.ResidualBudgetActualBefore,
                });

                RemainingBudgetOnOrdersDTO previousObject = null;
                items.ForEach(obj =>
                {
                    if (previousObject != null)
                    {
                        obj.ResidualBudgetActualBefore = previousObject.ResidualBudgetActualAfter;
                        obj.ResidualBudgetActualAfter = obj.ResidualBudgetActualBefore - obj.ActualConsumption;
                    }
                    previousObject = obj;
                });

                var start = (DateTime)items.First().OrderStart;
                var difference = ((DateTime)items.First().OrderEnd).Subtract(start).Days;

                previousObject = null;
                items.ForEach(item =>
                {
                    if (previousObject != null)
                    {
                        var current = item.Date.Subtract(start).Days;
                        item.TargetConsumption = Math.Round(previousObject.ResidualBudgetTargetAfter / difference * current);
                        item.ResidualBudgetTargetBefore = previousObject.ResidualBudgetTargetAfter;
                        item.ResidualBudgetTargetAfter = item.ResidualBudgetTargetBefore - item.TargetConsumption;
                    }
                    previousObject = item;
                });
            }

            return items;
        }

        #endregion

        #region Geschäftspartner

        public async Task<IEnumerable<T>> GetGeschaeftspartnerAsync<T>(CancellationToken cancellationToken = default) where T : Geschaeftspartner
        {

            IQueryable result;

            if (typeof(T) == typeof(Debitor))
            {
                result = DBContext.Debitoren
                    .Include(d => d.ZugehoerigerKunde)
                    .Include(d => d.ZugehoerigeAdresse).ThenInclude(a => a.ZugehoerigePostleitzahl);
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
            RaiseChangedEvent();
        }

        #endregion

        #region Masterdata

        public async Task<IEnumerable<Waehrung>> GetWaehrungenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0)
        {
            IQueryable<Waehrung> query = DBContext.Waehrungen;

            if ((currentPage.Value != 0) || (pageSize.Value != 0))
            {
                int skipCount = (currentPage.Value - 1) * pageSize.Value;
                query = query.Skip(skipCount).Take(pageSize.Value);
            }

            return await GetDataFromDbSetAsync(query, cancellationToken);
        }


        public async Task<int> GetWaehrungenCountAsync(CancellationToken cancellationToken = default)
        {
            var waehrungen = await DBContext.Waehrungen.CountAsync();
            return waehrungen;
        }

        public async Task<IEnumerable<Waehrung>> GetWaehrungAsync(int WaehrungID, CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(
                DBContext.Waehrungen.Where(w => w.WaehrungID == WaehrungID)
                , cancellationToken);
        }

        public async Task<Waehrung> CreateWaehrungAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Waehrungen, cancellationToken);
        }

        public async Task<Waehrung> CreateWaehrungAsync(String waehrungName, string waehrungZeichen, string waehrungISO, Dictionary<string, string> waehrungAdditions = null, CancellationToken cancellationToken = default)
        {
            Waehrung newWaehrung = await CreateWaehrungAsync(cancellationToken);
            newWaehrung.WaehrungName = waehrungName;
            newWaehrung.WaehrungZeichen = waehrungZeichen;
            newWaehrung.WaehrungISO = waehrungISO;
            newWaehrung.WaehrungAdditions = waehrungAdditions;

            return newWaehrung;
        }

        public async Task<IEnumerable<Abrechnungseinheit>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0)
        {
            IQueryable<Abrechnungseinheit> query = DBContext.Abrechnungseinheiten;

            if ((currentPage.Value != 0) || (pageSize.Value != 0))
            {
                int skipCount = (currentPage.Value - 1) * pageSize.Value;
                query = query.Skip(skipCount).Take(pageSize.Value);
            }

            return await GetDataFromDbSetAsync(query, cancellationToken);
        }

        public async Task<int> GetAbrechnungseinheitenCountAsync(CancellationToken cancellationToken = default)
        {
            var abrechnungseinheiten = await DBContext.Abrechnungseinheiten.CountAsync();
            return abrechnungseinheiten;
        }

        public async Task<IEnumerable<Abrechnungseinheit>> GetAbrechnungseinheitAsync(int AbrechnungseinheitID, CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(
                DBContext.Abrechnungseinheiten.Where(w => w.AbrechnungseinheitID == AbrechnungseinheitID)
                , cancellationToken);
        }

        public async Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(CancellationToken cancellationToken = default)
        {
            return await AddDataToDbSetAsync(DBContext.Abrechnungseinheiten, cancellationToken);
        }

        public async Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(string name, string abkuerzung, Dictionary<string, string> additions = null, CancellationToken cancellationToken = default)
        {
            Abrechnungseinheit newAbrechnungseinheit = await CreateAbrechnungseinheitAsync(cancellationToken);
            newAbrechnungseinheit.Name = name;
            newAbrechnungseinheit.Abkuerzung = abkuerzung;
            newAbrechnungseinheit.Additions = additions;

            return newAbrechnungseinheit;
        }

        public void RemoveAbrechnungseinheit(Abrechnungseinheit selectedAbrechnungseinheit)
        {
            _ = DBContext.Abrechnungseinheiten.Remove(selectedAbrechnungseinheit);
            RaiseChangedEvent();
        }

        public async Task<IEnumerable<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Geschaeftsjahre
                .Include(g => g.Auftraege)
                .ThenInclude(a => a.Auftragspositionen), cancellationToken);
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
            RaiseChangedEvent();
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
            RaiseChangedEvent();
        }

        public void RemoveKunde(Kunde selectedKunde)
        {
            _ = DBContext.Kunden.Remove(selectedKunde);
            RaiseChangedEvent();
        }

        public void RemoveWaehrung(Waehrung selectedWaehrung)
        {
            _ = DBContext.Waehrungen.Remove(selectedWaehrung);
            RaiseChangedEvent();
        }

        public void RemovePostleitzahl(Postleitzahl selectedPostleitzahl)
        {
            _ = DBContext.Postleitzahlen.Remove(selectedPostleitzahl);
            RaiseChangedEvent();
        }

        public async Task<IEnumerable<Adresse>> GetAdressenAsync(CancellationToken cancellationToken = default)
        {
            return await GetDataFromDbSetAsync(DBContext.Adressen, cancellationToken);
        }

        public void RemoveAdresse(Adresse selectedAdresse)
        {
            _ = DBContext.Adressen.Remove(selectedAdresse);
            RaiseChangedEvent();
        }

        public void RemoveKostenstellenart(Kostenstellenart selectedKostenstellenart)
        {
            _ = DBContext.Kostenstellenarten.Remove(selectedKostenstellenart);
            RaiseChangedEvent();
        }

        public void RemoveTaetigkeit(Taetigkeit selectedTaetigkeit)
        {
            _ = DBContext.Taetigkeiten.Remove(selectedTaetigkeit);
            RaiseChangedEvent();
        }


        #endregion

        #region Utility functions

        public bool CheckForChanges()
        {
            return DBContext.ChangeTracker.HasChanges();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            RaiseChangedEvent();
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

            RaiseChangedEvent();

            return newItem;
        }

        public async Task<T> AddDataToDbSetFromApiAsync<T>(DbSet<T> dbSet, T newItem, CancellationToken cancellationToken = default) where T : class, new()
        {
            _ = await dbSet.AddAsync(newItem, cancellationToken);

            RaiseChangedEvent();

            return newItem;
        }

        public async Task<T> UpdateDataToDbSetFromApiAsync<T>(DbSet<T> dbSet, int setId, T newItem, CancellationToken cancellationToken = default) where T : class, new()
        {
            T existingDbSet = await dbSet.FindAsync(setId);

            if (existingDbSet == null)
            {
                return null;
            }

            DBContext.Entry(existingDbSet).CurrentValues.SetValues(newItem);

            RaiseChangedEvent();

            return newItem;
        }

        #endregion

    }
}
