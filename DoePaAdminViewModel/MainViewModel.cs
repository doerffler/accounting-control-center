using DoePaAdmin.ViewModel.Model;
using DoePaAdmin.ViewModel.Services;
using Microsoft.Extensions.Options;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Diagnostics;
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

        private readonly ISampleService sampleService;
        private readonly AppSettings settings;

        public RelayCommand ExecuteCommand { get; }

        public MainViewModel(ISampleService sampleService, IOptions<AppSettings> options)
        {
            this.sampleService = sampleService;
            settings = options.Value;

            ExecuteCommand = new RelayCommand(async () => await ExecuteAsync());
        }

        private Task ExecuteAsync()
        {
            Debug.WriteLine($"Current value: {_input}");
            return Task.CompletedTask;
        }
    }
}
