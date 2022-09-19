using DoePaAdmin.ViewModel.Model;
using DoePaAdmin.ViewModel.Services;
using Microsoft.Extensions.Options;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
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

        public MainViewModel(IDoePaAdminService doePaAdminService, IOptions<AppSettings> options, IUserInteractionService userInteractionService)
        {
            this.doePaAdminService = doePaAdminService;
            settings = options.Value;

            ExecuteCommand = new RelayCommand(async () => await ExecuteAsync());
            GenerateTestdataCommand = new AsyncRelayCommand(DoGenerateTestdataAsync);
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
    }
}
