using DoePaAdmin.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DoePaAdmin.ViewModel
{
    public class DisplayAuftragsstatusViewModel : DoePaAdminViewModelBase
    {
        public PlotModel Model { get; set; }

        public DisplayAuftragsstatusViewModel(IDoePaAdminService doePaAdminService) : base(doePaAdminService)
        {
            // Create the plot model
            var chart = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };

            // Create two line series (markers are hidden by default)
            var soll = new LineSeries { Title = "Soll" };
            soll.Points.Add(new DataPoint(0, 10));
            soll.Points.Add(new DataPoint(1, 9));
            soll.Points.Add(new DataPoint(2, 8));
            soll.Points.Add(new DataPoint(3, 7));
            soll.Points.Add(new DataPoint(4, 6));
            soll.Points.Add(new DataPoint(5, 5));
            soll.Points.Add(new DataPoint(6, 4));
            soll.Points.Add(new DataPoint(7, 3));
            soll.Points.Add(new DataPoint(8, 2));
            soll.Points.Add(new DataPoint(9, 1));
            soll.Points.Add(new DataPoint(10, 0));

            var ist = new LineSeries { Title = "Ist" };
            ist.Points.Add(new DataPoint(0, 10));
            ist.Points.Add(new DataPoint(1, 10));
            ist.Points.Add(new DataPoint(2, 9));
            ist.Points.Add(new DataPoint(3, 9));
            ist.Points.Add(new DataPoint(4, 8));
            ist.Points.Add(new DataPoint(5, 6));
            ist.Points.Add(new DataPoint(6, 5));
            ist.Points.Add(new DataPoint(7, 4));
            ist.Points.Add(new DataPoint(8, 3));
            ist.Points.Add(new DataPoint(9, 1));
            ist.Points.Add(new DataPoint(10, 0));

            chart.Series.Add(soll);
            chart.Series.Add(ist);

            // Axes are created automatically if they are not defined
            chart.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 10, Title = "Rechnungsmonat" });
            chart.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 10, Title = "Restbudget" });

            // Set the Model property, the INotifyPropertyChanged event will make the WPF Plot control update its content
            Model = chart;
        }
    }
}
