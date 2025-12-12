using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraneeLibrary.Data;
using TraneeLibrary;
using Microsoft.EntityFrameworkCore;

namespace Tranee.servises
{
    public class SchemaService
    {
        private readonly LocalDBContext _context;

        public SchemaService(LocalDBContext context)
        {
            _context = context;
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
            _context.SaveChangesAsync();
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
