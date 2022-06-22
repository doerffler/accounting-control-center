using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DPApp
{
    public class Project : DPAppObject
    {

        public string ShortName { get; set; }

        public string Name { get; set; }

        public long CompanyId { get; set; }

    }
}
