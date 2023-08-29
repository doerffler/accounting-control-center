using ACCDataModel.DPApp;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataAdapter.DPApp
{
    public delegate T ReadDPObjectFromReaderDelegate<T>(NpgsqlDataReader reader) where T : DPAppObject;
}
