using System.Collections.Generic;
using ACCDataModel.DTO;
using OxyPlot;

namespace ACC.ViewModel
{
    public class ExportChartDTO
    {
        public IEnumerable<RemainingBudgetOnOrdersDTO> Table { get; set; }
        public PlotModel Chart { get; set; }
    }   
}