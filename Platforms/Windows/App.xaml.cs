using Microsoft.UI.Xaml;

namespace LetyAccesoriosApp.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        // En plataformas nativas de MAUI para Windows, no se debe llamar a InitializeComponent() 
        // de forma explícita en este constructor, ya que el motor base se encarga de la UI.
        
        this.UnhandledException += (sender, e) =>
        {
            System.Diagnostics.Debug.WriteLine($"[WINUI ERROR] Excepción de interfaz: {e.Message}");
            e.Handled = true; // Captura errores en caliente y evita el cierre repentino de la ventana
        };
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
