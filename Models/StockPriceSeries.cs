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
    public class StockPriceSeries : ViewModelBase
    {
        private List<string> _stockChartLabels;
        private Func<double, string> _stockYFormatter;
        private DataPullHelper _dataPullHelper;
        public SeriesCollection StockChartSeriesCollection { get; }

        public Func<double, string> StockYFormatter { get { return _stockYFormatter; } }
        //private HistoricalDataProvider yahooDl;
        public StockPriceSeries()
        {
            _dataPullHelper = new DataPullHelper();

            StockChartSeriesCollection= new SeriesCollection
            {

            };
            
            _stockChartLabels = new List<string>();
            _stockYFormatter = value => value.ToString("C");
        }

        public List<string> StockChartLabels
        {
            get
            {
                //
                return _stockChartLabels;
            }
        }


        public async Task UpdateChart(DataInputEventArgs dataInput)
        {
            HistoricalDataRecord[] yahooDl = await _dataPullHelper.GetStockPrice(dataInput.Ticker,dataInput.StartDate, dataInput.EndDate);
            StockChartSeriesCollection.Clear();
            StockChartLabels.Clear();
            
            bool newTicker = true;
            

            int latestIndex = 1;

            if (newTicker)
            {
                StockChartSeriesCollection.Add(new LineSeries
                {
                    Title = dataInput.Ticker,
                    Values = new ChartValues<double>(),
                    //DataLabels = true

                });
                latestIndex = StockChartSeriesCollection.Count;
            }
            //Labels = new List<string>();


            for (int i = 0; i < yahooDl.Length; i++)
            {
                this.StockChartLabels.Add(yahooDl[i].Date.ToString("MM/dd/yy"));
                StockChartSeriesCollection[latestIndex - 1].Values.Add((double)yahooDl[i].AdjustedClose);
            }
            for (int i = 0; i < StockChartSeriesCollection.Count; i++)
            {
                if (StockChartSeriesCollection[i].Title == dataInput.Ticker)
                {
                    newTicker = false;
                    latestIndex = i;
                }
            }

            OnPropertyChanged("StockChartLabels");
            
            OnPropertyChanged("StockChartSeriesCollection");
        }
    }



}
