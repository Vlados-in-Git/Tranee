using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraneeLibrary;
using Microsoft.EntityFrameworkCore;
using SQLite;
using System.IO;

namespace Tranee.Data
{
    public class LocalDBContext : DbContext
    {
        public LocalDBContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<TraningSession> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, "TraneeLocal.db");

            optionsBuilder.UseSqlite($"Filename = {path}");
            base.OnConfiguring(optionsBuilder);
        }

        
    }
}
