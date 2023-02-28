using ACC.ViewModel.Services;
using ACCDataModel.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.ViewModel
{
    public class ReceiveMitarbeiterPerformanceViewModel : ACCViewModelBase
    {
        public ReceiveMitarbeiterPerformanceViewModel(IACCService accService) : base(accService)
        {

        }

        public async Task<IEnumerable<EmployeeAccountingDTO>> GetEmployeeAccountingAsync(string email, DateTime from, DateTime to)
        {            
            return await ACCService.GetEmployeeAccountingAsync(email, from, to, default);
        }
    }
}
