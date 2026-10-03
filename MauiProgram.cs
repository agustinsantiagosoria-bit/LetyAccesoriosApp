using LetyAccesoriosApp.Data;
using LetyAccesoriosApp.Views;
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
                    fonts.AddFont("OpenSans-Regular", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold", "OpenSansSemibold");
                });

            // 1. REGISTRO ESENCIAL DE BASE DE DATOS (Singletons globales)
            builder.Services.AddSingleton<DatabaseService>();
            
            // 2. REGISTRO ESENCIAL DE VIEWMODELS 
            builder.Services.AddSingleton<InsumosViewModel>();

            // 3. REGISTRO DE VISTAS (Las registramos como Singletons para evitar que se reconstruyan en nulo al cambiar de pestaña)
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<InsumosPage>();
            builder.Services.AddSingleton<ProductoFormPage>();
            builder.Services.AddSingleton<VentasPage>();

            return builder.Build();
        }
    }
}
