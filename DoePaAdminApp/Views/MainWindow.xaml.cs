using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;
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
            ManagePostleitzahlenWindow managePostleitzahlen = App.ServiceProvider.GetRequiredService<ManagePostleitzahlenWindow>();
            managePostleitzahlen.Show();
        }

        private void RBOpenGeschaeftsjahrManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageGeschaeftsjahreWindow manageGeschaeftsjahre = App.ServiceProvider.GetRequiredService<ManageGeschaeftsjahreWindow>();
            manageGeschaeftsjahre.Show();
        }

        private void RBOpenAbrechnungseinheitManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageAbrechnungseinheitWindow manageAbrechnungseinheit = App.ServiceProvider.GetRequiredService<ManageAbrechnungseinheitWindow>();
            manageAbrechnungseinheit.Show();
        }

        private void RBOpenKostenstellenartManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageKostenstellenartWindow manageKostenstellenart = App.ServiceProvider.GetRequiredService<ManageKostenstellenartWindow>();
            manageKostenstellenart.Show();
        }

        private void RBOpenTaetigkeitManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageTaetigkeitWindow manageTaetigkeit = App.ServiceProvider.GetRequiredService<ManageTaetigkeitWindow>();
            manageTaetigkeit.Show();
        }

        private void RBOpenWaehrungManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageWaehrungWindow manageWaehrung = App.ServiceProvider.GetRequiredService<ManageWaehrungWindow>();
            manageWaehrung.Show();
        }

        private void RBOpenKundenManagement_Click(object sender, RoutedEventArgs e)
        {
            ManageKundenWindow manageKunden = App.ServiceProvider.GetRequiredService<ManageKundenWindow>();
            manageKunden.Show();
        }

        private void BTNExport_Click(object sender, RoutedEventArgs e)
        {
            ExportChartDataWindow exportChartData = App.ServiceProvider.GetRequiredService<ExportChartDataWindow>();
            exportChartData.Show();
        }
    }
}
