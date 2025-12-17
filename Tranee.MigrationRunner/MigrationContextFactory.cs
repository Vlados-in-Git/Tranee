using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using TraneeLibrary.Data;


namespace Tranee.MigrationRunner
{
    
    public class MigrationContextFactory : IDesignTimeDbContextFactory<LocalDBContext>
    {
        public LocalDBContext CreateDbContext(string[] args)
        {
            
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = System.IO.Path.Join(path, "Tranee_DEV_MIGRATION.db");

            Console.WriteLine($"[Runner] Creating context for migration. Path: {dbPath}");

            
            var optionsBuilder = new DbContextOptionsBuilder<LocalDBContext>();
            optionsBuilder.UseSqlite($"Data Source={dbPath}", b =>
               
                b.MigrationsAssembly("TraneeLibrary"));

            return new LocalDBContext(optionsBuilder.Options);
        }
    }
}