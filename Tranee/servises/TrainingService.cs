using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraneeLibrary;
using Microsoft.EntityFrameworkCore;
using TraneeLibrary.Data;

namespace Tranee.servises
{
    public class TrainingService
    {
        private readonly LocalDBContext _context;

        public TrainingService(LocalDBContext context)
        {
            _context = context;
        }


        public async Task<List<TraningSession>> GetHistoryAsync()
        {
            return await _context.Sessions 
                .Include(s => s.TrainingTemplate) 
                .OrderByDescending(s => s.Date)   
                .ToListAsync();
        }

        public async Task FinishSessionAsync(TraningSession session)
        {
            _context.Sessions.Update(session);

            await _context.SaveChangesAsync();

        }
    
        public async Task<TraningSession> GetSessionByIdAsync(int id)
        {
            return  _context.Sessions
                            .Include(e => e.Exercises)
                            .ThenInclude(s => s.Sets)
                            .FirstOrDefault(e => e.Id == id);
        }
        public async Task<List<TraningSession>> GetAllSessionAsync()
        {
            return await _context.Sessions
                .Include(x => x.Exercises)
                .ThenInclude(e => e.Sets)
                .ToListAsync();
        }

        public async Task AddSessionAsync(TraningSession session)
        {
           await _context.Sessions.AddAsync(session);
           await _context.SaveChangesAsync();
        }

        public async Task DeleteSessionAsync(TraningSession session)
        {
            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
}
