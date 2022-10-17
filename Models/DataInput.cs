using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueDriverDashboard.Models
{
    public class DataInput : ViewModelBase
    {
        private string _ticker;
        private DateTime? _startDate;
        private DateTime? _endDate;
        public DataInput(string ticker, DateTime? startDate, DateTime? endDate)
        {
            this._ticker = ticker;
            this._startDate = startDate;
            this._endDate = endDate;
        }
        public DateTime StartDate { get
            {
                if(this._startDate == null)
                {
                    return DateTime.Today.AddDays(-20.0);
                } else 
                return (DateTime)_startDate;
            } 
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public DateTime EndDate
        {
            get
            {
                if(this._endDate == null)
                {
                    return DateTime.Today;
                }
                return (DateTime)_endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }
        public string Ticker
        {
            get
            {
                return _ticker;
            }
            set
            {
                _ticker = value;
                OnPropertyChanged("Ticker");
            }
        }
    }
}
