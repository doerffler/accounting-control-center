using ACC.ViewModel.Services;
using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.ViewModel
{

    public class ManageKreditorenViewModel : ManageGeschaeftspartnerViewModel<Kreditor>
    {

        public ManageKreditorenViewModel(IACCService accService, IUserInteractionService userInteractionService) : base(accService, userInteractionService)
        {

        }

    }
}
