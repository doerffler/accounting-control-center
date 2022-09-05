using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public class ManageSkillsViewModel : DoePaAdminViewModelBase
    {
        public IRelayCommand AddSkillCommand { get; }
        public IRelayCommand RemoveSkillCommand { get; }

        private IUserInteractionService UserInteractionService { get; set; }

        #region Skill
        private ObservableCollection<Skill> _skills = new();
        public ObservableCollection<Skill> Skills
        {
            get => _skills;
            set => _skills = value;
        }

        private Skill _selectedSkill = new();
        public Skill SelectedSkill
        {
            get => _selectedSkill;
            set => SetProperty(ref _selectedSkill, value, true);
        }
        #endregion

        public ManageSkillsViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService)
        {
            Skills = new(Task.Run(async () => await DoePaAdminService.GetSkillsAsync()).Result);

            UserInteractionService = userInteractionService;

            AddSkillCommand = new AsyncRelayCommand(DoAddSkillAsync);

            //TODO: Implement CanExecute-Functionality
            RemoveSkillCommand = new RelayCommand(DoRemoveSkill);

        }

        private void DoRemoveSkill()
        {
            if (SelectedSkill != null)
            {
                DoePaAdminService.RemoveSkill(SelectedSkill);
                _ = Skills.Remove(SelectedSkill);
            }
        }

        private async Task DoAddSkillAsync(CancellationToken cancellationToken)
        {
            string skillName = UserInteractionService.AskForUserInput("Bitte geben Sie den Namen des neuen Skills ein:", "Skillnamen eingeben");

            if (skillName != null)
            {
                Skill newSkill = await DoePaAdminService.CreateSkillAsync(cancellationToken);
                newSkill.SkillName = skillName;
                Skills.Add(newSkill);
            }
        }
    }
}
