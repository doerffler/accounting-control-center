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

            if (dbContext.Database.EnsureCreated())
            {
                _ = Task.Run(async () => await dbContext.InitializeMasterdataTablesAsync());
            }

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

        public async Task<Kostenstelle> CreateKostenstelleAsync(CancellationToken cancellationToken = default)
        {
            Kostenstelle newKostenstelle = new();
            _ = await DBContext.Kostenstellen.AddAsync(newKostenstelle, cancellationToken);
            return newKostenstelle;
        }

        public async Task<bool> CheckForChangesAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => DBContext.ChangeTracker.HasChanges(), cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DBContext.SaveChangesAsync(cancellationToken);
        }

    }
}
