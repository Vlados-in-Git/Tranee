using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;

namespace Tranee.Data
{
    // Used by dotnet-ef at design time so EF tools don't need a runnable MAUI deps.json
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LocalDBContext>
    {
        public LocalDBContext CreateDbContext(string[] args)
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(folder, "TraneeLocal.DesignTime.db");
            var optionsBuilder = new DbContextOptionsBuilder<LocalDBContext>();
            optionsBuilder.UseSqlite($"Filename={path}");
            return new LocalDBContext(optionsBuilder.Options);
        }
    }
}