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
    public class TotalAssetsViewModel : ViewModelBase
    {
        public TotalAssetsSeries _AssetsSeries { get; set; }
        public TotalAssetsSeries AssetsSeries { get { return _AssetsSeries; } }
        //private DataInputViewModel _DataInputView;
        public TotalAssetsViewModel()
        {

            _AssetsSeries = new TotalAssetsSeries();

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
        public void UpdateChart(DataInputEventArgs dataInput)
        {
            _AssetsSeries.UpdateChart(dataInput);
        }

    }
}