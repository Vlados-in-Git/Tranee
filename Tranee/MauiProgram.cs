using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tranee.servises;
using Tranee.viewModels;
using Tranee.views;
using TraneeLibrary.Data;

namespace Tranee
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
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

            // Register navigation service, main VM and main page so DI can wire bindings/navigation
            builder.Services.AddSingleton<NavigationService>();
            builder.Services.AddTransient<MainPageViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<NewSchemaPage>();
            builder.Services.AddTransient<CurrentSchemaPage>();
            builder.Services.AddTransient<AnalizePage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
