
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.Models;


namespace VMS.Data
{
    public class DashboardData
    {
        public int ActiveVisitors { get; set; }
        public int ScheduledVisitors { get; set; }
        public int TotalVisitors { get; set; }
        public IEnumerable<Visitor> Visitors { get; set; }

    }
}
