using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraneeLibrary;
using Microsoft.EntityFrameworkCore;
using Tranee.Data;

namespace Tranee.servises
{
    public class TrainingService
    {
        private readonly LocalDBContext _context;

        public TrainingService(LocalDBContext context)
        {
            _context = context;
        }

        public async Task<List<TraningSession>> GetAllSessionAsync()
        {
            return await _context.Sessions
                .Include(x => x.Exercises)
                .ThenInclude(e => e.Sets)
                .ToListAsync();
        }
    }
}
