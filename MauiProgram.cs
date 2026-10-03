using LetyAccesoriosApp.Data; // Añade este using arriba
using LetyAccesoriosApp; // Añade este using arriba
using LetyAccesoriosApp.Views; // Añade este using arriba

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

        // ====== AGREGA ESTA LÍNEA AQUÍ ======
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddTransient<InsumosPage>();
        builder.Services.AddTransient<ProductoFormPage>();
        builder.Services.AddTransient<VentasPage>();

        return builder.Build();
    }   
}
