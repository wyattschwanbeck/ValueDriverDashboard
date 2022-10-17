

namespace ValueDriverDashboard.Commands
{
    using System.Windows.Input;
    using ValueDriverDashboard.ViewModels;
    using Yahoo.Finance;

    internal class GetStockPriceData : ICommand
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
        private DataInputModelView _DataInput;

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

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanUpdate;
        }

        public void Execute(object parameter)
        {
            _ViewModel.UpdateChart(_DataInput.DataInput);
        }
    }
}
