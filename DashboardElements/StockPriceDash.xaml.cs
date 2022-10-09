using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using NodaTime;
using System.Globalization;
using Yahoo.Finance;
using ValueDriverDashboard.Controls;
using System.Linq;
using ValueDriverDashboard.ControlEvents;

namespace ValueDriverDashboard.DashboardElements
{
    public partial class StockDashboard : UserControl
    {
        public List<string> StockChartLabels { get; set; }
        public Func<double, string> StockYFormatter { get; set; }
        public Func<DateTime, string> XFormatter { get; set; }
        public SeriesCollection StockChartSeriesCollection { get; set; }
        private HistoricalDataProvider yahooDl;

        public delegate void dataInputChanged(GetDataEventArg e);

        public StockDashboard()
        {

            yahooDl = new HistoricalDataProvider();
            StockChartSeriesCollection = new SeriesCollection
            {

            };

            StockChartLabels = new List<string>();
            StockYFormatter = value => value.ToString("C");
            //XFormatter = value => value.ToString("mm/dd/yy");

            InitializeComponent();
            


            DataContext = this;
        }

       

        public async void OnDataInputAsync(object source, GetDataEventArg pullData)
        {
            this.StockChartSeriesCollection.Clear();
            await yahooDl.DownloadHistoricalDataAsync(pullData.ticker, pullData.startDate, pullData.endDate);

            StockChartSeriesCollection.Add(new LineSeries
            {
                Title = pullData.ticker,
                Values = new ChartValues<double>(),
                //DataLabels = true

            });
            //Labels = new List<string>();
            int latestIndex = StockChartSeriesCollection.Count;

            for (int i = 0; i < yahooDl.HistoricalData.Length; i++)
            {
                this.StockChartLabels.Add(yahooDl.HistoricalData[i].Date.ToString("MM/dd/yy"));
                StockChartSeriesCollection[latestIndex - 1].Values.Add((double)yahooDl.HistoricalData[i].AdjustedClose);
            }
            
        }


        
       
    }
}