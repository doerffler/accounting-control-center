using ACC.ViewModel.Services;
using ACCDataModel.Stammdaten;
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
using System.ComponentModel;
using ACC.ViewModel.Messages;
using ACCDataModel.DTO;

namespace ACC.ViewModel
{
    public class ManageProjekteViewModel : ACCViewModelBase
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
            set 
            {
                SetProperty(ref _selectedProjekt, value);
                Skills = new(SelectedProjekt.Skills);
            }
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

        private Skill _selectedSkill;
        public Skill SelectedSkill
        {
            get => _selectedSkill;
            set => SetProperty(ref _selectedSkill, value);
        }

        public IRelayCommand RemoveSkillCommand { get; }
        #endregion



        #region (nicht)zugeordnete Auftraege
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

        public ManageProjekteViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            AddProjektCommand = new AsyncRelayCommand(DoAddProjektAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveProjektCommand = new RelayCommand(DoRemoveProjekt);
            RemoveSkillCommand = new RelayCommand(DoRemoveSkill);

            Projekte = new (Task.Run(async () => await ACCService.GetProjekteAsync()).Result);
            AllAuftraege = new (Task.Run(async () => await ACCService.GetAuftraegeAsync()).Result);

            Saving += HandleSaving;
            
            PropertyChanged += HandlePropertyChanged;

            Messenger.Register<ManageProjekteViewModel, SelectedSkillsMessage, string>(this, "SelectedSkills", (r, m) => r.OnSelectedSkillsMessageReceive(m));
            PropertyChanging += HandlePropertyChanging;

            AssignedAuftraege.CollectionChanged += HandleAssignedAuftraegeCollectionChanged;

            GetData();

            Messenger.Register<ManageProjekteViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Projekte = new(Task.Run(async () => await ACCService.GetProjekteAsync()).Result);
            AllAuftraege = new(Task.Run(async () => await ACCService.GetAuftraegeAsync()).Result);
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        void OnSelectedSkillsMessageReceive(SelectedSkillsMessage message)
        {
            Skills.Add(message.Data);
        }

        private void HandleSaving()
        {
            if (SelectedProjekt != null)
            {
                SelectedProjekt.Skills = Skills.ToList();
            }
        }

        private void HandleAssignedAuftraegeCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SelectedProjekt?.ZugehoerigeAuftraege != null)
            { 
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (var item in e.NewItems) 
                        {
                            SelectedProjekt.ZugehoerigeAuftraege.Add((Auftrag)item);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (var item in e.OldItems)
                        {
                            SelectedProjekt.ZugehoerigeAuftraege.Remove((Auftrag)item);
                            ((Auftrag)item).ZugehoerigesProjekt = null;
                        }
                        break;
                }
            }
        }

        private void HandlePropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedProjekt):
                    if (SelectedProjekt != null)
                    {
                        SelectedProjekt.Skills = Skills.ToList();
                    }
                    break;
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

                    // TODO: Fix Update bug with the collections
                    AllAuftraege = new ObservableCollection<Auftrag>(RemoveAssignedAuftraege(AssignedAuftraege));

                    Skills = new ObservableCollection<Skill>(SelectedProjekt.Skills);

                    break;
            }
        }

        private IEnumerable<Auftrag> RemoveAssignedAuftraege(IEnumerable<Auftrag> assignedAuftraege)
        {
            foreach (Auftrag auftrag in AllAuftraege)
            {
                if (auftrag.ZugehoerigesProjekt == null)
                {
                    yield return auftrag;
                }
            }
        }

        private async Task DoAddProjektAsync(CancellationToken cancellationToken = default)
        {
            Projekt newProjekt = await ACCService.CreateProjektAsync(cancellationToken);
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

        private void DoRemoveSkill()
        {
            if (SelectedSkill != null)
            {
                _ = Skills.Remove(SelectedSkill);
            }
        }
    }
}
