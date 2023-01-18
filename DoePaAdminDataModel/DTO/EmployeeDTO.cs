using System;
using System.Collections.Generic;

namespace DoePaAdminDataModel.DTO
{
    public class EmployeeDTO
    {

        public string Salutation {get; set; }
        public string Firstname {get; set; }
        public string Surname {get; set; }
        public DateTime Birthdate {get; set; }
        public string EmployeeCode {get; set; }
        public int StaffNumberDatev {get; set; }
        public string PostalCode {get; set; }
        public string StreetNumber {get; set; }
        public string Street {get; set; }
        public int CostCenterNumber {get; set; }
        public IList<HiringDetailDTO> HiringDetails { get; set; }

        public EmployeeDTO()
        {
            HiringDetails = new List<HiringDetailDTO>();
        }

    }
}
