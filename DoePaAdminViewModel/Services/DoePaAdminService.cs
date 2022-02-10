using DoePaAdmin.ViewModel.Model;
using DoePaAdminDataAdapter.DoePaAdmin;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel.Services
{
    public class DoePaAdminService : IDoePaAdminService
    {

        private string DoePaAdminConnectionString { get; set; }

        private DPAdminDataContext DBContext { get; set; }

        public DoePaAdminService(IOptions<DoePaAdminConnectionSettings> doePaAdminConnectionSettings)
        {
            DoePaAdminConnectionString = doePaAdminConnectionSettings.Value.ConnectionString;

            DPAdminDataContext dbContext = new()
            {
                ConnectionString = DoePaAdminConnectionString
            };

            dbContext.Database.EnsureCreated();
            DBContext = dbContext;
        }

        public async Task<ObservableCollection<Kostenstelle>> GetKostenstellenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Kostenstelle> result = DBContext.Kostenstellen;
            Task<List<Kostenstelle>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Kostenstelle> listKostenstellen = await taskToListAsync;
            ObservableCollection<Kostenstelle> kostenstellen = new(listKostenstellen);

            return kostenstellen;
        }

        public async Task<ObservableCollection<Kostenstellenart>> GetKostenstellenartenAsync(CancellationToken cancellationToken = default)
        {
            IQueryable<Kostenstellenart> result = DBContext.Kostenstellenarten;
            Task<List<Kostenstellenart>> taskToListAsync = result.ToListAsync(cancellationToken);
            List<Kostenstellenart> listKostenstellenarten = await taskToListAsync;
            ObservableCollection<Kostenstellenart> kostenstellenarten = new(listKostenstellenarten);

            return kostenstellenarten;
        }

    }
}
