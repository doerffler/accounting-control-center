using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel.Services
{
    public interface IDoePaAdminService
    {
        public Task<ObservableCollection<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default);
        public Task<ObservableCollection<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default);
    }
}
