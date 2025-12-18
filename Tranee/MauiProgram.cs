using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tranee.servises;
using Tranee.viewModels;
using Tranee.views;
using TraneeLibrary.Data;
using SkiaSharp.Views.Maui.Controls.Hosting;
using LiveChartsCore.SkiaSharpView.Maui;

namespace Tranee
{
    public static class MauiProgram
    {
       

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseLiveCharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            


            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "TraneeLocal.db");

            builder.Services.AddDbContext<LocalDBContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
                
            });

            
            builder.Services.AddTransient<TrainingService>();
            builder.Services.AddTransient<AddTrainingViewModel>();
            builder.Services.AddTransient<AddTrainPage>();

            builder.Services.AddTransient<CreatingTemplatePage>();
            builder.Services.AddTransient<CreatingTemplatePageViewModel>();

            builder.Services.AddTransient<HistoryViewModel>();
            builder.Services.AddTransient<HistoryPage>();

            builder.Services.AddTransient<AnalyticsPage>();
            builder.Services.AddTransient<AnalyticsViewModel>();

            builder.Services.AddTransient<ActiveTraningPage>();
            builder.Services.AddTransient<ActiveTraningViewModel>();

            builder.Services.AddTransient<SchemaService>();
            builder.Services.AddTransient<AddNewSchemaViewModel>();
            builder.Services.AddTransient<CurrentSchemaPage>();

           
            builder.Services.AddSingleton<NavigationService>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();

            
            builder.Services.AddTransient<CurrentSchemaPage>();
            builder.Services.AddTransient<AnalizePage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            InitializeDatabase(app);

            return app;
        }

        private static void InitializeDatabase(MauiApp app)
        {
           
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<LocalDBContext>();

                try
                {
                    

                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                   
                    Console.WriteLine($"Database migration failed: {ex.Message}");
                    
                }
            }

        }
    }
}
