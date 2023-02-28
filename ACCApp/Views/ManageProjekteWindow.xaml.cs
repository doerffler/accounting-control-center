using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ACCApp.Views
{
    /// <summary>
    /// Interaction logic for ManageProjekteWindow.xaml
    /// </summary>
    public partial class ManageProjekteWindow : Window
    {
        public ManageProjekteWindow()
        {
            InitializeComponent();
        }

        private void ManageSkills_Click(object sender, RoutedEventArgs e)
        {
            ManageSkillsWindow manageSkills = App.ServiceProvider.GetRequiredService<ManageSkillsWindow>();
            manageSkills.Show();
        }
    }
}
