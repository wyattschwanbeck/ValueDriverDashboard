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
namespace ValueDriverDashboard.Models
{
    public class StockPriceSeries : ViewModelBase
    {
        private List<string> _stockChartLabels;
        private Func<double, string> _stockYFormatter;
        public SeriesCollection StockChartSeriesCollection { get; }

        private HistoricalDataProvider yahooDl;
        public Func<double, string> StockYFormatter { get { return _stockYFormatter; } }
        //private HistoricalDataProvider yahooDl;
        public StockPriceSeries()
        {
            StockChartSeriesCollection= new SeriesCollection
            {

            };
            yahooDl = new HistoricalDataProvider();
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


        public async void UpdateChart(DataInputEventArgs dataInput)
        {
            StockChartSeriesCollection.Clear();
            StockChartLabels.Clear();
            await yahooDl.DownloadHistoricalDataAsync(dataInput.Ticker, dataInput.StartDate, dataInput.EndDate);
            bool newTicker = true;
            int latestIndex = 1;
            for (int i = 0; i < StockChartSeriesCollection.Count; i++)
            {
                if (StockChartSeriesCollection[i].Title == dataInput.Ticker)
                {
                    newTicker = false;
                    latestIndex = i;
                }
            }
            OnPropertyChanged("StockChartLabels");
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


            for (int i = 0; i < yahooDl.HistoricalData.Length; i++)
            {
                this.StockChartLabels.Add(yahooDl.HistoricalData[i].Date.ToString("MM/dd/yy"));
                StockChartSeriesCollection[latestIndex - 1].Values.Add((double)yahooDl.HistoricalData[i].AdjustedClose);
            }
            OnPropertyChanged("StockChartSeriesCollection");
        }
    }



}
