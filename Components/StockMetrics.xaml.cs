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
using ValueDriverDashboard.ViewModels;
using System.Linq;

namespace ValueDriverDashboard.Components
{
    public partial class StockMetricView : UserControl
    {
        public StockMetricView()
        {
            InitializeComponent();

        }
       
    }
}