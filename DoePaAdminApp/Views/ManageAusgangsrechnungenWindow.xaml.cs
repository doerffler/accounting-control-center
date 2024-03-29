﻿using Microsoft.Extensions.DependencyInjection;
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

namespace DoePaAdminApp.Views
{
    /// <summary>
    /// Interaktionslogik für ManageAusgangsrechnungenWindow.xaml
    /// </summary>
    public partial class ManageAusgangsrechnungenWindow : Window
    {
        public ManageAusgangsrechnungenWindow()
        {
            InitializeComponent();
        }

        private void HandleBtnManageDebitor_Click(object sender, RoutedEventArgs e)
        {
            ManageDebitorenWindow manageDebitoren = App.ServiceProvider.GetRequiredService<ManageDebitorenWindow>();
            manageDebitoren.Show();
        }
    }
}
