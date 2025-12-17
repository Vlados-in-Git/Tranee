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
            // 1. Отримуємо дані (тут все без змін)
            var sessions = await _context.Sessions
                .AsNoTracking()
                .Include(s => s.Exercises)
                .ThenInclude(e => e.Sets)
                .ToListAsync();

            // ЗМІНА: Тепер створюємо список нашого класу, а не кортежів
            var history = new List<ExerciseHistoryItem>();

            // 2. Проходимо по кожному тренуванню
            foreach (var session in sessions.OrderBy(s => s.Date))
            {
                var exercise = session.Exercises
                    .FirstOrDefault(e => e.Name.Equals(exerciseName, StringComparison.OrdinalIgnoreCase));

                // Якщо вправа була в цей день і є підходи
                if (exercise != null && exercise.Sets.Any())
                {

                    // 1. Максимальна вага (Сила)
                    double maxWeight = exercise.Sets.Max(s => s.Weight);

                    // 2. Об'єм (Сума: вага * повтори для кожного підходу)
                    double volume = exercise.Sets.Sum(s => s.Weight * s.Reps);

                    // 3. Загальна кількість повторів (Витривалість)
                    int totalReps = exercise.Sets.Sum(s => s.Reps);

                    // 4. 1ПМ (Теоретичний максимум за формулою Еплі)
                    // Рахуємо для кожного підходу і беремо найкращий результат за день
                    double oneRepMax = exercise.Sets.Max(s => s.Weight * (1 + (double)s.Reps / 30.0));

                    // Додаємо повний об'єкт в історію
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
