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
    public class MainViewModel
    {
        private DataInputViewModel _DataInput;
        private StockPriceViewModel _StockPriceViewModel;
        private TotalAssetsViewModel _TotalAssetsViewModel;

        public MainViewModel(DataInputViewModel dataInput, StockPriceViewModel stockPriceSeriesView, TotalAssetsViewModel assetsView)
        {
            _DataInput = dataInput;
            _StockPriceViewModel = stockPriceSeriesView;
            _TotalAssetsViewModel = assetsView;
            //Subscribe to data input submission events
            _DataInput.DataInputSubmitted += OnDataInputSubmitted;
            
            
        }
        public void OnDataInputSubmitted(object source, DataInputEventArgs e)
        {
            //In the event of data inputs being submitted, update these things
            _StockPriceViewModel.UpdateChart(e);
            _TotalAssetsViewModel.UpdateChart(e);

            
        }

        public DataInputViewModel DataInput { get { return _DataInput; } }
        public StockPriceViewModel StockPriceSeries { get { return _StockPriceViewModel; } }


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


    }
}
