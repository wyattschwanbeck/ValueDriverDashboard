using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ValueDriverDashboard.Models;
using ValueDriverDashboard.Commands;
using System.Windows;

namespace ValueDriverDashboard.ViewModels
{
    public class DataInputModelView : ViewModelBase
    {
        private DataInput _DataInput;
        private StockPriceViewModel _StockPriceSeriesView;
        public DataInputModelView(StockPriceViewModel stockPriceViewModel)
        {
            _DataInput = new DataInput(null, null, DateTime.Today);
            UpdateCommand = new UpdateDataInput(this);
            _StockPriceSeriesView = stockPriceViewModel;
        }

        public DataInput DataInput { get { return _DataInput; } }
        public ICommand UpdateCommand { get; set; }

        public bool CanUpdate
        {
            get
            {
                if (_DataInput == null)
                {
                    return false;
                }
                else if (String.IsNullOrEmpty(_DataInput.Ticker) || _DataInput.EndDate== null || _DataInput.EndDate== null)
                {
                       
                    return false;
                }
                    
                else
                {
                    
                    return true;
                }
                    
            }
        }


        public void UpdateViews()
        {
            _StockPriceSeriesView.UpdateChart(_DataInput);
        }
        
    }
}
