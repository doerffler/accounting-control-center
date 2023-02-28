using ACC.ViewModel.Messages;
using ACC.ViewModel.Services;
using ACCDataModel.DPApp;
using ACCDataModel.Stammdaten;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel
{
    public class SelectSkillsViewModel : ACCViewModelBase
    {
        private ObservableCollection<Skill> _skills = new();
        public ObservableCollection<Skill> Skills
        {
            get => _skills;
            set => SetProperty(ref _skills, value, true);
        }

        private ObservableCollection<Skill> _selectedSkills;
        public ObservableCollection<Skill> SelectedSkills
        {
            get => _selectedSkills;
            set => SetProperty(ref _selectedSkills, value);
        }

        public IRelayCommand AddSelectedSkillsCommand { get; }

        public SelectSkillsViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {
            AddSelectedSkillsCommand = new RelayCommand(DoAddSelectedSkills);
            Skills = new(Task.Run(async () => await ACCService.GetSkillsAsync()).Result);
        }

        private void DoAddSelectedSkills()
        {
            Messenger.Send(new SelectedSkillsMessage(SelectedSkills), "SelectedSkills");
        }
    }

}
