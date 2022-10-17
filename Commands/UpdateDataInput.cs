
namespace ValueDriverDashboard.Commands
{
    using System.Windows.Input;
    using ValueDriverDashboard.ViewModels;

    internal class UpdateDataInput : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the UpdateCustomerCommand class.
        /// </summary>
        public UpdateDataInput(DataInputModelView viewModel)
        {
            _ViewModel = viewModel;
        }

        private DataInputModelView _ViewModel;

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
            _ViewModel.UpdateViews();
        }
    }
}
