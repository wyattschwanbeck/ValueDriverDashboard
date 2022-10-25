using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueDriverDashboard.Models;

namespace ValueDriverDashboard.Models
{
    public class ChartViewBase : ViewModelBase
    {
        
        
        protected internal Func<double, string> _YFormatter;
        protected internal List<string> _chartLabels;
        protected internal SeriesCollection _chartSeriesCollection;
        protected internal AxesCollection _axisYCollection;

        public Func<double, string> YFormatter { get { return _YFormatter; } set { _YFormatter = value; } }
        public List<string> ChartLabels { get { return _chartLabels; } }
        public AxesCollection AxisYCollection { get { return _axisYCollection; } }
        public SeriesCollection ChartSeriesCollection { get { return _chartSeriesCollection; } }


        public ChartViewBase()
        {
            _chartSeriesCollection = new SeriesCollection();

            _axisYCollection = new AxesCollection();

            _chartLabels = new List<string>();
        }

       

    }
}
