using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ValueDriverDashboard.ViewModels;

namespace ValueDriverDashboard
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public StockPriceViewModel StockPriceViewModel { get; set; }
        public DataInputViewModel DataInputViewModel { get; set; }
        public TotalAssetsViewModel TotalAssetsViewModel { get; set; }
        
        public Dashboard()
        {
            InitializeComponent();
            
            StockPriceViewModel = new StockPriceViewModel();
            DataInputViewModel = new DataInputViewModel();
            TotalAssetsViewModel = new TotalAssetsViewModel();

            this.stockPriceChart.DataContext = StockPriceViewModel;
            this.dataSelector.DataContext = DataInputViewModel;
            this.totalAssetsChart.DataContext = TotalAssetsViewModel;
            DataContext = new MainViewModel(DataInputViewModel, StockPriceViewModel, TotalAssetsViewModel);
            
            
        }
    }
}
