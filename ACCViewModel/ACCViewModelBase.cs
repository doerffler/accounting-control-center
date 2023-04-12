using ACC.ViewModel.Messages;
using ACC.ViewModel.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
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
            SaveChangesCommand = new AsyncRelayCommand(SaveChangesAsync, CheckIfSaveChangesCanExecute);

            ACCService = accService;
            UserInteractionService = userInteractionService;
        }

        private bool CheckIfSaveChangesCanExecute()
        {
            return ACCService.CheckForChanges();
        }

        protected event Action Saving;

        private void RaiseSavingEvent()
        {
            Saving?.Invoke();
        }

        protected async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                RaiseSavingEvent();

                int countAenderungen = await ACCService.SaveChangesAsync(cancellationToken);

                if (countAenderungen > 0)
                {
                    string message = string.Format("{0} Änderung(en) wurden gespeichert", countAenderungen);
                    UserInteractionService.ShowMessageBox(message, "Dörffler und Partner GmbH");
                }
            }
            catch (Exception ex) 
            {
                Messenger.Send(new StatusbarMessage(ex.Message), "Statusbar");
            }
        }

    }
}
