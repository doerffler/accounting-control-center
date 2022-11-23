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
using System.Collections.Generic;
using Microsoft.VisualBasic;
using DoePaAdminDataModel.Kostenrechnung;

namespace DoePaAdmin.ViewModel
{
    public class DisplayAuftragsstatusViewModel : DoePaAdminViewModelBase
    {
        protected readonly List<OxyColor> Colors = new List<OxyColor>()
        {
                OxyColor.FromRgb(0x4E, 0x9A, 0x06),
                OxyColor.FromRgb(0xC8, 0x8D, 0x00),
                OxyColor.FromRgb(0xCC, 0x00, 0x00),
                OxyColor.FromRgb(0x20, 0x4A, 0x87),
                OxyColors.Red,
                OxyColors.Orange,
                OxyColors.Yellow,
                OxyColors.Green,
                OxyColors.Blue,
                OxyColors.Indigo,
                OxyColors.Violet
        };

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
                            LegendPosition = LegendPosition.BottomCenter,
                        });

                        // Zeitraum
                        double beginnDouble = Axis.ToDouble(auftrag.Auftragsbeginn);
                        double endeDouble = Axis.ToDouble(auftrag.Auftragsende);

                        Chart.Axes.Add(new DateTimeAxis()
                        {
                            Position = AxisPosition.Bottom,
                            Title = "Zeitraum",
                            StringFormat = "dd.MM.yyyy",
                            MinorIntervalType = DateTimeIntervalType.Days,
                            IntervalType = DateTimeIntervalType.Days,
                            MajorGridlineStyle = LineStyle.Solid,
                            MinorGridlineStyle = LineStyle.None,
                            Minimum = beginnDouble,
                            Maximum = endeDouble
                        });

                        Chart.Axes.Add(new LinearAxis()
                        {
                            Position = AxisPosition.Left,
                            Title = "Volumen",
                            MajorGridlineStyle = LineStyle.Solid,
                            MinorGridlineStyle = LineStyle.None,
                            Minimum = 0
                        });

                        foreach (Auftragsposition auftragsposition in auftrag.Auftragspositionen)
                        {
                            // Soll Verlauf
                            LineSeries soll = new()
                            {
                                StrokeThickness = 2,
                                Dashes = new double[] {1},
                                Color = Colors[auftragsposition.AuftragspositionNummer],
                                Title = string.Format("{0} (Soll)", auftragsposition.Positionsbezeichnung)
                            };
                            soll.Points.Add(new(beginnDouble, (double)auftragsposition.Auftragsvolumen));
                            soll.Points.Add(new(endeDouble, 0));
                            Chart.Series.Add(soll);

                            // Ist Verlauf
                            LineSeries ist = new()
                            {
                                StrokeThickness = 2,
                                Color = Colors[auftragsposition.AuftragspositionNummer],
                                Title = string.Format("{0} (Ist)", auftragsposition.Positionsbezeichnung)
                            };
                            ist.Points.Add(new(beginnDouble, (double)auftragsposition.Auftragsvolumen));

                            // Ausgangsrechnungspositionen abfragen
                            Task.Run(async () => await DoePaAdminService.GetAusgangsrechnungenAsync())
                                .Result
                                .SelectMany(rechnung => rechnung.Rechnungspositionen)
                                .Where(arp => arp.ZugehoerigeAuftragsposition.AuftragspositionID == auftragsposition.AuftragspositionID)
                                .GroupBy(arp => arp.LeistungszeitraumBis)
                                .Select(arp => new
                                {
                                    Date = arp.Key.Date,
                                    Remaining = arp.Sum(rb => rb.Stueckzahl)
                                }).ToList().ForEach(pos =>
                                {
                                    DataPoint last = ist.Points.Last();
                                    
                                    double day = Axis.ToDouble(pos.Date);
                                    double remaining = last.Y - (double)pos.Remaining;

                                    ist.Points.Add(new(day, remaining));
                                });

                            Chart.Series.Add(ist);
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
