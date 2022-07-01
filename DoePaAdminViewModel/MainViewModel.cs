using DoePaAdmin.ViewModel.Model;
using DoePaAdmin.ViewModel.Services;
using Microsoft.Extensions.Options;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using OxyPlot;
using OxyPlot.Series;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class MainViewModel : ObservableRecipient
    {

        private string _input;

        public string Input
        {
            get => _input;
            set => SetProperty(ref _input, value, true);
        }

        private readonly IDoePaAdminService doePaAdminService;
        private readonly AppSettings settings;

        public IRelayCommand ExecuteCommand { get; }

        public IRelayCommand GenerateTestdataCommand { get; }

        public MainViewModel(IDoePaAdminService doePaAdminService, IOptions<AppSettings> options)
        {
            this.doePaAdminService = doePaAdminService;
            settings = options.Value;

            ExecuteCommand = new RelayCommand(async () => await ExecuteAsync());
            GenerateTestdataCommand = new AsyncRelayCommand(DoGenerateTestdataAsync);

            // Create the plot model
            var tmp = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };

            // Create two line series (markers are hidden by default)
            var series1 = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Circle };
            series1.Points.Add(new DataPoint(0, 0));
            series1.Points.Add(new DataPoint(10, 18));
            series1.Points.Add(new DataPoint(20, 12));
            series1.Points.Add(new DataPoint(30, 8));
            series1.Points.Add(new DataPoint(40, 15));

            var series2 = new LineSeries { Title = "Series 2", MarkerType = MarkerType.Square };
            series2.Points.Add(new DataPoint(0, 4));
            series2.Points.Add(new DataPoint(10, 12));
            series2.Points.Add(new DataPoint(20, 16));
            series2.Points.Add(new DataPoint(30, 25));
            series2.Points.Add(new DataPoint(40, 5));


            // Add the series to the plot model
            tmp.Series.Add(series1);
            tmp.Series.Add(series2);

            // Axes are created automatically if they are not defined

            // Set the Model property, the INotifyPropertyChanged event will make the WPF Plot control update its content
            this.Model = tmp;
        }

        private async Task DoGenerateTestdataAsync(CancellationToken cancellationToken = default)
        {
            await DoePaAdminTestDataCreator.CreateCompleteTestDataAsync(doePaAdminService, cancellationToken);
        }

        private Task ExecuteAsync()
        {
            Debug.WriteLine($"Current value: {_input}");
            return Task.CompletedTask;
        }

        public PlotModel Model { get; private set; }
    }
}
