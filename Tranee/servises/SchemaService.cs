using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraneeLibrary;
using TraneeLibrary.Data;

namespace Tranee.servises
{
    public class SchemaService
    {
        private readonly LocalDBContext _context;
        private readonly NavigationService _navigationService;
        TrainingService _trainingService;

        public SchemaService(LocalDBContext context, NavigationService navigation, TrainingService trainingService)
        {
            _context = context;
            _navigationService = navigation;
            _trainingService = trainingService;
        }


        public async Task<int> StartSessionFromTemplateAsync(int templateId)
        {
            var template = _context.TrainingTemplates
                                         .Include(e => e.ExerciseTemplates)
                                         .FirstOrDefault(t => t.Id == templateId);

            if (template == null) return -1;

              var newSession = new TraningSession
              {
                  Date = DateTime.Now,
                  TrainingTemplateId = templateId,
                  Exercises = new List<Exercise>()

              };

            foreach (var tmpExetcise in template.ExerciseTemplates)
            {
                var realExercise = new Exercise
                {
                    Name = tmpExetcise.Name,
                    GroupOfMuscle = tmpExetcise.GroupOfMuscle,
                    RestBetweenSets = tmpExetcise.RestBetweenSets,
                    Sets = new ObservableCollection<Set>()
                };

                for (int i = 0; i < tmpExetcise.TargetSets; i++)
                {
                    realExercise.Sets.Add(new Set
                    {
                        Number = i + 1,
                        Reps = tmpExetcise.TargetReps,
                        Weight = 0, // Вага поки 0, юзер впише сам
                       
                    });
                }

                newSession.Exercises.Add(realExercise);
            }

            _context.Sessions.Add(newSession);
            await _context.SaveChangesAsync();


            return newSession.Id;
        }

        public async Task<List<TrainingTemplate>> GetAllTrainingTemplatesAsync()
        {
            return await _context.TrainingTemplates
                .Include(e => e.ExerciseTemplates)
                .ToListAsync();
        }

        public async Task AddTemplateAsync(TrainingTemplate template)
        {
            await _context.TrainingTemplates.AddAsync(template);
             await _context.SaveChangesAsync();
        }

        public async Task DeleteTemplateAsync(TrainingTemplate template)
        {
             _context.TrainingTemplates.Remove(template);
            await _context.SaveChangesAsync();
        }

        //TODO: Make a Upgrade metode that changes concreate object;
       
        /*public async Task UpdateTemplatesAsync(int id, TrainingTemplate updatedTemplate)
        {
            _context.TrainingTemplates.
        } */
    } 
}
