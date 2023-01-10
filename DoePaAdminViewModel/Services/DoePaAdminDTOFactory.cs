using DoePaAdminDataModel.DTO;
using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel.Services
{
    public static class DoePaAdminDTOFactory
    {

        public static async Task CreateEmployeeFromDTOAsync(EmployeeDTO employee, IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            IEnumerable<Kostenstelle> listKostenstellen = await doePaAdminService.GetKostenstellenAsync(cancellationToken);
            IEnumerable<Taetigkeit> listTaetigkeiten = await doePaAdminService.GetTaetigkeitenAsync(cancellationToken);
            IEnumerable<Postleitzahl> listPostleitzahlen = await doePaAdminService.GetPostleitzahlenAsync(cancellationToken);

            List<Anstellungsdetail> currentAnstellungshistorie = new();
            Anstellungsdetail currentAnstellungsdetail;
            Mitarbeiter currentMitarbeiter;

            currentMitarbeiter = await doePaAdminService.CreateMitarbeiterAsync(cancellationToken);
            currentMitarbeiter.Anrede = employee.Salutation;
            currentMitarbeiter.Vorname = employee.Firstname;
            currentMitarbeiter.Nachname = employee.Surname;
            currentMitarbeiter.Geburtsdatum = employee.Birthdate;
            currentMitarbeiter.Anstellungshistorie = currentAnstellungshistorie;
            currentMitarbeiter.Kuerzel = employee.EmployeeCode;
            currentMitarbeiter.PersonalnummerDatev = employee.StaffNumberDatev;

            currentMitarbeiter.ZugehoerigeAdresse = await doePaAdminService.CreateAdresseAsync(cancellationToken);
            currentMitarbeiter.ZugehoerigeAdresse.ZugehoerigePostleitzahl = listPostleitzahlen.Where(plz => plz.PLZ.Equals(employee.PostalCode)).First();
            currentMitarbeiter.ZugehoerigeAdresse.Hausnummer = employee.StreetNumber;
            currentMitarbeiter.ZugehoerigeAdresse.Strasse = employee.Street;

            currentMitarbeiter.ZugehoerigeKostenstelle = listKostenstellen.Where(kst => kst.KostenstellenNummer.Equals(employee.CostCenterNumber)).First();

            foreach (var hiringdetail in employee.HiringDetails)
            {
                currentAnstellungsdetail = await doePaAdminService.CreateAnstellungsdetailAsync(cancellationToken);
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

        public static async Task CreateCustomerFromDTOAsync(CustomerDTO customer, IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
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
            
            currentKunde = listKunden.First(k => k.Kundenname.Equals(customer.CustomerName));

            currentRechnungsempfaenger = await doePaAdminService.CreateGeschaeftspartnerAsync<Debitor>(cancellationToken);
            currentRechnungsempfaenger.Anschrift = customer.InvoiceRecipient;
            currentRechnungsempfaenger.ZugehoerigerKunde = currentKunde;

            currentRechnungsempfaenger.ZugehoerigeAdresse = await doePaAdminService.CreateAdresseAsync(cancellationToken);
            currentRechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl = listPostleitzahlen.First(plz => plz.PLZ.Equals(customer.PostalCode));
            currentRechnungsempfaenger.ZugehoerigeAdresse.Strasse = customer.Street;
            currentRechnungsempfaenger.ZugehoerigeAdresse.Hausnummer = customer.StreetNumber;

            currentKunde.Rechnungsempfaenger.Add(currentRechnungsempfaenger);

            foreach (ProjectDTO project in customer.Projects)
            { 
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

                    foreach (OrderItemDTO item in order.OrderItems)
                    {
                        currentAuftragsposition = await doePaAdminService.CreateAuftragspositionAsync(cancellationToken);
                        currentAuftragsposition.Abrechnungseinheit = listAbrechnungseinheiten.First(ae => ae.Abkuerzung.Equals(item.ItemBillingUnitCode));;
                        currentAuftragsposition.AuftragspositionNummer = item.OrderItemPosition;
                        currentAuftragsposition.Auftragsvolumen = item.OrderVolumeAmount;
                        currentAuftragsposition.Positionsbezeichnung = item.OrderItemDescription;
                        currentAuftragsposition.Waehrung = listWaehrungen.First(w => w.WaehrungISO.Equals(item.ItemCurrencyISO));

                        currentAuftragsposition.Auftrag = currentAuftrag;
                        currentAuftrag.Auftragspositionen.Add(currentAuftragsposition);
                    }
                }
            }
        }

    }
}
