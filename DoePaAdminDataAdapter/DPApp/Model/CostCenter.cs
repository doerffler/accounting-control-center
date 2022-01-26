using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataAdapter.DPApp.Model
{
    public class CostCenter : IDPAppModel
    {

        [DBColumn("id")]
        public long Id { get; set; }

        [DBColumn("created_at")]
        public DateTime Created_At { get; set; }

        [DBColumn("updated_at")]
        public DateTime Updated_At { get; set; }

        [DBColumn("number")]
        public int Number { get; set; }

        [DBColumn("name")]
        public string Name { get; set; }

        [DBColumn("active")]
        public bool Active { get; set; }

    }
}
