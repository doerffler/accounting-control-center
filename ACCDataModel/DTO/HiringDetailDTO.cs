using System;

namespace ACCDataModel.DTO
{
    public class HiringDetailDTO
    {

        public int MonthlySalaryCount {get; set; }
        public int WorkingHoursCount {get; set; }
        public DateTime ValidFrom {get; set; }
        public decimal MonthlySalaryAmount {get; set; }
        public bool IsTerminated { get; set; }
        public string JobDescription { get; set; }

    }
}
