using DoePaAdminDataModel.Kostenrechnung;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task<int> InitializeMasterdataTablesAsync(CancellationToken cancellationToken = default)
        {
            return await Database.ExecuteSqlRawAsync(Properties.Resources.InitializeMasterdataTables, cancellationToken);
        }

        public DbSet<Abrechnungseinheit> Abrechnungseinheiten { get; set; }

        public DbSet<Kostenstelle> Kostenstellen { get; set; }

        public DbSet<Kostenstellenart> Kostenstellenarten { get; set; }

        public DbSet<Mitarbeiter> Mitarbeiter { get; set; }

        public DbSet<Anstellungsdetail> Anstellungsdetails { get; set; }

        public DbSet<Taetigkeit> Taetigkeiten { get; set; }

        public DbSet<Adresse> Adressen { get; set; }

        public DbSet<Postleitzahl> Postleitzahlen { get; set; }

        public DbSet<Auftrag> Auftraege { get; set; }

        public DbSet<Auftragsposition> Auftragspositionen { get; set; }

        public DbSet<Kunde> Kunden { get; set; }

        public DbSet<Projekt> Projekte { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<Geschaeftsjahr> Geschaeftsjahre { get; set; }

        public DbSet<Ausgangsrechnung> Ausgangsrechnungen { get; set; }

        public DbSet<Ausgangsrechnungsposition> Ausgangsrechnungspositionen { get; set; }
    }
}
