using LiveCharts;
using LiveCharts.Wpf;
using System.Threading.Tasks;
using Yahoo.Finance;
using ValueDriverDashboard.Events;
using EdgarCacheFramework;

namespace ValueDriverDashboard.Models
{
    public class StockPriceSeries : ChartViewBase
    {
        private DataPullHelper _dataPullHelper;

        public StockPriceSeries()
        {
            _dataPullHelper = new DataPullHelper();
            this.YFormatter = value => value.ToString("C");
        }

        public async Task UpdateChart(DataInputEventArgs dataInput)
        {
            HistoricalDataRecord[] yahooDl = await _dataPullHelper.GetStockPrice(dataInput.Ticker,dataInput.StartDate, dataInput.EndDate);
            _chartSeriesCollection.Clear();
            _chartLabels.Clear();
            
            bool newTicker = true;
            

            int latestIndex = 1;

            if (newTicker)
            {
                _chartSeriesCollection.Add(new LineSeries
                {
                    Title = dataInput.Ticker,
                    Values = new ChartValues<double>(),
                    //DataLabels = true

                });
                latestIndex = _chartSeriesCollection.Count;
            }


            int stepCount = yahooDl.Length < 90 ? 1 : yahooDl.Length > 180 ? 5 : yahooDl.Length > 360 ? 30 : yahooDl.Length > 600 ? 90 : 120 ; 
            for (int i = 0; i < yahooDl.Length; i++)
            {
                if(i%stepCount == 0)
                {
                    this._chartLabels.Add(yahooDl[i].Date.ToString("MM/dd/yy"));
                    _chartSeriesCollection[latestIndex - 1].Values.Add((double)yahooDl[i].AdjustedClose);
                }
                
            }
            for (int i = 0; i < _chartSeriesCollection.Count; i++)
            {
                if (_chartSeriesCollection[i].Title == dataInput.Ticker)
                {
                    newTicker = false;
                    latestIndex = i;
                }
            }

            OnPropertyChanged("ChartLabels");
            
            OnPropertyChanged("ChartSeriesCollection");
        }

    }
}


