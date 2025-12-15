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


        
        public async Task<List<(DateTime Date, double MaxWeight)>> GetExerciseHistoryAsync(string exerciseName)
        {
           
            // Використовуємо AsNoTracking(), бо нам треба тільки прочитати дані (це швидше)
            var sessions = await _context.Sessions
                .AsNoTracking()
                .Include(s => s.Exercises)
                .ThenInclude(e => e.Sets)
                .ToListAsync();

            var history = new List<(DateTime Date, double MaxWeight)>();

            // 2. Проходимо по кожному тренуванню, сортуючи за датою
            foreach (var session in sessions.OrderBy(s => s.Date))
            {
                // 3. Шукаємо в цьому тренуванні вправу з потрібною назвою
                // StringComparison.OrdinalIgnoreCase дозволяє ігнорувати регістр (Жим == жим)
                var exercise = session.Exercises
                    .FirstOrDefault(e => e.Name.Equals(exerciseName, StringComparison.OrdinalIgnoreCase));

                // 4. Якщо вправа була в цей день і є хоч один підхід
                if (exercise != null && exercise.Sets.Any())
                {
                    // Знаходимо максимальну вагу, підняту в цей день
                    double maxWeight = exercise.Sets.Max(s => s.Weight);

                    // Додаємо точку в історію: (Дата, Вага)
                    history.Add((session.Date, maxWeight));
                }
            }

            return history;
        }


        public async Task<List<string>> GetUniqueExerciseNamesAsync()
        {
            // 1. Беремо всі сесії
            // 2. Витягуємо з них усі вправи (SelectMany)
            // 3. Беремо тільки назви (Select)
            // 4. Залишаємо тільки унікальні (Distinct)
            // 5. Сортуємо за алфавітом (OrderBy)

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
