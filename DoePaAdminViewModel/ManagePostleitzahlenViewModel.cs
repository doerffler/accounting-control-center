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
    public class ManagePostleitzahlenViewModel : DoePaAdminViewModelBase
    {

        private Postleitzahl _selectedPostleitzahl;

        public Postleitzahl SelectedPostleitzahl
        {
            get => _selectedPostleitzahl;
            set => SetProperty(ref _selectedPostleitzahl, value, true);
        }

        private ObservableCollection<Postleitzahl> _postleitzahlen;

        public ObservableCollection<Postleitzahl> Postleitzahlen
        {
            get => _postleitzahlen;
            set => SetProperty(ref _postleitzahlen, value, true);
        }

        public IRelayCommand AddPostleitzahlCommand { get; }

        public ManagePostleitzahlenViewModel(IDoePaAdminService doePaAdminService, IUserInteractionService userInteractionService) : base(doePaAdminService, userInteractionService)
        {
            AddPostleitzahlCommand = new AsyncRelayCommand(DoAddPostleitzhalAsync);

            Postleitzahlen = new(Task.Run(async () => await DoePaAdminService.GetPostleitzahlenAsync()).Result);
        }

        private async Task DoAddPostleitzhalAsync(CancellationToken cancellationToken = default)
        {

            Postleitzahl newPostleitzahl = await DoePaAdminService.CreatePostleitzahlAsync(cancellationToken);
            Postleitzahlen.Add(newPostleitzahl);

        }

    }
}
