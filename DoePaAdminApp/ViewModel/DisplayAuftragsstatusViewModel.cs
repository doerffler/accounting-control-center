using DoePaAdmin.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoePaAdminDataModel.Stammdaten;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveCharts;
using LiveCharts.Wpf;

namespace DoePaAdmin.ViewModel
{
    public class DisplayAuftragsstatusViewModel : DoePaAdminViewModelBase
    {
        private Geschaeftsjahr _selectedGeschaeftsjahr;
        public Geschaeftsjahr SelectedGeschaeftsjahr
        {
            get => _selectedGeschaeftsjahr;
            set => SetProperty(ref _selectedGeschaeftsjahr, value);
        }

        private SeriesCollection _chartModel;
        public SeriesCollection ChartModel
        {
            get => _chartModel;
            set => SetProperty(ref _chartModel, value);
        }

        private string[] _labels;
        public string[] Labels
        {
            get => _labels;
            set => SetProperty(ref _labels, value);
        }

        public DisplayAuftragsstatusViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            SelectedGeschaeftsjahr = Task.Run(async () => await DoePaAdminService
                .GetGeschaeftsjahreAsync())
                .Result
                .FirstOrDefault(g => g.DatumVon <= DateTime.Now && g.DatumBis >= DateTime.Now);

            ChartModel = new()
            {
                new ColumnSeries
                {
                    Title = "Draw Character Graphics",
                    Values = new ChartValues<double> { 800, 500, 400 }     
                },

                new ColumnSeries
                {
                    Title = "Implement Gameengine",
                    Values = new ChartValues<double> { 600, 500, 250 }
                },
            };

            Labels = new[] { "Januar", "Februar", "März" };
        }
    }
}
