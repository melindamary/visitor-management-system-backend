using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;
using VMS.Data;
using VMS.Repository.IRepository;

namespace VMS.Repository
{

    public class DashboardRepository : IDashboardRepository
    {
        private readonly DashboardData _context; // Replace with your actual DbContext  

        public DashboardRepository(DashboardData context)
        {
            _context = context;
        }

    /*    public int GetActiveVisitors()
        {
            // Implement logic to get active visitors from the database  
            return _context.Visitors.Count(v => v.IsActive); // Example query  
        }

        public int GetScheduledVisitors()
        {
            // Implement logic to get scheduled visitors  

            return _context.Visitors.Count(v => v.VisitorId);
        }

        public int GetTotalVisitors()
        {
            // Implement logic to get total visitors  
            return _context.Visitors.Count();
        }*/

    }
}
