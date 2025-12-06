using Microsoft.Extensions.Logging;
using Tranee.Data;
using Tranee.servises;
using Tranee.views;

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

            builder.Services.AddDbContext<LocalDBContext>();

            builder.Services.AddTransient<TrainingService>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<AddTrainPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
