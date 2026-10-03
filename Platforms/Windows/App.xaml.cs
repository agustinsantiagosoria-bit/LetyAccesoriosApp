using Microsoft.UI.Xaml;

namespace LetyAccesoriosApp.WinUI
{
    public partial class App : MauiWinUIApplication
    {
        public App()
        {
            this.InitializeComponent();

            this.UnhandledException += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"[WINUI ERROR] Excepción de interfaz: {e.Message}");
                e.Handled = true; // Evita el cierre repentino de la ventana
            };
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}