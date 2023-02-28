using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using ACC.ViewModel.Model;
using ACC.ViewModel.Services;
using ACC.ViewModel;
using ACCApp.Views;
using ACCApp.Services;
using System.Threading;

namespace ACCApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private readonly IHost host;
        private readonly dynamic config;

        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            config = System.Configuration.ConfigurationManager.AppSettings;
            Thread.CurrentThread.CurrentUICulture = new(config["Language"]);

            host = Host.CreateDefaultBuilder()  // Use default settings
                                                //new HostBuilder()          // Initialize an empty HostBuilder
                    .ConfigureAppConfiguration((context, builder) =>
                    {
                        // Add other configuration files...
                        builder.AddJsonFile("appsettings.Development.json", optional: true);
                    }).ConfigureServices((context, services) =>
                    {
                        ConfigureServices(context.Configuration, services);
                    })
                    .ConfigureLogging(logging =>
                    {
                        // Add other loggers...
                    })
                    .Build();

            ServiceProvider = host.Services;

        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {

            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
            services.Configure<DPAppConnectionSettings>(configuration.GetSection(nameof(DPAppConnectionSettings)));
            services.Configure<ACCConnectionSettings>(configuration.GetSection(nameof(ACCConnectionSettings)));

            services.AddScoped<IDPAppService, DPAppService>();
            services.AddScoped<IACCService, ACCService>();
            services.AddScoped<IUserInteractionService, UserInteractionService>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ManageKostenstellenViewModel>();
            services.AddSingleton<ManageMitarbeiterViewModel>();
            services.AddSingleton<ManageProjekteViewModel>();
            services.AddSingleton<ManageAuftraegeViewModel>();
            services.AddSingleton<ImportOutgoingInvoicesViewModel>();
            services.AddSingleton<ImportKostenstellenViewModel>();
            services.AddSingleton<ManageAusgangsrechnungenViewModel>();
            services.AddSingleton<ManageDebitorenViewModel>();
            services.AddSingleton<ManagePostleitzahlenViewModel>();
            services.AddSingleton<ManageKundenViewModel>();
            services.AddSingleton<ManageGeschaeftsjahreViewModel>();
            services.AddSingleton<ManageAbrechnungseinheitViewModel>();
            services.AddSingleton<ManageKostenstellenartViewModel>();
            services.AddSingleton<ManageTaetigkeitViewModel>();
            services.AddSingleton<ManageWaehrungViewModel>();
            services.AddSingleton<ManageSkillsViewModel>();
            services.AddSingleton<DisplayAuftragsstatusViewModel>();
            services.AddSingleton<ExportChartDataViewModel>();

            services.AddTransient<MainWindow>();
            services.AddTransient<ManageKostenstellenWindow>();
            services.AddTransient<ManageMitarbeiterWindow>();
            services.AddTransient<ManageProjekteWindow>();
            services.AddTransient<ImportOutgoingInvoicesWindow>();
            services.AddTransient<ImportKostenstellenWindow>();
            services.AddTransient<ManageAuftraegeWindow>();
            services.AddTransient<AskForUserInputWindow>();
            services.AddTransient<ManageAusgangsrechnungenWindow>();
            services.AddTransient<ManageDebitorenWindow>();
            services.AddTransient<ManagePostleitzahlenWindow>();
            services.AddTransient<ManageKundenWindow>();
            services.AddTransient<ManageGeschaeftsjahreWindow>();
            services.AddTransient<ManageAbrechnungseinheitWindow>();
            services.AddTransient<ManageKostenstellenartWindow>();
            services.AddTransient<ManageTaetigkeitWindow>();
            services.AddTransient<ManageWaehrungWindow>();
            services.AddTransient<ManageSkillsWindow>();
            services.AddTransient<ExportChartDataWindow>();
        }

        private static void ConfigureMessengers()
        {

        }
        
        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            ConfigureMessengers();

            var window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
