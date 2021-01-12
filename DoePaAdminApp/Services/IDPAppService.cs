using DoePaAdminDataModel.Kostenrechnung;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminApp.Services
{
    public interface IDPAppService
    {

        ICollection<Ausgangsrechnung> GetAusgangsrechnungen();

    }
}
