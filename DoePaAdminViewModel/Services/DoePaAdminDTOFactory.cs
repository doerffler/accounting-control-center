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

    }
}
