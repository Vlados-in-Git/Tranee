using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranee.viewModels;
using TraneeLibrary;
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



        public async Task<List<ExerciseHistoryItem>> GetExerciseHistoryAsync(string exerciseName)
        {
            
            var sessions = await _context.Sessions
                .AsNoTracking()
                .Include(s => s.Exercises)
                .ThenInclude(e => e.Sets)
                .ToListAsync();

           
            var history = new List<ExerciseHistoryItem>();

            
            foreach (var session in sessions.OrderBy(s => s.Date))
            {
                var exercise = session.Exercises
                    .FirstOrDefault(e => e.Name.Equals(exerciseName, StringComparison.OrdinalIgnoreCase));

                
                if (exercise != null && exercise.Sets.Any())
                {

                    
                    double maxWeight = exercise.Sets.Max(s => s.Weight);

                    
                    double volume = exercise.Sets.Sum(s => s.Weight * s.Reps);

                   
                    int totalReps = exercise.Sets.Sum(s => s.Reps);

                    
                    double oneRepMax = exercise.Sets.Max(s => s.Weight * (1 + (double)s.Reps / 30.0));

                   
                    history.Add(new ExerciseHistoryItem
                    {
                        Date = session.Date,
                        MaxWeight = maxWeight,
                        Volume = volume,
                        TotalReps = totalReps,
                        OneRepMax = oneRepMax
                    });
                }
            }

            return history;
        }



        public async Task<List<string>> GetUniqueExerciseNamesAsync()
        {
           

            return await _context.Sessions
                .AsNoTracking()
                .SelectMany(s => s.Exercises)
                .Select(e => e.Name)
                .Distinct()
                .OrderBy(n => n)
                .ToListAsync();
        }

        public async Task<List<TraningSession>> GetHistoryAsync()
        {
            return await _context.Sessions 
                .Include(s => s.TrainingTemplate) 
                .Include(e => e.Exercises)
                    .ThenInclude(s => s.Sets)
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
