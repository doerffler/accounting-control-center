using DoePaAdmin.ViewModel.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using DoePaAdminDataModel.Stammdaten;
using System.Collections.ObjectModel;
using System.ComponentModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Legends;

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

        private ObservableCollection<Auftrag> _auftraege;
        public ObservableCollection<Auftrag> Auftraege
        {
            get => _auftraege;
            set => SetProperty(ref _auftraege, value);
        }

        public DisplayAuftragsstatusViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            SelectedGeschaeftsjahr = Task.Run(async () => await DoePaAdminService
                .GetGeschaeftsjahreAsync())
                .Result
                .FirstOrDefault(g => g.DatumVon <= DateTime.Now && g.DatumBis >= DateTime.Now);

            Controller = new PlotController();
            Controller.UnbindMouseDown(OxyMouseButton.Left);
            Controller.BindMouseEnter(PlotCommands.HoverSnapTrack);

            Charts = new();

            Auftraege = new();

            PropertyChanged += HandlePropertyChanged;
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedGeschaeftsjahr):
                    Auftraege = new ObservableCollection<Auftrag>(SelectedGeschaeftsjahr.Auftraege);

                    Charts.Clear();

                    foreach (Auftrag auftrag in Auftraege)
                    {                        
                        PlotModel Chart = new() { Title = auftrag.Auftragsname };
                        Chart.Legends.Add(new Legend()
                        {
                            LegendTitle = "Legende",
                            LegendPosition = LegendPosition.TopCenter,
                        });

                        CategoryAxis Month = new() { Position = AxisPosition.Bottom, Title = "Monat" };
                        Month.Labels.Add("Januar");
                        Month.Labels.Add("Februar");
                        Month.Labels.Add("März");
                        Chart.Axes.Add(Month);

                        Chart.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Restbudget" });

                        foreach (Auftragsposition auftragsposition in auftrag.Auftragspositionen)
                        {
                            LineSeries soll = new();
                            soll.Title = string.Format("{0} (Soll)", auftragsposition.Positionsbezeichnung);

                            soll.Points.Add(new DataPoint(0, 660));
                            soll.Points.Add(new DataPoint(1, 520));
                            soll.Points.Add(new DataPoint(2, 380));

                            Chart.Series.Add(soll);
                        }

                        Charts.Add(Chart);
                    }

                    break;
            }
        }

        public ObservableCollection<PlotModel> Charts { get; private set; }
        public PlotController Controller { get; private set; }
    }
}
