using DoePaAdmin.ViewModel.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Options;
using DoePaAdminDataAdapter.DPApp;
using System.Threading.Tasks;
using System.Threading;
using DoePaAdminDataModel.DPApp;
using DoePaAdminDataModel.DataMigration;

namespace DoePaAdmin.ViewModel.Services
{
    public class DPAppService : IDPAppService
    {

        public string DPAppConnectionString { get; set; }

        public DPAppService(IOptions<DPAppConnectionSettings> dpAppSettings)
        {
            DPAppConnectionString = dpAppSettings.Value.ConnectionString;
        }

        public async Task <IEnumerable<OutgoingInvoiceMigration>> GetOutgoingInvoicesAsync(CancellationToken cancellationToken = default)
        {

            List<OutgoingInvoiceMigration> listMigrations = new();
            List<OutgoingInvoicePositionMigration> listPositionMigrations;

            OutgoingInvoiceDAL dal = new(DPAppConnectionString);
            MasterdataDAL mdDal = new(DPAppConnectionString);
            
            Task<IEnumerable<OutgoingInvoice>> outgoingInvoicesTask = dal.ReadOutgoingInvoicesAsync(cancellationToken);
            Task<IEnumerable<OutgoingInvoicePosition>> outgoingInvoicePositionsTask = dal.ReadOutgoingInvoicePositionsAsync(cancellationToken);
            Task<IEnumerable<CostCenter>> costCentersTask = mdDal.ReadCostCentersAsync(cancellationToken);
            Task<IEnumerable<CostType>> costTypesTask = mdDal.ReadCostTypesAsync(cancellationToken);
            Task<IEnumerable<Project>> projectsTask = mdDal.ReadProjectsAsync(cancellationToken);
            Task<IEnumerable<BusinessYear>> businessYearsTask = mdDal.ReadBusinessYearsAsync(cancellationToken);
            Task<IEnumerable<Contact>> contactsTask = mdDal.ReadContactsAsync(cancellationToken);
            Task<IEnumerable<Address>> addressesTask = mdDal.ReadAddressesAsync(cancellationToken);
            Task<IEnumerable<Department>> departmentsTask = mdDal.ReadDepartmentsAsync(cancellationToken);
            Task<IEnumerable<Staff>> staffTask = mdDal.ReadStaffAsync(cancellationToken);
            Task<IEnumerable<Company>> companiesTask = mdDal.ReadCompaniesAsync(cancellationToken);

            await Task.WhenAll(
                outgoingInvoicesTask,
                outgoingInvoicePositionsTask,
                costCentersTask,
                costTypesTask,
                projectsTask,
                businessYearsTask,
                contactsTask,
                addressesTask,
                departmentsTask,
                staffTask,
                companiesTask
                );

            IEnumerable<OutgoingInvoice> outgoingInvoices = outgoingInvoicesTask.Result;
            IEnumerable<OutgoingInvoicePosition> outgoingInvoicePositions = outgoingInvoicePositionsTask.Result;
            IEnumerable<CostCenter> costCenters = costCentersTask.Result;
            IEnumerable<CostType> costTypes = costTypesTask.Result;
            IEnumerable<Project> projects = projectsTask.Result;
            IEnumerable<BusinessYear> businessYears = businessYearsTask.Result;
            IEnumerable<Contact> contacts = contactsTask.Result;
            IEnumerable<Address> addresses = addressesTask.Result;
            IEnumerable<Department> departments = departmentsTask.Result;
            IEnumerable<Staff> staff = staffTask.Result;
            IEnumerable<Company> companies = companiesTask.Result;

            IEnumerable<OutgoingInvoicePosition> listOutgoingInvoicePositions;
            long? costCenterId;
            long? costTypeId;
            long? projectId;
            long? departmentId;
            long? addressId;

            foreach (OutgoingInvoice currentInvoice in outgoingInvoices)
            {
                listOutgoingInvoicePositions = outgoingInvoicePositions.Where(oip => oip.RelatedInvoiceId.Equals(currentInvoice.Id)).ToList();

                listPositionMigrations = new();
                foreach (OutgoingInvoicePosition currentInvoicePosition in listOutgoingInvoicePositions)
                {
                    costCenterId = currentInvoicePosition.CostCenterId.HasValue ? currentInvoicePosition.CostCenterId : currentInvoice.CostCenterIdDefault;
                    costTypeId = currentInvoicePosition.CostTypeId.HasValue ? currentInvoicePosition.CostTypeId : currentInvoice.CostTypeIdDefault;
                    projectId = currentInvoicePosition.ProjectId.HasValue ? currentInvoicePosition.ProjectId : currentInvoice.ProjectIdDefault;


                    if (costCenterId.HasValue)
                    { 
                        currentInvoicePosition.RelatedCostCenter = costCenters.Where(cc => cc.Id.Equals(costCenterId.Value)).FirstOrDefault();
                    }
                    if (costTypeId.HasValue)
                    {
                        currentInvoicePosition.RelatedCostType = costTypes.Where(ct => ct.Id.Equals(costTypeId.Value)).FirstOrDefault();
                    }
                    if (projectId.HasValue)
                    {
                        currentInvoicePosition.RelatedProject = projects.Where(p => p.Id.Equals(projectId.Value)).FirstOrDefault();
                    }

                    listPositionMigrations.Add(new OutgoingInvoicePositionMigration() { OutgoingInvoicePositionForImport = currentInvoicePosition });
                }

                currentInvoice.RelatedInvoicePositions = listOutgoingInvoicePositions;

                currentInvoice.RelatedBusinessYear = businessYears.Where(by => by.Id.Equals(currentInvoice.BusinessYearId)).FirstOrDefault();
                
                if (currentInvoice.ContactId.HasValue)
                {
                    currentInvoice.RelatedContact = contacts.Where(c => c.Id.Equals(currentInvoice.ContactId)).FirstOrDefault();
                }

                departmentId = currentInvoice.DepartmentId.HasValue ? currentInvoice.DepartmentId.Value : currentInvoice.RelatedContact?.DepartmentId;

                if (departmentId.HasValue)
                {
                    currentInvoice.RelatedDepartment = departments.Where(d => d.Id.Equals(departmentId.Value)).FirstOrDefault();

                    if (currentInvoice.RelatedDepartment.CompanyId.HasValue)
                    { 
                        currentInvoice.RelatedCompany = companies.Where(c => c.Id.Equals(currentInvoice.RelatedDepartment.CompanyId.Value)).FirstOrDefault();
                    }
                }

                addressId = currentInvoice.AddressId.HasValue ? currentInvoice.AddressId : currentInvoice.RelatedDepartment?.AddressId;

                if (addressId.HasValue)
                {
                    currentInvoice.RelatedAddress = addresses.Where(a => a.Id.Equals(addressId)).FirstOrDefault();
                }

                if (currentInvoice.StaffIdSignature.HasValue)
                {
                    currentInvoice.SignatureBy = staff.Where(s => s.Id.Equals(currentInvoice.StaffIdSignature.Value)).FirstOrDefault();
                }

                if (currentInvoice.StaffIdSendBy.HasValue)
                {
                    currentInvoice.SentBy = staff.Where(s => s.Id.Equals(currentInvoice.StaffIdSendBy.Value)).FirstOrDefault();
                }

                listMigrations.Add(new OutgoingInvoiceMigration() { OutgoingInvoiceForImport = currentInvoice, OutgoingInvoicePositions = listPositionMigrations });
            }

            return listMigrations;
        }

        public async Task<DataTable> GetCostCentersAsync(CancellationToken cancellationToken = default)
        {
            MasterdataDAL dal = new(DPAppConnectionString);
            return await dal.ReadCostCenterAsync(cancellationToken);
        }

    }
}
