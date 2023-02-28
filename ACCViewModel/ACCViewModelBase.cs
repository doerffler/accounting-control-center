using ACC.ViewModel.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ACC.ViewModel
{
    public abstract class ACCViewModelBase : ObservableRecipient
    {

        protected IACCService ACCService { get; set; }
        protected IUserInteractionService UserInteractionService { get; set; }

        public IRelayCommand SaveChangesCommand { get; }

        public ACCViewModelBase(IACCService accService, IUserInteractionService userInteractionService = null)
        {
            //TODO: Implement CanExecute-Functionality
            SaveChangesCommand = new AsyncRelayCommand(SaveChangesAsync);

            ACCService = accService;
            UserInteractionService = userInteractionService;
        }

        protected async Task<bool> CheckIfSaveChangesCanExecuteAsync(CancellationToken cancellationToken = default)
        {
            return await ACCService.CheckForChangesAsync(cancellationToken);
        }

        protected event Action Saving;

        private void RaiseSavingEvent()
        {
            Saving?.Invoke();
        }

        protected async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            RaiseSavingEvent();

            int countAenderungen = await ACCService.SaveChangesAsync(cancellationToken);

            if (countAenderungen > 0)
            {
                string message = string.Format("{0} Änderung(en) wurden gespeichert", countAenderungen);
                UserInteractionService.ShowMessageBox(message, "Dörffler und Partner GmbH");
            }
        }

    }
}
