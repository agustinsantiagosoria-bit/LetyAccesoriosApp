using LetyAccesoriosApp.Data;
using LetyAccesoriosApp;
using LetyAccesoriosApp.Views;
using LetyAccesoriosApp.ViewModels; // <-- ASEGÚRATE DE TENER ESTE USING

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

        // 1. Registro del Servicio de Base de Datos (Único para toda la app)
        builder.Services.AddSingleton<DatabaseService>();
        
        // 2. REGISTRO ESENCIAL DE VIEWMODELS (Esto evita el crash inmediato al arrancar)
        builder.Services.AddTransient<InsumosViewModel>();
        builder.Services.AddTransient<StockViewModel>();

        // 3. Registro de las vistas de la aplicación
        builder.Services.AddTransient<InsumosPage>();
        builder.Services.AddTransient<ProductoFormPage>();
        builder.Services.AddTransient<VentasPage>();

        return builder.Build();
    }   
}
