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
    public class TotalAssetsSeries : ChartViewBase
    {
        //private HistoricalDataProvider yahooDl;
        private DataPullHelper db;
        public TotalAssetsSeries()
        {

            
            _YFormatter = value => value > 999999999 ? value.ToString("$#,##0,,,.##B", CultureInfo.InvariantCulture) : value > 999999 ?
                value.ToString("$#,##0,,.##M", CultureInfo.InvariantCulture) : value > 999 ? value.ToString("$#,##0,.#K", CultureInfo.InvariantCulture) : value.ToString("C");
            db = new DataPullHelper();

        }

        public async void UpdateChart(DataInputEventArgs dataInput)
        {
            int yearsPrior = (int)-((dataInput.StartDate - DateTime.Today).TotalDays / 365);
            string reportType = yearsPrior > 4 ? "10-K" : "10-Q";

            FinancialStatement[] fs = await db.GetFinancialStatements(dataInput.Ticker, reportType, yearsPrior);

            _chartSeriesCollection.Clear();
            _chartLabels.Clear();

            OnPropertyChanged("ChartLabels");
            OnPropertyChanged("SeriesCollection");




            _chartSeriesCollection.Add(new ColumnSeries
            {
                Title = "Assets",
                Values = new ChartValues<double>(),
                //DataLabels = true

            });

            _chartSeriesCollection.Add(new ColumnSeries
            {
                Title = "Liabilities",
                Values = new ChartValues<double>()
            });


            for (int i = 0; i < fs.Length; i++)
            {
                //this.AssetsSeriesCollection.Add(yahooDl.HistoricalData[i].Date.ToString("yy"));
                _chartSeriesCollection[0].Values.Add((double)fs[i].Assets);
                _chartSeriesCollection[1].Values.Add((double)fs[i].Liabilities);
                _chartLabels.Add(((DateTime)fs[i].PeriodEnd).ToString("MM/yy"));
            }
            OnPropertyChanged("AssetsChartLabels");
            OnPropertyChanged("AssetsSeriesCollection");
        }





    }
}
