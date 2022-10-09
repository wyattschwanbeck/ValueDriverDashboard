using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueDriverDashboard.ControlEvents
{
    public class GetDataEventArg : EventArgs
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string ticker { get; set; }
    }
}
