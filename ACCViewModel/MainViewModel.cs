using ACC.ViewModel.Model;
using ACC.ViewModel.Services;
using Microsoft.Extensions.Options;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel
{
    public class MainViewModel : ObservableRecipient
    {

        private string _input;

        public string Input
        {
            get => _input;
            set => SetProperty(ref _input, value, true);
        }

        private readonly IACCService doePaAdminService;
        private readonly AppSettings settings;

        public IRelayCommand ExecuteCommand { get; }

        public IRelayCommand GenerateTestdataCommand { get; }

        public MainViewModel(IACCService accService, IOptions<AppSettings> options, IUserInteractionService userInteractionService)
        {
            this.doePaAdminService = accService;
            settings = options.Value;

            ExecuteCommand = new RelayCommand(async () => await ExecuteAsync());
            GenerateTestdataCommand = new AsyncRelayCommand(DoGenerateTestdataAsync);
        }

        private async Task DoGenerateTestdataAsync(CancellationToken cancellationToken = default)
        {
            await ACCTestDataCreator.CreateCompleteTestDataAsync(doePaAdminService, cancellationToken);
        }

        private Task ExecuteAsync()
        {
            Debug.WriteLine($"Current value: {_input}");
            return Task.CompletedTask;
        }
    }
}
