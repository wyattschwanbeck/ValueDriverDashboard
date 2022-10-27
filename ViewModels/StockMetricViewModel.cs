using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ValueDriverDashboard.Models;
using ValueDriverDashboard.Commands;
using Yahoo.Finance;
using LiveCharts.Wpf;
using LiveCharts;
using ValueDriverDashboard.Events;

namespace ValueDriverDashboard.ViewModels
{
    public class StockMetricViewModel : ViewModelBase
    {
        public StockMetricSeries _StockMetricSeries { get; set; }
        public StockMetricSeries StockMetricSeries { get { return _StockMetricSeries; } }
        //private DataInputViewModel _DataInputView;
        public StockMetricViewModel()
        {

            _StockMetricSeries = new StockMetricSeries();

        }
        public ICommand UpdateCommand { get; set; }

        //private HistoricalDataProvider yahooDl;
        public bool CanUpdate
        {

            get
            {
                return true;
            }

        }
        public async Task UpdateChart(DataInputEventArgs dataInput)
        {
            await _StockMetricSeries.UpdateChart(dataInput);
        }

    }
}