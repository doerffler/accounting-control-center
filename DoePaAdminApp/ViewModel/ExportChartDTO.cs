using System.Collections.Generic;
using DoePaAdminDataModel.DTO;
using OxyPlot;

namespace DoePaAdmin.ViewModel
{
    public class ExportChartDTO
    {
        public IEnumerable<RemainingBudgetOnOrdersDTO> Table { get; set; }
        public PlotModel Chart { get; set; }
    }   
}