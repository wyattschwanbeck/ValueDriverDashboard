using LiveCharts;
using LiveCharts.Wpf;
using System;
using ValueDriverDashboard.Events;
using EdgarCacheFramework;
using EdgarCacheFramework.DataAccess;
using EdgarCacheFramework.Models;
using Xbrl.FinancialStatement;
using System.Globalization;
using System.Threading.Tasks;
using Yahoo.Finance;
using System.Windows.Media;

namespace ValueDriverDashboard.Models
{
    public class StockMetricSeries : ChartViewBase
    {
        //private HistoricalDataProvider yahooDl;
        private StockPriceInstance[] historicalStockPrice;
        private DataPullHelper db;
        public StockMetricSeries()
        {

            
            _YFormatter = value => Math.Abs(value) > 999999999 ? value.ToString("$#,##0,,,.##B", CultureInfo.InvariantCulture) : Math.Abs(value) > 999999 ?
                value.ToString("$#,##0,,.##M", CultureInfo.InvariantCulture) : Math.Abs(value) > 999 ? value.ToString("$#,##0,.#K", CultureInfo.InvariantCulture) : value.ToString("C");
            db = new DataPullHelper();
            _axisYCollection = new AxesCollection
            {
                new Axis { Title = "Price to Earnings", Foreground = Brushes.Gray , LabelFormatter = _YFormatter},
                new Axis { Title = $"Dividends & EPS", Foreground = Brushes.Gray, LabelFormatter=_YFormatter, Position= AxisPosition.RightTop}
            };
        }

        public async Task UpdateChart(DataInputEventArgs dataInput)
        {
            
            int yearsPrior = (int)-((dataInput.StartDate - DateTime.Today).TotalDays / 365);
            //string reportType = yearsPrior > 4 ? "10-K" : "10-Q";
            string reportType = "10-K";
            FinancialStatement[] fs = await db.GetFinancialStatements(dataInput.Ticker, reportType, yearsPrior);
            
            _chartSeriesCollection.Clear();
            _chartLabels.Clear();

            OnPropertyChanged("ChartLabels");
            OnPropertyChanged("SeriesCollection");




            _chartSeriesCollection.Add(new ColumnSeries
            {
                Title = "EarningsPerShare",
                Values = new ChartValues<double>(),
                ScalesYAt = 1

            });
            
            _chartSeriesCollection.Add(new ColumnSeries
            {
                Title = "DividendsPerShare",
                Values = new ChartValues<double>(),
                ScalesYAt = 1
            });

            _chartSeriesCollection.Add(new LineSeries
            {
                Title = "Price To Earnings",
                Values = new ChartValues<double>(),
                ScalesYAt= 0
            });

            int fsI = 0;
            //bool financialPeriodCorrect = false;
            double eps = 0;
            double dividendsPerShare = 0;
            historicalStockPrice = DbHelper.GetStockData(dataInput.Ticker, dataInput.StartDate, dataInput.EndDate);
            int stepCount = historicalStockPrice.Length < 90 ? 1 : historicalStockPrice.Length > 180 ? 5 : historicalStockPrice.Length > 360 ? 30 : historicalStockPrice.Length > 600 ? 90 : 120;

            for (int i = 0; i < fs.Length; i++)
            {
                /*
                financialPeriodCorrect = (fs[fsI].PeriodEnd.Value.ToFileTimeUtc() >= historicalStockPrice[i].Date) && fs[fsI].PeriodStart.Value.ToFileTimeUtc() <= historicalStockPrice[i].Date;
                while (!financialPeriodCorrect && fsI<fs.Length-1)
                {
                    fsI += 1;
                    financialPeriodCorrect = (fs[fsI].PeriodEnd.Value.ToFileTimeUtc() <= historicalStockPrice[i].Date) && fs[fsI].PeriodStart.Value.ToFileTimeUtc() <= historicalStockPrice[i].Date;
                }
                fsInstance = fs[fsI];
               */
                historicalStockPrice = DbHelper.GetStockData(dataInput.Ticker, fs[i].PeriodStart.Value, fs[i].PeriodEnd.Value);
                DateTime stockDate = DateTime.FromFileTimeUtc(historicalStockPrice[0].Date);
                if (fs[i].DividendsPaid == null)
                    fs[i].DividendsPaid = 0;

                    dividendsPerShare = (double)(fs[i].DividendsPaid / fs[i].CommonStockSharesOutstanding);
                        
                
                    eps = (double)(fs[i].NetIncome / fs[i].CommonStockSharesOutstanding);
                    _chartSeriesCollection[0].Values.Add(eps);
                    _chartSeriesCollection[1].Values.Add(dividendsPerShare);
                //_chartSeriesCollection[0].Values.Add(0.0d);
                //_chartSeriesCollection[1].Values.Add(0.0d);
                float averageStockPrice = 0;
                foreach (StockPriceInstance record in historicalStockPrice)
                    averageStockPrice +=record.AdjustedClose;

                averageStockPrice = averageStockPrice / historicalStockPrice.Length;
                    _chartSeriesCollection[2].Values.Add((double)averageStockPrice/ eps);
                    //_chartSeriesCollection[2].Values.Add((double)historicalStockPrice[historicalStockPrice.Length-1].AdjustedClose / eps);
                    //_chartLabels.Add(fs[i].PeriodStart.Value.ToString("MM/yy"));
                    _chartLabels.Add(fs[i].PeriodEnd.Value.ToString("MM/yy"));

           
            }
            OnPropertyChanged("ChartLabels");
            OnPropertyChanged("SeriesCollection");
        }





    }
}
