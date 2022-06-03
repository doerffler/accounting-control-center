using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DPApp
{
    public class Staff : DPAppObject
    {

        public string ShortName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PersonalNo { get; set; }

        public long RoleId { get; set; }

        public long CostCenterId { get; set; }

        public long? AddressId { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime LeavingDate { get; set; }

        public DateTime? Birthday { get; set; }

    }   
}
