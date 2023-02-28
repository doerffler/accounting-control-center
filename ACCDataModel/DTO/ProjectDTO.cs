using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DTO
{
    public class ProjectDTO
    {

        public string ProjectName { get; set; }

        public DateTime ProjectStartDate { get; set; }

        public DateTime ProjectEndDate { get; set; }

        public string CustomerName { get; set; }

        public string InvoiceRecipient { get; set; }

        public string PostalCode { get; set; }

        public string StreetNumber { get; set; }

        public string Street { get; set; }

        public IList<string> Skills { get; set; }

        public IList<OrderDTO> Orders { get; set; }

        public ProjectDTO()
        {
            Skills = new List<string>();
            Orders = new List<OrderDTO>();
        }

    }
}
