using Microsoft.Extensions.DependencyInjection;

namespace DoePaAdminApp.ViewModels
{
    public class ViewModelLocator
    {

        public MainViewModel MainViewModel => App.ServiceProvider.GetRequiredService<MainViewModel>();

        public ImportKostenstellenViewModel ImportKostenstellenViewModel => App.ServiceProvider.GetRequiredService<ImportKostenstellenViewModel>();

    }
}
