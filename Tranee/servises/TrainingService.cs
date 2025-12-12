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


        public async Task<int>  StartSessionFromTemplateAsync(int templateId)
        {
            var template =  _context.TrainingTemplates
                                         .Include(e => e.ExerciseTemplates)
                                         .FirstOrDefault(t => t.Id == templateId);

            if (template == null) return -1;

          /*  var newSession = new TraningSession
            {
                Date = DateTime.Now,
                TrainingTemplateId = templateId,


            }; */



            return 0;
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
