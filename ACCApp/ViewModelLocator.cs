using ACC.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace ACCApp.ViewModels
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
        
        public ImportIncomingInvoicesViewModel ImportIncomingInvoicesViewModel => App.ServiceProvider.GetRequiredService<ImportIncomingInvoicesViewModel>();

        public ManageAusgangsrechnungenViewModel ManageAusgangsrechnungenViewModel => App.ServiceProvider.GetRequiredService<ManageAusgangsrechnungenViewModel>();

        public ManageEingangsrechnungenViewModel ManageEingangsrechnungenViewModel => App.ServiceProvider.GetRequiredService<ManageEingangsrechnungenViewModel>();

        public ManageDebitorenViewModel ManageDebitorenViewModel => App.ServiceProvider.GetRequiredService<ManageDebitorenViewModel>();

        public ManagePostleitzahlenViewModel ManagePostleitzahlenViewModel => App.ServiceProvider.GetRequiredService<ManagePostleitzahlenViewModel>();

        public ManageGeschaeftsjahreViewModel ManageGeschaeftsjahreViewModel => App.ServiceProvider.GetRequiredService<ManageGeschaeftsjahreViewModel>();

        public ManageKundenViewModel ManageKundenViewModel => App.ServiceProvider.GetRequiredService<ManageKundenViewModel>();

        public ManageWaehrungViewModel ManageWaehrungViewModel => App.ServiceProvider.GetRequiredService<ManageWaehrungViewModel>();

        public ManageTaetigkeitViewModel ManageTaetigkeitViewModel => App.ServiceProvider.GetRequiredService<ManageTaetigkeitViewModel>();

        public ManageKostenstellenartViewModel ManageKostenstellenartViewModel => App.ServiceProvider.GetRequiredService<ManageKostenstellenartViewModel>();

        public ManageAbrechnungseinheitViewModel ManageAbrechnungseinheitViewModel => App.ServiceProvider.GetRequiredService<ManageAbrechnungseinheitViewModel>();

        public ManageSkillsViewModel ManageSkillsViewModel => App.ServiceProvider.GetRequiredService<ManageSkillsViewModel>();

        public DisplayAuftragsstatusViewModel DisplayAuftragsstatusViewModel => App.ServiceProvider.GetRequiredService<DisplayAuftragsstatusViewModel>();

        public ExportChartDataViewModel ExportChartDataViewModel => App.ServiceProvider.GetRequiredService<ExportChartDataViewModel>();

        public SelectSkillsViewModel SelectSkillsViewModel => App.ServiceProvider.GetRequiredService<SelectSkillsViewModel>();
        
        public ManageKreditorenViewModel ManageKreditorenViewModel => App.ServiceProvider.GetRequiredService<ManageKreditorenViewModel>();
    }
}
