
namespace ValueDriverDashboard.Commands
{
    using System.Windows.Input;
    using ValueDriverDashboard.Events;
    using ValueDriverDashboard.ViewModels;

    public class UpdateDataInput : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the UpdateCustomerCommand class.
        /// </summary>
        //private StockPriceViewModel _stockPriceViewModel;

        

        public UpdateDataInput(DataInputViewModel viewModel)
        {
            //_stockPriceViewModel = stockPriceViewModel;
            _dataInputModel = viewModel;
        }

        private DataInputViewModel _dataInputModel;

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
            return _dataInputModel.CanUpdate;
        }

        public void Execute(object parameter)
        {
            _dataInputModel.Submitted();
            //_stockPriceViewModel.UpdateChart(_dataInputModel.DataInput);
        }



    }
}
