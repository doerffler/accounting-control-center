using System.Text;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ACCApp.Views;

public partial class ExportChartDataWindow : Window
{
    public ExportChartDataWindow()
    {
        InitializeComponent();
    }

    private void BtnExport_Click(object sender, RoutedEventArgs e)
    {
        Export.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
        Export.SelectAll();

        ApplicationCommands.Copy.Execute(null, Export);

        Export.UnselectAll();
    }
}