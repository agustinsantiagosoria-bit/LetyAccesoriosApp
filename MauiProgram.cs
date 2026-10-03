using Microsoft.Extensions.Logging;
using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.ViewModels;

namespace LetyAccesoriosApp
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

            // Registro de Servicios y ViewModels
            builder.Services.AddSingleton<DatabaseContext>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<StockViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}