using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.ComponentModel;
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
    public class ManageKostenstellenViewModel : ObservableRecipient
    {

        private IDoePaAdminService DoePaAdminService { get; set; }

        private ObservableCollection<Kostenstelle> _kostenstellen = new();
        public ObservableCollection<Kostenstelle> Kostenstellen
        {
            get => _kostenstellen;
            set => SetProperty(ref _kostenstellen, value, true);
        }

        private ObservableCollection<Kostenstellenart> _kostenstellenarten = new();
        public ObservableCollection<Kostenstellenart> Kostenstellenarten
        {
            get => _kostenstellenarten;
            set => SetProperty(ref _kostenstellenarten, value, true);
        }

        private Kostenstelle _selectedKostenstelle;

        public Kostenstelle SelectedKostenstelle
        {
            get => _selectedKostenstelle;
            set => SetProperty(ref _selectedKostenstelle, value);
        }

        public IRelayCommand AddKostenstelleCommand { get; }

        public ManageKostenstellenViewModel(IDoePaAdminService doePaAdminService)
        {

            AddKostenstelleCommand = new RelayCommand(DoAddKostenstelle);

            DoePaAdminService = doePaAdminService;

            Kostenstellen = Task.Run(async () => await DoePaAdminService.GetKostenstellenAsync()).Result;
            Kostenstellenarten = Task.Run(async () => await DoePaAdminService.GetKostenstellenartenAsync()).Result;

        }

        private void DoAddKostenstelle()
        {
            throw new NotImplementedException();
        }

    }
}
