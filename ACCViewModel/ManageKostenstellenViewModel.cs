using ACC.ViewModel.Messages;
using ACC.ViewModel.Services;
using ACCDataModel.Stammdaten;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel
{
    public class ManageKostenstellenViewModel : ACCViewModelBase
    {
        private ObservableCollection<Kostenstelle> _kostenstellen;
        public ObservableCollection<Kostenstelle> Kostenstellen
        {
            get => _kostenstellen;
            set => SetProperty(ref _kostenstellen, value, true);
        }

        private ObservableCollection<Kostenstellenart> _kostenstellenarten;
        public ObservableCollection<Kostenstellenart> Kostenstellenarten
        {
            get => _kostenstellenarten;
            set => SetProperty(ref _kostenstellenarten, value, true);
        }

        private ObservableCollection<Kostenstelle> _uebergeordneteKostenstellen = new();
        public ObservableCollection<Kostenstelle> UebergeordneteKostenstellen
        {
            get => _uebergeordneteKostenstellen;
            set => SetProperty(ref _uebergeordneteKostenstellen, value, true);
        }

        private Kostenstelle _selectedKostenstelle;

        public Kostenstelle SelectedKostenstelle
        {
            get => _selectedKostenstelle;
            set => SetProperty(ref _selectedKostenstelle, value);
        }

        public IRelayCommand AddKostenstelleCommand { get; }

        public IRelayCommand RemoveKostenstelleCommand { get; }

        public IRelayCommand AddKostenstellenartCommand { get; }

        public ManageKostenstellenViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {

            UserInteractionService = userInteractionService;

            //TODO: Implement CanExecute-Functionality
            AddKostenstelleCommand = new AsyncRelayCommand(DoAddKostenstelleAsync);
            RemoveKostenstelleCommand = new RelayCommand(DoRemoveKostenstelle);
            AddKostenstellenartCommand = new AsyncRelayCommand(DoAddKostenstellenartAsync);

            Saving += HandleSaving;
            PropertyChanged += HandlePropertyChanged;
            PropertyChanging += HandlePropertyChanging;

            GetData();

            Messenger.Register<ManageKostenstellenViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Kostenstellen = new(Task.Run(async () => await ACCService.GetKostenstellenAsync()).Result);
            Kostenstellenarten = new(Task.Run(async () => await ACCService.GetKostenstellenartenAsync()).Result);

            Messenger.Send(new StatusbarMessage("ManageKostenstellenViewModel loaded"), "Statusbar");
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        private void HandleSaving()
        {
            if (SelectedKostenstelle != null)
            {
                SelectedKostenstelle.UebergeordneteKostenstellen = UebergeordneteKostenstellen.ToList();
            }            
        }

        private void HandlePropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedKostenstelle):
                    if (SelectedKostenstelle != null)
                    { 
                        SelectedKostenstelle.UebergeordneteKostenstellen = UebergeordneteKostenstellen.ToList();
                    }

                    break;
            }
        }

        private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedKostenstelle):
                    this.UebergeordneteKostenstellen = new ObservableCollection<Kostenstelle>(SelectedKostenstelle.UebergeordneteKostenstellen);

                    break;
            }
        }

        private void DoRemoveKostenstelle()
        {

            if (SelectedKostenstelle != null)
            {
                ACCService.RemoveKostenstelle(SelectedKostenstelle);
                _ = Kostenstellen.Remove(SelectedKostenstelle);
            }

        }

        private async Task DoAddKostenstelleAsync(CancellationToken cancellationToken = default)
        {
            string kostenstellenBezeichnung = UserInteractionService.AskForUserInput("Bitte geben Sie die Bezeichnung der neuen Kostenstelle ein:", "Kostenstellenbezeichnung eingeben");

            if (kostenstellenBezeichnung != null)
            { 
                Kostenstelle newKostenstelle = await ACCService.CreateKostenstelleAsync(cancellationToken);
                newKostenstelle.Kostenstellenbezeichnung = kostenstellenBezeichnung;
                Kostenstellen.Add(newKostenstelle);
            }
        }

        private async Task DoAddKostenstellenartAsync(CancellationToken cancellationToken = default)
        {
            string kostenstellenartBezeichnung = UserInteractionService.AskForUserInput("Bitte geben Sie die Bezeichnung der neuen Kostenstellenart ein:", "Kostenstellenartbezeichnung eingeben");

            if (kostenstellenartBezeichnung != null)
            {
                Kostenstellenart newKostenstellenart = await ACCService.CreateKostenstellenartAsync(cancellationToken);
                newKostenstellenart.Kostenstellenartbezeichnung = kostenstellenartBezeichnung;
                Kostenstellenarten.Add(newKostenstellenart);
            }
        }

    }
}
