using ACCDataModel.DPApp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataAdapter.DPApp
{
    public delegate T ReadDPObjectFromReaderDelegate<T>(SqlDataReader reader) where T : DPAppObject;
}
