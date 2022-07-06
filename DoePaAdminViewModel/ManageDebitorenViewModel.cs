using DoePaAdmin.ViewModel.Services;
using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdmin.ViewModel
{

    public class ManageDebitorenViewModel : ManageGeschaeftspartnerViewModel<Debitor>
    {

        public ManageDebitorenViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {

        }

    }
}
