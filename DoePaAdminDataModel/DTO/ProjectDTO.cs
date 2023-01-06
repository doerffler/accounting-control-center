using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DTO
{
    public class ProjectDTO
    {

        public string ProjectName { get; set; }

        public DateTime ProjectStartDate { get; set; }

        public DateTime ProjectEndDate { get; set; }

        public IList<string> Skills { get; set; }

        public IList<OrderDTO> Orders { get; set; }

        public ProjectDTO()
        {
            Skills = new List<string>();
            Orders = new List<OrderDTO>();
        }

    }
}
