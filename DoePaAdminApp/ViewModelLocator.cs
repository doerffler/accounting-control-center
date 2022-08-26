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

        public ManageProjekteViewModel ManageProjekteViewModel => App.ServiceProvider.GetRequiredService<ManageProjekteViewModel>();

        public ImportOutgoingInvoicesViewModel ImportOutgoingInvoicesViewModel => App.ServiceProvider.GetRequiredService<ImportOutgoingInvoicesViewModel>();

        public ManageAusgangsrechnungenViewModel ManageAusgangsrechnungenViewModel => App.ServiceProvider.GetRequiredService<ManageAusgangsrechnungenViewModel>();

        public ManageDebitorenViewModel ManageDebitorenViewModel => App.ServiceProvider.GetRequiredService<ManageDebitorenViewModel>();

        public ManagePostleitzahlenViewModel ManagePostleitzahlenViewModel => App.ServiceProvider.GetRequiredService<ManagePostleitzahlenViewModel>();

        public ManageGeschaeftsjahreViewModel ManageGeschaeftsjahreViewModel => App.ServiceProvider.GetRequiredService<ManageGeschaeftsjahreViewModel>();

        public ManageFeiertageViewModel ManageFeiertageViewModel => App.ServiceProvider.GetRequiredService<ManageFeiertageViewModel>();

        public ManageKundenViewModel ManageKundenViewModel => App.ServiceProvider.GetRequiredService<ManageKundenViewModel>();

    }
}
