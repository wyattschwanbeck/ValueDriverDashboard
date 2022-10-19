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
using EdgarCacheFramework.DataAccess;
using Xbrl.FinancialStatement;
using System.Globalization;

namespace ValueDriverDashboard.Models
{
    public class TotalAssetsSeries : ViewModelBase
    {
        private List<string> _assetsChartLabels;
        private Func<double, string> _assetsYFormatter;
        public SeriesCollection AssetsSeriesCollection { get; }

        private HistoricalDataProvider yahooDl;
        public Func<double, string> AssetsYFormatter { get { return _assetsYFormatter; } }
        //private HistoricalDataProvider yahooDl;
        public TotalAssetsSeries()
        {
            AssetsSeriesCollection = new SeriesCollection
            {

            };
            //yahooDl = new HistoricalDataProvider();
            _assetsChartLabels = new List<string>();
            _assetsYFormatter = value => value > 999999999 ? value.ToString("$#,##0,,,.##B", CultureInfo.InvariantCulture) : value > 999999?
            value.ToString("$#,##0,,.##M", CultureInfo.InvariantCulture) : value > 999 ? value.ToString("$#,##0,.#K", CultureInfo.InvariantCulture) : value.ToString("C");
            db = new DataPullHelper();

        }

        public List<string> AssetsChartLabels
        {
            get
            {
                //
                return _assetsChartLabels;
            }
        }
        private DataPullHelper db; 

        public async void UpdateChart(DataInputEventArgs dataInput)
        {
            
            DataPullHelper db = new DataPullHelper();
            FinancialStatement[] fs = await db.GetFinancialStatements(dataInput.Ticker, "10-Q", 1);
            AssetsSeriesCollection.Clear();
            AssetsChartLabels.Clear();
            //await yahooDl.DownloadHistoricalDataAsync(dataInput.Ticker, dataInput.StartDate, dataInput.EndDate);
            bool newTicker = true;
            int latestIndex = 1;
            for (int i = 0; i < AssetsSeriesCollection.Count; i++)
            {
                if (AssetsSeriesCollection[i].Title == dataInput.Ticker)
                {
                    newTicker = false;
                    latestIndex = i;
                }
            }
            
            if (newTicker)
            {
                AssetsSeriesCollection.Add(new LineSeries
                {
                    Title = dataInput.Ticker,
                    Values = new ChartValues<double>(),
                    //DataLabels = true

                });
                latestIndex = AssetsSeriesCollection.Count;
            }
            //Labels = new List<string>();


            for (int i = 0; i < fs.Length; i++)
            {
                //this.AssetsSeriesCollection.Add(yahooDl.HistoricalData[i].Date.ToString("yy"));
                AssetsSeriesCollection[latestIndex - 1].Values.Add((double)fs[i].Assets);
                AssetsChartLabels.Add(((DateTime)fs[i].PeriodEnd).ToString("MM/yy"));
            }
            OnPropertyChanged("AssetsChartLabels");
            OnPropertyChanged("AssetsSeriesCollection");
        }
    }



}
