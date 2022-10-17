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
using ValueDriverDashboard.ViewModels;

namespace ValueDriverDashboard.Components
{
    /// <summary>
    /// Interaction logic for DataInput.xaml
    /// </summary>
    public partial class DataInputControl : UserControl
    {
        public DataInputControl()
        {
            InitializeComponent();
            //DataContext = new DataInputModelView();

        }
    }
}
