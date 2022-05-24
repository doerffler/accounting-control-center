using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DPApp
{
    public class Contact : DPAppObject
    {

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Language { get; set; }

        public string Sex { get; set; }

        public string Position { get; set; }

        public bool Formal { get; set; }

        public long StaffIdMainContact { get; set; }

        public string StaffContacts { get; set; }

        public string EmailPrivate { get; set; }

        public long? AddressIdPrivate { get; set; }

        public long? DepartmentId { get; set; }

        public string Email { get; set; }

        public string Telephon { get; set; }

        public string Mobil { get; set; }

        public string Remark { get; set; }

        public DateTime? ValidFrom { get; set; }


    }
}
