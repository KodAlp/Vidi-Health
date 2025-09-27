using Microsoft.Extensions.Logging;
using Vidi_Health.Models;
using Vidi_Health.Services;
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

            builder.Services.AddScoped<IBodyFatCalculatorService, BodyFatCalculatorService>();
            builder.Services.AddScoped<IBmrCalculatorService, BmrCalculatorService>();
            builder.Services.AddDbContext<DietContext>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
