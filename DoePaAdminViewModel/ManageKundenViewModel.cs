using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ManageKundenViewModel : DoePaAdminViewModelBase
    {

        private IUserInteractionService UserInteractionService { get; set; }

        private ObservableCollection<Kunde> _kunden = new();

        public ObservableCollection<Kunde> Kunden
        {
            get => _kunden;
            set => SetProperty(ref _kunden, value, true);
        }

        public IRelayCommand AddKundeCommand
        {
            get;
        }

        public ManageKundenViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService)
        {

            UserInteractionService = userInteractionService;

            AddKundeCommand = new AsyncRelayCommand(DoAddKundeAsync);

            Kunden = new(Task.Run(async () => await DoePaAdminService.GetKundenAsync()).Result);

        }

        private async Task DoAddKundeAsync(CancellationToken cancellationToken = default)
        {

            //TODO: Introduce string localization for this message:
            string kundenName = UserInteractionService.AskForUserInput("Bitte geben Sie den Namen des neuen Kunden ein:", "Kundennamen eingeben");

            if (kundenName != null)
            {
                Kunde newKunde = await DoePaAdminService.CreateKundeAsync(cancellationToken);
                newKunde.Kundenname = kundenName;
                Kunden.Add(newKunde);
            }
        }

    }
}
