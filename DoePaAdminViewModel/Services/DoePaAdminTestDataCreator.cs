using DoePaAdminDataModel.DTO;
using DoePaAdminDataModel.Kostenrechnung;
using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel.Services
{
    internal static class DoePaAdminTestDataCreator
    {

        public static async Task CreateCompleteTestDataAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            await CreateMasterdataAsync(doePaAdminService, cancellationToken);
            
            await CreateKostenstellenAsync(doePaAdminService, cancellationToken);
                                                
            await CreateMitarbeiterAsync(doePaAdminService, cancellationToken);

            await CreateKundenAsync(doePaAdminService, cancellationToken);

            await CreateAuftraegeAsync(doePaAdminService, cancellationToken);

            await CreateAusgangsrechnungenAsync(doePaAdminService, cancellationToken);
                        
        }


        public static async Task CreateMasterdataAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            _ = await doePaAdminService.CreateTaetigkeitAsync("Game Designer", cancellationToken);
            _ = await doePaAdminService.CreateTaetigkeitAsync("Technical Director", cancellationToken);
            _ = await doePaAdminService.CreateTaetigkeitAsync("Artist", cancellationToken);
                        
            _ = await doePaAdminService.CreateKostenstellenartAsync("Angestellte Mitarbeiter/innen", cancellationToken);
            _ = await doePaAdminService.CreateKostenstellenartAsync("Geschäftsräume", cancellationToken);
            _ = await doePaAdminService.CreateKostenstellenartAsync("Freie Mitarbeiter/innen", cancellationToken);
            _ = await doePaAdminService.CreateKostenstellenartAsync("Sonstige Kostenstellen", cancellationToken);
            
            _ = await doePaAdminService.CreateAbrechnungseinheitAsync("Stunden", "h", cancellationToken);
            _ = await doePaAdminService.CreateAbrechnungseinheitAsync("Personentage", "PT", cancellationToken);
            _ = await doePaAdminService.CreateAbrechnungseinheitAsync("Stück", "Stk", cancellationToken);

            _ = await doePaAdminService.CreateWaehrungAsync("Euro", "€", "EUR", cancellationToken);
            _ = await doePaAdminService.CreateWaehrungAsync("US Dollar", "$", "USD", cancellationToken);
            _ = await doePaAdminService.CreateWaehrungAsync("Schweizer Franken", "Fr", "CHF", cancellationToken);
            _ = await doePaAdminService.CreateWaehrungAsync("Britisches Pfund", "£", "GBP", cancellationToken);
                        
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1991, 12, 31), new(1991, 1, 1), "1991", "1991", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1992, 12, 31), new(1992, 1, 1), "1992", "1992", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1993, 6, 30), new(1993, 1, 1), "1993", "1993", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1994, 6, 30), new(1993, 7, 1), "1993/1994", "1993", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1995, 6, 30), new(1994, 7, 1), "1994/1995", "1994", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2020, 12, 31), new(2020, 1, 1), "2020", "2020", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2021, 6, 30), new(2021, 1, 1),  "2021", "2021", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2022, 6, 30), new(2021, 7, 1), "2021/2022", "2021", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2023, 6, 30), new(2022, 7, 1), "2022/2023", "2022", cancellationToken);
                        
            _ = await doePaAdminService.CreatePostleitzahlAsync("Niedersachsen", "Deutschland", "Hannover", "30173", cancellationToken);
            _ = await doePaAdminService.CreatePostleitzahlAsync("Baden-Württemberg", "Deutschland", "Mühlhausen (Kraichgau)", "69242", cancellationToken);
            _ = await doePaAdminService.CreatePostleitzahlAsync("Bayern", "Deutschland", "München", "80807", cancellationToken);

            Skill techSkill = await doePaAdminService.CreateSkillAsync("Technische Skills", cancellationToken);
            Skill progSkill = await doePaAdminService.CreateSkillAsync("Programmiersprachen", cancellationToken);
            Skill netSkill = await doePaAdminService.CreateSkillAsync(".NET", cancellationToken);
            Skill csSkill = await doePaAdminService.CreateSkillAsync("C#", cancellationToken);
            Skill graphicSkill = await doePaAdminService.CreateSkillAsync("Grafische Gestaltung", cancellationToken);

            techSkill.ChildSkills.Add(progSkill);
            techSkill.ChildSkills.Add(graphicSkill);
            progSkill.ChildSkills.Add(netSkill);
            netSkill.ChildSkills.Add(csSkill);

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

        private static async Task CreateKostenstellenAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            IEnumerable<Kostenstellenart> listKostenstellenarten = await doePaAdminService.GetKostenstellenartenAsync(cancellationToken);
            
            //Create a cost center for a freelancer
            _ = await doePaAdminService.CreateKostenstelleAsync("Bobby Prince", 2002, listKostenstellenarten.Where(ka => ka.Kostenstellenartbezeichnung.Equals("Freie Mitarbeiter/innen")).First(), cancellationToken);

            //Create a cost center for the office, we'll use this later to assign it to the staff
            Kostenstelle kstOfficeRichardson = await doePaAdminService.CreateKostenstelleAsync("Office Richardson", 5020, listKostenstellenarten.Where(ka => ka.Kostenstellenartbezeichnung.Equals("Geschäftsräume")).First(), cancellationToken);

            //Create some staff cost center
            Kostenstelle currentKostenstelle;
            foreach (var currentEmployee in new[] { new { Name = "John Carmack", KstNummer = 1003 }, new { Name = "John Romero", KstNummer = 1006 }, new { Name = "Adrian Carmack", KstNummer = 1009 }, new { Name = "Tom Hall", KstNummer = 1012 } })
            { 
                currentKostenstelle = await doePaAdminService.CreateKostenstelleAsync(currentEmployee.Name, currentEmployee.KstNummer, listKostenstellenarten.Where(ka => ka.Kostenstellenartbezeichnung.Equals("Angestellte Mitarbeiter/innen")).First(), cancellationToken);
                currentKostenstelle.UebergeordneteKostenstellen.Add(kstOfficeRichardson);
                kstOfficeRichardson.UntergeordneteKostenstellen.Add(currentKostenstelle);
            }

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

        private static async Task CreateMitarbeiterAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken)
        {

            EmployeeDTO[] listEmployees = new[]
            {
                /*
                * John Carmack
                */
                new EmployeeDTO() {
                    Salutation = "Herr",
                    Firstname = "John",
                    Surname = "Carmack",
                    Birthdate = new DateTime(1970, 8, 20),
                    EmployeeCode = "JOCA",
                    StaffNumberDatev = 1,
                    PostalCode = "30173",
                    StreetNumber = "23",
                    Street = "Freundallee",
                    CostCenterNumber = 1003,
                    HiringDetails = new HiringDetailDTO[]
                    {
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 2, 1), MonthlySalaryAmount = 250, IsTerminated = false, JobDescription = "Technical Director"},
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 11, 30), MonthlySalaryAmount = 2090, IsTerminated = false, JobDescription = "Technical Director"},
                        new HiringDetailDTO() { MonthlySalaryCount = 0, WorkingHoursCount = 0, ValidFrom = new DateTime(2013, 11, 22), MonthlySalaryAmount = 0, IsTerminated = true, JobDescription = string.Empty}
                    }
                },
                /*
                * John Romero
                */
                new EmployeeDTO() {
                    Salutation = "Herr",
                    Firstname = "John",
                    Surname = "Romero",
                    Birthdate = new DateTime(1967, 10, 28),
                    EmployeeCode = "JORO",
                    StaffNumberDatev = 2,
                    PostalCode = "30173",
                    StreetNumber = "59",
                    Street = "Georgstr.",
                    CostCenterNumber = 1006,
                    HiringDetails = new HiringDetailDTO[]
                    {
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 2, 1), MonthlySalaryAmount = 250, IsTerminated = false, JobDescription = "Game Designer"},
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 11, 30), MonthlySalaryAmount = 2090, IsTerminated = false, JobDescription = "Game Designer"},
                        new HiringDetailDTO() { MonthlySalaryCount = 0, WorkingHoursCount = 0, ValidFrom = new DateTime(1996, 8, 6), MonthlySalaryAmount = 0, IsTerminated = true, JobDescription = string.Empty}
                    }
                },
                /*
                * Tom Hall
                */
                new EmployeeDTO() {
                    Salutation = "Herr",
                    Firstname = "Tom",
                    Surname = "Hall",
                    Birthdate = DateTime.MinValue,
                    EmployeeCode = "TOHA",
                    StaffNumberDatev = 3,
                    PostalCode = "30173",
                    StreetNumber = "1",
                    Street = "Heinrich-Heine-Str. 1",
                    CostCenterNumber = 1012,
                    HiringDetails = new HiringDetailDTO[]
                    {
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 2, 1), MonthlySalaryAmount = 250, IsTerminated = false, JobDescription = "Game Designer"},
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 11, 30), MonthlySalaryAmount = 2090, IsTerminated = false, JobDescription = "Game Designer"},
                        new HiringDetailDTO() { MonthlySalaryCount = 0, WorkingHoursCount = 0, ValidFrom = new DateTime(1993, 8, 1), MonthlySalaryAmount = 0, IsTerminated = true, JobDescription = string.Empty}
                    }
                },
                /*
                * Adrian Carmack
                */
                new EmployeeDTO() {
                    Salutation = "Herr",
                    Firstname = "Adrian",
                    Surname = "Carmack",
                    Birthdate = new DateTime(1969, 5, 5),
                    EmployeeCode = "ADCA",
                    StaffNumberDatev = 4,
                    PostalCode = "30173",
                    StreetNumber = "43",
                    Street = "Marienstr.",
                    CostCenterNumber = 1009,
                    HiringDetails = new HiringDetailDTO[]
                    {
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 2, 1), MonthlySalaryAmount = 250, IsTerminated = false, JobDescription = "Artist"},
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 11, 30), MonthlySalaryAmount = 2090, IsTerminated = false, JobDescription = "Artist"},
                        new HiringDetailDTO() { MonthlySalaryCount = 0, WorkingHoursCount = 0, ValidFrom = new DateTime(2005, 1, 1), MonthlySalaryAmount = 0, IsTerminated = true, JobDescription = string.Empty}
                    }
                }
            };

            foreach (EmployeeDTO employee in listEmployees)
            {
                await DoePaAdminDTOFactory.CreateEmployeeFromDTOAsync(employee, doePaAdminService, cancellationToken);
            }

            await doePaAdminService.SaveChangesAsync(cancellationToken);

        }

        private static async Task CreateKundenAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {
            _ = await doePaAdminService.CreateKundeAsync("Softdisk", "Softdisk Magazette", cancellationToken);
            _ = await doePaAdminService.CreateKundeAsync("Apogee", cancellationToken: cancellationToken);
            _ = await doePaAdminService.CreateKundeAsync("Gamestop", cancellationToken: cancellationToken);

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

        private static async Task CreateAuftraegeAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken = default)
        {

            Kunde currentKunde;
            Debitor currentRechnungsempfaenger;
            Projekt currentProjekt;
            Auftrag currentAuftrag;
            Auftragsposition currentAuftragsposition;

            Abrechnungseinheit aeStunden = (await doePaAdminService.GetAbrechnungseinheitenAsync(cancellationToken)).Where(ae => ae.Name.Equals("Stunden")).First();
            Waehrung wEuro = (await doePaAdminService.GetWaehrungenAsync(cancellationToken)).Where(w => w.WaehrungName.Equals("Euro")).First();
            
            IEnumerable<Mitarbeiter> listMitarbeiter = await doePaAdminService.GetMitarbeiterAsync(cancellationToken);
            IEnumerable<Kunde> listKunden = await doePaAdminService.GetKundenAsync(cancellationToken);
            IEnumerable<Postleitzahl> listPostleitzahlen = await doePaAdminService.GetPostleitzahlenAsync(cancellationToken);
            IEnumerable<Skill> listSkills = await doePaAdminService.GetSkillsAsync(cancellationToken);
            IEnumerable<Geschaeftsjahr> listGeschaeftsjahre = await doePaAdminService.GetGeschaeftsjahreAsync(cancellationToken);

            CustomerDTO customer = new()
            {
                CustomerName = "Softdisk",
                InvoiceRecipient = "Gamer's Edge",
                PostalCode = "80807",
                StreetNumber = "17",
                Street = "Walter-Gropius-Straße",
                Projects = new ProjectDTO[]
                {
                    new ProjectDTO()
                    {
                        ProjectName = "Vertragliche Verbindlichkeiten gegenüber Softdisk",
                        ProjectStartDate = new(1991, 2, 1),
                        ProjectEndDate = new(1992, 12, 31),
                        Skills = { "C#", "Grafische Gestaltung" },
                        Orders = new OrderDTO[]
                        {
                            new OrderDTO()
                            {
                                OrderName = "Gamer's Edge Q1 1991",
                                OrderDate = new(1991, 2, 1),
                                OrderStartDate = new(1991, 2, 1),
                                OrderEndDate = new(1991, 03, 31),
                                ContractNumber = 1,
                                BusinessYear = "1991",
                                CodeOfEmployeeInCharge = "TOHA"
                            },
                            new OrderDTO()
                            {

                            }
                        }
                    }
                }
            };

            currentKunde = listKunden.Where(k => k.Kundenname.Equals(customer.CustomerName)).First();

            currentRechnungsempfaenger = await doePaAdminService.CreateGeschaeftspartnerAsync<Debitor>(cancellationToken);
            currentRechnungsempfaenger.Anschrift = customer.InvoiceRecipient;
            currentRechnungsempfaenger.ZugehoerigerKunde = currentKunde;

            currentRechnungsempfaenger.ZugehoerigeAdresse = await doePaAdminService.CreateAdresseAsync(cancellationToken);
            currentRechnungsempfaenger.ZugehoerigeAdresse.ZugehoerigePostleitzahl = listPostleitzahlen.Where(plz => plz.PLZ.Equals(customer.PostalCode)).First();
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
                    currentProjekt.Skills.Add(listSkills.Where(s => s.SkillName.Equals(skill)).First());
                }

                foreach (OrderDTO order in project.Orders)
                { 
                    currentAuftrag = await doePaAdminService.CreateAuftragAsync(cancellationToken);
                    currentAuftrag.Auftragsbeginn = order.OrderStartDate;
                    currentAuftrag.Auftragsdatum = order.OrderDate;
                    currentAuftrag.Auftragsende = order.OrderEndDate;
                    currentAuftrag.Auftragsname = order.OrderName;
                    currentAuftrag.ZugehoerigesGeschaeftsjahr = listGeschaeftsjahre.Where(gj => gj.Name.Equals(order.BusinessYear)).First();

                    currentAuftrag.ZugehoerigesProjekt = currentProjekt;
                    currentProjekt.ZugehoerigeAuftraege.Add(currentAuftrag);

                    currentAuftrag.VerantwortlicherMitarbeiter = listMitarbeiter.Where(m => m.Kuerzel.Equals(order.CodeOfEmployeeInCharge)).First();
                    currentAuftrag.Vertragsnummer = order.ContractNumber;

                    currentAuftragsposition = await doePaAdminService.CreateAuftragspositionAsync(cancellationToken);
                    currentAuftragsposition.Abrechnungseinheit = aeStunden;
                    currentAuftragsposition.AuftragspositionNummer = 1;
                    currentAuftragsposition.Auftragsvolumen = 480;
                    currentAuftragsposition.Positionsbezeichnung = "Spieleentwicklung";
                    currentAuftragsposition.Waehrung = wEuro;

                    currentAuftragsposition.Auftrag = currentAuftrag;
                    currentAuftrag.Auftragspositionen.Add(currentAuftragsposition);
                }
            }

            await doePaAdminService.SaveChangesAsync(cancellationToken);

        }

        private static async Task CreateAusgangsrechnungenAsync(IDoePaAdminService doePaAdminService, CancellationToken cancellationToken)
        {

            Ausgangsrechnung currentAusgangsrechnung;
            Ausgangsrechnungsposition currentAusgangsrechnungsposition;

            Geschaeftsjahr gJahr1991 = (await doePaAdminService.GetGeschaeftsjahreAsync(cancellationToken)).Where(gj => gj.Name.Equals("1991")).First();
            Debitor gamersEdge = (await doePaAdminService.GetGeschaeftspartnerAsync<Debitor>(cancellationToken)).Where(gp => gp.Anschrift.Equals("Gamer's Edge")).First();
            Waehrung wEuro = (await doePaAdminService.GetWaehrungenAsync(cancellationToken)).Where(w => w.WaehrungName.Equals("Euro")).First();
            Abrechnungseinheit aeStunden = (await doePaAdminService.GetAbrechnungseinheitenAsync(cancellationToken)).Where(ae => ae.Name.Equals("Stunden")).First();
            Auftragsposition apGESpieleentwicklung = (await doePaAdminService.GetAuftragspositionAsync(cancellationToken)).Where(ap => ap.Positionsbezeichnung.Equals("Spieleentwicklung")).First();
            Kostenstelle kstCarmack = (await doePaAdminService.GetKostenstellenAsync(cancellationToken)).Where(kst => kst.Kostenstellenbezeichnung.Equals("John Carmack")).First();
            Kostenstelle kstHall = (await doePaAdminService.GetKostenstellenAsync(cancellationToken)).Where(kst => kst.Kostenstellenbezeichnung.Equals("Tom Hall")).First();

            currentAusgangsrechnung = await doePaAdminService.CreateAusgangsrechnungAsync(cancellationToken);
            
            currentAusgangsrechnung.RechnungsDatum = new(1991, 2, 28);
            currentAusgangsrechnung.BezahltDatum = new(1991,3,15);
            currentAusgangsrechnung.RechnungsNummer = "19910001";
            currentAusgangsrechnung.ZugehoerigesGeschaeftsjahr = gJahr1991;
            currentAusgangsrechnung.Rechnungsempfaenger = gamersEdge;

            currentAusgangsrechnungsposition = await doePaAdminService.CreateAusgangsrechnungspositionAsync(cancellationToken);

            currentAusgangsrechnungsposition.PositionsNummer = 1;
            currentAusgangsrechnungsposition.LeistungszeitraumBis = new(1991, 2, 28);
            currentAusgangsrechnungsposition.LeistungszeitraumVon = new(1991, 2, 1);
            currentAusgangsrechnungsposition.NettobetragWaehrung = wEuro;
            currentAusgangsrechnungsposition.Positionsbeschreibung = "Entwicklung der Engine für Commander Keen";
            currentAusgangsrechnungsposition.Steuersatz = 0.16M;
            currentAusgangsrechnungsposition.StueckpreisNetto = 50M;
            currentAusgangsrechnungsposition.Stueckzahl = 100;
            currentAusgangsrechnungsposition.ZugehoerigeAbrechnungseinheit = aeStunden;
            currentAusgangsrechnungsposition.ZugehoerigeAuftragsposition = apGESpieleentwicklung;
            currentAusgangsrechnungsposition.ZugehoerigeKostenstelle = kstCarmack;
            currentAusgangsrechnungsposition.ZugehoerigeRechnung = currentAusgangsrechnung;
            
            currentAusgangsrechnung.Rechnungspositionen.Add(currentAusgangsrechnungsposition);

            currentAusgangsrechnungsposition = await doePaAdminService.CreateAusgangsrechnungspositionAsync(cancellationToken);

            currentAusgangsrechnungsposition.PositionsNummer = 2;
            currentAusgangsrechnungsposition.LeistungszeitraumBis = new(1991, 2, 28);
            currentAusgangsrechnungsposition.LeistungszeitraumVon = new(1991, 2, 1);
            currentAusgangsrechnungsposition.NettobetragWaehrung = wEuro;
            currentAusgangsrechnungsposition.Positionsbeschreibung = "Design der Sprites für Commander Keen";
            currentAusgangsrechnungsposition.Steuersatz = 0.16M;
            currentAusgangsrechnungsposition.StueckpreisNetto = 45M;
            currentAusgangsrechnungsposition.Stueckzahl = 50;
            currentAusgangsrechnungsposition.ZugehoerigeAbrechnungseinheit = aeStunden;
            currentAusgangsrechnungsposition.ZugehoerigeAuftragsposition = apGESpieleentwicklung;
            currentAusgangsrechnungsposition.ZugehoerigeKostenstelle = kstHall;
            currentAusgangsrechnungsposition.ZugehoerigeRechnung = currentAusgangsrechnung;

            currentAusgangsrechnung.Rechnungspositionen.Add(currentAusgangsrechnungsposition);

            currentAusgangsrechnung = await doePaAdminService.CreateAusgangsrechnungAsync(cancellationToken);

            currentAusgangsrechnung.RechnungsDatum = new(1991, 3, 5);
            currentAusgangsrechnung.BezahltDatum = new(1991, 3, 5);
            currentAusgangsrechnung.RechnungsNummer = "19910002";
            currentAusgangsrechnung.ZugehoerigesGeschaeftsjahr = gJahr1991;
            currentAusgangsrechnung.Rechnungsempfaenger = gamersEdge;

            currentAusgangsrechnungsposition = await doePaAdminService.CreateAusgangsrechnungspositionAsync(cancellationToken);

            currentAusgangsrechnungsposition.PositionsNummer = 1;
            currentAusgangsrechnungsposition.LeistungszeitraumBis = new(1991, 2, 28);
            currentAusgangsrechnungsposition.LeistungszeitraumVon = new(1991, 2, 1);
            currentAusgangsrechnungsposition.NettobetragWaehrung = wEuro;
            currentAusgangsrechnungsposition.Positionsbeschreibung = "Gutschrift für nicht akkzeptierte Entwicklungsstunden an der Engine für Commander Keen";
            currentAusgangsrechnungsposition.Steuersatz = 0.16M;
            currentAusgangsrechnungsposition.StueckpreisNetto = 50M;
            currentAusgangsrechnungsposition.Stueckzahl = -20;
            currentAusgangsrechnungsposition.ZugehoerigeAbrechnungseinheit = aeStunden;
            currentAusgangsrechnungsposition.ZugehoerigeAuftragsposition = apGESpieleentwicklung;
            currentAusgangsrechnungsposition.ZugehoerigeKostenstelle = kstCarmack;
            currentAusgangsrechnungsposition.ZugehoerigeRechnung = currentAusgangsrechnung;

            currentAusgangsrechnung.Rechnungspositionen.Add(currentAusgangsrechnungsposition);

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

    }
}
