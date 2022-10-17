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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yahoo.Finance;
using ValueDriverDashboard.ControlEvents;

namespace ValueDriverDashboard.Controls
{
    /// <summary>
    /// Interaction logic for DataSelect.xaml
    /// </summary>
    public partial class DataSelect : UserControl
    {
        public delegate void getDataEventHandler(object source, GetDataEventArg args);
        public event getDataEventHandler getDataEvent;
        
        public DataSelect()
        {
            //getDataEvent += OnGetDataButtonClicked;
            InitializeComponent();
        }

        private void startDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void endDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckButton();
        }

        private void CheckButton()
        {
            if (this.startDatePicker != null && this.endDatePicker != null && this.txtTickerInput!=null &&
               this.txtTickerInput.Text.Length > 0 &&
                   this.startDatePicker.SelectedDate < this.endDatePicker.SelectedDate)
            {
                this.btnGetStock.IsEnabled = true;
            }
            else if(this.txtTickerInput != null)
            {
                this.btnGetStock.IsEnabled = false; 
            }
        }

        private async void btnGetStock_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //OnGetDataButtonClicked();

                ControlEvents.GetDataEventArg dataEvent = new ControlEvents.GetDataEventArg();
                dataEvent.endDate = (DateTime)this.endDatePicker.SelectedDate;
                dataEvent.startDate = (DateTime)this.startDatePicker.SelectedDate;
                dataEvent.ticker = this.txtTickerInput.Text;
                getDataEvent(this, dataEvent);
           
            
        }
        public delegate GetDataEventArg getDataDelegate();

        private void txtTickerInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.CheckButton();
            if (this.txtTickerInput.Text != "")
            {
                this.txtTickerLabel.Opacity = 0;
            } else
            {
                this.txtTickerLabel.Opacity = 100;
            }
        }
    }
}
