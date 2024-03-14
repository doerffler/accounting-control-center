using ACCDataAdapter.ACC;
using ACCDataModel.DTO;
using ACCDataModel.Enum;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel.Services
{
    public interface IACCService
    {
        public bool HasChanges { get; }

        public event EventHandler Changed;

        public string GetConnectionInformations();
        
        public string GetFileShare();
        
        public ACCDataContext GetDbContext();

        #region Kostenstelle

        public Task<IEnumerable<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0);

        public Task<int> GetKostenstellenCountAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Kostenstelle>> GetKostenstelleAsync(int KostenstelleID, CancellationToken cancellationToken = default);

        public Task<Kostenstelle> CreateKostenstelleAsync(CancellationToken cancellationToken = default);
        
        public Task<Kostenstelle> CreateKostenstelleAsync(string kostenstellenBezeichnung, int kostenstellenNummer, Kostenstellenart kostenstellenArt, CancellationToken cancellationToken = default);
        
        public void RemoveKostenstelle(Kostenstelle kostenstelleToRemove);

        #endregion 

        #region Mitarbeiter

        public Task<IEnumerable<Mitarbeiter>> GetMitarbeiterAsync(CancellationToken cancellationToken = default);

        public Task<Mitarbeiter> CreateMitarbeiterAsync(CancellationToken cancellationToken = default);

        public void RemoveMitarbeiter(Mitarbeiter mitarbeiterToRemove);

        public Task<Anstellungsdetail> CreateAnstellungsdetailAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<EmployeeAccountingDTO>> GetEmployeeAccountingAsync(string email, DateTime from, DateTime to, CancellationToken cancellationToken = default);

        #endregion

        #region Kunde

        public Task<IEnumerable<Kunde>> GetKundenAsync(CancellationToken cancellationToken = default);
        public Task<Kunde> CreateKundeAsync(CancellationToken cancellationToken = default);
        public Task<Kunde> CreateKundeAsync(string kundenname, string kundennameLang = null, CancellationToken cancellationToken = default);
        public void RemoveKunde(Kunde selectedKunde);

        #endregion

        #region Projekt

        public Task<IEnumerable<Projekt>> GetProjekteAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0);
        public Task<int> GetProjekteCountAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Projekt>> GetProjektAsync(int ProjektID, CancellationToken cancellationToken = default);
        public Task<Projekt> CreateProjektAsync(CancellationToken cancellationToken = default);
        public void RemoveProjekt(Projekt projektToRemove);

        public Task<Skill> CreateSkillAsync(CancellationToken cancellationToken = default);
        public Task<Skill> CreateSkillAsync(string skillName, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Skill>> GetSkillsAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Skill>> GetSkillTreeAsync(CancellationToken cancellationToken = default);
        public void RemoveSkill(Skill selectedSkill);

        #endregion

        #region Auftrag

        public Task<IEnumerable<Auftrag>> GetAuftraegeAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0);

        public Task<int> GetAuftraegeCountAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Auftrag>> GetAuftragAsync(int AuftragID, CancellationToken cancellationToken = default);

        public Task<Auftrag> CreateAuftragAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Auftragsposition>> GetAuftragspositionenAsync(CancellationToken cancellationToken = default);
        
        public Task<Auftragsposition> CreateAuftragspositionAsync(CancellationToken cancellationToken = default);

        public void RemoveAuftrag(Auftrag auftragToRemove);

        #endregion

        #region Leistungsnachweis

        public Task<IEnumerable<Leistungsnachweis>> GetLeistungsnachweiseAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0);

        public Task<int> GetLeistungsnachweiseCountAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Leistungsnachweis>> GetLeistungsnachweisAsync(int LeistungsnachweisID, CancellationToken cancellationToken = default);

        public Task<Leistungsnachweis> CreateLeistungsnachweisAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Leistungsnachweisposition>> GetLeistungsnachweispositionAsync(CancellationToken cancellationToken = default);

        public Task<Leistungsnachweisposition> CreateLeistungsnachweispositionAsync(CancellationToken cancellationToken = default);

        public void RemoveLeistungsnachweis(Leistungsnachweis leistungsnachweisToRemove);

        #endregion

        #region Ausgangsrechnungen

        public Task<IEnumerable<Ausgangsrechnung>> GetAusgangsrechnungenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0, OutgoingInvoiceStatus? status = null);

        public Task<int> GetAusgangsrechnungenCountAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Ausgangsrechnung>> GetAusgangsrechnungAsync(int AusgangsrechnungID, CancellationToken cancellationToken = default);

        public Task<IEnumerable<RemainingBudgetOnOrdersDTO>> GetRemainingBudgetOnOrdersAsync(int AuftragspositionID, CancellationToken cancellationToken = default);

        public Task<Ausgangsrechnung> CreateAusgangsrechnungAsync(CancellationToken cancellationToken = default);

        public Task AddAusgangsrechnungAsync(Ausgangsrechnung ausgangsrechnungToAdd, CancellationToken cancellationToken = default);

        public void RemoveAusgangsrechnung(Ausgangsrechnung ausgangsrechnungToRemove);

        public Task<Ausgangsrechnungsposition> CreateAusgangsrechnungspositionAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Eingangsrechnungen

        public Task<IEnumerable<Eingangsrechnung>> GetEingangsrechnungenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0, IncomingInvoiceStatus? status = null);

        public Task<int> GetEingangsrechnungenCountAsync(CancellationToken cancellationToken = default);

        public Task<IEnumerable<Eingangsrechnung>> GetEingangsrechnungAsync(int EingangsrechnungID, CancellationToken cancellationToken = default);

        public Task<Eingangsrechnung> CreateEingangsrechnungAsync(CancellationToken cancellationToken = default);

        public Task AddEingangsrechnungAsync(Eingangsrechnung eingangsrechnungToAdd, CancellationToken cancellationToken = default);

        public void RemoveEingangsrechnung(Eingangsrechnung eingangsrechnungToRemove);

        public Task<Eingangsrechnungsposition> CreateEingangsrechnungspositionAsync(CancellationToken cancellationToken = default);

        #endregion

        #region Masterdata

        public Task<IEnumerable<Waehrung>> GetWaehrungenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0);
        public Task<int> GetWaehrungenCountAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Waehrung>> GetWaehrungAsync(int WaehrungID, CancellationToken cancellationToken = default);
        public Task<Waehrung> CreateWaehrungAsync(CancellationToken cancellationToken = default);
        public Task<Waehrung> CreateWaehrungAsync(String waehrungName, string waehrungZeichen, string waehrungISO, Dictionary<string, string> waehrungAdditions = null, CancellationToken cancellationToken = default);
        void RemoveWaehrung(Waehrung selectedWaehrung);

        public Task<IEnumerable<Abrechnungseinheit>> GetAbrechnungseinheitenAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0);
        public Task<int> GetAbrechnungseinheitenCountAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Abrechnungseinheit>> GetAbrechnungseinheitAsync(int AbrechnungseinheitID, CancellationToken cancellationToken = default); 
        public Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(CancellationToken cancellationToken = default);
        public Task<Abrechnungseinheit> CreateAbrechnungseinheitAsync(string name, string abkuerzung, Dictionary<string, string> additions = null, CancellationToken cancellationToken = default);
        void RemoveAbrechnungseinheit(Abrechnungseinheit selectedAbrechnungseinheit);

        public Task<IEnumerable<T>> GetGeschaeftspartnerAsync<T>(CancellationToken cancellationToken = default) where T : Geschaeftspartner;
        public Task<T> CreateGeschaeftspartnerAsync<T>(CancellationToken cancellationToken = default) where T : Geschaeftspartner, new();
        public void RemoveGeschaeftspartner<T>(T debitorToRemove) where T : Geschaeftspartner;

        public Task<IEnumerable<Geschaeftsjahr>> GetGeschaeftsjahreAsync(CancellationToken cancellationToken = default, int? currentPage = 0, int? pageSize = 0);
        public Task<int> GetGeschaeftsjahreCountAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Geschaeftsjahr>> GetGeschaeftsjahrAsync(int GeschaeftsjahrID, CancellationToken cancellationToken = default); 
        public Task<Geschaeftsjahr> CreateGeschaeftsjahrAsync(CancellationToken cancellationToken = default);
        public Task<Geschaeftsjahr> CreateGeschaeftsjahrAsync(DateTime datumBis, DateTime datumVon, string geschaeftsjahrName, string rechnungsprefix, CancellationToken cancellationToken = default);
        public void RemoveGeschaeftsjahr(Geschaeftsjahr selectedGeschaeftsjahr);

        public Task<IEnumerable<Feiertag>> GetFeiertageAsync(CancellationToken cancellationToken = default);
        public Task<Feiertag> CreateFeiertagAsync(CancellationToken cancellationToken = default);
        public void RemoveFeiertag(Feiertag selectedFeiertag);

        public Task<IEnumerable<Postleitzahl>> GetPostleitzahlenAsync(CancellationToken cancellationToken = default);
        public Task<Postleitzahl> CreatePostleitzahlAsync(CancellationToken cancellationToken = default);
        public Task<Postleitzahl> CreatePostleitzahlAsync(string bundesland, string land, string ortsname, string plz, CancellationToken cancellationToken = default);
        public void RemovePostleitzahl(Postleitzahl selectedPostleitzahl);

        public Task<IEnumerable<Adresse>> GetAdressenAsync(CancellationToken cancellationToken = default);
        public Task<Adresse> CreateAdresseAsync(CancellationToken cancellationToken = default);
        public void RemoveAdresse(Adresse selectedAdresse);

        public Task<IEnumerable<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default);
        public Task<Kostenstellenart> CreateKostenstellenartAsync(CancellationToken cancellationToken = default);
        public Task<Kostenstellenart> CreateKostenstellenartAsync(string bezeichnung, CancellationToken cancellationToken = default);
        public void RemoveKostenstellenart(Kostenstellenart selectedKostenstellenart);

        public Task<IEnumerable<Taetigkeit>> GetTaetigkeitenAsync(CancellationToken cancellationToken = default);
        public Task<Taetigkeit> CreateTaetigkeitAsync(CancellationToken cancellationToken = default);
        public Task<Taetigkeit> CreateTaetigkeitAsync(String taetigkeitsbeschreibung, CancellationToken cancellationToken = default);
        public void RemoveTaetigkeit(Taetigkeit selectedTaetigkeit);

        #endregion

        #region Utility functions

        public bool CheckForChanges();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        public Task<T> AddDataToDbSetFromApiAsync<T>(DbSet<T> dbSet, T newItem, CancellationToken cancellationToken = default) where T : class, new();

        public Task<T> UpdateDataToDbSetFromApiAsync<T>(DbSet<T> dbSet, int setId, T newItem, CancellationToken cancellationToken = default) where T : class, new();
        #endregion


    }
}
