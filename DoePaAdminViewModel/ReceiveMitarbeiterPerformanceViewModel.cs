using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.API;
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
        private ObservableCollection<EmployeeInvoicedHours> _invoicedHours = new();
        public ObservableCollection<EmployeeInvoicedHours> InvoicedHours
        {
            get => _invoicedHours;
            set => SetProperty(ref _invoicedHours, value, true);
        }
        #endregion

        public ReceiveMitarbeiterPerformanceViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

        }

        public  IEnumerable<EmployeeInvoicedHours> GetEmployeeInvoicedHours(int UserId)
        {
            // Filter InvoicedHours

            return _invoicedHours;
        }
    }
}
