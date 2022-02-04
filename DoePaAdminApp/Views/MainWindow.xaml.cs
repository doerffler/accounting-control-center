using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void RBOpenCostCenterImport_Click(object sender, RoutedEventArgs e)
        {
            ImportKostenstellen viewImportCostCenter = new ImportKostenstellen
            {
                Owner = this
            };

            viewImportCostCenter.Show();
        }
    }
}
