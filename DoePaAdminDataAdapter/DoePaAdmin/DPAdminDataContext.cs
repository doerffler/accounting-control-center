using DoePaAdminDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataAdapter.DoePaAdmin
{
    public class DPAdminDataContext : DbContext
    {

        public string ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            builder.UseSqlServer(ConnectionString);

        }

        public DbSet<Kostenstelle> Kostenstellen { get; set; }

        public DbSet<Kostenstellenart> Kostenstellenarten { get; set; }

    }
}
