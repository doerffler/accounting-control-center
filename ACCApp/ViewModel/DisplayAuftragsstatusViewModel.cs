using ACC.ViewModel.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using ACCDataModel.Stammdaten;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Legends;
using System.Collections.Generic;
using System.Data;
using ACCDataModel.DTO;
using System.Threading;
using CommunityToolkit.Mvvm.Input;
using ACC.ViewModel.Messages;

namespace ACC.ViewModel
{
    public class DisplayAuftragsstatusViewModel : ACCViewModelBase
    {
        public IRelayCommand ExportCommand { get; }
        
        private Geschaeftsjahr _selectedGeschaeftsjahr;
        public Geschaeftsjahr SelectedGeschaeftsjahr
        {
            get => _selectedGeschaeftsjahr;
            set => SetProperty(ref _selectedGeschaeftsjahr, value);
        }

        private ObservableCollection<Auftrag> _auftraege;
        public ObservableCollection<Auftrag> Auftraege
        {
            get => _auftraege;
            set => SetProperty(ref _auftraege, value);
        }

        public DisplayAuftragsstatusViewModel(IACCService doePaAdminService) : base(doePaAdminService)
        {
            SelectedGeschaeftsjahr = Task.Run(async () => await ACCService.GetGeschaeftsjahreAsync()).Result
                .FirstOrDefault(g => g.DatumVon <= DateTime.Now && g.DatumBis >= DateTime.Now);

            Controller = new PlotController();
            Controller.UnbindMouseDown(OxyMouseButton.Left);
            Controller.BindMouseEnter(PlotCommands.HoverSnapTrack);

            Charts = new();

            Auftraege = new();

            PropertyChanged += HandlePropertyChanged;
            
            ExportCommand = new RelayCommand<IEnumerable<RemainingBudgetOnOrdersDTO>>(Export);
        }

        private void Export(IEnumerable<RemainingBudgetOnOrdersDTO> data)
        {
            Messenger.Send(new ExportMessage(data), "ExportChartData");
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedGeschaeftsjahr):
                    Auftraege = new ObservableCollection<Auftrag>(SelectedGeschaeftsjahr.Auftraege);

                    //TODO: Implement Task.Run() here:
                    _ = DrawChartsAsync();

                    break;
            }
        }

        private async Task DrawChartsAsync(CancellationToken cancellationToken = default)
        {
            Charts.Clear();

            foreach (Auftrag auftrag in Auftraege)
            {
                PlotModel Chart = new() { Title = auftrag.Auftragsname };
                var Table = new List<RemainingBudgetOnOrdersDTO>();

                Chart.Legends.Add(new Legend()
                {
                    LegendTitle = "Legende",
                    LegendPosition = LegendPosition.BottomCenter,
                });

                // Zeitraum
                double beginnDouble = Axis.ToDouble(auftrag.Auftragsbeginn);
                double endeDouble = Axis.ToDouble(auftrag.Auftragsende);

                Chart.Axes.Add(new DateTimeAxis
                {
                    Position = AxisPosition.Bottom,
                    Title = "Zeitraum",
                    StringFormat = "dd.MM.yyyy",
                    IsPanEnabled = false,
                    IsZoomEnabled = false,
                    MinorIntervalType = DateTimeIntervalType.Days,
                    IntervalType = DateTimeIntervalType.Days,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.None,
                    Minimum = beginnDouble,
                    Maximum = endeDouble
                });

                Chart.Axes.Add(new LinearAxis
                {
                    Position = AxisPosition.Left,
                    Title = "Volumen",
                    IsPanEnabled = false,
                    IsZoomEnabled = false,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.None,
                    Minimum = 0
                });

                foreach (Auftragsposition auftragsposition in auftrag.Auftragspositionen)
                {
                    OxyColor color = GetRandomOxyColor();

                    // Soll Verlauf
                    LineSeries soll = new()
                    {
                        StrokeThickness = 2,
                        Dashes = new double[] { 1 },
                        Color = color,
                        Title = string.Format("{0} (Soll)", auftragsposition.Positionsbezeichnung)
                    };
                    soll.Points.Add(new(beginnDouble, (double)auftragsposition.Auftragsvolumen));
                    soll.Points.Add(new(endeDouble, 0));
                    
                    Chart.Series.Add(soll);

                    // Ist Verlauf
                    LineSeries ist = new()
                    {
                        StrokeThickness = 2,
                        Color = color,
                        Title = string.Format("{0} (Ist)", auftragsposition.Positionsbezeichnung)
                    };
                    
                    IEnumerable<RemainingBudgetOnOrdersDTO> chartPositions = await ACCService.GetRemainingBudgetOnOrdersAsync(auftragsposition.AuftragspositionID, cancellationToken);
                    
                    chartPositions.ToList().ForEach(pos =>
                    {
                        double day = Axis.ToDouble(pos.Date);
                        double remaining = (double)pos.ResidualBudgetActualAfter;

                        ist.Points.Add(new(day, remaining));
                    });

                    Chart.Series.Add(ist);
                    Table.AddRange(chartPositions);
                    Table = Table.OrderBy(pos => pos.Date).ToList();
                }

                Charts.Add(new ExportChartDTO{Chart = Chart, Table = Table});
            }
        }

        public ObservableCollection<ExportChartDTO> Charts { get; }
        public PlotController Controller { get; }

        private static OxyColor GetRandomOxyColor()
        {
            Random random = new();

            byte r = (byte)random.Next(0, 255);
            byte g = (byte)random.Next(0, 255);
            byte b = (byte)random.Next(0, 255);

            return OxyColor.FromRgb(r, g, b);
        }
    }
}
