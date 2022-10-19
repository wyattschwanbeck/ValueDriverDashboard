using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ValueDriverDashboard.Models;
using ValueDriverDashboard.Commands;
using ValueDriverDashboard.Events;

namespace ValueDriverDashboard.ViewModels
{
    public class DataInputViewModel : ViewModelBase
    {
        private DataInput _DataInput;
        //private StockPriceViewModel _StockPriceSeriesView;

        
        public DataInputViewModel()
        {
            _DataInput = new DataInput(null, null, DateTime.Today);
            UpdateCommand = new UpdateDataInput(this);


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
        public delegate void DataInputSubmittedEventHandler(object source, Events.DataInputEventArgs args);
        public event DataInputSubmittedEventHandler DataInputSubmitted;

        protected virtual void OnDataInputSubmitted()
        {
            if (DataInputSubmitted != null)
            {
                DataInputEventArgs dataInputArgs = new DataInputEventArgs { EndDate = DataInput.EndDate, StartDate = DataInput.StartDate, Ticker = DataInput.Ticker };
                DataInputSubmitted(this, dataInputArgs);
            }
            

        }
        public void Submitted()
        {
            OnDataInputSubmitted();
        }

        
    }
}
