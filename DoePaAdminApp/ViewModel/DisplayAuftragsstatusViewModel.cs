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
using System.Windows.Media;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

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

        public DisplayAuftragsstatusViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            SelectedGeschaeftsjahr = Task.Run(async () => await DoePaAdminService
                .GetGeschaeftsjahreAsync())
                .Result
                .FirstOrDefault(g => g.DatumVon <= DateTime.Now && g.DatumBis >= DateTime.Now);

            Charts = new();

            var Chart = new PlotModel { Title = "Example 1" };
            Chart.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

            Charts.Add(Chart);

            Chart = new PlotModel { Title = "Example 2" };
            Chart.Series.Add(new FunctionSeries(Math.Tan, 0, 10, 0.1, "tan(x)"));

            Charts.Add(Chart);

            Chart = new PlotModel { Title = "Example 3" };
            Chart.Series.Add(new FunctionSeries(Math.Sin, 0, 10, 0.1, "sin(x)"));

            Charts.Add(Chart);
        }

        public ObservableCollection<PlotModel> Charts { get; private set; }
    }
}
