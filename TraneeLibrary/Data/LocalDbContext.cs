using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraneeLibrary;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace TraneeLibrary.Data
{
    public class LocalDBContext : DbContext
    {

        

        public LocalDBContext()
        {
          
        }

        public LocalDBContext(DbContextOptions<LocalDBContext> options)
           : base(options)
        {
         
        }


        public DbSet<TrainingTemplate> TrainingTemplates { get; set; }
        public DbSet<ExerciseTemplate> ExerciseTemplates { get; set; }
        public DbSet<TraningSession> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }

     

        
    }
}
