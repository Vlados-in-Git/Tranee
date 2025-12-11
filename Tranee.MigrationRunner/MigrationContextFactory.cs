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
            // 1. Визначаємо шлях до ТИМЧАСОВОЇ бази на Windows
            // Це потрібно лише щоб інструмент міг кудись "плюнути" і перевірити модель
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = System.IO.Path.Join(path, "Tranee_DEV_MIGRATION.db");

            Console.WriteLine($"[Runner] Creating context for migration. Path: {dbPath}");

            // 2. Налаштовуємо SQLite
            var optionsBuilder = new DbContextOptionsBuilder<LocalDBContext>();
            optionsBuilder.UseSqlite($"Data Source={dbPath}", b =>
                // !!! НАЙВАЖЛИВІШЕ !!!
                // Кажемо: "Я (Runner) запускаю процес, але самі файли міграцій
                // поклади, будь ласка, в бібліотеку (TraneeLibrary)"
                b.MigrationsAssembly("TraneeLibrary"));

            return new LocalDBContext(optionsBuilder.Options);
        }
    }
}