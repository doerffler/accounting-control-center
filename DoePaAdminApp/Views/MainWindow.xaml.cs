using Microsoft.Extensions.DependencyInjection;
using System;
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
        private void RBOpenProjectManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageProjekteWindow viewManageProjekte = App.ServiceProvider.GetRequiredService<ManageProjekteWindow>();
            viewManageProjekte.Show();
        }

        private void RBOpenOutgoingInvoices_Click(object sender, RoutedEventArgs e)
        {
            ManageAusgangsrechnungenWindow manageOutgoingInvoices = App.ServiceProvider.GetRequiredService<ManageAusgangsrechnungenWindow>();
            manageOutgoingInvoices.Show();
        }

        private void RBOpenOutgoingInvoicesImport_Click(object sender, RoutedEventArgs e)
        {
            ImportOutgoingInvoicesWindow importOutgoingInvoices = App.ServiceProvider.GetRequiredService<ImportOutgoingInvoicesWindow>();
            importOutgoingInvoices.Show();
        }

        private void RBOpenDebitorManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageDebitorenWindow manageDebitoren = App.ServiceProvider.GetRequiredService<ManageDebitorenWindow>();
            manageDebitoren.Show();
        }

        private void RBOpenKreditorManagement_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RBOpenPostleitzahlManagement_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RBOpenGeschaeftsjahrManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageGeschaeftsjahreWindow manageGeschaeftsjahre = App.ServiceProvider.GetRequiredService<ManageGeschaeftsjahreWindow>();
            manageGeschaeftsjahre.Show();
        }
    }
}
