using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.ViewModel.Services
{
    public interface IUserInteractionService
    {

        bool ShowMessageBox(string message, string caption);

        string AskForUserInput(string text, string caption);

    }
}
