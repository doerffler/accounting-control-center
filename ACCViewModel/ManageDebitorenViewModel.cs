using ACC.ViewModel.Services;
using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.ViewModel
{

    public class ManageDebitorenViewModel : ManageGeschaeftspartnerViewModel<Debitor>
    {

        public ManageDebitorenViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {

        }

    }
}
