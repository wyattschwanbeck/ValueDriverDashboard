

namespace ValueDriverDashboard.Commands
{
    using System.Windows.Input;
    using ValueDriverDashboard.ViewModels;
    using Yahoo.Finance;

    internal class GetStockPriceData : CommandBase
    {
        /// <summary>
        /// Initializes a new instance of the GetStockPriceData class which handles retreival from cached db and yahoo finance.
        /// </summary>
        public GetStockPriceData(MainViewModel mainView)
        {
            _ViewModel = mainView.StockPriceSeries;
            _DataInput = mainView.DataInput;
        }
        
        private StockPriceViewModel _ViewModel;
        private DataInputViewModel _DataInput;

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        //public event GetDataEventArgs 

        public override bool CanExecute(object parameter)
        {
            return _ViewModel.CanUpdate;
            //return true;
        }

        public override void Execute(object parameter)
        {

        }

    }
}
