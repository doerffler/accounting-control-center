using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DTO
{
    public class CustomerDTO
    {
        public string CustomerName { get; set; }

        public string InvoiceRecipient { get; set; }
        public string PostalCode { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public IList<ProjectDTO> Projects { get; set; }

        public CustomerDTO()
        {
            Projects = new List<ProjectDTO>();
        }
    }
}
