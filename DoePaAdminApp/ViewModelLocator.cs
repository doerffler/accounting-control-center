using DoePaAdmin.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace DoePaAdminApp.ViewModels
{
    public class ViewModelLocator
    {

        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();

        public ImportKostenstellenViewModel ImportKostenstellenViewModel => App.ServiceProvider.GetRequiredService<ImportKostenstellenViewModel>();

        public ManageKostenstellenViewModel ManageKostenstellenViewModel => App.ServiceProvider.GetRequiredService<ManageKostenstellenViewModel>();

        public ManageMitarbeiterViewModel ManageMitarbeiterViewModel => App.ServiceProvider.GetRequiredService<ManageMitarbeiterViewModel>();

        public ManageAuftraegeViewModel ManageAuftraegeViewModel => App.ServiceProvider.GetRequiredService<ManageAuftraegeViewModel>();

    }
}
