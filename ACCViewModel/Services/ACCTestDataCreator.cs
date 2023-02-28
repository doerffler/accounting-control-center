using ACCDataModel.DTO;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel.Services
{
    internal static class ACCTestDataCreator
    {

        public static async Task CreateCompleteTestDataAsync(IACCService accService, CancellationToken cancellationToken = default)
        {

            await CreateMasterdataAsync(accService, cancellationToken);

            await CreateKostenstellenAsync(accService, cancellationToken);

            await CreateMitarbeiterAsync(accService, cancellationToken);

            await CreateKundenAsync(accService, cancellationToken);

            await CreateAuftraegeAsync(accService, cancellationToken);

            await CreateAusgangsrechnungenAsync(accService, cancellationToken);

        }


        public static async Task CreateMasterdataAsync(IACCService doePaAdminService, CancellationToken cancellationToken = default)
        {

            _ = await doePaAdminService.CreateTaetigkeitAsync("Game Designer", cancellationToken);
            _ = await doePaAdminService.CreateTaetigkeitAsync("Technical Director", cancellationToken);
            _ = await doePaAdminService.CreateTaetigkeitAsync("Artist", cancellationToken);

            _ = await doePaAdminService.CreateKostenstellenartAsync("Angestellte Mitarbeiter/innen", cancellationToken);
            _ = await doePaAdminService.CreateKostenstellenartAsync("Geschäftsräume", cancellationToken);
            _ = await doePaAdminService.CreateKostenstellenartAsync("Freie Mitarbeiter/innen", cancellationToken);
            _ = await doePaAdminService.CreateKostenstellenartAsync("Sonstige Kostenstellen", cancellationToken);

            _ = await doePaAdminService.CreateAbrechnungseinheitAsync("Stunden", "h", new(1) { { "DPAppKey", "Stunden" } }, cancellationToken);
            _ = await doePaAdminService.CreateAbrechnungseinheitAsync("Personentage", "PT", new(1) { { "DPAppKey", "Tage" } }, cancellationToken);
            _ = await doePaAdminService.CreateAbrechnungseinheitAsync("Stück", "Stk", new(1) { { "DPAppKey", "Preis" } }, cancellationToken);

            _ = await doePaAdminService.CreateWaehrungAsync("Euro", "€", "EUR", new(1) { { "DPAppKey", "€" } }, cancellationToken);
            _ = await doePaAdminService.CreateWaehrungAsync("US Dollar", "$", "USD", null, cancellationToken);
            _ = await doePaAdminService.CreateWaehrungAsync("Schweizer Franken", "Fr", "CHF", new(1) { { "DPAppKey", "CHF" } }, cancellationToken);
            _ = await doePaAdminService.CreateWaehrungAsync("Britisches Pfund", "£", "GBP", null, cancellationToken);

            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1991, 12, 31), new(1991, 1, 1), "1991", "1991", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1992, 12, 31), new(1992, 1, 1), "1992", "1992", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1993, 6, 30), new(1993, 1, 1), "1993", "1993", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1994, 6, 30), new(1993, 7, 1), "1993/1994", "1993", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(1995, 6, 30), new(1994, 7, 1), "1994/1995", "1994", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2020, 12, 31), new(2020, 1, 1), "2020", "2020", cancellationToken);
            _ = await doePaAdminService.CreateGeschaeftsjahrAsync(new(2021, 6, 30), new(2021, 1, 1), "2021", "2021", cancellationToken);
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

        private static async Task CreateKostenstellenAsync(IACCService doePaAdminService, CancellationToken cancellationToken = default)
        {

            IEnumerable<Kostenstellenart> listKostenstellenarten = await doePaAdminService.GetKostenstellenartenAsync(cancellationToken);

            //Create a cost center for a freelancer
            _ = await doePaAdminService.CreateKostenstelleAsync("Bobby Prince", 2002, listKostenstellenarten.First(ka => ka.Kostenstellenartbezeichnung.Equals("Freie Mitarbeiter/innen")), cancellationToken);

            //Create a cost center for the office, we'll use this later to assign it to the staff
            Kostenstelle kstOfficeRichardson = await doePaAdminService.CreateKostenstelleAsync("Office Richardson", 5020, listKostenstellenarten.First(ka => ka.Kostenstellenartbezeichnung.Equals("Geschäftsräume")), cancellationToken);

            //Create some staff cost center
            Kostenstelle currentKostenstelle;
            foreach (var currentEmployee in new[] { new { Name = "John Carmack", KstNummer = 1003 }, new { Name = "John Romero", KstNummer = 1006 }, new { Name = "Adrian Carmack", KstNummer = 1009 }, new { Name = "Tom Hall", KstNummer = 1012 } })
            {
                currentKostenstelle = await doePaAdminService.CreateKostenstelleAsync(currentEmployee.Name, currentEmployee.KstNummer, listKostenstellenarten.First(ka => ka.Kostenstellenartbezeichnung.Equals("Angestellte Mitarbeiter/innen")), cancellationToken);
                currentKostenstelle.UebergeordneteKostenstellen.Add(kstOfficeRichardson);
                kstOfficeRichardson.UntergeordneteKostenstellen.Add(currentKostenstelle);
            }

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

        private static async Task CreateMitarbeiterAsync(IACCService doePaAdminService, CancellationToken cancellationToken)
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
                    HiringDetails = new[]
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
                    HiringDetails = new[]
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
                    HiringDetails = new[]
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
                    HiringDetails = new[]
                    {
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 2, 1), MonthlySalaryAmount = 250, IsTerminated = false, JobDescription = "Artist"},
                        new HiringDetailDTO() { MonthlySalaryCount = 12, WorkingHoursCount = 40, ValidFrom = new DateTime(1991, 11, 30), MonthlySalaryAmount = 2090, IsTerminated = false, JobDescription = "Artist"},
                        new HiringDetailDTO() { MonthlySalaryCount = 0, WorkingHoursCount = 0, ValidFrom = new DateTime(2005, 1, 1), MonthlySalaryAmount = 0, IsTerminated = true, JobDescription = string.Empty}
                    }
                }
            };

            foreach (EmployeeDTO employee in listEmployees)
            {
                await ACCDTOFactory.CreateEmployeeFromDTOAsync(employee, doePaAdminService, cancellationToken);
            }

            await doePaAdminService.SaveChangesAsync(cancellationToken);

        }

        private static async Task CreateKundenAsync(IACCService doePaAdminService, CancellationToken cancellationToken = default)
        {
            _ = await doePaAdminService.CreateKundeAsync("Softdisk", "Softdisk Magazette", cancellationToken);
            _ = await doePaAdminService.CreateKundeAsync("Apogee", cancellationToken: cancellationToken);
            _ = await doePaAdminService.CreateKundeAsync("Gamestop", cancellationToken: cancellationToken);

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

        private static async Task CreateAuftraegeAsync(IACCService doePaAdminService, CancellationToken cancellationToken = default)
        {

            IEnumerable<ProjectDTO> projects = new[]
            {
                new ProjectDTO()
                {
                    CustomerName = "Softdisk",
                    InvoiceRecipient = "Gamer's Edge",
                    PostalCode = "80807",
                    StreetNumber = "17",
                    Street = "Walter-Gropius-Straße",
                    ProjectName = "Vertragliche Verbindlichkeiten gegenüber Softdisk",
                    ProjectStartDate = new(1991, 2, 1),
                    ProjectEndDate = new(1992, 12, 31),
                    Skills = { "C#", "Grafische Gestaltung" },
                    Orders = new[]
                    {
                        new OrderDTO()
                        {
                            OrderName = "Gamer's Edge Q1 1991",
                            OrderDate = new(1991, 2, 1),
                            OrderStartDate = new(1991, 2, 1),
                            OrderEndDate = new(1991, 03, 31),
                            ContractNumber = 1,
                            BusinessYear = "1991",
                            CodeOfEmployeeInCharge = "TOHA",
                            CurrencyISO = "EUR",
                            OrderItems = new[]
                            {
                                new OrderItemDTO()
                                {
                                    OrderItemPosition = 1,
                                    ItemBillingUnitCode = "h",
                                    OrderItemDescription = "Spieleentwicklung",
                                    OrderVolumeAmount = 480,
                                    NetUnitPrice = 50
                                },
                                new OrderItemDTO()
                                {
                                    OrderItemPosition = 2,
                                    ItemBillingUnitCode = "h",
                                    OrderItemDescription = "Dokumentation",
                                    OrderVolumeAmount = 50,
                                    NetUnitPrice = 45
                                }
                            }
                        },
                        new OrderDTO()
                        {
                            OrderName = "Gamer's Edge Q2 1991",
                            OrderDate = new(1991, 4, 1),
                            OrderStartDate = new(1991, 4, 1),
                            OrderEndDate = new(1991, 6, 30),
                            ContractNumber = 2,
                            BusinessYear = "1991",
                            CodeOfEmployeeInCharge = "TOHA",
                            CurrencyISO = "EUR",
                            OrderItems = new[]
                            {
                                new OrderItemDTO()
                                {
                                    OrderItemPosition = 1,
                                    ItemBillingUnitCode = "h",
                                    OrderItemDescription = "Spieleentwicklung",
                                    OrderVolumeAmount = 480,
                                    NetUnitPrice = 50
                                }
                            }
                        },
                        new OrderDTO()
                        {
                            OrderName = "Gamer's Edge Q3 1991",
                            OrderDate = new(1991, 7, 1),
                            OrderStartDate = new(1991, 7, 1),
                            OrderEndDate = new(1991, 9, 30),
                            ContractNumber = 3,
                            BusinessYear = "1991",
                            CodeOfEmployeeInCharge = "TOHA",
                            CurrencyISO = "EUR",
                            OrderItems = new[]
                            {
                                new OrderItemDTO()
                                {
                                    OrderItemPosition = 1,
                                    ItemBillingUnitCode = "h",
                                    OrderItemDescription = "Spieleentwicklung",
                                    OrderVolumeAmount = 480,
                                    NetUnitPrice = 50
                                }
                            }
                        },
                        new OrderDTO()
                        {
                            OrderName = "Gamer's Edge Q4 1991",
                            OrderDate = new(1991, 10, 1),
                            OrderStartDate = new(1991, 10, 1),
                            OrderEndDate = new(1991, 12, 31),
                            ContractNumber = 4,
                            BusinessYear = "1991",
                            CodeOfEmployeeInCharge = "TOHA",
                            CurrencyISO = "EUR",
                            OrderItems = new[]
                            {
                                new OrderItemDTO()
                                {
                                    OrderItemPosition = 1,
                                    ItemBillingUnitCode = "h",
                                    OrderItemDescription = "Spieleentwicklung",
                                    OrderVolumeAmount = 480,
                                    NetUnitPrice = 50
                                }
                            }
                        }
                    }
                },
                new ProjectDTO()
                {
                    CustomerName="Apogee",
                    InvoiceRecipient = "Scott Miller",
                    PostalCode = "80807",
                    StreetNumber = "5",
                    Street = "Walter-Gropius-Straße",
                    ProjectName = "Wolfenstein 3D",
                    ProjectStartDate = new(1991, 1, 1),
                    ProjectEndDate = new(1992, 5, 5),
                    Skills = { "C#", "Grafische Gestaltung" },
                    Orders = new[]
                    {
                        new OrderDTO()
                        {
                            OrderName = "Wolfenstein 3D 1991",
                            OrderDate = new(1991, 1, 1),
                            OrderStartDate = new(1991, 1, 1),
                            OrderEndDate = new(1991, 12, 31),
                            ContractNumber = 5,
                            BusinessYear = "1991",
                            CodeOfEmployeeInCharge = "JOCA",
                            CurrencyISO = "EUR",
                            OrderItems = new[]
                            {
                                new OrderItemDTO()
                                {
                                    OrderItemPosition = 1,
                                    ItemBillingUnitCode = "h",
                                    OrderItemDescription = "Spieleentwicklung",
                                    OrderVolumeAmount = 1600,
                                    NetUnitPrice = 100
                                }
                            }
                        },
                        new OrderDTO()
                        {
                            OrderName = "Wolfenstein 3D 1992",
                            OrderDate = new(1992, 1, 1),
                            OrderStartDate = new(1992, 1, 1),
                            OrderEndDate = new(1992, 5, 5),
                            ContractNumber = 6,
                            BusinessYear = "1992",
                            CodeOfEmployeeInCharge = "JOCA",
                            CurrencyISO = "EUR",
                            OrderItems = new[]
                            {
                                new OrderItemDTO()
                                {
                                    OrderItemPosition = 1,
                                    ItemBillingUnitCode = "h",
                                    OrderItemDescription = "Spieleentwicklung",
                                    OrderVolumeAmount = 480,
                                    NetUnitPrice = 100
                                }
                            }
                        }
                    }
                }
            };

            foreach (ProjectDTO project in projects)
            {
                await ACCDTOFactory.CreateProjectFromDTOAsync(project, doePaAdminService, cancellationToken);
            }

            await doePaAdminService.SaveChangesAsync(cancellationToken);

        }

        private static async Task CreateAusgangsrechnungenAsync(IACCService doePaAdminService, CancellationToken cancellationToken)
        {

            IEnumerable<InvoiceDTO> invoices = new[]
            {
                new InvoiceDTO()
                {
                    InvoiceNumber = "19910001",
                    InvoiceDate = new(1991, 2, 28),
                    DatePaid = new(1991, 3, 15),
                    BusinessYear = "1991",
                    InvoiceRecipient = "Gamer's Edge",
                    PostalCode = "80807",
                    StreetNumber = "17",
                    Street = "Walter-Gropius-Straße",
                    CurrencyISO = "EUR",
                    InvoiceItems = new[]
                    {
                        new InvoiceItemDTO()
                        {
                            ItemNumber = 1,
                            DateServiceFrom = new(1991, 2, 1),
                            DateServiceUntil = new(1991, 2, 28),
                            ItemDescription = "Entwicklung der Engine für Commander Keen",
                            TaxRateDecimal = 0.16M,
                            NetUnitPrice = 50M,
                            UnitQuantity = 100,
                            CostCenterNumber = 1003,
                            ItemBillingUnitCode = "h",
                            OrderContractNumber = 1,
                            OrderItemPosition = 1
                        },
                        new InvoiceItemDTO()
                        {
                            ItemNumber = 2,
                            DateServiceFrom = new(1991, 2, 1),
                            DateServiceUntil = new(1991, 2, 28),
                            ItemDescription = "Design der Sprites für Commander Keen",
                            TaxRateDecimal = 0.16M,
                            NetUnitPrice = 50M,
                            UnitQuantity = 50,
                            CostCenterNumber = 1012,
                            ItemBillingUnitCode = "h",
                            OrderContractNumber = 1,
                            OrderItemPosition = 1
                        }
                    }
                },
                new InvoiceDTO()
                {
                    InvoiceNumber = "19910002",
                    InvoiceDate = new(1991, 3, 5),
                    DatePaid = new(1991, 3, 5),
                    BusinessYear = "1991",
                    InvoiceRecipient = "Gamer's Edge",
                    PostalCode = "80807",
                    StreetNumber = "17",
                    Street = "Walter-Gropius-Straße",
                    CurrencyISO = "EUR",
                    InvoiceItems = new[]
                    {
                        new InvoiceItemDTO()
                        {
                            ItemNumber = 1,
                            DateServiceFrom = new(1991, 2, 1),
                            DateServiceUntil = new(1991, 2, 28),
                            ItemDescription = "Gutschrift für nicht akkzeptierte Entwicklungsstunden an der Engine für Commander Keen",
                            TaxRateDecimal = 0.16M,
                            NetUnitPrice = 50M,
                            UnitQuantity = -20,
                            CostCenterNumber = 1003,
                            ItemBillingUnitCode = "h",
                            OrderContractNumber = 1,
                            OrderItemPosition = 1
                        }
                    }
                },
                new InvoiceDTO()
                {
                    InvoiceNumber = "19910003",
                    InvoiceDate = new(1991, 3, 1),
                    DatePaid = new(1991, 3, 20),
                    BusinessYear = "1991",
                    InvoiceRecipient = "Scott Miller",
                    PostalCode = "80807",
                    StreetNumber = "5",
                    Street = "Walter-Gropius-Straße",
                    CurrencyISO = "EUR",
                    InvoiceItems = new[]
                    {
                        new InvoiceItemDTO()
                        {
                            ItemNumber = 1,
                            DateServiceFrom = new(1991, 1, 1),
                            DateServiceUntil = new(1991, 1, 31),
                            ItemDescription = "Pizza Money January",
                            TaxRateDecimal = 0.16M,
                            NetUnitPrice = 100M,
                            UnitQuantity = 120,
                            CostCenterNumber = 1006,
                            ItemBillingUnitCode = "h",
                            OrderContractNumber = 5,
                            OrderItemPosition = 1
                        },
                        new InvoiceItemDTO()
                        {
                            ItemNumber = 2,
                            DateServiceFrom = new(1991, 2, 1),
                            DateServiceUntil = new(1991, 2, 28),
                            ItemDescription = "Pizza Money February",
                            TaxRateDecimal = 0.16M,
                            NetUnitPrice = 100M,
                            UnitQuantity = 120,
                            CostCenterNumber = 1006,
                            ItemBillingUnitCode = "h",
                            OrderContractNumber = 5,
                            OrderItemPosition = 1
                        }
                    }
                },
                new InvoiceDTO()
                {
                    InvoiceNumber = "19910004",
                    InvoiceDate = new(1991, 4, 1),
                    DatePaid = new(1991, 4, 20),
                    BusinessYear = "1991",
                    InvoiceRecipient = "Scott Miller",
                    PostalCode = "80807",
                    StreetNumber = "5",
                    Street = "Walter-Gropius-Straße",
                    CurrencyISO = "EUR",
                    InvoiceItems = new[]
                    {
                        new InvoiceItemDTO()
                        {
                            ItemNumber = 1,
                            DateServiceFrom = new(1991, 3, 1),
                            DateServiceUntil = new(1991, 3, 31),
                            ItemDescription = "Pizza Money March",
                            TaxRateDecimal = 0.16M,
                            NetUnitPrice = 100M,
                            UnitQuantity = 120,
                            CostCenterNumber = 1006,
                            ItemBillingUnitCode = "h",
                            OrderContractNumber = 5,
                            OrderItemPosition = 1
                        }
                    }
                },
                new InvoiceDTO()
                {
                    InvoiceNumber = "19910005",
                    InvoiceDate = new(1991, 4, 2),
                    DatePaid = new(1991, 4, 5),
                    BusinessYear = "1991",
                    InvoiceRecipient = "Gamer's Edge",
                    PostalCode = "80807",
                    StreetNumber = "17",
                    Street = "Walter-Gropius-Straße",
                    CurrencyISO = "EUR",
                    InvoiceItems = new[]
                    {
                        new InvoiceItemDTO()
                        {
                            ItemNumber = 1,
                            DateServiceFrom = new(1991, 3, 1),
                            DateServiceUntil = new(1991, 3, 31),
                            ItemDescription = "Dokumentation der Engine",
                            TaxRateDecimal = 0.16M,
                            NetUnitPrice = 45M,
                            UnitQuantity = 48,
                            CostCenterNumber = 1003,
                            ItemBillingUnitCode = "h",
                            OrderContractNumber = 1,
                            OrderItemPosition = 2
                        }
                    }
                }
            };

            foreach (InvoiceDTO invoice in invoices)
            {
                await ACCDTOFactory.CreateOutgoingInvoiceFromDTOAsync(invoice, doePaAdminService, cancellationToken);
            }

            await doePaAdminService.SaveChangesAsync(cancellationToken);
        }

    }
}
