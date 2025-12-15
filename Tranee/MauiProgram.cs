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
        /* Compleated TODOS:
        //TODOS: Make a page for creating Template +++
        //TODOS: all elements not fit in creating Template Page ++
        //TODOS: Make a detailed Templates +++
        //TODOS: make to write description when create Exercise in template +++ 
         //TODOS: Make a detailed history (like menu in template) when see group  of muscle and data and button to see all information about training ++++

        */


        //TODOS: Make chance to write notes to sets in ActiveTraining ++

        //TODOS: create a enums for muscle group and quality and change everywhere it used

        //TODOS: add ability to change date of todays traning, and show date until minutes

        //TODOS: make a analize page with functional


        //TODOS: fix all speiling errors and make sesible names
        //TODOS: Make pretty ui for app with animations, image 

        //TODOS: Make API
        //TODOS: Make MySql Database
        //TODOS: Make Authorization
        //TODOS: Deploy ALL of it On AZURE CLOUD

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


            // "TraneeLocal.db"


            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "TraneeLocal.db");

            builder.Services.AddDbContext<LocalDBContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
                // Тут НЕ треба вказувати MigrationsAssembly, бо бібліотека підключена напряму
            });

            // Services and viewmodels/pages
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

            // Register navigation service, main VM and main page so DI can wire bindings/navigation
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
            // Створюємо тимчасову область (Scope), щоб дістати DbContext
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<LocalDBContext>();

                try
                {
                    // db.Database.EnsureDeleted();

                    db.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Логування помилки, якщо щось піде не так
                    Console.WriteLine($"Database migration failed: {ex.Message}");
                    // Можна додати Debug.WriteLine або App.MainPage.DisplayAlert...
                }
            }

        }
    }
}
