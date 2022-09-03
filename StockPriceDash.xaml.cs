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

using System.Linq;
namespace ValueDriverDashboard
{
    public partial class StockDashboard : UserControl
    {
        private HistoricalDataProvider yahooDl;
        public StockDashboard()
        {
            yahooDl = new HistoricalDataProvider();

            InitializeComponent();

            SeriesCollection = new SeriesCollection
                {

                };

            Labels = new List<string>();
            YFormatter = value => value.ToString("C");
            //XFormatter = value => value.ToString("mm/dd/yy");
            

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public Func<DateTime, string> XFormatter { get; set; }

        private async void btnGetStock_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await yahooDl.DownloadHistoricalDataAsync(txtTickerInput.Text, DateTime.Parse("8/1/2022"), DateTime.Today);
            
            SeriesCollection.Add(new LineSeries
            {
                Title = txtTickerInput.Text,
                Values = new ChartValues<double>(),
                //DataLabels = true
                
            });
            //Labels = new List<string>();
            int latestIndex = SeriesCollection.Count;
            
            for(int i = 0; i<yahooDl.HistoricalData.Length;i++)
            {
                if(!LabelsPopulated)
                    this.Labels.Add(yahooDl.HistoricalData[i].Date.ToString("MM/dd/yy"));
                SeriesCollection[latestIndex-1].Values.Add((double)yahooDl.HistoricalData[i].AdjustedClose);
            }
            LabelsPopulated = true;
        }
        bool LabelsPopulated = false;
    }
}