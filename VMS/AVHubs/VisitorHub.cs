
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using VMS.Data;
using VMS.Models;

namespace VMS.AVHubs

{
    public class VisitorHub : Hub
    {
        private readonly VisitorManagementDbContext _context;
        public VisitorHub(VisitorManagementDbContext context)
        {
            _context = context;
        }

        public async Task UpdateVisitorCount(int count)
        {
            await Clients.All.SendAsync("ReceiveVisitorCount", count);
        }
    }
}
