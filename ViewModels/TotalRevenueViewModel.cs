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
    public class TotalRevenueViewModel : ViewModelBase
    {
        public TotalRevenueSeries _TotalRevenueSeries { get; set; }
        public TotalRevenueSeries TotalRevenueSeries { get { return _TotalRevenueSeries; } }
        //private DataInputViewModel _DataInputView;
        public TotalRevenueViewModel()
        {

            _TotalRevenueSeries = new TotalRevenueSeries();

            //this._DataInputView = dataInput;
            //yahooDl = new HistoricalDataProvider();
        }

        //public DataInputViewModel DataInputView { get { return _DataInputView; } }
        public ICommand UpdateCommand { get; set; }

        private HistoricalDataProvider yahooDl;
        public bool CanUpdate
        {

            get
            {
                return true;
            }

        }
        public async Task UpdateChart(DataInputEventArgs dataInput)
        {
            await _TotalRevenueSeries.UpdateChart(dataInput);
        }

    }
}