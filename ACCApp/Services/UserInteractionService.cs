using ACC.ViewModel.Services;
using ACCApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ACCApp.Services
{
    public class UserInteractionService : IUserInteractionService
    {
        public string AskForUserInput(string text, string caption)
        {
            AskForUserInputWindow viewAskForUserInput = App.ServiceProvider.GetRequiredService<AskForUserInputWindow>();
            viewAskForUserInput.Message = text;
            viewAskForUserInput.Caption = caption;

            bool? dialogResult = viewAskForUserInput.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value)
            {
                return viewAskForUserInput.UserInput;
            }
            else return null;
            
        }

        public bool ShowMessageBox(string message, string caption)
        {
            MessageBoxResult result = MessageBox.Show(message, caption);

            return result == MessageBoxResult.Yes || result == MessageBoxResult.OK;
        }
    }
}
