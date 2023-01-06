using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.DTO;
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
        public ReceiveMitarbeiterPerformanceViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

        }

        public async Task<IEnumerable<EmployeeAccountingDTO>> GetEmployeeAccountingAsync(string email, DateTime from, DateTime to)
        {            
            return await DoePaAdminService.GetEmployeeAccountingAsync(email, from, to, default);
        }
    }
}
