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
    public class MainViewModel
    {
        private DataInputModelView _DataInput;
        private StockPriceViewModel _StockPriceSeries;

        public MainViewModel(DataInputModelView dataInput, StockPriceViewModel stockPriceSeriesView)
        {
            _DataInput = dataInput;
            _StockPriceSeries = stockPriceSeriesView;
            UpdateCommand = dataInput.UpdateCommand;
            //dataInput.UpdateCommand = new GetStockPriceData(this);
        }

        public DataInputModelView DataInput { get { return _DataInput; } }
        public StockPriceViewModel StockPriceSeries { get { return _StockPriceSeries; } }
        public ICommand UpdateCommand { get; private set; }
        public bool CanUpdate
        {
            get
            {
                if (_DataInput.CanUpdate)
                {
                    
                    return true;
                }
                else
                {
                    
                    return false;

                }

            }
        }


        public void SaveChanges()
        {

        }

    }
}
