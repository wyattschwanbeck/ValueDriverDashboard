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

namespace ValueDriverDashboard.ViewModels
{
    public class StockPriceViewModel : ViewModelBase
    {
        public StockPriceSeries _StockPriceSeries { get; set; }
        public StockPriceSeries StockPriceSeries { get { return _StockPriceSeries; } }
        //private DataInputModelView _DataInputView;
        public StockPriceViewModel()
        {

            _StockPriceSeries = new StockPriceSeries();

            //this._DataInputView = dataInput;
            yahooDl = new HistoricalDataProvider();
        }

        //public DataInputModelView DataInputView { get { return _DataInputView; } }
        public ICommand UpdateCommand { get; set; }

        private HistoricalDataProvider yahooDl;
        public bool CanUpdate
        {

            get
            {
                return true;
            }

        }
        public void UpdateChart(DataInput dataInput)
        {
            _StockPriceSeries.UpdateChart(dataInput);
        }

    }
}