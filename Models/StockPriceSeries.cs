using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

            OnPropertyChanged("StockChartLabels");
            
            OnPropertyChanged("StockChartSeriesCollection");
        }

        private double Erf(double x)
        {
            //https://www.johndcook.com/blog/csharp_erf/
            // constants
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            // Save the sign of x
            int sign = 1;
            if (x < 0)
                sign = -1;
            x = Math.Abs(x);

            // A&S formula 7.1.26
            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }

    }
}


