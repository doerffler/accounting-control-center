using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.Specialized;

using System.Windows;

namespace DoePaAdmin.ViewModel
{
    public class ManageProjekteViewModel : DoePaAdminViewModelBase
    {
        #region Projekt
        private ObservableCollection<Projekt> _projekte = new();

        public ObservableCollection<Projekt> Projekte
        {
            get => _projekte;
            set => SetProperty(ref _projekte, value, true);
        }

        private Projekt _selectedProjekt;
        public Projekt SelectedProjekt
        {
            get => _selectedProjekt;
            set => SetProperty(ref _selectedProjekt, value);
        }

        public IRelayCommand AddProjektCommand { get; }

        public IRelayCommand RemoveProjektCommand { get; }
        #endregion



        #region Skills
        private ObservableCollection<Skill> _skills = new();

        public ObservableCollection<Skill> Skills
        {
            get => _skills;
            set => SetProperty(ref _skills, value, true);
        }
        #endregion



        #region (nicht)zugeordnete Auftraege
        // ggf. später Filtern bei Anzeige nur nicht zugeordnete
        private ObservableCollection<Auftrag> _assignedAuftraege = new();
        public ObservableCollection<Auftrag> AssignedAuftraege
        {
            get => _assignedAuftraege;
            set => SetProperty(ref _assignedAuftraege, value, true);
        }

        private ObservableCollection<Auftrag> _allAuftraege = new();
        public ObservableCollection<Auftrag> AllAuftraege
        {
            get => _allAuftraege;
            set => SetProperty(ref _allAuftraege, value, true);
        }
        
        // hp: hierCollectionView für Filter auf AllAuftraege erstellen



        private Auftrag _selectedZugeordneterAuftrag;
        public Auftrag SelectedZugeordneterAuftrag
        {
            get => _selectedZugeordneterAuftrag;
            set => SetProperty(ref _selectedZugeordneterAuftrag, value);
        }

        private Auftrag _selectedNichtZugeordneterAuftrag;
        public Auftrag SelectedNichtZugeordneterAuftrag
        {
            get => _selectedNichtZugeordneterAuftrag;
            set => SetProperty(ref _selectedNichtZugeordneterAuftrag, value);
        }


        public IRelayCommand MoveAuftragCommand { get; }
        public IRelayCommand RemoveAuftragCommand { get; }
        #endregion

        public ManageProjekteViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {
            AddProjektCommand = new AsyncRelayCommand(DoAddProjektAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveProjektCommand = new RelayCommand(DoRemoveProjekt);

            // Auftraege (nicht )Zuordnen
            MoveAuftragCommand = new RelayCommand(DoMoveAuftrag);
            RemoveAuftragCommand = new RelayCommand(DoRemoveAuftrag);

            Projekte = new (Task.Run(async () => await DoePaAdminService.GetProjekteAsync()).Result);
            AllAuftraege = new (Task.Run(async () => await DoePaAdminService.GetAuftraegeAsync()).Result);

            PropertyChanged += HandlePropertyChanged;
            AssignedAuftraege.CollectionChanged += HandleAssignedAuftraegeCollectionChanged;
        }

        private void HandleAssignedAuftraegeCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SelectedProjekt?.ZugehoerigeAuftraege != null)
            { 
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (var item in e.NewItems) {
                            SelectedProjekt.ZugehoerigeAuftraege.Add((Auftrag)item);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (var item in e.OldItems)
                        {
                            SelectedProjekt.ZugehoerigeAuftraege.Remove((Auftrag)item);
                        }
                        break;
                }
            }
        }

        private void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedProjekt):
                    AssignedAuftraege.CollectionChanged -= HandleAssignedAuftraegeCollectionChanged;
                    AssignedAuftraege = new ObservableCollection<Auftrag>(SelectedProjekt.ZugehoerigeAuftraege);
                    AssignedAuftraege.CollectionChanged += HandleAssignedAuftraegeCollectionChanged;
                    break;
                case nameof(SelectedNichtZugeordneterAuftrag):
                    break;
                case nameof(SelectedZugeordneterAuftrag):
                    break;
            }
        }

        private async Task DoAddProjektAsync(CancellationToken cancellationToken = default)
        {

            Projekt newProjekt = await DoePaAdminService.CreateProjektAsync(cancellationToken);
            newProjekt.Projektname = "Neues Projekt";
            Projekte.Add(newProjekt);
        }
        private void DoRemoveProjekt()
        {
            if (SelectedProjekt != null)
            {
                _ = Projekte.Remove(SelectedProjekt);
            }
        }

        private void DoMoveAuftrag()
        {
            if (AssignedAuftraege != null && SelectedNichtZugeordneterAuftrag != null)
            {
                AssignedAuftraege.Add(SelectedNichtZugeordneterAuftrag);
                AllAuftraege.Remove(SelectedNichtZugeordneterAuftrag);
            }
        }

        private void DoRemoveAuftrag()
        {
            if (AssignedAuftraege != null && SelectedZugeordneterAuftrag != null)
            {
                AllAuftraege.Add(SelectedZugeordneterAuftrag);
                AssignedAuftraege.Remove(SelectedZugeordneterAuftrag);
            }
        }
    }
}
