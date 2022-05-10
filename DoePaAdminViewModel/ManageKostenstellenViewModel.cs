using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ManageKostenstellenViewModel : DoePaAdminViewModelBase
    {

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

        public ManageKostenstellenViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

            AddKostenstelleCommand = new AsyncRelayCommand(DoAddKostenstelleAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveKostenstelleCommand = new RelayCommand(DoRemoveKostenstelle);
                       
            Kostenstellen = Task.Run(async () => await DoePaAdminService.GetKostenstellenAsync()).Result;
            Kostenstellenarten = Task.Run(async () => await DoePaAdminService.GetKostenstellenartenAsync()).Result;

        }

        private void DoRemoveKostenstelle()
        {

            this.UebergeordneteKostenstellen.Add(this.Kostenstellen.First());
            
            //if (SelectedKostenstelle != null)
            //{
            //    DoePaAdminService.RemoveKostenstelle(SelectedKostenstelle);
            //    _ = Kostenstellen.Remove(SelectedKostenstelle);
            //}

        }

        private async Task DoAddKostenstelleAsync(CancellationToken cancellationToken = default)
        {

            Kostenstelle newKostenstelle = await DoePaAdminService.CreateKostenstelleAsync(cancellationToken);
            newKostenstelle.Kostenstellenbezeichnung = "Neue Kostenstelle";
            Kostenstellen.Add(newKostenstelle);

        }

    }
}
