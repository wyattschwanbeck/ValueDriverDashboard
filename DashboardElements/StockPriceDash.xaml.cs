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
            InitializeComponent();
            


            DataContext = this;
        }

       

        public async void OnDataInputAsync(object source, GetDataEventArg pullData)
        {
            this.lblLoading.Visibility = System.Windows.Visibility.Visible;
            //this.StockChartSeriesCollection.Clear();
            await yahooDl.DownloadHistoricalDataAsync(pullData.ticker, pullData.startDate, pullData.endDate);
            bool newTicker = true;
            int latestIndex = 1;
            for(int i = 0; i < StockChartSeriesCollection.Count; i++)
            {
               if(StockChartSeriesCollection[i].Title == pullData.ticker)
                {
                    newTicker = false;
                    latestIndex = i;
                }
            }
            if (newTicker)
            {
                StockChartSeriesCollection.Add(new LineSeries
                {
                    Title = pullData.ticker,
                    Values = new ChartValues<double>(),
                    //DataLabels = true

                });
                latestIndex = StockChartSeriesCollection.Count;
            }
            //Labels = new List<string>();
            

            for (int i = 0; i < yahooDl.HistoricalData.Length; i++)
            {
                DateTime pulledDate = yahooDl.HistoricalData[i].Date;
                DateTime compare = i > 0 ? DateTime.Parse(this.StockChartLabels[i - 1]) : this.StockChartLabels.Count==0? DateTime.MinValue:DateTime.Parse(this.StockChartLabels[i]);
                if (i>0 && compare > pulledDate)
                {
                    this.StockChartLabels.Insert(i-1, pulledDate.ToString("MM/dd/yy"));
                    StockChartSeriesCollection[latestIndex - 1].Values.Insert(i-1,(double)yahooDl.HistoricalData[i].AdjustedClose);
                }

                else if (i>0 && compare< pulledDate)
                {
                    this.StockChartLabels.Add(pulledDate.ToString("MM/dd/yy"));
                    StockChartSeriesCollection[latestIndex - 1].Values.Add((double)yahooDl.HistoricalData[i].AdjustedClose);
                } else
                {
                    if(this.StockChartLabels.Count==0)
                        this.StockChartLabels.Add(pulledDate.ToString("MM/dd/yy"));
                    StockChartSeriesCollection[latestIndex - 1].Values.Add((double)yahooDl.HistoricalData[i].AdjustedClose);
                }
               
            }
            //this.LabelAxis.LabelFormatter = StockXFormatter;
            this.lblLoading.Visibility = System.Windows.Visibility.Hidden;
        }


        
       
    }
}