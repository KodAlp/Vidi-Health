using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Vidi_Health
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
            // Database path setup
            string dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "diet_tracker.db");

            builder.UseMauiApp<App>().UseMauiCommunityToolkit(); ;
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
