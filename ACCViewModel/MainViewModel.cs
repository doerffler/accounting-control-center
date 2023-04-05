using ACC.ViewModel.Model;
using ACC.ViewModel.Services;
using Microsoft.Extensions.Options;
using CommunityToolkit.Mvvm.Input;
using System.Threading;
using System.Threading.Tasks;
using System;
using ACC.ViewModel.Messages;
using ACCDataModel.Stammdaten;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ACC.ViewModel
{
    public class MainViewModel : ACCViewModelBase 
    {

        private string _input;

        public string Input
        {
            get => _input;
            set => SetProperty(ref _input, value, true);
        }

        private Geschaeftsjahr _selectedGeschaeftsjahr;
        public Geschaeftsjahr SelectedGeschaeftsjahr
        {
            get 
            {
                return _selectedGeschaeftsjahr;
            }
            set
            {
                SetProperty(ref _selectedGeschaeftsjahr, value);
                Messenger.Send(new BusinessYearChangedMessage(SelectedGeschaeftsjahr), "Business year changed");
            }
        }

        private readonly IACCService accService;
        private readonly AppSettings settings;

        public IRelayCommand RefreshCommand { get; }
        
        public IRelayCommand ExecuteCommand { get; }

        public IRelayCommand GenerateTestdataCommand { get; }

        public IRelayCommand MainViewLoadedCommand { get; }

        private string _statusbarMessage;
        public string StatusbarMessage
        {
            get => _statusbarMessage;
            set => SetProperty(ref _statusbarMessage, value, true);
        }

        public MainViewModel(IACCService accService, IOptions<AppSettings> options, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            Messenger.Register<MainViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
            Messenger.Register<MainViewModel, StatusbarMessage, string>(this, "Statusbar", (r, m) => r.OnStatusbarReceive(m));

            this.accService = accService;

            RefreshCommand = new RelayCommand(RefreshAsync);
            GenerateTestdataCommand = new AsyncRelayCommand(DoGenerateTestdataAsync);
            MainViewLoadedCommand = new AsyncRelayCommand(MainViewLoaded);

            GetData();
        }

        private async Task MainViewLoaded()
        {
            await Task.Delay(3000);

            if (accService.GetDbContext().Database.CanConnect())
            {
                Messenger.Send(new StatusbarMessage("Successfully connect to the database"), "Statusbar");
            }
            else
            {
                Messenger.Send(new StatusbarMessage("No connection to the database"), "Statusbar");
            }
            
            await Task.Delay(3000);
            
            if (accService.GetDbContext().Database.EnsureCreated())
            {
                Messenger.Send(new StatusbarMessage("All database structures are up to date with the data context"), "Statusbar");
            }
            else
            {
                Messenger.Send(new StatusbarMessage("All data structures were loaded into the data context"), "Statusbar");
            }
            
            await Task.Delay(3000);

            if (accService.GetFileShare().IsNullOrEmpty())
            {
                Messenger.Send(new StatusbarMessage("File share could not be found"), "Statusbar");
            }
            else
            {
                Messenger.Send(new StatusbarMessage("File share was found and is ready"), "Statusbar");
            }
        }

        private async Task OnRefreshReceive(RefreshMessage message)
        {
            GetData();

            StatusbarMessage = "Data refreshed";
            await Task.Delay(3000);
            StatusbarMessage = "";
        }

        private async Task OnStatusbarReceive(StatusbarMessage message)
        {
            StatusbarMessage = message.Text;
            await Task.Delay(3000);
            StatusbarMessage = "";
        }

        private void GetData()
        {
            SelectedGeschaeftsjahr = Task.Run(async () => await accService.GetGeschaeftsjahreAsync())
                .Result.ToList().FirstOrDefault(g => g.DatumVon <= DateTime.Now && g.DatumBis >= DateTime.Now);

            Messenger.Send(new StatusbarMessage("MainViewModel loaded"), "Statusbar");
        }

        private void RefreshAsync()
        {
            Messenger.Send(new RefreshMessage("all"), "Refresh");
        }

        private async Task DoGenerateTestdataAsync(CancellationToken cancellationToken = default)
        {
            await ACCTestDataCreator.CreateCompleteTestDataAsync(accService, cancellationToken);
            Messenger.Send(new StatusbarMessage("Testdata generated successfully"), "Statusbar");
            await Task.Delay(3000, cancellationToken);
            Messenger.Send(new RefreshMessage("all"), "Refresh");
        }


    }
}
