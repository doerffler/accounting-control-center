using DoePaAdmin.ViewModel.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{
    public abstract class DoePaAdminViewModelBase : ObservableRecipient
    {

        protected IDoePaAdminService DoePaAdminService { get; set; }

        public IRelayCommand SaveChangesCommand { get; }

        public DoePaAdminViewModelBase(IDoePaAdminService doePaAdminService)
        {
            //TODO: Implement CanExecute-Functionality
            SaveChangesCommand = new AsyncRelayCommand(SaveChangesAsync);

            DoePaAdminService = doePaAdminService;
        }
        
        protected async Task<bool> CheckIfSaveChangesCanExecuteAsync(CancellationToken cancellationToken = default)
        {
            return await DoePaAdminService.CheckForChangesAsync(cancellationToken);
        }

        protected event Action Saving;

        private void RaiseSavingEvent()
        {
            if (Saving != null)
            {
                Saving();
            }
        }

        protected async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            RaiseSavingEvent();
            await DoePaAdminService.SaveChangesAsync(cancellationToken);
        }

    }
}
