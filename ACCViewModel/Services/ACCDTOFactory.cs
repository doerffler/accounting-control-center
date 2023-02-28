using ACCDataModel.DTO;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel.Services
{
    public static class ACCDTOFactory
    {

        public static async Task CreateEmployeeFromDTOAsync(EmployeeDTO employee, IACCService accService, CancellationToken cancellationToken = default)
        {

            IEnumerable<Kostenstelle> listKostenstellen = await accService.GetKostenstellenAsync(cancellationToken);
            IEnumerable<Taetigkeit> listTaetigkeiten = await accService.GetTaetigkeitenAsync(cancellationToken);
            IEnumerable<Postleitzahl> listPostleitzahlen = await accService.GetPostleitzahlenAsync(cancellationToken);

            List<Anstellungsdetail> currentAnstellungshistorie = new();
            Anstellungsdetail currentAnstellungsdetail;
            Mitarbeiter currentMitarbeiter;

            currentMitarbeiter = await accService.CreateMitarbeiterAsync(cancellationToken);
            currentMitarbeiter.Anrede = employee.Salutation;
            currentMitarbeiter.Vorname = employee.Firstname;
            currentMitarbeiter.Nachname = employee.Surname;
            currentMitarbeiter.Geburtsdatum = employee.Birthdate;
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = employee.EmployeeCode;
            currentMitarbeiter.PersonalnummerDatev = employee.StaffNumberDatev;

            currentMitarbeiter.ZugehoerigeAdresse = await accService.CreateAdresseAsync(cancellationToken);
            currentMitarbeiter.ZugehoerigeAdresse.ZugehoerigePostleitzahl = listPostleitzahlen.Where(plz => plz.PLZ.Equals(employee.PostalCode)).First();
            currentMitarbeiter.ZugehoerigeAdresse.Hausnummer = employee.StreetNumber;
            currentMitarbeiter.ZugehoerigeAdresse.Strasse = employee.Street;

            currentMitarbeiter.ZugehoerigeKostenstelle = listKostenstellen.Where(kst => kst.KostenstellenNummer.Equals(employee.CostCenterNumber)).First();

            foreach (var hiringdetail in employee.HiringDetails)
            {
                currentAnstellungsdetail = await accService.CreateAnstellungsdetailAsync(cancellationToken);
                currentAnstellungsdetail.ZugehoerigerMitarbeiter = currentMitarbeiter;
                currentAnstellungsdetail.AnzahlMonatsgehaelter = hiringdetail.MonthlySalaryCount;
                currentAnstellungsdetail.AnzahlArbeitsstunden = hiringdetail.WorkingHoursCount;
                currentAnstellungsdetail.GueltigAb = hiringdetail.ValidFrom;
                currentAnstellungsdetail.Monatsgehalt = hiringdetail.MonthlySalaryAmount;
                currentAnstellungsdetail.IstGekuendigt = hiringdetail.IsTerminated;
                currentAnstellungsdetail.ZugehoerigeTaetigkeit = listTaetigkeiten.Where(t => t.Taetigkeitsbeschreibung.Equals(hiringdetail.JobDescription)).FirstOrDefault();

                currentAnstellungshistorie.Add(currentAnstellungsdetail);
            }

        }

        public static async Task CreateProjectFromDTOAsync(ProjectDTO project, IACCService doePaAdminService, CancellationToken cancellationToken = default)
        {

            IEnumerable<Mitarbeiter> listMitarbeiter = await doePaAdminService.GetMitarbeiterAsync(cancellationToken);
            IEnumerable<Kunde> listKunden = await doePaAdminService.GetKundenAsync(cancellationToken);
            IEnumerable<Postleitzahl> listPostleitzahlen = await doePaAdminService.GetPostleitzahlenAsync(cancellationToken);
            IEnumerable<Skill> listSkills = await doePaAdminService.GetSkillsAsync(cancellationToken);
            IEnumerable<Geschaeftsjahr> listGeschaeftsjahre = await doePaAdminService.GetGeschaeftsjahreAsync(cancellationToken);
            IEnumerable<Abrechnungseinheit> listAbrechnungseinheiten = await doePaAdminService.GetAbrechnungseinheitenAsync(cancellationToken);
            IEnumerable<Waehrung> listWaehrungen = await doePaAdminService.GetWaehrungenAsync(cancellationToken);

            Kunde currentKunde;
            Debitor currentRechnungsempfaenger;
            Projekt currentProjekt;
            Auftrag currentAuftrag;
            Auftragsposition currentAuftragsposition;

            currentKunde = listKunden.First(k => k.Kundenname.Equals(project.CustomerName));

            currentRechnungsempfaenger = await doePaAdminService.CreateGeschaeftspartnerAsync<Debitor>(cancellationToken);
            currentRechnungsempfaenger.Anschrift = project.InvoiceRecipient;
            currentRechnungsempfaenger.ZugehoerigerKunde = currentKunde;

            currentRechnungsempfaenger.ZugehoerigeAdresse = await doePaAdminService.CreateAdresseAsync(cancellationToken);
            currentRechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl = listPostleitzahlen.First(plz => plz.PLZ.Equals(project.PostalCode));
            currentRechnungsempfaenger.ZugehoerigeAdresse.Strasse = project.Street;
            currentRechnungsempfaenger.ZugehoerigeAdresse.Hausnummer = project.StreetNumber;

            currentKunde.Rechnungsempfaenger.Add(currentRechnungsempfaenger);

            currentProjekt = await doePaAdminService.CreateProjektAsync(cancellationToken);
            currentProjekt.Projektstart = project.ProjectStartDate;
            currentProjekt.Projektende = project.ProjectEndDate;
            currentProjekt.Projektname = project.ProjectName;

            currentProjekt.Rechnungsempfaenger = currentRechnungsempfaenger;
            currentRechnungsempfaenger.Projekte.Add(currentProjekt);

            foreach (string skill in project.Skills)
            {
                currentProjekt.Skills.Add(listSkills.First(s => s.SkillName.Equals(skill)));
            }

            foreach (OrderDTO order in project.Orders)
            {
                currentAuftrag = await doePaAdminService.CreateAuftragAsync(cancellationToken);
                currentAuftrag.Auftragsbeginn = order.OrderStartDate;
                currentAuftrag.Auftragsdatum = order.OrderDate;
                currentAuftrag.Auftragsende = order.OrderEndDate;
                currentAuftrag.Auftragsname = order.OrderName;
                currentAuftrag.ZugehoerigesGeschaeftsjahr = listGeschaeftsjahre.First(gj => gj.Name.Equals(order.BusinessYear));

                currentAuftrag.ZugehoerigesProjekt = currentProjekt;
                currentProjekt.ZugehoerigeAuftraege.Add(currentAuftrag);

                currentAuftrag.VerantwortlicherMitarbeiter = listMitarbeiter.First(m => m.Kuerzel.Equals(order.CodeOfEmployeeInCharge));
                currentAuftrag.Vertragsnummer = order.ContractNumber;
                currentAuftrag.ZugehoerigeWaehrung = listWaehrungen.First(w => w.WaehrungISO.Equals(order.CurrencyISO));

                foreach (OrderItemDTO item in order.OrderItems)
                {
                    currentAuftragsposition = await doePaAdminService.CreateAuftragspositionAsync(cancellationToken);
                    currentAuftragsposition.Abrechnungseinheit = listAbrechnungseinheiten.First(ae => ae.Abkuerzung.Equals(item.ItemBillingUnitCode)); ;
                    currentAuftragsposition.AuftragspositionNummer = item.OrderItemPosition;
                    currentAuftragsposition.Auftragsvolumen = item.OrderVolumeAmount;
                    currentAuftragsposition.Positionsbezeichnung = item.OrderItemDescription;
                    currentAuftragsposition.StueckpreisNetto = item.NetUnitPrice;

                    currentAuftragsposition.Auftrag = currentAuftrag;
                    currentAuftrag.Auftragspositionen.Add(currentAuftragsposition);
                }
            }
        }

        public static async Task CreateOutgoingInvoiceFromDTOAsync(InvoiceDTO invoice, IACCService doePaAdminService, CancellationToken cancellationToken = default)
        {

            IEnumerable<Geschaeftsjahr> listGeschaeftsjahre = await doePaAdminService.GetGeschaeftsjahreAsync(cancellationToken);
            IEnumerable<Debitor> listDebitoren = await doePaAdminService.GetGeschaeftspartnerAsync<Debitor>(cancellationToken);
            IEnumerable<Waehrung> listWaehrungen = await doePaAdminService.GetWaehrungenAsync(cancellationToken);
            IEnumerable<Kostenstelle> listKostenstellen = await doePaAdminService.GetKostenstellenAsync(cancellationToken);
            IEnumerable<Abrechnungseinheit> listAbrechnungseinheiten = await doePaAdminService.GetAbrechnungseinheitenAsync(cancellationToken);
            IEnumerable<Auftragsposition> listAuftragspositionen = await doePaAdminService.GetAuftragspositionenAsync(cancellationToken);

            Ausgangsrechnung currentAusgangsrechnung;
            Ausgangsrechnungsposition currentAusgangsrechnungsposition;
            
            currentAusgangsrechnung = await doePaAdminService.CreateAusgangsrechnungAsync(cancellationToken);

            currentAusgangsrechnung.RechnungsDatum = invoice.InvoiceDate;
            currentAusgangsrechnung.BezahltDatum = invoice.DatePaid;
            currentAusgangsrechnung.RechnungsNummer = invoice.InvoiceNumber;
            currentAusgangsrechnung.ZugehoerigesGeschaeftsjahr = listGeschaeftsjahre.First(gj => gj.Name.Equals(invoice.BusinessYear));
            currentAusgangsrechnung.ZugehoerigeWaehrung = listWaehrungen.First(w => w.WaehrungISO.Equals(invoice.CurrencyISO));
            currentAusgangsrechnung.Rechnungsempfaenger = listDebitoren.First(gp =>
                gp.Anschrift.Equals(invoice.InvoiceRecipient) &&
                gp.ZugehoerigeAdresse.ZugehoerigePostleitzahl.PLZ.Equals(invoice.PostalCode) &&
                gp.ZugehoerigeAdresse.Hausnummer.Equals(invoice.StreetNumber) &&
                gp.ZugehoerigeAdresse.Strasse.Equals(invoice.Street)
                );

            foreach (InvoiceItemDTO position in invoice.InvoiceItems)
            {

                currentAusgangsrechnungsposition = await doePaAdminService.CreateAusgangsrechnungspositionAsync(cancellationToken);

                currentAusgangsrechnungsposition.PositionsNummer = position.ItemNumber;
                currentAusgangsrechnungsposition.LeistungszeitraumBis = position.DateServiceUntil;
                currentAusgangsrechnungsposition.LeistungszeitraumVon = position.DateServiceFrom;
                currentAusgangsrechnungsposition.Positionsbeschreibung = position.ItemDescription;
                currentAusgangsrechnungsposition.Steuersatz = position.TaxRateDecimal;
                currentAusgangsrechnungsposition.StueckpreisNetto = position.NetUnitPrice;
                currentAusgangsrechnungsposition.Stueckzahl = position.UnitQuantity;
                currentAusgangsrechnungsposition.ZugehoerigeAbrechnungseinheit = listAbrechnungseinheiten.First(ae => ae.Abkuerzung.Equals(position.ItemBillingUnitCode)); ;
                currentAusgangsrechnungsposition.ZugehoerigeAuftragsposition = listAuftragspositionen.First(ap => ap.AuftragspositionNummer.Equals(position.OrderItemPosition) && ap.Auftrag.Vertragsnummer.Equals(position.OrderContractNumber));
                currentAusgangsrechnungsposition.ZugehoerigeKostenstelle = listKostenstellen.First(kst => kst.KostenstellenNummer.Equals(position.CostCenterNumber));

                currentAusgangsrechnungsposition.ZugehoerigeRechnung = currentAusgangsrechnung;
                currentAusgangsrechnung.Rechnungspositionen.Add(currentAusgangsrechnungsposition);

            }
        }

    }
}
