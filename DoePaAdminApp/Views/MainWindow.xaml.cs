using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls.Ribbon;

namespace DoePaAdminApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RBOpenCostCenterManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageKostenstellenWindow viewManageKostenstellen = App.ServiceProvider.GetRequiredService<ManageKostenstellenWindow>();
            viewManageKostenstellen.Show();
        }

        private void RBOpenWorkforceManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageMitarbeiterWindow viewManageMitarbeiter = App.ServiceProvider.GetRequiredService<ManageMitarbeiterWindow>();
            viewManageMitarbeiter.Show();
        }

        private void RBOpenOrderManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageAuftraegeWindow viewManageAuftraege = App.ServiceProvider.GetRequiredService<ManageAuftraegeWindow>();
            viewManageAuftraege.Show();
        }
    }
}
