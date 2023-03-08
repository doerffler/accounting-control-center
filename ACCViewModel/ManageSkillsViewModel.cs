using ACC.ViewModel.Messages;
using ACC.ViewModel.Services;
using ACCDataModel.Stammdaten;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel
{
    public class ManageSkillsViewModel : ACCViewModelBase
    {
        public IRelayCommand AddSkillCommand { get; }
        public IRelayCommand RemoveSkillCommand { get; }
        public IRelayCommand DragDropCommand { get; }

        #region Skill
        private ObservableCollection<Skill> _skills = new();
        public ObservableCollection<Skill> Skills
        {
            get => _skills;
            set => SetProperty(ref _skills, value, true);
        }

        private Skill _selectedSkill = new();
        public Skill SelectedSkill
        {
            get => _selectedSkill;
            set => SetProperty(ref _selectedSkill, value, true);
        }
        #endregion

        public ManageSkillsViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            AddSkillCommand = new AsyncRelayCommand(DoAddSkillAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveSkillCommand = new RelayCommand(DoRemoveSkill);

            DragDropCommand = new RelayCommand(DoDragDrap);

            GetData();

            Messenger.Register<ManageSkillsViewModel, RefreshMessage, string>(this, "Refresh", (r, m) => r.OnRefreshReceive(m));
        }

        private void GetData()
        {
            Skills = new(Task.Run(async () => await ACCService.GetSkillTreeAsync()).Result);
        }

        private void OnRefreshReceive(RefreshMessage message)
        {
            GetData();
        }

        private void DoDragDrap()
        {

        }

        private void DoRemoveSkill()
        {
            if (SelectedSkill != null)
            {
                ACCService.RemoveSkill(SelectedSkill);
                _ = Skills.Remove(SelectedSkill);
            }
        }

        private async Task DoAddSkillAsync(CancellationToken cancellationToken)
        {
            string skillName = UserInteractionService.AskForUserInput("Bitte geben Sie den Namen des neuen Skills ein:", "Skillnamen eingeben");

            if (skillName != null)
            {
                Skill newSkill = await ACCService.CreateSkillAsync(cancellationToken);
                newSkill.SkillName = skillName;
                Skills.Add(newSkill);
            }
        }
    }
}
