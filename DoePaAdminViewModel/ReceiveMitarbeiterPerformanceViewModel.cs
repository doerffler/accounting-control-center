using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.DTO;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ReceiveMitarbeiterPerformanceViewModel : DoePaAdminViewModelBase
    {
        #region InvoicedHours
        private IEnumerable<EmployeeAccountingDTO> _accountingInformation;
        public IEnumerable<EmployeeAccountingDTO> AccountingInformation
        {
            get => _accountingInformation;
            set => SetProperty(ref _accountingInformation, value, true);
        }
        #endregion

        public ReceiveMitarbeiterPerformanceViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

        }

        public async Task<IEnumerable<EmployeeAccountingDTO>> GetEmployeeAccountingAsync(string email, DateTime from, DateTime to)
        {            
            return await DoePaAdminService.GetEmployeeAccountingAsync(email, from, to, default);
        }
    }
}
